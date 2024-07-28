using System;
using System.Collections.Generic;
using System.Linq;
using HousekeeperServiceProject.Mocking;
using Moq;
using NUnit.Framework;

namespace HousekeeperServiceProject.Tests
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;

        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(unitOfWork => unitOfWork.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName ?? string.Empty, _statementDate))
                .Returns(() => _statementFileName);
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);
        }

        private void SetupStatementFileName(string statementFileName)
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName ?? string.Empty, _statementDate))
                .Returns(statementFileName);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName ?? string.Empty, _statementDate));
        }

        [TestFixture]
        public class InvalidEmailTests : HousekeeperServiceTests
        {
            [Test]
            [TestCase(" ")] // Whitespace
            [TestCase("")] // Empty string
            public void SendStatementEmails_InvalidEmail_ShouldNotGenerateStatements(string email)
            {
                // Arrange
                _houseKeeper.Email = email;

                // Act
                _service.SendStatementEmails(_statementDate);

                // Assert
                _statementGenerator.Verify(sg =>
                    sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName ?? string.Empty, _statementDate),
                    Times.Never());
            }
        }

        [TestFixture]
        public class EmailStatementTests : HousekeeperServiceTests
        {
            [Test]
            public void SendStatementEmails_WhenCalled_EmailStatement()
            {
                // Arrange
                SetupStatementFileName(_statementFileName);

                // Act
                _service.SendStatementEmails(_statementDate);

                // Assert
                _emailSender.Verify(es =>
                    es.EmailFile(_houseKeeper.Email, _houseKeeper.StatementEmailBody ?? string.Empty, _statementFileName, "Sandpiper Statement 2017-01 b"));
            }

            [Test]
            [TestCase("")] // Empty string
            [TestCase(" ")] // Whitespace
            public void SendStatementEmails_InvalidStatementFileName_ShouldNotEmailStatement(string statementFileName)
            {
                // Arrange
                SetupStatementFileName(statementFileName);

                // Act
                _service.SendStatementEmails(_statementDate);

                // Assert
                _emailSender.Verify(es =>
                    es.EmailFile(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                      Times.Never);
            }

            [Test]
            public void SendStatementEmails_EmailSendingFails_ShouldShowMessageBox()
            {
                // Arrange
                SetupStatementFileName(_statementFileName);
                _emailSender.Setup(es => es.EmailFile(_houseKeeper.Email, _houseKeeper.StatementEmailBody ?? string.Empty, _statementFileName, "Sandpiper Statement 2017-01 b"))
                    .Throws<Exception>();

                // Act
                _service.SendStatementEmails(_statementDate);

                // Assert
                _xtraMessageBox.Verify(mb =>
                    mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
            }
        }
    }
}
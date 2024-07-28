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

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(unitOfWork => unitOfWork.Query<Housekeeper>()).Returns(new List<Housekeeper>
    {
        _houseKeeper
    }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);

        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName ?? string.Empty, _statementDate));

        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements()
        {
            //Arrange
            _houseKeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            // Assert
            // Verify that SaveStatement was never called because there's no email address.
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName ?? string.Empty, _statementDate),
                Times.Never());

        }

    }

}
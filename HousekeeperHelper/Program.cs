using System;
using HousekeeperServiceProject.Mocking;

namespace HousekeeperHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var unitOfWork = new UnitOfWork();
            var statementGenerator = new StatementGenerator();
            var emailSender = new EmailSender();
            var xtraMessageBox = new XtraMessageBox();

            var housekeeperService = new HousekeeperService(unitOfWork, statementGenerator, emailSender, xtraMessageBox);

            housekeeperService.SendStatementEmails(DateTime.Now);
        }
    }
}
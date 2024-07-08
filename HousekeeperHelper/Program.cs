using System;
using HousekeeperHelperProject.Mocking;

namespace HousekeeperHelperProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var unitOfWork = new UnitOfWork();
            var housekeeperHelper = new HousekeeperHelper(unitOfWork);

            housekeeperHelper.SendStatementEmails(DateTime.Now);
        }
    }
}
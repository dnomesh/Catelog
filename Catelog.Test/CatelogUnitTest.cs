using AutoFixture;
using Catelog.Console.Services;
using Catelog.Domain.Models;
using Catelog.Repository.Repositories;
using ExcelDataReader.Log;
using Microsoft.EntityFrameworkCore;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Catelog.Test
{
    public class CatelogUnitTest
    {
        private Mock<CatelogDbContext> db;
        private ICatelog catelog;
        [OneTimeSetUp]
        public void Setup()
        {
            db = IEmployeeRepositoryMock.GetCategoriesMock();
        }

        [Test]
        public void ReadMigrationDataFromExcelFile_TestResult()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new ThrowingRecursionBehavior());

            catelog = new CatelogService(db.Object);
            string fileName = Path.Combine("App_Data", "CatelogData.xlsx");
            int expected = catelog.ReadMigrationDataFromExcelFile(fileName);
            Assert.AreEqual(expected, 0);
        }
    }
}
using Catelog.Console.Services;
using Catelog.Repository.Repositories;
using Moq;

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
            catelog = new CatelogService(db.Object);
            string fileName = Path.Combine("App_Data", "CatelogData.xlsx");
            int expected = catelog.ReadMigrationDataFromExcelFile(fileName);
            Assert.AreEqual(expected, 0);
        }
    }
}
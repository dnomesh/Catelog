// See https://aka.ms/new-console-template for more information

using Catelog.Console.Services;
using Catelog.Repository.Repositories;

string? catelogConnectionString = string.Empty;
CatelogDbContext db = new CatelogDbContext();
string fileName = Path.Combine("App_Data", "CatelogData.xlsx");

ICatelog catelog = new CatelogService(db);

Console.WriteLine("Data Insertion Started...");
int result = catelog.ReadMigrationDataFromExcelFile(fileName);
Console.WriteLine("Data Insertion Ended...");


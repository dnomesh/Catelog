// See https://aka.ms/new-console-template for more information

using Catelog.Console.Services;
using Catelog.Domain.Models;
using Catelog.Repository.Repositories;
using ExcelDataReader;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

string? catelogConnectionString = string.Empty;
CatelogDbContext db = new CatelogDbContext();
string fileName = Path.Combine("App_Data", "CatelogData.xlsx");

ICatelog catelog = new CatelogService(db);

Console.WriteLine("Data Insertion Started...");
//InitializeVariables();
int result = catelog.ReadMigrationDataFromExcelFile(fileName);
Console.WriteLine("Data Insertion Ended...");


//void InitializeVariables()
//{
//    string jsonFileName = "appsettings.json";
//    var config = new ConfigurationBuilder().AddJsonFile(jsonFileName, true, true);
//    var configurationRoot = config.Build();
//    catelogConnectionString = configurationRoot.GetSection("ConnectionStrings").GetSection("Catelog").Value;
//    db = new CatelogDbContext();
//}




// See https://aka.ms/new-console-template for more information

using Catelog.Domain.Models;
using Catelog.Repository.Repositories;
using ExcelDataReader;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

string? catelogConnectionString = string.Empty;
CatelogDbContext db;
string fileName = Path.Combine("App_Data", "CatelogData.xlsx");

Console.WriteLine("Data Insertion Started...");
InitializeVariables();
ReadMigrationDataFromExcelFile();
Console.WriteLine("Data Insertion Ended...");


void InitializeVariables()
{
    string jsonFileName = "appsettings.json";
    var config = new ConfigurationBuilder().AddJsonFile(jsonFileName, true, true);
    var configurationRoot = config.Build();
    catelogConnectionString = configurationRoot.GetSection("ConnectionStrings").GetSection("Catelog").Value;
    db = new CatelogDbContext();
}


void ReadMigrationDataFromExcelFile()
{
    List<Category> categories = new List<Category>();
    List<Product> products = new List<Product>();

    using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encoding = Encoding.GetEncoding("UTF-8");
        using (var reader = ExcelReaderFactory.CreateReader(stream,
            new ExcelReaderConfiguration() { FallbackEncoding = encoding }))
        {
            var result = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
            });

            if(result.Tables.Count > 0)
            {
                foreach(DataRow row in result.Tables[0].Rows)
                {
                    Category category = new Category();
                    category.Name = row["Name"] as string;
                    category.Code = row["Code"] as string;
                    category.CreationDate = Convert.ToDateTime(row["CreationDate"] as DateTime? ?? null);

                    categories.Add(category);
                }

                db.AddRange(categories);
                db.SaveChanges();

                foreach (DataRow row in result.Tables[1].Rows)
                {
                    Product product = new Product();
                    product.Name = row["Name"] as string;
                    product.Code = row["Code"] as string;
                    var categoryCode = row["CategoryCode"] as string;

                    Category category = db.Categories.FirstOrDefault(args => args.Code == categoryCode);

                    product.CategoryId = category.Id;
                    product.CreationDate = Convert.ToDateTime(row["CreationDate"] as DateTime? ?? null);

                    products.Add(product);
                }

                db.AddRange(products);
                db.SaveChanges();
            }
        }
    }

}

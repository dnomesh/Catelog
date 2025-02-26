using Catelog.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catelog.Console.Services
{
    public interface ICatelog
    {
        int ReadMigrationDataFromExcelFile(string fileName);
    }
}

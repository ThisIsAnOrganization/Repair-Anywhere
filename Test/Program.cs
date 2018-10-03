using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDb dbi = new CreateDb();
            dbi.InsertInTable();

            RetrieveFromDb rdb = new RetrieveFromDb();
            rdb.Retieve();

            DeleteDb ddb = new DeleteDb();
            ddb.DeleteFromTable();
        }
    }
}

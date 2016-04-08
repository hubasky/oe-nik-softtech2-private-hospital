using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_test
{
    public class TestDb : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public TestDb()
            : base("server=hubasky.ddns.net;user=peet;database=HP_TEST;port=3306;password=szoftech;")
        {

        }

        public TestDb(DbConnection dbConn, bool contextOwnsConn) : base(dbConn, contextOwnsConn)
        {

        }

    }
}

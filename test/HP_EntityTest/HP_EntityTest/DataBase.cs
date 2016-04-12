using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP_EntityTest
{
    public class DataBase : DbContext
    {
        public DbSet<Employee> Employees { get; set; }


        public DataBase() :
            base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KutyaAdatbazis;Integrated Security=True;AttachDBFilename=d:\--- Document ---\Suli\4. felev\Szoftvertechnologia II\test\HP_EntityTest\HP_EntityTest\EntityTestDb.mdf")
        {
        }
    }
}

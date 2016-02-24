using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_test
{
    class DataBase
    {

        public string Server { get; private set; }
        public string User { get; private set; }
        public string Database { get; private set; }
        public string Port { get; private set; }
        public string Password { get; private set; }

        public DataBase(string server, string user, string database, string port, string password)
        {
            this.Server = server;
            this.User = user;
            this.Database = database;
            this.Port = port;
            this.Password = password;
        }

        public string GetConnectionString()
        {
            return string.Format("server={0};user={1};database={2};port={3};password={4};",
                this.Server, this.User, this.Database, this.Port, this.Password);
        }
    }
}

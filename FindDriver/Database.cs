using System;
using DotNetEnv;

namespace FindDriver
{
    class MySQLDBConnection
    {
        private string server;
        private string uid;
        private string database;
        private string pwd;
        private string port;

        public MySQLDBConnection()
        {
            DotNetEnv.Env.Load(".env");
        
            this.server = "35.213.165.89";
            this.uid = "hoangtv";
            this.database = "railway";
            this.pwd = "Arnotran123456";
            this.port = "36487";
            

        }

        public string GetConnectionString()
        {
            return $"Server={server};Port={port};Database={database};Uid={uid};Pwd={pwd}";
        }
    }
}
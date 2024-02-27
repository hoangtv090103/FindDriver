using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace Authentication
{
    [ComVisible(true)]
    [Guid("F78CBB1C-FCF4-4D3E-9024-EB61BFD091EA")]
    public interface IAuthentication
    {
        string Login(string phoneUser, string password);
        string Logout(string email);
    }
    

    [ComVisible(true)]
    [Guid("25F68E31-430E-499D-8E38-0EEB25328611")]
    [ProgId("FindDriver.Authentication")]
    public class Authentication : IAuthentication
    {
        public string Login(string phoneEmail, string password)
        {
            string res = "";
            string connString = new FindDriver.MySQLDBConnection().GetConnectionString();
            // check if email and password are correct
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT * FROM users WHERE (email = @phoneEmail OR phone = @phoneEmail) AND password = @password";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@phoneEmail", phoneEmail);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string uid = reader["id"].ToString();
                            string ufirstName = reader["first_name"].ToString();
                            string ulastName = reader["last_name"].ToString();
                            string uphone = reader["phone"].ToString();
                            string uEmail = reader["email"].ToString();
                            res = $"{{\"id\": {uid}, \"first_name\": \"{ufirstName}\", \"last_name\": \"{ulastName}\", \"email\": \"{uEmail}\", \"phone\": \"{uphone}\"}}";
                        }
                    }
                }
            }
            return res;   
        }

        public string Logout(string email)
        {
            return "Logout";
        }
    }
}
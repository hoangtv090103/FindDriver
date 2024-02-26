using System;
using System.Runtime.InteropServices;

using MySql.Data.MySqlClient;
namespace FindDriver
{
    [ComVisible(true)]
    [Guid("3C51B0D2-08DC-46F3-A979-1911C0F1D587")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVehicleTypes
    {
        string GetVehicleTypes();
    }

    [ComVisible(true)]
    [Guid("18731562-0B85-47A7-A82A-1571337BB6CB")]
    [ProgId("FindDriver.VehicleTypesService")]
    public class VehicleTypes : IVehicleTypes
    {
        public string GetVehicleTypes()
        {
            string res = "";
            string connString = new MySQLDBConnection().GetConnectionString();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string query = "SELECT * FROM vehicle_types";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["id"].ToString();
                                string type = reader["type"].ToString();
                                res = $"{{\"id\": {id}, \"type\": \"{type}\"}}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                res = connString;
            }

            return res;
        }
    }
}
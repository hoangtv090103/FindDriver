
using System;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace FindDriver
{
    [ComVisible(true)]
    [Guid("8B8CD165-5F1D-4053-BDBB-5B1E973B77C2")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]

    public interface IRegister
    {
        string RegisterUser(string firstName, string lastName, string email, string password, string phone);
        string RegisterCustomer(string firstName, string lastName, string email, string password, string phone);
        string RegisterDriver(string firstName, string lastName, string email, string password, string phone, string licenseNumber);
        string RegisterVehicle(string model, string make, string color, string licensePlate, long typeId, long driverId);
    }


    [ComVisible(true)]
    [Guid("450313F1-235C-47B4-ADAE-ABEAA484DF7C")]
    [ProgId("FindDriver.Register")]
    [ClassInterface(ClassInterfaceType.None)]

    public class Register : IRegister
    {


        public Register()
        {
            System.Console.WriteLine("Register Service has been created");
        }

        public string RegisterUser(string firstName, string lastName, string email, string password, string phone)
        {
            string connString = new MySQLDBConnection().GetConnectionString();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = "INSERT INTO user (first_name, last_name, email, password, phone) VALUES (@firstName, @lastName, @email, @password, @phone)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@phone", phone);

                    cmd.ExecuteNonQuery();
                }
            }

            // return json string of inserted user
            string res = $"{{\"first_name\": \"{firstName}\", \"last_name\": \"{lastName}\", \"email\": \"{email}\", \"password\": \"{password}\", \"phone\": \"{phone}\"}}";
            return res;

        }

        public string RegisterCustomer(string firstName, string lastName, string email, string password, string phone)
        {
            string connString = new MySQLDBConnection().GetConnectionString();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Register user first
                string query = "INSERT INTO Users (first_name, last_name, email, password, phone) VALUES (@firstName, @lastName, @email, @password, @phone)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@phone", phone);

                    cmd.ExecuteNonQuery();
                }

                // Get the user id
                query = "SELECT id FROM users WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        long userId = reader.GetInt64(0);

                        // Register customer
                        query = "INSERT INTO Customer (user_id, first_name, last_name, email, phone) VALUES (@userId, @firstName, @lastName, @email, @phone)";
                        using (MySqlCommand customerCmd = new MySqlCommand(query, conn))
                        {
                            customerCmd.Parameters.AddWithValue("@userId", userId);
                            customerCmd.Parameters.AddWithValue("@firstName", firstName);
                            customerCmd.Parameters.AddWithValue("@lastName", lastName);
                            customerCmd.Parameters.AddWithValue("@email", email);
                            customerCmd.Parameters.AddWithValue("@phone", phone);

                            customerCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // return json string of inserted customer
            string res = $"{{\"first_name\": \"{firstName}\", \"last_name\": \"{lastName}\", \"email\": \"{email}\", \"password\": \"{password}\", \"phone\": \"{phone}\"}}";
            return res;


        }

        public string RegisterDriver(string firstName, string lastName, string email, string password, string phone, string licenseNumber)
        {
            string connString = new MySQLDBConnection().GetConnectionString();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // Register user first
                string query = "INSERT INTO users (first_name, last_name, email, password, phone) VALUES (@firstName, @lastName, @email, @password, @phone)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@phone", phone);

                    cmd.ExecuteNonQuery();
                }

                // Get the user id
                query = "SELECT id FROM users WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        long userId = reader.GetInt64(0);

                        // Register driver
                        query = "INSERT INTO Driver (user_id, first_name, last_name, email, phone, license_number) VALUES (@userId, @firstName, @lastName, @email, @phone, @licenseNumber)";
                        using (MySqlCommand driverCmd = new MySqlCommand(query, conn))
                        {
                            driverCmd.Parameters.AddWithValue("@userId", userId);
                            driverCmd.Parameters.AddWithValue("@firstName", firstName);
                            driverCmd.Parameters.AddWithValue("@lastName", lastName);
                            driverCmd.Parameters.AddWithValue("@email", email);
                            driverCmd.Parameters.AddWithValue("@phone", phone);
                            driverCmd.Parameters.AddWithValue("@licenseNumber", licenseNumber);

                            driverCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // return json string of inserted driver
            string res = $"{{\"first_name\": \"{firstName}\", \"last_name\": \"{lastName}\", \"email\": \"{email}\", \"password\": \"{password}\", \"phone\": \"{phone}\", \"license_number\": \"{licenseNumber}\"}}";
            return res;
        }

        public string RegisterVehicle(string model, string make, string color, string licensePlate, long typeId, long driverId)
        {
            string connString = new MySQLDBConnection().GetConnectionString();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = "INSERT INTO Vehicle (model, make, color, license_plate, type_id, driver_id) VALUES (@model, @make, @color, @licensePlate, @typeId, @driverId)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@make", make);
                    cmd.Parameters.AddWithValue("@color", color);
                    cmd.Parameters.AddWithValue("@licensePlate", licensePlate);
                    cmd.Parameters.AddWithValue("@typeId", typeId);
                    cmd.Parameters.AddWithValue("@driverId", driverId);

                    cmd.ExecuteNonQuery();
                }
            }

            // return json string of inserted vehicle
            string res = $"{{\"model\": \"{model}\", \"make\": \"{make}\", \"color\": \"{color}\", \"license_plate\": \"{licensePlate}\", \"type_id\": \"{typeId}\", \"driver_id\": \"{driverId}\"}}";
            return res;
        }
    }
}
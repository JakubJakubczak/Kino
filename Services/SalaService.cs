using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;



namespace Kino.Services
{
    public class SalaService
    {
        private readonly string _connectionString;

        public SalaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public async Task AddSalaRecordAsync(string salaName, int capacity)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO sala (name, capacity) VALUES (@name, @capacity)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", salaName);
                command.Parameters.AddWithValue("@capacity", capacity);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions appropriately (logging, error handling, etc.)
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}

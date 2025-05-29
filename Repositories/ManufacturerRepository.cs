using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class ManufacturerRepository : IManufacturerRepository
{
    private const string DatabasePath = "videocard_store.db";

    public List<Manufacturer> GetAllManufacturers()
    {
        List<Manufacturer> manufacturers = new List<Manufacturer>();
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT * FROM Manufacturers";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Manufacturer manufacturer = new Manufacturer
                        {
                            ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]),
                            Name = reader["Name"].ToString(),
                            Country = reader["Country"].ToString()
                        };
                        manufacturers.Add(manufacturer);
                    }
                }
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"Ошибка при работе с базой данных: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            throw;
        }
        return manufacturers;
    }

    public Manufacturer GetManufacturerById(int id)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT * FROM Manufacturers WHERE ManufacturerID = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Manufacturer manufacturer = new Manufacturer
                            {
                                ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]),
                                Name = reader["Name"].ToString(),
                                Country = reader["Country"].ToString()
                            };
                            return manufacturer;
                        }
                        else
                        {
                            return null; // Производитель не найден
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            throw;
        }

        
    }
}

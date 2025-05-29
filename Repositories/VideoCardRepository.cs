using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class VideoCardRepository : IVideoCardRepository
{
    private const string DatabasePath = "videocard_store.db";

    public List<VideoCard> GetAllVideoCards()
    {
        List<VideoCard> videoCards = new List<VideoCard>();
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT * FROM VideoCards";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VideoCard videoCard = new VideoCard
                        {
                            VideoCardID = Convert.ToInt32(reader["VideoCardID"]),
                            GPUID = Convert.ToInt32(reader["GPUID"]),
                            ModelName = reader["ModelName"].ToString(),
                            ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]),
                            Price = Convert.ToDouble(reader["Price"]),
                            ClockSpeedMHz = Convert.ToInt32(reader["ClockSpeedMHz"]),
                            BoostClockSpeedMHz = Convert.ToInt32(reader["BoostClockSpeedMHz"])
                        };
                        videoCards.Add(videoCard);
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
        return videoCards;
    }

    public VideoCard GetVideoCardById(int id)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT * FROM VideoCards WHERE VideoCardID = @id";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            VideoCard videoCard = new VideoCard
                            {
                                VideoCardID = Convert.ToInt32(reader["VideoCardID"]),
                                GPUID = Convert.ToInt32(reader["GPUID"]),
                                ModelName = reader["ModelName"].ToString(),
                                ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]),
                                Price = Convert.ToDouble(reader["Price"]),
                                ClockSpeedMHz = Convert.ToInt32(reader["ClockSpeedMHz"]),
                                BoostClockSpeedMHz = Convert.ToInt32(reader["BoostClockSpeedMHz"])
                            };
                            return videoCard;
                        }
                        else
                        {
                            return null; // Видеокарта не найдена
                        }
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
    }

    public void AddVideoCard(VideoCard videoCard)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = @"
                    INSERT INTO VideoCards (GPUID, ModelName, ManufacturerID, Price, ClockSpeedMHz, BoostClockSpeedMHz)
                    VALUES (@GPUID, @ModelName, @ManufacturerID, @Price, @ClockSpeedMHz, @BoostClockSpeedMHz);
                    SELECT last_insert_rowid();"; // Получаем ID добавленной записи

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GPUID", videoCard.GPUID);
                    command.Parameters.AddWithValue("@ModelName", videoCard.ModelName);
                    command.Parameters.AddWithValue("@ManufacturerID", videoCard.ManufacturerID);
                    command.Parameters.AddWithValue("@Price", videoCard.Price);
                    command.Parameters.AddWithValue("@ClockSpeedMHz", videoCard.ClockSpeedMHz);
                    command.Parameters.AddWithValue("@BoostClockSpeedMHz", videoCard.BoostClockSpeedMHz);

                    videoCard.VideoCardID = Convert.ToInt32(command.ExecuteScalar());  // Получаем ID
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
    }

    public void UpdateVideoCard(VideoCard videoCard)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = @"
                    UPDATE VideoCards
                    SET GPUID = @GPUID,
                        ModelName = @ModelName,
                        ManufacturerID = @ManufacturerID,
                        Price = @Price,
                        ClockSpeedMHz = @ClockSpeedMHz,
                        BoostClockSpeedMHz = @BoostClockSpeedMHz
                    WHERE VideoCardID = @VideoCardID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VideoCardID", videoCard.VideoCardID);
                    command.Parameters.AddWithValue("@GPUID", videoCard.GPUID);
                    command.Parameters.AddWithValue("@ModelName", videoCard.ModelName);
                    command.Parameters.AddWithValue("@ManufacturerID", videoCard.ManufacturerID);
                    command.Parameters.AddWithValue("@Price", videoCard.Price);
                    command.Parameters.AddWithValue("@ClockSpeedMHz", videoCard.ClockSpeedMHz);
                    command.Parameters.AddWithValue("@BoostClockSpeedMHz", videoCard.BoostClockSpeedMHz);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Видеокарта с указанным ID не найдена.");
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
    }

    public void DeleteVideoCard(int id)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = "DELETE FROM VideoCards WHERE VideoCardID = @VideoCardID";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VideoCardID", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Видеокарта с указанным ID не найдена.");
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
    }

    public List<VideoCard> SearchVideoCardsByName(string name)
    {
        try
        {
            List<VideoCard> videoCards = new List<VideoCard>();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();
                string query = "SELECT * FROM VideoCards WHERE ModelName LIKE @name";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", "%" + name + "%");
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VideoCard videoCard = new VideoCard
                            {
                                VideoCardID = Convert.ToInt32(reader["VideoCardID"]),
                                GPUID = Convert.ToInt32(reader["GPUID"]),
                                ModelName = reader["ModelName"].ToString(),
                                ManufacturerID = Convert.ToInt32(reader["ManufacturerID"]),
                                Price = Convert.ToDouble(reader["Price"]),
                                ClockSpeedMHz = Convert.ToInt32(reader["ClockSpeedMHz"]),
                                BoostClockSpeedMHz = Convert.ToInt32(reader["BoostClockSpeedMHz"])
                            };
                            videoCards.Add(videoCard);
                        }
                    }
                }
            }
            return videoCards;
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"Ошибка при работе с базой данных: {ex.Message}");
            throw; // Важно пробросить исключение, чтобы caller знал об ошибке
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            throw; // Важно пробросить исключение, чтобы caller знал об ошибке
        }
    }
}

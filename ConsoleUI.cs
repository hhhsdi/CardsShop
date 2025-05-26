using System;

using System.Collections.Generic;

public class ConsoleUI
{
    private static readonly VideoCardRepository videoCardRepository = new VideoCardRepository();
    private static readonly ManufacturerRepository manufacturerRepository = new ManufacturerRepository();

    public static void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Магазин видеокарт ---");
            Console.WriteLine("1. Список видеокарт");
            Console.WriteLine("2. Поиск видеокарты по названию");
            Console.WriteLine("3. Добавить видеокарту");
            Console.WriteLine("4. Редактировать видеокарту");
            Console.WriteLine("5. Удалить видеокарту");
            Console.WriteLine("6. Список производителей");
            Console.WriteLine("7. Выход");
            Console.WriteLine("8. Активировать Sql-скрипт");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListVideoCards();
                    break;
                case "2":
                    SearchVideoCard();
                    break;
                case "3":
                    AddVideoCard();
                    break;
                case "4":
                    EditVideoCard();
                    break;
                case "5":
                    DeleteVideoCard();
                    break;
                case "6":
                    ListManufacturers();
                    break;
                case "7":
                    Console.WriteLine("Выход из программы.");
                    return;
                case "8":
                    DatabaseManager.ExecuteSqlScript("SqlScript.sql");
                    break;
                default:
                    Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static void ListVideoCards()
    {
        try
        {
            List<VideoCard> videoCards = videoCardRepository.GetAllVideoCards();
            Console.WriteLine("Список видеокарт:");
            foreach (var card in videoCards)
            {
                Console.WriteLine($"ID: {card.VideoCardID}, Модель: {card.ModelName}, Цена: {card.Price}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void SearchVideoCard()
    {
        Console.Write("Введите название видеокарты для поиска: ");
        string searchName = Console.ReadLine();

        try
        {
            List<VideoCard> videoCards = videoCardRepository.SearchVideoCardsByName(searchName);

            if (videoCards.Count == 0)
            {
                Console.WriteLine("Видеокарта не найдена.");
                return;
            }

            Console.WriteLine("Найденные видеокарты:");
            foreach (var card in videoCards)
            {
                Console.WriteLine($"ID: {card.VideoCardID}, Модель: {card.ModelName}, Цена: {card.Price}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void AddVideoCard()
    {
        try
        {
            Console.Write("Введите название модели: ");
            string modelName = Console.ReadLine();

            Console.Write("Введите ID GPU: ");
            if (!int.TryParse(Console.ReadLine(), out int gpuId))
            {
                Console.WriteLine("Некорректный ID GPU.");
                return;
            }

            Console.Write("Введите ID производителя: ");
            if (!int.TryParse(Console.ReadLine(), out int manufacturerId))
            {
                Console.WriteLine("Некорректный ID производителя.");
                return;
            }

            Console.Write("Введите цену: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Некорректная цена.");
                return;
            }

            Console.Write("Введите частоту ядра (МГц): ");
            if (!int.TryParse(Console.ReadLine(), out int clockSpeed))
            {
                Console.WriteLine("Некорректная частота ядра.");
                return;
            }

            Console.Write("Введите частоту в бусте (МГц): ");
            if (!int.TryParse(Console.ReadLine(), out int boostClockSpeed))
            {
                Console.WriteLine("Некорректная частота в бусте.");
                return;
            }

            VideoCard newVideoCard = new VideoCard
            {
                GPUID = gpuId,
                ModelName = modelName,
                ManufacturerID = manufacturerId,
                Price = price,
                ClockSpeedMHz = clockSpeed,
                BoostClockSpeedMHz = boostClockSpeed
            };

            videoCardRepository.AddVideoCard(newVideoCard);
            Console.WriteLine("Видеокарта добавлена с ID: " + newVideoCard.VideoCardID);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void EditVideoCard()
    {
        Console.Write("Введите ID видеокарты для редактирования: ");
        if (!int.TryParse(Console.ReadLine(), out int videoCardId))
        {
            Console.WriteLine("Некорректный ID.");
            return;
        }

        try
        {
            VideoCard existingVideoCard = videoCardRepository.GetVideoCardById(videoCardId);
            if (existingVideoCard == null)
            {
                Console.WriteLine("Видеокарта с таким ID не найдена.");
                return;
            }

            Console.Write("Введите новую цену: ");
            if (!double.TryParse(Console.ReadLine(), out double newPrice))
            {
                Console.WriteLine("Некорректная цена.");
                return;
            }

            existingVideoCard.Price = newPrice;  // Обновляем только цену

            videoCardRepository.UpdateVideoCard(existingVideoCard);
            Console.WriteLine("Цена видеокарты обновлена.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DeleteVideoCard()
    {
        Console.Write("Введите ID видеокарты для удаления: ");
        if (!int.TryParse(Console.ReadLine(), out int videoCardId))
        {
            Console.WriteLine("Некорректный ID.");
            return;
        }

        try
        {
            videoCardRepository.DeleteVideoCard(videoCardId);
            Console.WriteLine("Видеокарта удалена.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void ListManufacturers()
    {
        try
        {
            List<Manufacturer> manufacturers = manufacturerRepository.GetAllManufacturers();

            Console.WriteLine("Список производителей:");
            foreach (var manufacturer in manufacturers)
            {
                Console.WriteLine($"ID: {manufacturer.ManufacturerID}, Название: {manufacturer.Name}, Страна: {manufacturer.Country}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
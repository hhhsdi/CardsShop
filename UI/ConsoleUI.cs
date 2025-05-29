using System;
using System.Collections.Generic;
using System.Diagnostics;

public class ConsoleUI
{
    private readonly VideoCardService _videoCardService;
    private readonly ManufacturerService _manufacturerService;

    public ConsoleUI(VideoCardService videoCardService, ManufacturerService manufacturerService)
    {
        _videoCardService = videoCardService;
        _manufacturerService = manufacturerService;
    }

    public void Run()
    {
        Console.WriteLine("Инициализация системы...");
        Console.WriteLine("Добро пожаловать в систему управления видеокартами!\n");

        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            try
            {
                ProcessUserChoice(choice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine("Пожалуйста, попробуйте еще раз.\n");
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\n--- Меню управления ---");
        Console.WriteLine("1. Просмотреть все видеокарты");
        Console.WriteLine("2. Найти видеокарту по названию");
        Console.WriteLine("3. Добавить новую видеокарту");
        Console.WriteLine("4. Редактировать видеокарту");
        Console.WriteLine("5. Удалить видеокарту");
        Console.WriteLine("6. Просмотреть производителей");
        Console.WriteLine("7. Выход");
        Console.Write("Выберите действие: ");
    }

    private void ProcessUserChoice(string choice)
    {
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
                ExitApplication();
                break;
            default:
                Console.WriteLine("Неверный ввод. Пожалуйста, выберите действие от 1 до 7.");
                break;
        }
    }

    private void ListVideoCards()
    {
        Console.WriteLine("\n--- Список видеокарт ---");
        var videoCards = _videoCardService.GetAllVideoCards();

        if (videoCards.Count == 0)
        {
            Console.WriteLine("Видеокарты не найдены.");
            return;
        }

        foreach (var card in videoCards)
        {
            DisplayVideoCardDetails(card);
        }
    }

    private void SearchVideoCard()
    {
        Console.Write("\nВведите название видеокарты для поиска: ");
        string searchName = Console.ReadLine();

        var videoCards = _videoCardService.SearchVideoCards(searchName);

        if (videoCards.Count == 0)
        {
            Console.WriteLine("Видеокарты не найдены.");
            return;
        }

        Console.WriteLine("\n--- Результаты поиска ---");
        foreach (var card in videoCards)
        {
            DisplayVideoCardDetails(card);
        }
    }

    private void AddVideoCard()
    {
        Console.WriteLine("\n--- Добавление новой видеокарты ---");

        string modelName = GetInput("Название модели: ", validate: s => !string.IsNullOrWhiteSpace(s));
        int gpuId = int.Parse(GetInput("ID GPU: ", validate: s => int.TryParse(s, out _)));
        int manufacturerId = int.Parse(GetInput("ID производителя: ", validate: s => int.TryParse(s, out _)));
        double price = double.Parse(GetInput("Цена: ", validate: s => double.TryParse(s, out _)));
        int clockSpeed = int.Parse(GetInput("Частота ядра (МГц): ", validate: s => int.TryParse(s, out _)));
        int boostClockSpeed = int.Parse(GetInput("Частота в бусте (МГц): ", validate: s => int.TryParse(s, out _)));

        var newVideoCard = new VideoCard
        {
            ModelName = modelName,
            GPUID = gpuId,
            ManufacturerID = manufacturerId,
            Price = price,
            ClockSpeedMHz = clockSpeed,
            BoostClockSpeedMHz = boostClockSpeed
        };

        _videoCardService.AddVideoCard(newVideoCard);
        Console.WriteLine($"\nВидеокарта успешно добавлена с ID: {newVideoCard.VideoCardID}");
    }

    private void EditVideoCard()
    {
        Console.WriteLine("\n--- Редактирование видеокарты ---");
        int id = int.Parse(GetInput("Введите ID видеокарты: ", validate: s => int.TryParse(s, out _)));

        var existingCard = _videoCardService.GetVideoCardById(id);
        DisplayVideoCardDetails(existingCard);

        Console.WriteLine("\nВведите новые данные (оставьте пустым для сохранения текущего значения):");

        existingCard.ModelName = GetInput($"Название модели [{existingCard.ModelName}]: ", required: false) ?? existingCard.ModelName;
        existingCard.Price = GetDoubleInput($"Цена [{existingCard.Price}]: ", existingCard.Price);
        existingCard.ClockSpeedMHz = GetIntInput($"Частота ядра [{existingCard.ClockSpeedMHz}]: ", existingCard.ClockSpeedMHz);
        existingCard.BoostClockSpeedMHz = GetIntInput($"Частота в бусте [{existingCard.BoostClockSpeedMHz}]: ", existingCard.BoostClockSpeedMHz);

        _videoCardService.UpdateVideoCard(existingCard);
        Console.WriteLine("\nВидеокарта успешно обновлена.");
    }

    private void DeleteVideoCard()
    {
        Console.WriteLine("\n--- Удаление видеокарты ---");
        int id = int.Parse(GetInput("Введите ID видеокарты для удаления: ", validate: s => int.TryParse(s, out _)));

        Console.Write($"Вы уверены, что хотите удалить видеокарту с ID {id}? (y/n): ");
        if (Console.ReadLine().ToLower() == "y")
        {
            _videoCardService.DeleteVideoCard(id);
            Console.WriteLine("Видеокарта успешно удалена.");
        }
        else
        {
            Console.WriteLine("Удаление отменено.");
        }
    }

    private void ListManufacturers()
    {
        Console.WriteLine("\n--- Список производителей ---");
        var manufacturers = _manufacturerService.GetAllManufacturers();

        if (manufacturers.Count == 0)
        {
            Console.WriteLine("Производители не найдены.");
            return;
        }

        foreach (var manufacturer in manufacturers)
        {
            Console.WriteLine($"ID: {manufacturer.ManufacturerID}");
            Console.WriteLine($"Название: {manufacturer.Name}");
            Console.WriteLine($"Страна: {manufacturer.Country}");
            Console.WriteLine("---------------------");
        }
    }

    private void ExitApplication()
    {
        Console.WriteLine("\nЗавершение работы приложения...");
        Environment.Exit(0);
    }

    #region Вспомогательные методы

    private void DisplayVideoCardDetails(VideoCard card)
    {
        Console.WriteLine($"\nID: {card.VideoCardID}");
        Console.WriteLine($"Модель: {card.ModelName}");
        Console.WriteLine($"ID GPU: {card.GPUID}");
        Console.WriteLine($"ID производителя: {card.ManufacturerID}");
        Console.WriteLine($"Цена: {card.Price:C}");
        Console.WriteLine($"Частота ядра: {card.ClockSpeedMHz} МГц");
        Console.WriteLine($"Частота в бусте: {card.BoostClockSpeedMHz} МГц");
        Console.WriteLine("---------------------");
    }

    private string GetInput(string prompt, bool required = true, Func<string, bool> validate = null)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (!required && string.IsNullOrEmpty(input))
                return null;

            if (required && string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Поле не может быть пустым!");
                continue;
            }

            if (validate != null && !validate(input))
            {
                Console.WriteLine("Некорректный ввод!");
                continue;
            }

            return input;
        }
    }

    private double GetDoubleInput(string prompt, double defaultValue)
    {
        string input = GetInput(prompt, required: false);
        return string.IsNullOrEmpty(input) ? defaultValue : double.Parse(input);
    }

    private int GetIntInput(string prompt, int defaultValue)
    {
        string input = GetInput(prompt, required: false);
        return string.IsNullOrEmpty(input) ? defaultValue : int.Parse(input);
    }

    #endregion
}
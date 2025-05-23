## Структура базы данных магазина видеокарт

### Таблицы

*   **Manufacturers:**
    *   `ManufacturerID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `Name` TEXT NOT NULL
    *   `Country` TEXT

*   **GPUs:**
    *   `GPUID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `ManufacturerID` INTEGER NOT NULL, FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)
    *   `Model` TEXT NOT NULL
    *   `MemoryGB` INTEGER NOT NULL
    *   `Architecture` TEXT

*   **VideoCards:**
    *   `VideoCardID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `GPUID` INTEGER NOT NULL, FOREIGN KEY (GPUID) REFERENCES GPUs(GPUID)
    *   `ModelName` TEXT NOT NULL
    *   `ManufacturerID` INTEGER NOT NULL, FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)
    *   `Price` REAL NOT NULL
    *   `ClockSpeedMHz` INTEGER
    *   `BoostClockSpeedMHz` INTEGER

*   **Shops:**
    *   `ShopID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `Name` TEXT NOT NULL
    *   `Address` TEXT

*   **Stocks:**
    *   `StockID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `ShopID` INTEGER NOT NULL, FOREIGN KEY (ShopID) REFERENCES Shops(ShopID)
    *   `VideoCardID` INTEGER NOT NULL, FOREIGN KEY (VideoCardID) REFERENCES VideoCards(VideoCardID)
    *   `Quantity` INTEGER NOT NULL

*   **Customers:**
    *   `CustomerID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `FirstName` TEXT NOT NULL
    *   `LastName` TEXT NOT NULL
    *   `Email` TEXT UNIQUE
    *   `PhoneNumber` TEXT

*   **Orders:**
    *   `OrderID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `CustomerID` INTEGER NOT NULL, FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
    *   `OrderDate` DATETIME DEFAULT CURRENT_TIMESTAMP
    *   `TotalAmount` REAL

*   **OrderItems:**
    *   `OrderItemID` INTEGER PRIMARY KEY AUTOINCREMENT
    *   `OrderID` INTEGER NOT NULL, FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
    *   `VideoCardID` INTEGER NOT NULL, FOREIGN KEY (VideoCardID) REFERENCES VideoCards(VideoCardID)
    *   `Quantity` INTEGER NOT NULL
    *   `Price` REAL NOT NULL

### Связи

*   **Один ко многим:**
    *   `Manufacturers` -> `GPUs`
    *   `Manufacturers` -> `VideoCards`
    *   `Shops` -> `Stocks`
    *   `Customers` -> `Orders`
    *   `GPUs` -> `VideoCards`
*   **Многие ко многим:**
    *   `Orders` <-> `VideoCards` (через `OrderItems`)

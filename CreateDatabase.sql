-- Manufacturers
CREATE TABLE Manufacturers (
    ManufacturerID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Country TEXT
);

-- GPUs
CREATE TABLE GPUs (
    GPUID INTEGER PRIMARY KEY AUTOINCREMENT,
    ManufacturerID INTEGER NOT NULL,
    Model TEXT NOT NULL,
    MemoryGB INTEGER NOT NULL,
    Architecture TEXT,
    FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)
);

-- VideoCards
CREATE TABLE VideoCards (
    VideoCardID INTEGER PRIMARY KEY AUTOINCREMENT,
    GPUID INTEGER NOT NULL,
    ModelName TEXT NOT NULL,
    ManufacturerID INTEGER NOT NULL,
    Price REAL NOT NULL,
    ClockSpeedMHz INTEGER,
    BoostClockSpeedMHz INTEGER,
    FOREIGN KEY (GPUID) REFERENCES GPUs(GPUID),
    FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)
);

-- Shops
CREATE TABLE Shops (
    ShopID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Address TEXT
);

-- Stocks
CREATE TABLE Stocks (
    StockID INTEGER PRIMARY KEY AUTOINCREMENT,
    ShopID INTEGER NOT NULL,
    VideoCardID INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    FOREIGN KEY (ShopID) REFERENCES Shops(ShopID),
    FOREIGN KEY (VideoCardID) REFERENCES VideoCards(VideoCardID)
);

-- Customers
CREATE TABLE Customers (
    CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    Email TEXT UNIQUE,
    PhoneNumber TEXT
);

-- Orders
CREATE TABLE Orders (
    OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerID INTEGER NOT NULL,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalAmount REAL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- OrderItems
CREATE TABLE OrderItems (
    OrderItemID INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderID INTEGER NOT NULL,
    VideoCardID INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    Price REAL NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (VideoCardID) REFERENCES VideoCards(VideoCardID)
);


-- Sample Data
INSERT INTO Manufacturers (Name, Country) VALUES
('NVIDIA', 'USA'),
('AMD', 'USA'),
('ASUS', 'Taiwan'),
('MSI', 'Taiwan'),
('Gigabyte', 'Taiwan');

INSERT INTO GPUs (ManufacturerID, Model, MemoryGB, Architecture) VALUES
(1, 'RTX 3070', 8, 'Ampere'),
(1, 'RTX 3080', 10, 'Ampere'),
(2, 'RX 6800 XT', 16, 'RDNA 2');

INSERT INTO VideoCards (GPUID, ModelName, ManufacturerID, Price, ClockSpeedMHz, BoostClockSpeedMHz) VALUES
(1, 'RTX 3070 Gaming OC', 3, 650.00, 1500, 1750),
(2, 'RTX 3080 Ventus 3X', 4, 850.00, 1440, 1710),
(3, 'RX 6800 XT Gaming OC', 5, 780.00, 1825, 2250);

INSERT INTO Shops (Name, Address) VALUES
('Tech Store', '123 Main St'),
('PC Paradise', '456 Oak Ave');

INSERT INTO Stocks (ShopID, VideoCardID, Quantity) VALUES
(1, 1, 10),
(1, 2, 5),
(2, 3, 8);

INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber) VALUES
('John', 'Doe', 'john.doe@example.com', '555-1234'),
('Jane', 'Smith', 'jane.smith@example.com', '555-5678');

INSERT INTO Orders (CustomerID, TotalAmount) VALUES
(1, 650.00),
(2, 1630.00);

INSERT INTO OrderItems (OrderID, VideoCardID, Quantity, Price) VALUES
(1, 1, 1, 650.00),
(2, 2, 1, 850.00),
(2, 3, 1, 780.00);
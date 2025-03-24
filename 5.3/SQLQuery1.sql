CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName VARCHAR(100),
    Stock INT
);

INSERT INTO Products (ProductName, Stock) VALUES 
('Laptop', 50), 
('Smartphone', 30), 
('Tablet', 20);

INSERT INTO Products (ProductName, Stock) VALUES
('iPhone 14 Pro', 50),
('Samsung Galaxy S23', 40),
('Xiaomi Redmi Note 12', 70),
('OnePlus 11', 30),
('Google Pixel 7', 25),
('Huawei P50', 20),
('Realme GT Neo 3', 35),
('Asus ROG Phone 6', 15),
('Sony Xperia 1 IV', 10),
('Nokia X30', 18);

--Task: Decrease Stock for Each Product by 5 Using Cursor
DECLARE @Stock VARCHAR(50);
declare @ProductId int;
DECLARE products_cursor CURSOR FOR 
SELECT ProductID, Stock FROM Products;

OPEN products_cursor; 

FETCH NEXT FROM products_cursor INTO @ProductId, @Stock;

WHILE @@FETCH_STATUS = 0  
BEGIN
    update Products
	set Stock = @Stock - 5
	where ProductID = @ProductId;
	
    FETCH NEXT FROM products_cursor INTO @ProductId, @Stock;
END

CLOSE products_cursor;
DEALLOCATE products_cursor;

select * from Products

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName VARCHAR(100)
);

INSERT INTO Customers (CustomerName) VALUES 
('John Doe'), 
('Alice Smith'), 
('Bob Johnson'), 
('Emma Brown');
INSERT INTO Customers (CustomerName) VALUES
('Ahmadjon Karimov'),
('Dilnoza Saidova'),
('Jasur Xolmatov'),
('Madina Yoqubova'),
('Shoxrux Abduqodirov'),
('Nilufar Rahmonova'),
('Bekzod Ismoilov'),
('Gulnora Tursunova'),
('Sardor Normurodov'),
('Zarnigor Olimova');

--Task: Print All Customer Names Using Cursor
DECLARE @CustomerName VARCHAR(50);
DECLARE customer_cursor CURSOR FOR 
SELECT CustomerName FROM Customers;

OPEN customer_cursor; 

FETCH NEXT FROM customer_cursor INTO @CustomerName;

WHILE @@FETCH_STATUS = 0  
BEGIN
    PRINT 'Processing Customer: ' + @CustomerName;
    
    FETCH NEXT FROM customer_cursor INTO @CustomerName;
END

CLOSE customer_cursor;
DEALLOCATE customer_cursor;
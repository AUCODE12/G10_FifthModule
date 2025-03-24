--1
create table Users (
	UserId bigint identity(1, 1) primary key,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) null,
	Email nvarchar(100) unique not null,
	Balance decimal(10, 2) default 0,
	CreatedAt datetime default getdate()
);

create table Products (
	ProductId bigint identity(1, 1) primary key,
	Name nvarchar(100) not null,
	Description nvarchar(255) null,
	Price decimal(10, 2) not null,
	StockQuantity int default 0,
	CreatedAt datetime default getdate()
);

create table Orders (
	OrderId bigint identity(1, 1) primary key,
	TotalAmount decimal(10, 2) not null,
	Status nvarchar(50) default 'Pending',
	CreatedAt datetime default getdate(),

	UserId bigint not null,
	Constraint FK_Orders_Users foreign key (UserId) references Users(UserId)
);

create table OrderDeTails (
	OrderDeTailId bigint identity(1, 1) primary key,
	Quantity int not null,
	Price decimal(10, 2) not null,

	OrderId bigint not null,
	ProductId bigint not null,
	Constraint FK_OrderDeTails_Orders foreign key (OrderId) references Orders(OrderId),
	Constraint FK_OrderDeTails_Products foreign key (ProductId) references Products(ProductId)
);

create table Payments (
	PaymentId bigint identity(1, 1) primary key,
	Amount decimal(10, 2) not null,
	PaymentMethod nvarchar(50) not null,
	Status nvarchar(50) default 'Pending',
	CreatedAt datetime default getdate(),

	OrderId bigint not null,
	UserId bigint not null,
	Constraint FK_Payments_Orders foreign key (OrderId) references Orders(OrderId),
	Constraint FK_Payments_Users foreign key (UserId) references Users(UserId)
);

--2
-- Users
INSERT INTO Users (FirstName, LastName, Email, Balance) VALUES
('Ahmad', 'Aliyev', 'ahmad@mail.com', 150.00),
('Bekzod', 'Karimov', 'bekzod@mail.com', 200.50),
('Zuhra', 'Mamatova', 'zuhra@mail.com', 300.75),
('Ilyos', 'Saidov', 'ilyos@mail.com', 50.00),
('Nilufar', 'Xodjayeva', 'nilufar@mail.com', 500.00),
('Jamshid', 'Shukurov', 'jamshid@mail.com', 100.00),
('Madina', 'Tursunova', 'madina@mail.com', 250.00),
('Shahzod', 'Raximov', 'shahzod@mail.com', 175.00),
('Umida', 'Ibrohimova', 'umida@mail.com', 225.00),
('Sardor', 'Norboyev', 'sardor@mail.com', 300.00);

-- Products
INSERT INTO Products (Name, Description, Price, StockQuantity) VALUES
('Laptop', 'High-end gaming laptop', 1200.00, 10),
('Smartphone', 'Latest Android smartphone', 800.00, 15),
('Tablet', 'Lightweight tablet for work', 500.00, 20),
('Smartwatch', 'Fitness tracking smartwatch', 200.00, 30),
('Headphones', 'Noise-canceling headphones', 150.00, 25),
('Keyboard', 'Mechanical gaming keyboard', 100.00, 50),
('Mouse', 'Wireless ergonomic mouse', 50.00, 40),
('Monitor', '27-inch 4K monitor', 400.00, 15),
('Printer', 'All-in-one printer', 250.00, 10),
('Speaker', 'Bluetooth portable speaker', 75.00, 20);

-- Orders
INSERT INTO Orders (TotalAmount, Status, UserId) VALUES
(1200.00, 'Pending', 1),
(800.00, 'Completed', 2),
(500.00, 'Shipped', 3),
(200.00, 'Pending', 4),
(150.00, 'Completed', 5),
(100.00, 'Cancelled', 6),
(50.00, 'Pending', 7),
(400.00, 'Completed', 8),
(250.00, 'Shipped', 9),
(75.00, 'Pending', 10);

-- OrderDetails
INSERT INTO OrderDetails (Quantity, Price, OrderId, ProductId) VALUES
(1, 1200.00, 1, 1),
(1, 800.00, 2, 2),
(2, 250.00, 3, 3),
(1, 200.00, 4, 4),
(1, 150.00, 5, 5),
(2, 50.00, 6, 6),
(1, 50.00, 7, 7),
(1, 400.00, 8, 8),
(1, 250.00, 9, 9),
(2, 75.00, 10, 10);

-- Payments
INSERT INTO Payments (Amount, PaymentMethod, Status, OrderId, UserId) VALUES
(1200.00, 'Credit Card', 'Pending', 1, 1),
(800.00, 'PayPal', 'Completed', 2, 2),
(500.00, 'Bank Transfer', 'Completed', 3, 3),
(200.00, 'Credit Card', 'Pending', 4, 4),
(150.00, 'Cash', 'Completed', 5, 5),
(100.00, 'Credit Card', 'Cancelled', 6, 6),
(50.00, 'Bank Transfer', 'Pending', 7, 7),
(400.00, 'PayPal', 'Completed', 8, 8),
(250.00, 'Credit Card', 'Shipped', 9, 9),
(75.00, 'Cash', 'Pending', 10, 10);


--3 
-- 1 Qaysi foydalanuvchi eng ko‘p mahsulot sotib olgan?
create procedure GetUsersBuyMostProducts
as
begin
	select top 1 u.UserId, u.FirstName, Sum(oi.Quantity) as Quantity 
	from OrderDeTails oi
	inner join Orders o
	on o.OrderId = oi.OrderId
	inner join Users u
	on u.UserId = o.UserId
	Group by u.UserId, u.FirstName
	Order by Quantity desc
end

exec dbo.GetUsersBuyMostProducts 

-- 2 Qaysi mahsulot eng ko‘p sotilgan?
create procedure GetSellMostProduct
as 
begin
	select top 1 p.ProductId, p.Name, Sum(oi.Quantity) as Quantity
	from OrderDeTails oi
	inner join Products p
	on p.ProductId = oi.ProductId
	group by p.ProductId, p.Name
	order by Quantity desc
end

-- 3 Oxirgi 30 kun ichida o‘rtacha harid narxi qancha bo‘lgan?
create procedure GetLast30DaysAvgSum
as
begin
	select avg(oi.Price) as TotalPrice
	from Orders o
	inner join OrderDeTails oi 
	on o.OrderId = oi.OrderId
	where o.CreatedAt >= DATEADD(DAY, -30, GETDATE())
end

-- 4 Har bir foydalanuvchi qancha pul sarflagan?
create procedure UsersSpendPrice
as
begin
	select u.UserId, u.FirstName, sum(oi.Price * oi.Quantity) as Spend
	from Users u
	inner join Orders o
	on o.UserId = u.UserId
	inner join OrderDeTails oi
	on oi.OrderId = o.OrderId
	group by u.UserId, u.FirstName
	order by Spend desc
end

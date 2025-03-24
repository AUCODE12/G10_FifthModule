CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100)
);


CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    OrderDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Price DECIMAL(10,2)
);

CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY IDENTITY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT,
    Price DECIMAL(10,2)
);

INSERT INTO Customers (Name, Email) VALUES
('John Doe', 'john.doe@email.com'),
('Alice Smith', 'alice.smith@email.com'),
('Bob Johnson', 'bob.johnson@email.com'),
('Emma Brown', 'emma.brown@email.com'),
('James Wilson', 'james.wilson@email.com'),
('Olivia Martinez', 'olivia.martinez@email.com'),
('Liam Garcia', 'liam.garcia@email.com'),
('Sophia Lee', 'sophia.lee@email.com'),
('Mason Hall', 'mason.hall@email.com'),
('Isabella Allen', 'isabella.allen@email.com'),
('Ethan Young', 'ethan.young@email.com'),
('Ava King', 'ava.king@email.com'),
('Michael Wright', 'michael.wright@email.com'),
('Charlotte Scott', 'charlotte.scott@email.com'),
('Daniel Green', 'daniel.green@email.com'),
('Harper Adams', 'harper.adams@email.com'),
('Benjamin Baker', 'benjamin.baker@email.com'),
('Amelia Gonzalez', 'amelia.gonzalez@email.com'),
('Lucas Nelson', 'lucas.nelson@email.com'),
('Mia Carter', 'mia.carter@email.com');


INSERT INTO Products (Name, Price) VALUES
('Laptop', 1200.00),
('Smartphone', 800.00),
('Headphones', 150.00),
('Smartwatch', 250.00),
('Keyboard', 50.00),
('Mouse', 30.00),
('Monitor', 300.00),
('External HDD', 100.00),
('Gaming Chair', 400.00),
('Desk Lamp', 40.00),
('USB Flash Drive', 20.00),
('Wireless Router', 120.00),
('Graphics Card', 500.00),
('Power Bank', 70.00),
('Webcam', 60.00),
('Microphone', 110.00),
('VR Headset', 700.00),
('Portable Speaker', 90.00),
('Coffee Maker', 130.00),
('Fitness Tracker', 200.00);


INSERT INTO Orders (CustomerID, OrderDate) VALUES
(1, '2024-01-15'),
(2, '2024-01-20'),
(3, '2024-02-05'),
(4, '2024-02-10'),
(5, '2024-02-15'),
(6, '2024-02-20'),
(7, '2024-03-05'),
(8, '2024-03-10'),
(9, '2024-03-15'),
(10, '2024-03-20'),
(11, '2024-04-05'),
(12, '2024-04-10'),
(13, '2024-04-15'),
(14, '2024-04-20'),
(15, '2024-05-05'),
(16, '2024-05-10'),
(17, '2024-05-15'),
(18, '2024-05-20'),
(19, '2024-06-05'),
(20, '2024-06-10');


INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price) VALUES
(1, 1, 1, 1200.00), 
(1, 2, 2, 800.00),
(2, 3, 3, 150.00),
(2, 4, 1, 250.00),
(3, 5, 2, 50.00),
(3, 6, 1, 30.00),
(4, 7, 1, 300.00),
(4, 8, 2, 100.00),
(5, 9, 1, 400.00),
(5, 10, 3, 40.00),
(6, 11, 4, 20.00),
(6, 12, 1, 120.00),
(7, 13, 1, 500.00),
(7, 14, 2, 70.00),
(8, 15, 1, 60.00),
(8, 16, 1, 110.00),
(9, 17, 1, 700.00),
(9, 18, 2, 90.00),
(10, 19, 1, 130.00),
(10, 20, 3, 200.00);


-- Procedures
-- 1 Yangi mijoz qo‘shish uchun Name va Email bilan stored procedure yozing.
create procedure AddCustomer
	@name nvarchar(100),
	@email nvarchar(100)
As 
begin
	if exists (select 1 from Customers c Where c.Email = @email)
		begin
			print 'Bunday foydalanuvchi alaqachon bot'
			return;
		end

	insert into Customers (Name, Email)
	values (@name, @email);
end
select * from Customers
exec AddCustomer 'Ahmadjon', 'Qahmadjon11@gmail.com'

-- 2 Yangi mahsulot qo‘shish uchun Name va Price bilan stored procedure yozing.
create procedure AddProduct
	@name nvarchar(100),
	@price decimal(10, 2)
As 
begin
	insert into products (Name, Price)
	values (@name, @price);
end

exec AddProduct 'olma', 19.99

-- 3 Berilgan CustomerID uchun yangi buyurtma qo‘shish uchun stored procedure yozing.
create procedure AddOrder
	@CustomerId int,
	@OrderDate DateTime
As
begin
	if not exists (select 1 from Customers c where c.CustomerId = @CustomerId)
	begin
		print 'bunday foydalanuvchi yoq'
		return;
	end

	insert into Orders (CustomerId, OrderDate)
	values (@CustomerId, @OrderDate)
end

exec AddOrder 2, '2022-01-15'

-- 4 Buyurtma elementi qo‘shish uchun OrderID, ProductID, Quantity va Price bilan stored procedure yozing.
create procedure AddOrderItems
	@OrderId int,
	@ProductId int,
	@Quantity int,
	@Price decimal(10, 2) 
As
begin
	if not exists (select 2 from Orders o, Products p where o.OrderId = @OrderId and p.ProductID = @ProductId)
	begin
		print 'bunday order yoki product yo''q'
		return;
	end

	insert into OrderItems (OrderID, ProductID, Quantity, Price)
	values (@OrderId ,@ProductId, @Quantity, @Price)
end

exec AddOrderItems 2, 1, 5, 500

-- 5 Mijozning elektron pochtasini yangilash uchun CustomerID bo‘yicha stored procedure yozing.
create procedure EditEmailByCustomerId
	@CustomerId int ,
	@Email nvarchar(100)
as
begin
	if not exists (select 1 from Customers c where c.CustomerID = @CustomerId)
	begin
		print 'buday foydalanuvchi yo''q'
		return
	end
	if exists (select 1 from Customers c where c.Email = @Email)
	begin 
		print 'bunday email bor'
		return
	end 
	Update Customers
	set Email = @Email
	where CustomerID = @CustomerId
end

exec EditEmailByCustomerId 2, 'ahmadjonahmadjon464@gmail.com'

-- 6 Mahsulot narxini yangilash uchun ProductID bo‘yicha stored procedure yozing.
create procedure EditPriceByProductId
	@ProductId int,
	@Price decimal(10, 2)
as
begin
	if not exists (select 1 from Products p where p.ProductID = @ProductId) 
	begin
		print 'Bunday mahsulot yo''q'
		return
	end
	Update Products 
	set Price = @Price
	where ProductID = @ProductId
end

exec EditPriceByProductId 2, 999.99

-- 7 Mijozni CustomerID bo‘yicha o‘chirish uchun stored procedure yozing. Agar mijozda buyurtmalar bo‘lsa, ularni ham o‘chiring.
create procedure DeleteCustomerAndOrdersByCustomerId
	@CustomerId int
as
begin
	if not exists (select 1 from Customers c where c.CustomerID = @CustomerId)
	begin
		print 'bunday mijoz yo''q'
		return;
	end
	delete from Customers where CustomerID = @CustomerId;
	if not exists (select 1 from Orders o where o.CustomerId = @CustomerId)
	begin
		print 'bunday mijozni buyurtmasi yo''q'
		return
	end
	delete from Orders where CustomerId = @CustomerId;
end

-- 8 Buyurtmani OrderID bo‘yicha va uning barcha buyurtma elementlarini o‘chirish uchun stored procedure yozing.
create procedure DeleteOrdersAndOrderItemsByOrderId
	@OrderId int
as
begin
	if not exists (select 1 from Orders where OrderID = @OrderId)
	begin
		print 'bunday buyurtma yo''q'
		return;
	end
	delete from Orders where OrderID = @OrderId;
	if not exists (select 1 from OrderItems where OrderId = @OrderId)
	begin
		print 'bunday orderni itemi yo''q'
		return
	end
	delete from OrderItems where OrderID = @OrderId;
end

exec DeleteOrdersAndOrderItemsByOrderId 2

-- 9 Mijozning buyurtma ma’lumotlarini olish uchun stored procedure yozing (INNER JOIN ishlating).
create procedure GetCustomersAndOrders 
as
begin
	select c.CustomerId, c.Name, o.OrderId, o.OrderDate
	from Customers c
	inner join Orders o
	on c.CustomerID = o.CustomerID
end

exec GetCustomersAndOrders

-- 10 Barcha buyurtmalarni ularning buyurtma elementlari bilan olish uchun stored procedure yozing (LEFT JOIN ishlating).
create procedure GetOrdersAndOrderItems 
as
begin
	select o.OrderId, o.OrderDate, oi.OrderItemID, oi.Price, oi.Quantity 
	from Orders o
	left join OrderItems oi
	on o.OrderID = oi.OrderID
	order by o.OrderID
end

exec GetOrdersAndOrderItems

-- 11 Barcha mahsulotlarni ularning buyurtma tafsilotlari bilan olish uchun stored procedure yozing (LEFT JOIN ishlating).
create procedure GetProductAndItems
as 
begin
	select p.ProductID, p.Name, p.Price, oi.OrderItemID, oi.OrderID, oi.Price, oi.Quantity
	from Products p
	left join OrderItems oi
	on oi.ProductID = p.ProductID
	group by p.ProductID, p.Name, p.Price, oi.OrderItemID, oi.OrderID, oi.Price, oi.Quantity  
	order by p.ProductID
end

exec GetProductAndItems

-- 12 Har bir mijoz tomonidan sarflangan umumiy summani hisoblash uchun stored procedure yozing.
create procedure CustumorTotalMoney
as
begin
	select c.CustomerID, c.Name, Sum(oi.Price * oi.Quantity) as TotalPrice
	from Customers c
	inner join Orders o
	on o.CustomerID = c.CustomerID
	inner join OrderItems oi
	on oi.OrderId = o.OrderID
	Group by c.CustomerID, c.Name
	order by TotalPrice
end

exec CustumorTotalMoney

-- 13 Har bir mahsulot bo‘yicha umumiy tushumni hisoblash uchun stored procedure yozing.
create procedure ProductTotalMoney
as
begin
	select p.ProductID, p.Name, sum(oi.Price * oi.Quantity) as TotalPrice
	from Products p
	left join OrderItems oi
	on oi.ProductID = p.ProductID
	group by p.ProductID, p.Name
	order by TotalPrice
end

exec ProductTotalMoney

-- 14 Mijoz mavjudligini CustomerID orqali tekshirish uchun stored procedure yozing (IF EXISTS ishlating).
create procedure ExistsCustomerById
	@CustomerId int
as
begin
	if not exists (select 1 from Customers where CustomerID = @CustomerId)
	begin
		print 'Bunday mijoz yo''q'
		return
	end
	print 'Bunday mijoz bor'
	return
end

exec ExistsCustomerById 1

-- 15 Eng so‘nggi N ta buyurtmani OrderDate bo‘yicha saralab olish uchun stored procedure yozing.
create procedure GetNOrders
	@n int 
as
begin
	select top (@n) *
	from Orders
	order by OrderDate
end

exec GetNOrders 5

-- 16 Hech qachon buyurtma qilinmagan mahsulotlarni topish uchun stored procedure yozing.
create procedure GetProductNoOrder
as 
begin
	select p.ProductID, p.Name
	from Products p
	left join OrderItems oi
	on oi.ProductID = p.ProductID
	where oi.ProductID is null
	group by p.ProductID, p.Name
end

exec GetProductNoOrder


--Functions
-- 1. Berilgan CustomerID bo‘yicha mijozning email manzilini qaytaruvchi funksiya yozing.
-- Bu funksiya CustomerID ni qabul qilib, mijozning email manzilini qaytarishi kerak.
CREATE FUNCTION GetCustomerEmailById (@CustomerId INT)
returns nvarchar(100)
as
begin
	declare @Email nvarchar(100); 
	
	select @Email = Email
	from Customers
	where CustomerID = @CustomerId

	return @Email;
end

select dbo.GetCustomerEmailById(3)

-- 2. Berilgan CustomerID bo‘yicha mijoz joylashtirgan buyurtmalar sonini qaytaruvchi funksiya yozing.
-- Funksiya CustomerID ni qabul qilib, shu mijoz joylashtirgan umumiy buyurtmalar sonini qaytarishi kerak.
CREATE FUNCTION GetOrdersByCutomerId (@CustomerId INT)
returns int
as
begin
	declare @Count int; 
	
	select @Count = Count(OrderID)
	from Orders
	where CustomerID = @CustomerId

	return @Count;
end

select dbo.GetOrdersByCutomerId(2)

-- 3. Berilgan OrderID bo‘yicha buyurtma umumiy summasini qaytaruvchi funksiya yozing.
-- Funksiya OrderID ni qabul qilib, Quantity × Price bo‘yicha umumiy summani qaytarishi kerak.
create function GetTotalPriceByOrderId (@OrderId int)
returns int
as 
begin
	declare @TotoalPrice int;

	select @TotoalPrice = Sum(Quantity * Price)
	from OrderItems 
	where OrderID = @OrderId

	return @TotoalPrice;
end

select dbo.GetTotalPriceByOrderId(4)

-- 4. Berilgan CustomerID bo‘yicha barcha buyurtmalarni qaytaruvchi funksiya yozing.
-- Funksiya jadval sifatida OrderID va OrderDate ustunlarini qaytarishi kerak.
create function GetOrderByCustomeId (@CustomerId int)
returns table
as
return 
(
	select OrderId, OrderDate
	from Orders
	where CustomerID = @CustomerId
);

select * from GetOrderByCustomeId(2)
-- 5. Eng qimmat mahsulotni qaytaruvchi funksiya yozing.
-- Bu funksiya ProductID, Name va Price ustunlarini qaytarishi kerak.
create function GetProductMostPrice()
returns table
as
return 
(
	select top 1 ProductID, Name, Price
	from Products
	Order by Price desc
);

select * from GetProductMostPrice()

-- 6. Berilgan OrderID bo‘yicha barcha mahsulotlarni qaytaruvchi funksiya yozing.
-- Funksiya ProductID, Name, Quantity va Price ustunlarini qaytarishi kerak.
create function GetProductByOrderId(@OrderId int)
returns table
as
return 
(
	select p.ProductID, p.Name, p.Price, oi.Quantity
	from Products p
	inner join OrderItems oi
	on oi.ProductID = p.ProductID
	inner join Orders o
	on o.OrderID = oi.OrderID
	where o.OrderID = @OrderId
);

select * from GetProductByOrderId(5)
-- 7. Berilgan ProductID bo‘yicha umumiy daromadni qaytaruvchi funksiya yozing.
-- Funksiya ProductID ni qabul qilib, ushbu mahsulotdan olingan umumiy daromadni qaytarishi kerak.
create function GetProductTotalProfitById (@ProductId int)
returns int
as
begin
	declare @TotalProfit int;
	select @TotalProfit = Sum(oi.Price * oi.Quantity)
	from Products p
	left join OrderItems oi
	on p.ProductID = oi.ProductID
	where p.ProductID = @ProductId;
	return @TotalProfit
end

select dbo.GetProductTotalProfitById(10)

-- 8. Berilgan CustomerID bo‘yicha mijoz buyurtma bergan-bermaganligini tekshiruvchi funksiya yozing.
-- Funksiya agar mijoz buyurtma bergan bo‘lsa 1, aks holda 0 qaytarishi kerak.
create function ExistsOrderInCustomer (@CustomerId int)
returns int
as
begin
	declare @Exists bit;
	if exists (select 1 from Orders where CustomerID = @CustomerId)
	begin
		set @Exists = 1;
	end
	else
	begin
		set @Exists = 0;
	end
	return @Exists;
end

INSERT INTO Customers (Name, Email) VALUES
('Uwil Hunting', 'ahamdjon@email.com')

select dbo.ExistsOrderInCustomer(21)

-- 9. Berilgan CustomerID bo‘yicha eng so‘nggi buyurtma sanasini qaytaruvchi funksiya yozing.
-- Funksiya CustomerID ni qabul qilib, ushbu mijozning eng so‘nggi buyurtma sanasini qaytarishi kerak.
create function LastOrderInCustomerId (@CustomerId int)
returns table
as
return
(
	select top 1 * 
	from Orders
	where CustomerID = @CustomerId
	Order by OrderDate desc
);

select * from Orders
INSERT INTO Orders (CustomerID, OrderDate) VALUES
(1, '2024-01-16')

select * from LastOrderInCustomerId(1)

-- 10. Berilgan OrderID bo‘yicha buyurtmada nechta turdagi mahsulot borligini qaytaruvchi funksiya yozing.
-- Funksiya OrderID ni qabul qilib, buyurtmada necha xil mahsulot borligini qaytarishi kerak.
create function ProductAmountInOrder (@OrderId int)
returns int
as
begin
	declare @Count int;
	SELECT @Count = Count(ProductID) FROM OrderItems WHERE OrderID = @OrderID;
	return @Count;
end

drop function dbo.ProductAmountInOrder
select * from OrderItems

INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price) VALUES
(10, 1, 1, 1200.00) 

select dbo.ProductAmountInOrder(10)
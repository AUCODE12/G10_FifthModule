-- 5.3 all task

--Tasks


--Task 1: Generate Random Usernames from First & Last Names

CREATE TABLE FirstNames (
    Name NVARCHAR(50)
);

INSERT INTO FirstNames (Name) VALUES ('Cool'), ('Smart');

CREATE TABLE LastNames (
    Name NVARCHAR(50)
);

INSERT INTO LastNames (Name) VALUES ('Tiger'), ('Eagle');

create procedure GenerateUsername
as
begin
	select (f.Name + l.Name) as Username
	from FirstNames f
	cross join LastNames l
end


--Task 2: Find Customers Who Can Afford a Product


CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    Name NVARCHAR(50),
    Budget DECIMAL(10,2)
);

INSERT INTO Customers (CustomerID, Name, Budget) VALUES
(1, 'Alice', 1200.00), 
(2, 'Bob', 800.00), 
(3, 'Charlie', 400.00);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(50),
    Price DECIMAL(10,2)
);

INSERT INTO Products (ProductID, ProductName, Price) VALUES
(1, 'Laptop', 1000.00), 
(2, 'Phone', 500.00);

create procedure GetAffordCustomerInProducts
as
begin
	select c.Name, c.Budget, p.ProductName, p.Price
	from Customers c
	cross join Products p
	where c.Budget >= p.Price
end


--Task 3: Pair All Students & Subjects and Sort by Student Name


CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    StudentName NVARCHAR(50)
);

INSERT INTO Students (StudentID, StudentName) VALUES
(1, 'David'), (2, 'Sophia'), (3, 'Alice');

CREATE TABLE Subjects (
    SubjectID INT PRIMARY KEY,
    SubjectName NVARCHAR(50)
);

INSERT INTO Subjects (SubjectID, SubjectName) VALUES
(1, 'Math'), (2, 'History');

create procedure GetPairSSSortStudentName
as
begin
	select st.StudentName, su.SubjectName
	from Students st
	cross join Subjects su
	order by st.StudentName
end


-- Tasks for CURSOR

-- Task: Print All Customer Names Using Cursor

CREATE TABLE Customers1 (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName VARCHAR(100)
);

INSERT INTO Customers1 (CustomerName) VALUES 
('John Doe'), 
('Alice Smith'), 
('Bob Johnson'), 
('Emma Brown');

declare @CustomerName varchar(100);
declare print_cursor cursor for
select CustomerName from Customers1;

open print_cursor;
fetch next from print_cursor into @CustomerName;

while @@FETCH_STATUS = 0
begin
	print @CustomerName;

	fetch next from print_cursor into @CustomerName;
end

close print_cursor; 
deallocate print_cursor;

----- 
-- Task: Decrease Stock for Each Product by 5 Using Cursor

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName VARCHAR(100),
    Stock INT
);

INSERT INTO Products (ProductName, Stock) VALUES 
('Laptop', 50), 
('Smartphone', 30), 
('Tablet', 20);


create procedure DecreaseProductStock @Decrease int
as 
begin
	declare @ProductId int;
	declare decrease_cursor cursor for
	select Stock from Products;

	open decrease_cursor;
	fetch next from decrease_cursor into @ProductId;

	while @@Fetch_status = 0
	begin
		update Products 
		set Stock -= 10
		where ProductID = @ProductId;

		fetch next from decrease_cursor into @ProductId;
	end

	close decrease_cursor;
	deallocate decrease_cursor;
end
----------------
-- Task: Delete Customers Who Have Never Placed an Order Using Cursor

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

INSERT INTO Orders (CustomerID) VALUES (1), (2);  -- Only John and Alice placed orders


----------------------------------- 

-- Task: Set Active = 0 for Accounts with Zero Balance Using Cursor

CREATE TABLE PDPOnlineAccounts (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    AccountHolder VARCHAR(100),
    Balance DECIMAL(10,2),
    Active BIT DEFAULT 1  -- 1 = Active, 0 = Inactive
);

INSERT INTO PDPOnlineAccounts (AccountHolder, Balance) VALUES 
('John Doe', 5000.00), 
('Alice Smith', 0.00),  -- Will be deactivated
('Bob Johnson', 7000.00), 
('Emma Brown', 0.00);   -- Will be deactivated


-----


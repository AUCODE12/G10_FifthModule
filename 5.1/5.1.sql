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

create procedure AddProduct
	@name nvarchar(100),
	@price decimal(10, 2)
As 
begin
	insert into products (Name, Price)
	values (@name, @price);
end

exec AddProduct 'olma', 19.99


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

drop procedure AddOrder

exec AddOrder 4, '2022-01-15'


create procedure AddOrderItems
	@OrderId int,
	@ProductId int,
	@Quantity int,
	@price decimal(10, 2) 
As
begin
	if not exists (select 1 from Customers c where c.OrderId = @CustomerId)
	begin
		print 'bunday foydalanuvchi yoq'
		return;
	end

	insert into Orders (CustomerId, OrderDate)
	values (@CustomerId, @OrderDate)
end
-- Создание бд
USE master;

CREATE DATABASE FurnitureFabric;

GO
-- Создание таблиц
USE [FurnitureFabric];

GO

CREATE TABLE [Employees](
	Id INT PRIMARY KEY,
	FIO NVARCHAR(100) NOT NULL,
	Position NVARCHAR(50) NOT NULL,
	Education NVARCHAR(200) NOT NULL
)

CREATE TABLE [Clients](
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(25) NOT NULL,
	RepresentativeFIO NVARCHAR(100) NOT NULL,
	[Number] INT NOT NULL,
	[Address] NVARCHAR(40) NOT NULL
)

CREATE TABLE [Furniture](
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(200) NOT NULL,
	Material NVARCHAR(60) NOT NULL,
	Price DECIMAL NOT NULL,
	[Count] INT NOT NULL
)

CREATE TABLE [Orders](
	Id INT PRIMARY KEY,
	ClientId INT NOT NULL FOREIGN KEY REFERENCES [Clients] (Id),
	FurnitureId INT NOT NULL FOREIGN KEY REFERENCES [Furniture] (Id),
	FurnitureCount INT NOT NULL,
	Price DECIMAL NOT NULL,
	DiscountPercent INT NOT NULL,
	IsCompleted INT NOT NULL,
	EmployeeId INT NOT NULL REFERENCES [Employees] (Id)
)

CREATE TABLE [Waybills](
	Id INT PRIMARY KEY,
	ProviderId INT NOT NULL,
	ProviderName NVARCHAR(100) NOT NULL,
	DateOfSupply DATETIME NOT NULL,
	Material NVARCHAR(60) NOT NULL,
	Price DECIMAL NOT NULL,
	[Weight] FLOAT NOT NULL,
	FurnitureId INT NOT NULL FOREIGN KEY REFERENCES [Furniture] (Id),
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES [Employees] (Id)
)

GO

--Создание процедур

CREATE PROCEDURE [dbo].[AddOrder]
    @Id INT,
    @Client NVARCHAR(25),
    @Furniture NVARCHAR(50),
    @Count INT,
	@Price DECIMAL,
	@Discount INT,
	@IsCompl INT,
	@Employee NVARCHAR(100)
AS
DECLARE @ClientId INT, @FurnitId INT, @EmplId INT;
SET @ClientId = (SELECT TOP(1) Id FROM [Clients] WHERE Clients.[Name] = @Client);
SET @FurnitId = (SELECT TOP(1) Id FROM [Furniture] WHERE Furniture.[Name] = @Furniture);
SET @EmplId = (SELECT TOP(1) Id FROM [Employees] WHERE Employees.[FIO] = @Employee);
INSERT INTO [Orders](Id, ClientId, FurnitureId, FurnitureCount, Price, DiscountPercent, IsCompleted, EmployeeId) 
	VALUES(@Id, @ClientId, @FurnitId, @Count, @Price, @Discount, @IsCompl, @EmplId)
UPDATE Furniture SET [Count] = [Count] - @Count 
	WHERE Id = @FurnitId

GO

CREATE PROCEDURE [dbo].[UpdateOrder]
    @Id INT,
    @Client NVARCHAR(25),
    @Furniture NVARCHAR(50),
    @Count INT,
	@Price DECIMAL,
	@Discount INT,
	@IsCompl INT,
	@Employee NVARCHAR(100)
AS
DECLARE @ClientId INT, @FurnitId INT, @EmplId INT;
SET @ClientId = (SELECT TOP(1) Id FROM [Clients] WHERE Clients.[Name] = @Client);
SET @FurnitId = (SELECT TOP(1) Id FROM [Furniture] WHERE Furniture.[Name] = @Furniture);
SET @EmplId = (SELECT TOP(1) Id FROM [Employees] WHERE Employees.[FIO] = @Employee);
UPDATE Orders SET ClientId = @Client, FurnitureId = @FurnitId, FurnitureCount = @Count, Price = @Price, DiscountPercent = @Discount, IsCompleted = @IsCompl, EmployeeId = @EmplId
	WHERE Id = @Id

GO 

CREATE PROCEDURE [dbo].[AddWaybill]
    @Id INT,
	@Provider INT,
	@ProvName NVARCHAR(100),
	@Date DATETIME,
	@Material NVARCHAR(60),
	@Price DECIMAL,
	@Weight FLOAT,
	@Furniture NVARCHAR(50),
	@Employee INT
AS
DECLARE @EmplId INT, @FurnitId INT;
SET @EmplId = (SELECT TOP(1) Id FROM [Employees] WHERE Employees.[FIO] = @Employee);
SET @FurnitId = (SELECT TOP(1) Id FROM Furniture WHERE Furniture.[Name] = @Furniture);
INSERT INTO [Waybills] (Id, ProviderId, ProviderName, DateOfSupply, Material, Price, [Weight], FurnitureId, EmployeeId) 
	VALUES(@Id, @Provider, @ProvName, @Date, @Material, @Price, @Weight, @FurnitId, @EmplId)

GO

CREATE PROCEDURE [dbo].[UpdateWaybill]
    @Id INT,
	@Provider INT,
	@ProvName NVARCHAR(100),
	@Date DATETIME,
	@Material NVARCHAR(60),
	@Price DECIMAL,
	@Weight FLOAT,
	@Furniture NVARCHAR(50),
	@Employee INT
AS
DECLARE @EmplId INT, @FurnitId INT;
SET @EmplId = (SELECT TOP(1) Id FROM [Employees] WHERE Employees.[FIO] = @Employee);
SET @FurnitId = (SELECT TOP(1) Id FROM Furniture WHERE Furniture.[Name] = @Furniture);
UPDATE Waybills SET ProviderId = @Provider, ProviderName = @ProvName, DateOfSupply = @Date, Material = @Material, Price = @Price, [Weight] = @Weight, FurnitureId = @FurnitId, EmployeeId = @EmplId
	WHERE Id = @Id

GO

-- Создание View

CREATE VIEW OrderView AS
SELECT Orders.Id, Clients.[Name] AS Client, Furniture.[Name] AS Furniture, Furniture.Material, Orders.FurnitureCount, Orders.Price, Orders.DiscountPercent, Orders.IsCompleted, Employees.FIO AS EmployeeFIO
	FROM Orders
	JOIN Clients ON Clients.Id = Orders.ClientId
	JOIN Furniture ON Furniture.Id = Orders.FurnitureId
	JOIN Employees ON Employees.Id = Orders.EmployeeId

GO

CREATE VIEW WaybillsView AS
SELECT Waybills.Id, Waybills.ProviderId, Waybills.ProviderName, Waybills.DateOfSupply, .Waybills.Material, Waybills.Price, Waybills.[Weight], Furniture.[Name] As Furniture, Employees.FIO AS EmployeeFIO
	FROM Waybills
	JOIN Employees ON Employees.Id = Waybills.EmployeeId
	JOIN Furniture ON Furniture.Id = Waybills.FurnitureId

GO

CREATE VIEW FurnitureView AS
SELECT Furniture.Id, Furniture.[Name], Furniture.[Description], Furniture.Material, .Furniture.Price, Furniture.[Count]
	FROM Furniture

GO

DECLARE @step INT = 1;
DECLARE @Symbol CHAR(52) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';

-- Заполнение таблицы клиентов
DECLARE @secondStep INT;
DECLARE @Position INT;
DECLARE @Length INT;
DECLARE @Name NVARCHAR(25);
DECLARE @RepFIO NVARCHAR(100);
DECLARE @Address NVARCHAR(40);

WHILE @step <= 20
	BEGIN
		SET @Length = 5 + RAND()*(25-5)
		SET @Name = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Name = @Name + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(100-5)
		SET @RepFIO = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @RepFIO = @RepFIO + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(40-5)
		SET @Address = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Address = @Address + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END
		INSERT INTO Clients (Id, [Name], RepresentativeFIO, Number, [Address]) VALUES (@step, @Name, @RepFIO, 1 + RAND() * 10000000, @Address)
		SET @step = @step + 1;
	END;

-- Заполнение таблицы работников
SET @step = 21;
DECLARE @FIO NVARCHAR(100);
DECLARE @Pos NVARCHAR(50);
DECLARE @Education NVARCHAR(200);

WHILE @step <= 40
	BEGIN
		SET @Length = 5 + RAND()*(100-5)
		SET @FIO = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @FIO = @FIO + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(50-5)
		SET @Pos = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Pos = @Pos + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(200-5)
		SET @Education = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Education = @Education + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END
		INSERT INTO Employees (Id, FIO, Position, Education) VALUES (@step, @FIO, @Pos, @Education)
		SET @step = @step + 1;
	END;

-- Заполнение таблицы мебели
SET @step = 41;
DECLARE @secondName NVARCHAR(50);
DECLARE @Descr NVARCHAR(200);
DECLARE @Material NVARCHAR(60);

WHILE @step <= 60
	BEGIN
		SET @Length = 5 + RAND()*(50-5)
		SET @secondName = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @secondName = @secondName + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(200-5)
		SET @Descr = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Descr = @Descr + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(60-5)
		SET @Material = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Material = @Material + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END
		INSERT INTO Furniture (Id, [Name], [Description] , Material, Price, [Count]) VALUES (@step, @secondName, @Descr, @Material, 1.0 + RAND() * 100.0 + RAND(), 1 + RAND()*10);
		SET @step = @step + 1;
	END;

-- Заполнение таблицы заказов
SET @step = 61;
DECLARE @Client INT;
DECLARE @Furnit INT;
DECLARE @Empl INT;
WHILE @step <= 160
	BEGIN
		SET @Client = 1 + RAND() * 10;
		SET @Furnit = 41 + RAND() * 10;
		SET @Empl = 21 + RAND() * 10;
		WHILE @Client > 20
		BEGIN
			SET @Client = 1 + RAND() * 10;
		END

		WHILE @Furnit > 60
		BEGIN
			SET @Furnit = 41 + RAND() * 10;
		END

		WHILE @Empl > 40
		BEGIN
			SET @Empl = 21 + RAND() * 10;
		END

		INSERT INTO Orders (Id, ClientId, FurnitureId ,FurnitureCount, Price, DiscountPercent, IsCompleted, EmployeeId) VALUES (@step, @Client, @Furnit, 1 + RAND() * 100, 1.0 + RAND() * 100.0 + RAND(), 1 + RAND()*10, ROUND(RAND(), 1), @Empl);
		SET @step = @step + 1;
	END;

-- Заполнение таблицы накладными
SET @step = 161;
DECLARE @ProvName NVARCHAR(100);
DECLARE @FurnitId INT;
WHILE @step <= 260
	BEGIN
		SET @Empl = 21 + RAND() * 10;
		SET @FurnitId = 41 + RAND() * 10;
		SET @Length = 5 + RAND()*(100-5);
		SET @ProvName = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @ProvName = @ProvName + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		SET @Length = 5 + RAND()*(60-5);
		SET @Material = '';
		SET @secondStep = 1;
		WHILE @secondStep <= @Length
		BEGIN
			SET @Position=RAND()*52;
			SET @Material = @Material + SUBSTRING(@Symbol, @Position, 1)
			SET @secondStep = @secondStep + 1
		END

		WHILE @Empl > 40
		BEGIN
			SET @Empl = 21 + RAND() * 10;
		END

		WHILE @FurnitId > 60
		BEGIN
			SET @FurnitId = 41 + RAND() * 10;
		END

		INSERT INTO Waybills (Id, ProviderId, ProviderName ,DateOfSupply, Material, Price, [Weight], FurnitureId, EmployeeId) VALUES (@step, 1 + RAND() * 1000, @ProvName, DATEADD(d, -1 * RAND() * 100, GETDATE()), @Material, 1.0 + RAND() * 100.0 + RAND(), 1 + RAND()*10, @FurnitId, @Empl);
		SET @step = @step + 1;
	END;
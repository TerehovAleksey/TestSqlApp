-- создание баз и таблиц---

CREATE DATABASE BD1
GO

USE BD1
GO

CREATE TABLE Names(
[Id] INT PRIMARY KEY IDENTITY(1,1),
[Article] UNIQUEIDENTIFIER NOT NULL,
[Name] NVARCHAR(250) NOT NULL
)
GO

CREATE TABLE Amounts(
[Id] INT PRIMARY KEY IDENTITY(1,1),
[Article] UNIQUEIDENTIFIER NOT NULL,
[Quantity] INT NOT NULL
)
GO

CREATE TABLE Prices(
[Id] INT PRIMARY KEY IDENTITY(1,1),
[Article] UNIQUEIDENTIFIER NOT NULL,
[Price] MONEY NOT NULL
)
GO

CREATE DATABASE BD2
GO

USE BD2
GO

CREATE TABLE Products(
[Id] INT PRIMARY KEY IDENTITY(1,1),
[Article] UNIQUEIDENTIFIER NOT NULL,
[Name] NVARCHAR(250) NOT NULL,
[Quantity] INT NOT NULL,
[Price] MONEY NOT NULL
)
GO

-- заполнение данными --

--- 20 позиций для BD2 ---
INSERT INTO Products ([Article], [Name], [Quantity], [Price]) VALUES
(NEWID(), N'Product 1', 10, 100), 
(NEWID(), N'Product 2', 20, 200),
(NEWID(), N'Product 3', 30, 300),
(NEWID(), N'Product 4', 40, 400),
(NEWID(), N'Product 5', 50, 500),
(NEWID(), N'Product 6', 60, 600),
(NEWID(), N'Product 7', 70, 700),
(NEWID(), N'Product 8', 80, 800),
(NEWID(), N'Product 9', 90, 900),
(NEWID(), N'Product 10', 100, 1000),
(NEWID(), N'Product 11', 110, 1100),
(NEWID(), N'Product 12', 120, 1200),
(NEWID(), N'Product 13', 130, 1300),
(NEWID(), N'Product 14', 140, 1400),
(NEWID(), N'Product 15', 150, 1500),
(NEWID(), N'Product 16', 160, 1600),
(NEWID(), N'Product 17', 170, 1700),
(NEWID(), N'Product 18', 180, 1800),
(NEWID(), N'Product 19', 190, 1900),
(NEWID(), N'Product 20', 200, 2000)
GO

--- 10 совпадающих позиций в базах
DECLARE @article UNIQUEIDENTIFIER, @name NVARCHAR(250), @qt INT, @price MONEY;
DECLARE @cursor CURSOR
SET @cursor = CURSOR SCROLL 
	FOR SELECT TOP(10)
		p.[Article],
		p.[Name],
		p.[Quantity],
		p.[Price]
		FROM Products p
		ORDER BY [Article]
OPEN @cursor
FETCH NEXT FROM @cursor
    INTO @article, @name, @qt, @price
WHILE @@FETCH_STATUS = 0
BEGIN
INSERT INTO BD1.dbo.Names ([Article], [Name]) VALUES (@article, @name);
INSERT INTO BD1.dbo.Amounts ([Article], [Quantity]) VALUES (@article, @qt);
INSERT INTO BD1.dbo.Prices ([Article], [Price]) VALUES (@article, @price);
FETCH NEXT FROM @cursor
    INTO @article, @name, @qt, @price
END
CLOSE @cursor
DEALLOCATE @cursor
GO

--- 10 доп. позиций для первой базы
DECLARE @i int = 20, @article UNIQUEIDENTIFIER

WHILE @i < 30
BEGIN  
    SET @article = NEWID();

	INSERT INTO BD1.dbo.Names ([Article], [Name])
	VALUES (@article, ('Product ' + CONVERT(nvarchar(10), @i)));

	INSERT INTO BD1.dbo.Amounts([Article], [Quantity])
	VALUES (@article, @i * 10);

	INSERT INTO BD1.dbo.Prices([Article], [Price])
	VALUES (@article, @i * 100);

	SET @i = @i + 1
END

--- то, чего нет в БД1
SELECT [Article]
      ,[Name]
      ,[Quantity]
      ,[Price]
  INTO Tab2
  FROM [BD2].[dbo].[Products]
  EXCEPT
  SELECT N.[Article],
         N.[Name],
		 A.[Quantity],
		 P.[Price]
  FROM [BD1].[dbo].Names N
  LEFT JOIN [BD1].[dbo].Amounts A
  ON N.Article = A.Article
  LEFT JOIN [BD1].[dbo].Prices P
  ON N.Article = P.Article

-- то, что осталось
SELECT [Article]
      ,[Name]
      ,[Quantity]
      ,[Price]
  INTO Tab1
  FROM [BD2].[dbo].[Products]
  WHERE  [Article] NOT IN (SELECT Article FROM Tab2)
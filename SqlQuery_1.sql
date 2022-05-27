DROP PROCEDURE createProduct;
DROP PROCEDURE updateProduct;
DROP PROCEDURE deleteProduct;
DROP PROCEDURE createCategory;
DROP PROCEDURE updateCategory;
DROP PROCEDURE deleteCategory;

CREATE TABLE [dbo].[Category] (
    [Id]   INT IDENTITY(1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE PROCEDURE createProduct
	@name VARCHAR(50),
	@description TEXT,
	@imgPath TEXT,
	@price MONEY
AS
	INSERT INTO Product (Name, Description, ImgPath, Price) VALUES (@name, @description, @imgPath, @price);


GO
CREATE PROCEDURE updateProduct
	@name VARCHAR(50),
	@description TEXT,
	@imgPath TEXT,
	@price MONEY,
	@productId int
AS
	UPDATE Product SET Name=@name, Price=@price, Description=@description, ImgPath=@imgPath WHERE Id=@productId;


GO
CREATE PROCEDURE deleteProduct
	@productId int
AS
	DELETE FROM Product WHERE Id=@productId



	/* kategorie */
GO
CREATE PROCEDURE createCategory
	@name VARCHAR(50)
AS
	INSERT INTO Category (name) VALUES (@name);


GO
CREATE PROCEDURE updateCategory
	@name VARCHAR(50),
	@categoryId int
AS
	UPDATE Category SET name=@name WHERE Id=@categoryId;


GO
CREATE PROCEDURE deleteCategory
	@categoryId int
AS
	DELETE FROM Category WHERE Id=@categoryId
DROP PROCEDURE createProduct;
DROP PROCEDURE updateProduct;
DROP PROCEDURE deleteProduct;
DROP PROCEDURE createCategory;
DROP PROCEDURE updateCategory;
DROP PROCEDURE deleteCategory;
DROP PROCEDURE createUser;
DROP PROCEDURE updateUser;
DROP PROCEDURE deleteUser;

GO
CREATE PROCEDURE createProduct
	@name NVARCHAR(MAX),
	@description TEXT,
	@imgPath TEXT,
	@price MONEY
AS
	INSERT INTO Product (Name, Description, ImgPath, Price) VALUES (@name, @description, @imgPath, @price);


GO
CREATE PROCEDURE updateProduct
	@name NVARCHAR(MAX),
	@description NVARCHAR(MAX),
	@imgPath NVARCHAR(MAX),
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
	@name NVARCHAR(MAX)
AS
	INSERT INTO Category (Name) VALUES (@name);


GO
CREATE PROCEDURE updateCategory
	@name NVARCHAR(MAX),
	@categoryId int
AS
	UPDATE Category SET Name=@name WHERE Id=@categoryId;


GO
CREATE PROCEDURE deleteCategory
	@categoryId int
AS
	DELETE FROM Category WHERE Id=@categoryId

		/* uzytkownicy */
GO
CREATE PROCEDURE createUser
	@username NVARCHAR(MAX),
	@password NVARCHAR(MAX),
	@roleId int
AS
	INSERT INTO [User] (Username, Password, RoleId) VALUES (@username, @password, @roleId);


GO
CREATE PROCEDURE updateUser
	@userId int,
	@username NVARCHAR(MAX),
	@password NVARCHAR(MAX),
	@roleId int
AS
	UPDATE [User] SET Username=@username, Password=@password, RoleId=@roleId WHERE Id=@userId;


GO
CREATE PROCEDURE deleteUser
	@userId int
AS
	DELETE FROM [User] WHERE Id=@userId
--CREATE DATABASE IceCreamEShop;

USE IceCreamEShop;
GO

CREATE TABLE Product ( 
	ProductId INT PRIMARY KEY IDENTITY, 
	ProductName NVARCHAR(50) NOT NULL, 
	Category NVARCHAR(30) NOT NULL, 
	ProductDescription NVARCHAR(255) NOT NULL, 
	ProductPrice INT NOT NULL, 
	ProductPicUrl NVARCHAR(255) NOT NULL, 
	Quantity INT NOT NULL 
); 

CREATE TABLE UserAccount ( 
	UserAccountId INT PRIMARY KEY IDENTITY, 
	UserName NVARCHAR(20) NOT NULL, 
	EnPassword NVARCHAR(255) NOT NULL, 
	Email NVARCHAR(50) NOT NULL, 
	PhoneNumber NVARCHAR(20) NOT NULL, 
	ShoppingCartId INT NULL, 
	UNIQUE(UserName, Email)
); 

CREATE TABLE ShoppingCart (  
	ShoppingCartId INT PRIMARY KEY IDENTITY, 
	UserAccountId INT NOT NULL,
	CONSTRAINT SC_UserAccount FOREIGN KEY(UserAccountId) REFERENCES UserAccount(UserAccountId)  
);

CREATE TABLE CartItem ( 
	ShoppingCartId INT NOT NULL, 
	ProductId INT NOT NULL, 
	Qunatity INT NOT NULL, 
	CONSTRAINT CartItemId PRIMARY KEY(ShoppingCartId, ProductId), 
	CONSTRAINT CI_ShoppingCart FOREIGN KEY(ShoppingCartId) REFERENCES ShoppingCart(ShoppingCartId), 
	CONSTRAINT CI_Product FOREIGN KEY(ProductId) REFERENCES Product(ProductId)
); 

CREATE TABLE OrderInfo (
	OrderInfoId INT PRIMARY KEY IDENTITY, 
	UserAccountId INT NOT NULL,
	CONSTRAINT OI_UserAccount FOREIGN KEY(UserAccountId) REFERENCES UserAccount(UserAccountId)  
);

CREATE TABLE Payment (
	PaymentId INT PRIMARY KEY IDENTITY, 
	OrderInfoId INT NOT NULL, 
	PaymentAmount INT NOT NULL, 
	PaymentProvider NVARCHAR(20) NOT NULL, 
	PaymentStatus INT NOT NULL, 
	CONSTRAINT P_OrderInfo FOREIGN KEY(OrderInfoId) REFERENCES OrderInfo(OrderInfoId)
); 

CREATE TABLE WishList ( 
	ProductId INT NOT NULL, 
	UserAccountId INT NOT NULL, 
	CONSTRAINT WishListId PRIMARY KEY(UserAccountId, ProductId), 
	CONSTRAINT WL_UserAccount FOREIGN KEY(UserAccountId) REFERENCES UserAccount(UserAccountId), 
	CONSTRAINT WL_Product FOREIGN KEY(ProductId) REFERENCES Product(ProductId)
);  

CREATE TABLE OrderItem (
	OrderInfoId INT NOT NULL, 
	ProductId INT NOT NULL, 
	Qunatity INT NOT NULL, 
	CONSTRAINT OrderItemId PRIMARY KEY(OrderInfoId, ProductId), 
	CONSTRAINT OI_OrderInfo FOREIGN KEY(OrderInfoId) REFERENCES OrderInfo(OrderInfoId), 
	CONSTRAINT OI_Product FOREIGN KEY(ProductId) REFERENCES Product(ProductId)
);
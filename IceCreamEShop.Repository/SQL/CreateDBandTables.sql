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
    UserAccountId INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(256) NOT NULL,                        
    EnPassword NVARCHAR(255) NOT NULL,  
    Username NVARCHAR(50) NOT NULL,                      
    PhoneNumber NVARCHAR(20) NULL,    
    UserRole INT NOT NULL DEFAULT 1,   
    IsActive BIT NOT NULL DEFAULT 1,  
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(), 
    UpdatedAt DATETIME2 NULL,     
    CONSTRAINT UQ_UserAccount_Email UNIQUE(Email)
);


CREATE TABLE ShoppingCart (  
	ShoppingCartId INT PRIMARY KEY IDENTITY, 
	UserAccountId INT NOT NULL UNIQUE,
	CONSTRAINT SC_UserAccount FOREIGN KEY(UserAccountId) REFERENCES UserAccount(UserAccountId)  
);

CREATE TABLE CartItem ( 
	ShoppingCartId INT NOT NULL, 
	ProductId INT NOT NULL, 
	Quantity INT NOT NULL, 
	CONSTRAINT CartItemId PRIMARY KEY(ShoppingCartId, ProductId), 
	CONSTRAINT CI_ShoppingCart FOREIGN KEY(ShoppingCartId) REFERENCES ShoppingCart(ShoppingCartId), 
	CONSTRAINT CI_Product FOREIGN KEY(ProductId) REFERENCES Product(ProductId)
); 

CREATE TABLE OrderInfo (
	OrderInfoId INT PRIMARY KEY IDENTITY, 
	UserAccountId INT NOT NULL,
	PaymentAmount INT NOT NULL, 
	PaymentProvider NVARCHAR(20) NOT NULL, 
	PaymentStatus INT NOT NULL, 
	CONSTRAINT OI_UserAccount FOREIGN KEY(UserAccountId) REFERENCES UserAccount(UserAccountId)  
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
	Quantity INT NOT NULL, 
	CONSTRAINT OrderItemId PRIMARY KEY(OrderInfoId, ProductId), 
	CONSTRAINT OI_OrderInfo FOREIGN KEY(OrderInfoId) REFERENCES OrderInfo(OrderInfoId), 
	CONSTRAINT OI_Product FOREIGN KEY(ProductId) REFERENCES Product(ProductId)
);
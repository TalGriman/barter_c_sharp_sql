CREATE DATABASE barter
   ON 
     ( NAME = 'barter_Data', 
       FILENAME = 'D:\barter\barter.MDF', 
       SIZE = 10, 
       FILEGROWTH = 10% ) 
   LOG ON 
     ( NAME = 'barter_Log', 
       FILENAME = 'D:\barter\barter_log.LDF' 
	 )
 COLLATE Hebrew_CI_AS
Go

Use barter
GO

-- יצירת טבלאות
-- Drop Table users
CREATE TABLE users  
(
	 ID int identity not null,
	 Email nvarchar (150) unique not null,
	 FirstName nvarchar(150),
	 LastName nvarchar(150),
	 PhoneNumber nvarchar(150),
	 Password nvarchar(150),
	 Img nvarchar(max),
	 IsActive bit default 1,
	 IsFaceBook bit default 0
)
GO

-- Drop Table user_products
CREATE TABLE user_products  
(
	 ID int identity not null,
	 UserID int not null,
	 Title nvarchar(150) not null,
	 Description nvarchar(300),
	 Address nvarchar(150),
	 Category int,
	 Latitude float,
	 Longitude float,
	 Img nvarchar(150),
	 Date DateTime
)
GO

Alter TABLE user_products
Alter column Longitude float
go

-- Drop Table exchange
CREATE TABLE exchange 
(
	ProductID int not null,
	CategoryID int not null,
)
GO

-- Drop Table category
CREATE TABLE category 
(
	 ID int identity not null,
	 Description nvarchar(150),
	 Icon nvarchar(150)
)
GO

-- Drop Table Rating
Create Table Rating
(
	sellerID int not null,
	userID int not null,
	points int not null,
	Date DateTime not null default getdate(),
)
go

 -- Drop Table favorites
CREATE TABLE favorites
(
	 UserID int not null,
	 ProductID int not null,
)
GO

-- יצירת מפתחות ראשיים
ALTER TABLE users 
ADD
Constraint pk_users_id Primary key (ID) 
GO

ALTER TABLE user_products 
ADD
Constraint pk_user_products_id Primary key (ID) 
GO

ALTER TABLE exchange 
ADD
Constraint pk_exchange_productID_categoryID Primary key (ProductID,CategoryID) 
GO

ALTER TABLE category 
ADD
Constraint pk_category_id Primary key (ID) 
GO

ALTER TABLE favorites 
ADD
Constraint pk_favorites_userID_productID Primary key ( UserID , ProductID ) 
GO

ALTER TABLE Rating 
ADD
Constraint pk_rating_sellerID_userID Primary key ( sellerID , userID ) 
GO

-- הגדרת מפתחות זרים - קשרי גומלין לנירמול


ALTER TABLE user_products
ADD
CONSTRAINT [fk_user_products_userID] FOREIGN KEY 
	       (UserID) REFERENCES 
                users (ID),
CONSTRAINT [fk_user_products_category] FOREIGN KEY 
	       (Category) REFERENCES 
                category (ID)
go

ALTER TABLE exchange
ADD
CONSTRAINT [fk_exchange_productID] FOREIGN KEY 
	       (ProductID) REFERENCES 
                user_products (ID),
CONSTRAINT [fk_exchange_categoryID] FOREIGN KEY 
	       (CategoryID) REFERENCES 
                category (ID)
go


ALTER TABLE favorites
ADD
CONSTRAINT [fk_favorites_productID] FOREIGN KEY 
	       (ProductID) REFERENCES 
                user_products (ID),
CONSTRAINT [fk_favorites_userID] FOREIGN KEY 
	       (UserID) REFERENCES 
                users (ID)
go

ALTER TABLE Rating
ADD
CONSTRAINT [fk_Rating_sellerID] FOREIGN KEY 
	       (sellerID) REFERENCES 
                users (ID),
CONSTRAINT [fk_Rating_userID] FOREIGN KEY 
	       (UserID) REFERENCES 
                users (ID)
go



-- ***************** טבלאות עזר ********************
Create table TempProductWithFavorite
(
	UserID int,
	ProductID int,
	Product_Description nvarchar(max),
	Address nvarchar(max),
	Img nvarchar(max),
	Date dateTime,
	Latitude float,
	Longitude float,
	Category_Description nvarchar(max),
	Email nvarchar(max),
	PhoneNumber nvarchar(max),
	Title nvarchar(max),
	UserName nvarchar(max),
	CategoryID int,
	Rating int,
	Favorite bit,
	Exchanges nvarchar(max)
)
Go
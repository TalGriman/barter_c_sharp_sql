
--************************************ כל מה שקשור בפרטי מוצר ************************************--
/* Functions */

--Is Favorite
alter function IsFavorite(@id int, @productId int)
returns bit
as
	begin
		declare @res bit = 1
		if not exists(select * from [favorites] where [UserID]=@id and [ProductID]=@productId)
			set @res = 0
		return @res 
	end
go

--select dbo.IsFavorite(1,2)

--rating per user
alter function UserRating(@id int)
returns int
as
	begin --{
		return IsNull(Round((select AVG([points]) from [Rating] where [sellerID] = @id),0),-1)
	end --}
go

--select dbo.UserRating(1)

--Exchange
alter function CanExchange(@productId int)
returns nvarchar(max)
as
	begin
		declare @categories nvarchar(max) = ''
		if not exists(select * from [Product_Exchange] where [ProductID] = @productId)
			return ''
		set @categories = stuff((select ',' + [Description] from [Product_Exchange] where ProductID = @productId for xml path('')), 1,1,'')  
		return @categories
	end
go


--products details proc
create proc AllProductsByCategory
	@categoryID int,	
	@currentUserId int
as
	select UserID, ProductID, Title, UserName, CategoryID, 
		 [site11].UserRating(UserID) as Rating, 
		 [site11].IsFavorite(@currentUserId, ProductID) as Favorite,  
		 [site11].CanExchange(ProductID) as Exchanges
	from [Product_Details] where [CategoryID] = @categoryIDAdd
go
	
exec AllProductsByCategory 2, 100


--**************************************************************
Alter proc AllProductsByCategory
	@categoryID int,	
	@currentUserId int
as
	select UserID, ProductID,Product_Description,Address,Img,Date,Latitude,Longitude,Category_Description,Email,PhoneNumber, Title, UserName, CategoryID, 
		 [site11].UserRating(UserID) as Rating, 
		 [site11].IsFavorite(@currentUserId, ProductID) as Favorite,  
		 [site11].CanExchange(ProductID) as Exchanges
	from [Product_Details] where [CategoryID] = @categoryID
	ORDER BY Date DESC
go
	
exec AllProductsByCategory 4, 8



Create proc AllProductsByUser	
	@currentUserId int
as
	select UserID, ProductID,Product_Description,Address,Img,Date,Latitude,Longitude,Category_Description,Email,PhoneNumber, Title, UserName, CategoryID, 
		 [site11].UserRating(UserID) as Rating, 
		 [site11].IsFavorite(@currentUserId, ProductID) as Favorite,  
		 [site11].CanExchange(ProductID) as Exchanges
	from [Product_Details] where [UserID] = @currentUserId
	ORDER BY Date DESC
go

exec AllProductsByUser 7
go






--************** פייבוריט של משתמש **************

Create proc AllProductsWithFavoritesColumn
	@currentUserId int
as
	select UserID, ProductID,Product_Description,Address,Img,Date,Latitude,Longitude,Category_Description,Email,PhoneNumber, Title, UserName, CategoryID, 
		 [site11].UserRating(UserID) as Rating, 
		 [site11].IsFavorite(@currentUserId, ProductID) as Favorite,  
		 [site11].CanExchange(ProductID) as Exchanges
	from [Product_Details]
	ORDER BY Date DESC
go

exec AllProductsWithFavoritesColumn 8
go

Create proc Proc_Del_Tbl_TempProductWithFavorite
As
Delete From TempProductWithFavorite
Go

-- Drop Proc UserFavoriteProducts
Create Proc UserFavoriteProducts
@currentUserId int
As
Exec Proc_Del_Tbl_TempProductWithFavorite
Insert TempProductWithFavorite (UserID, ProductID,Product_Description,Address,Img,Date,Latitude,Longitude,Category_Description,Email,PhoneNumber, Title, UserName, CategoryID,Rating,Favorite,Exchanges)
Exec AllProductsWithFavoritesColumn @currentUserId

Select * From TempProductWithFavorite Where Favorite = 1
Go

Exec UserFavoriteProducts 8
Go

--**************************************************************************

--************************************ כל מה שקשור בפרטי פרופיל ************************************--

--Drop Proc AddUser
Create Proc AddUser 
@Email nvarchar(150),
@Password nvarchar(150),
@FirstName nvarchar(150),
@LastName nvarchar(150),
@PhoneNumber nvarchar(150),
@Img nvarchar(150),
@ID int OUTPUT
As
	if exists(Select * from users where Upper(Email) = Upper(@Email) and IsActive = 1)
	begin
		Select @ID =  -1
		return
	end
	else if exists(Select * from users where Upper(Email) = Upper(@Email) and IsActive = 0)
		begin
		Select @ID =  -2
		return
	end
	Insert [users] (Email,Password,FirstName,LastName,PhoneNumber,Img)
	Values (@Email,@Password,@FirstName,@LastName,@PhoneNumber,@Img)
	Select @ID =  ID from Users Where Email = @Email
Go


--Drop Proc SelectUser
Alter Proc SelectUser
@Email nvarchar(150),
@Password nvarchar(150)
As
Select [ID],[Email],[FirstName],[LastName],[PhoneNumber],[Img],[IsActive],[IsFaceBook] from [users] Where Upper([Email]) = Upper(@Email) And [Password] = @Password
GO


--Drop Proc UpdateUserDetails
Alter Proc UpdateUserDetails
@ID int,
@Password nvarchar(150),
@FirstName nvarchar(150),
@LastName nvarchar(150),
@PhoneNumber nvarchar(150)
As
	Update [users]
	Set FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber
	Where ID = @ID
	if not(@Password = '')
		begin
			Update [users]
			Set [Password] = @Password
			Where ID = @ID
		end

	Select [ID],[Email],[FirstName],[LastName],[PhoneNumber],[Img],[IsActive],[IsFaceBook]
	from [users]
	Where ID = @ID
Go

--Drop Proc UpdateUserImage
Alter Proc UpdateUserImage
@ID int,
@Img nvarchar(150)
As
Update [users]
Set Img = @Img
Where ID = @ID
GO

-- user rating proc
--Drop Proc UserAvgRating
Alter proc UserAvgRating
	@ID int,
	@Result int out
as
	set @Result = [site11].UserRating(@ID)
Go

-- Drop Proc ActivateRegularAccount
Alter proc ActivateRegularAccount
@Email nvarchar(150),
@Password nvarchar(150)
As
Update [users]
Set IsActive = 1
Where Upper(Email) = Upper(@Email) and [Password] = @Password
GO

-- Drop Proc CheckIfFacebookUserIsExist
Alter proc CheckIfFacebookUserIsExist
	@Email nvarchar(150),
	@FirstName nvarchar(150),
	@LastName nvarchar(150),
	@Img nvarchar(max),
	@Result int out
As
	set @Result = 0
	if exists(select * from users Where Upper([Email]) = Upper(@Email) and [IsFaceBook] = 0)
	begin
		set @Result = -1
		return
	end
	Update [users]
	Set [FirstName] = @FirstName, [LastName] = @LastName, [Img] = @Img, [IsActive] = 1, [IsFaceBook] = 1
	Where Upper(Email) = Upper(@Email)
	Select [ID],[Email],[FirstName],[LastName],[PhoneNumber],[Img],[IsActive],[IsFaceBook] from [users] Where Upper([Email]) = Upper(@Email)
Go

-- Drop Proc AddFacebookUser
Alter proc AddFacebookUser
	@Email nvarchar(150),
	@FirstName nvarchar(150),
	@LastName nvarchar(150),
	@PhoneNumber nvarchar(150),
	@Img nvarchar(max)
As
	Insert [users] (Email,FirstName,LastName,PhoneNumber,Img,IsFaceBook)
	Values (@Email,@FirstName,@LastName,@PhoneNumber,@Img,1)
	Select [ID],[Email],[FirstName],[LastName],[PhoneNumber],[Img],[IsActive],[IsFaceBook] from [users] Where Upper([Email]) = Upper(@Email)
Go

---- Drop Proc DeleteUser
Create proc DeleteUser
@ID int
As
Update [users]
Set IsActive = 0
Where [ID] = @ID
GO

--************************************ כל מה שקשור בפרטי פרופיל ************************************--

--************************************ כל מה שקשור בדף הוספת מוצר ************************************--
-- Drop Proc SelectAllCategories
Create proc SelectAllCategories
As
Select * from [category]
Go

-- Drop Proc AddUserProduct
Alter proc AddUserProduct
	 @UserID int,
	 @Title nvarchar(150),
	 @Description nvarchar(300),
	 @Address nvarchar(150),
	 @Category int,
	 @Latitude Decimal(8,6),
	 @Longitude Decimal(9,6),
	 @Img nvarchar(150),
	 @ID int out
As
	Insert [user_products] (UserID,Title,Description,Address,Category,Latitude,Longitude,Img,Date)
	Values (@UserID,@Title,@Description,@Address,@Category,@Latitude,@Longitude,@Img,GETDATE())
	set @ID = @@IDENTITY
GO

--Drop Proc UpdateProductImage
Create Proc UpdateProductImage
@ID int,
@Img nvarchar(150)
As
Update [user_products]
Set Img = @Img
Where ID = @ID
GO

--Drop Proc AddExchanges
Create Proc AddExchanges
@ProductID int,
@CategoryID int
As
	Insert [exchange] ([ProductID],[CategoryID])
	Values (@ProductID,@CategoryID)
GO

--Drop Proc HandleFavorite
Create proc HandleFavorite
@UserID int,
@ProductID int
As
	if not exists(Select * From [favorites] Where [UserID]=@UserID and [ProductID]=@ProductID)
		begin
			Insert [favorites] ([UserID],[ProductID])
			Values (@UserID,@ProductID)
		end
	else
		begin
			Delete From [favorites] Where [UserID]=@UserID and [ProductID]=@ProductID
		end
Go


--************************************************************************************************--

--******************* otherUserDetails ********************

-- drop proc otherProfileStartDetails
Create proc otherProfileStartDetails
@currentUserID int,
@otherUserID int	
as
	select [Email],[FirstName],[LastName],[PhoneNumber],[Img],[IsFaceBook],
	[site11].UserRating(@otherUserID) as Rating,
	[site11].OtherProfilePoints(@currentUserID,@otherUserID) as CurrentUserPoints
	From [users] Where [ID] = @otherUserID
go

Exec otherProfileStartDetails 9,8
Go

-- drop proc handleRating
Create Proc handleRating
@otherUserID int,
@currentUserID int,
@points int,
@newAvg int out
As
	if exists(Select * from [Rating] Where sellerID = @otherUserID and userID = @currentUserID)
		Begin
			Update [Rating]
			Set points = @points
			Where sellerID = @otherUserID and userID = @currentUserID
		End
	else
		Begin
			Insert [Rating] ([sellerID],[userID],[points])
			Values (@otherUserID,@currentUserID,@points)
		End
	Set @newAvg = [site11].UserRating(@otherUserID)
GO


Create function OtherProfilePoints(@currentUserID int,@otherUserID int)
returns int
as
	begin --{
		if exists(Select * from [site11].[Rating] where [sellerID] = @otherUserID and [userID] = @currentUserID)
			begin
				return (Select [points] from [site11].[Rating] where [sellerID] = @otherUserID and [userID] = @currentUserID)
			end
		return -1
	end --}
go

--*****************************************************************************************************--

--************************************ כל מה שקשור בדף עריכת מוצר ************************************--
-- drop proc EditProduct
Create proc EditProduct
	@ID int,
	@Title nvarchar(150),
	@Description nvarchar(300),
	@Address nvarchar(150),
	@Category int,
	@Latitude Decimal(8,6),
	@Longitude Decimal(9,6),
	@Img nvarchar(150)
As
	Update [user_products]
	Set Title = @Title, Description = @Description, Address = @Address, Category = @Category, Latitude = @Latitude, Longitude = @Longitude, Img = @Img
	Where [ID] = @ID
Go

Create Proc ClearExchanges
	@ProductID int
As
	Delete From [exchange]
	Where [ProductID] = @ProductID
Go

Create Proc ClearFavorites
	@ProductID int
As
	Delete From [favorites]
	Where [ProductID] = @ProductID
Go

Create Proc DeleteProduct
	@ProductID int
As
	Delete From [site11].[user_products]
	Where [ID] = @ProductID
Go

Create Proc DeleteProductFull
	@ProductID int
As
	Exec ClearFavorites @ProductID
	Exec ClearExchanges @ProductID
	Exec DeleteProduct @ProductID
Go

--******************************************************************************************************--
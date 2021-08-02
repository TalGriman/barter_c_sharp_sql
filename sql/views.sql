
alter view Product_Exchange
as
	SELECT exchange.ProductID, exchange.CategoryID, category.Description
	FROM     category INNER JOIN
					  exchange ON category.ID = exchange.CategoryID INNER JOIN
					  user_products ON exchange.ProductID = user_products.ID
go

--select * from Product_Exchange

alter view Product_Details
as
SELECT        site11.users.ID AS UserID, site11.user_products.ID AS ProductID, site11.user_products.Title, site11.users.FirstName + N' ' + site11.users.LastName AS UserName, site11.category.ID AS CategoryID, site11.users.IsActive
FROM            site11.category INNER JOIN
                         site11.user_products ON site11.category.ID = site11.user_products.Category INNER JOIN
                         site11.users ON site11.user_products.UserID = site11.users.ID
WHERE        (site11.users.IsActive = 1)
go

--****************************************************************
alter view Product_Details
as
SELECT        site11.users.ID AS UserID, site11.user_products.ID AS ProductID, site11.user_products.Title, site11.users.FirstName + N' ' + site11.users.LastName AS UserName, site11.category.ID AS CategoryID, site11.users.IsActive, 
                         site11.user_products.Description AS Product_Description, site11.user_products.Address, site11.user_products.Img, site11.user_products.Date, site11.user_products.Latitude, site11.user_products.Longitude, 
                         site11.category.Description AS Category_Description, site11.users.Email, site11.users.PhoneNumber
FROM            site11.category INNER JOIN
                         site11.user_products ON site11.category.ID = site11.user_products.Category INNER JOIN
                         site11.users ON site11.user_products.UserID = site11.users.ID
WHERE        (site11.users.IsActive = 1)
go

--***************************************************************


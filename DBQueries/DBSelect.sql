
--select * from furnituretypes;
--select * from manufacturers;
--select furnitureid,furniturename,furnitureprice,furniturequantity,furnituretypename,manufacturername  from furnitures join furnituretypes on furnituretypes.furnituretypeid = furnitures.furnituretypeid join manufacturers on manufacturers.manufacturerid = furnitures.manufacturerid ;

select * from furnituresets;
/*
select furnituresetitemid,furnituresetname,furniturename,(furniturePrice + furniturePrice * manufacturermarkup + furniturePrice * furnituretypemarkup) as retailprice
from furnituresetitems join FurnitureSets on FurnitureSets.FurnitureSetId = FurnitureSetItems.FurnitureSetId 
join Furnitures on Furnitures.FurnitureId = FurnitureSetItems.FurnitureId 
join Manufacturers on Manufacturers.ManufacturerId = Furnitures.ManufacturerId
join furnituretypes on furnituretypes.furnituretypeId = Furnitures.furnituretypeId;
*/

/*
select furniturename,furnituresaledquantity, sum(furnitureprice * furnituresaledquantity) as TotalCost 
from sales 
join Furnitures on Furnitures.FurnitureId = Sales.FurnitureId 
group by furniturename,furnituresaledquantity ;
*/

--select roleid,rolename from roles;
--select userid,rolename,username,password from users join Roles on Roles.RoleId = Users.RoleId;
--select UserName,RoleName,Password from UserView;



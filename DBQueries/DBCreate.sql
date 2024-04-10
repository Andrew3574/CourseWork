create table Roles
(
	RoleId smallserial primary key,
	RoleName varchar(20) not null
);

create table Users
(
	UserId serial primary key,
	RoleId smallint references Roles(RoleId),
	UserName varchar(30) unique not null,
	Password varchar(10) not null
);

create table FurnitureTypes
(
	FurnitureTypeId smallserial primary key,
	FurnitureTypeName varchar(30) not null,
	FurnitureTypeMarkup decimal not null
);

create table Manufacturers
(
	ManufacturerId smallserial primary key,
	ManufacturerName varchar(30) not null,
	ManufacturerMarkup decimal not null
);

create table Furnitures
(
	FurnitureId serial primary key,
	FurnitureName varchar(30) not null,
	FurniturePrice numeric(7,2) not null,
	FurnitureQuantity int not null,
	FurnitureTypeId smallint references FurnitureTypes(FurnitureTypeId) on delete cascade,
	ManufacturerId smallint references Manufacturers(ManufacturerId) on delete cascade
);
create table FurnitureSets
(
	FurnitureSetId serial primary key,
	FurnitureSetName varchar(30) not null
);

create table FurnitureSetItems
(
	FurnitureSetItemId serial primary key,
	FurnitureSetId int references FurnitureSets(FurnitureSetId) on delete cascade ,
	FurnitureId int references Furnitures(FurnitureId) on delete cascade
	
);

create table Sales 
(
	SaleId serial primary key,
	FurnitureId int references Furnitures(FurnitureId),
	FurnitureQuantity int not null check
		(FurnitureQuantity > 0)
);

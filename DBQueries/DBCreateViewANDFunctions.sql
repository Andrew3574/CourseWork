create view furnitureView as
	select furnituretypeid,furnituretypename,manufacturerid,manufacturername 
	from furnituretypes,manufacturers 
	
create function GetTypeId(param varchar)
returns smallint as '
select furnituretypeid from furnituretypes where param = furnituretypename
' language sql

create function GetManufacturerId(param varchar)
returns smallint as '
select manufacturerid from manufacturers where param = manufacturername
' language sql

select GetManufacturerId('ООО «ДЕЛКОМ40»')
select GetTypeId('Офисная')
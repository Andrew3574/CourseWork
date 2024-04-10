insert into Roles values
	(1,'Admin'),
	(2,'Salesman'),
	(3,'Manager');
	
insert into Users values
	(1,1,'Admin','1234'),
	(2,2,'Salesman','2233'),
	(3,3,'Manager','1111');
	
insert into FurnitureTypes values
	(1,'Офисная',0.10),
	(2,'Кухонная',0.08),
	(3,'Спальная',0.15),
	(4,'Гостинная',0.10);
	
insert into Manufacturers values
	(1,'ООО «ДЕЛКОМ40»',0.05),
	(2,'ОАО «Речицадрев»',0.06),
	(3,'ЗАО «Мозырьлес»',0.08);
	
insert into Furnitures values
	(1,'Обеденный стол',2100.00,14,2,1),
	(2,'Кухонный уголок',550.00,20,2,1),
	(3,'Комод',1000.00,15,4,2),
	(4,'Сервировочный стол', 2500.00,9,4,2),
	(5,'Офисный диван',3700.00,17,1,3),
	(6,'Офисный шкаф',4000.60,10,1,3);
	
insert into FurnitureSets values
	(1,'Кухонный гарнитур'),
	(2,'Офисный гарнитур');
	
insert into FurnitureSetItems values
	(1,1,1),
	(2,1,2),
	(3,2,5),
	(4,2,6);
	
insert into Sales values
	(1,3,7),
	(2,6,11);
	
	
	
	
	
	
	
	
	
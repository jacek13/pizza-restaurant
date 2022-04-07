--Create new admin
INSERT INTO [pizza_restaurant_ver_6].[dbo].[account] (e_mail, login, password, name, surname, account_creation_date, phone_number, role)
VALUES ('root@root.pl','root','1234','Admin','szef', GETDATE(), '123654789', 'Admin');

INSERT INTO pizza_restaurant_ver_6.dbo.client (points,address,account_id_account)
VALUES (0,'Fryderyka Chopina 9', (SELECT COUNT(*) FROM pizza_restaurant_ver_6.dbo.account));

--Create new Admin
INSERT INTO [pizza_restaurant_ver_6].[dbo].[account] (e_mail, login, password, name, surname, account_creation_date, phone_number, role)
VALUES ('random@o2.pl','borubar4321','1234','Fidol','Pêpenek', GETDATE(), '123654789', 'Client');

INSERT INTO pizza_restaurant_ver_6.dbo.client (points,address,account_id_account)
VALUES (0,'Zwyciêstwa 2', (SELECT COUNT(*) FROM pizza_restaurant_ver_6.dbo.account));

--Create new Manager
INSERT INTO [pizza_restaurant_ver_6].[dbo].[account] (e_mail, login, password, name, surname, account_creation_date, phone_number, role)
VALUES ('kowalski@o2.pl','Kowal','1234','Janek','Kowalski', GETDATE(), '123654789', 'Manager');

INSERT INTO pizza_restaurant_ver_6.dbo.client (points,address,account_id_account)
VALUES (0,'Amarantowa 1', (SELECT COUNT(*) FROM pizza_restaurant_ver_6.dbo.account));

--Create new pizza type
INSERT INTO pizza_restaurant_ver_6.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (24, 14, 1, 'capriciosa', 32, 2);
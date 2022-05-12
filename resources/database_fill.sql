-- Create new admin
INSERT INTO [pizza_restaurant_ver_9].[dbo].[account] (e_mail, login, password, name, surname, account_creation_date, phone_number, role)
VALUES ('root@root.pl','root','?E?r?O-?e??K?x?)','Admin','szef', GETDATE(), '123654789', 'Admin');

INSERT INTO pizza_restaurant_ver_9.dbo.client (points, address, account_id_account)
VALUES (0,'Fryderyka Chopina 9', (	SELECT [pizza_restaurant_ver_9].[dbo].[account].[id_account] 
					FROM pizza_restaurant_ver_9.dbo.account 
					WHERE pizza_restaurant_ver_9.dbo.account.name = 'Admin'));

-- Create new Client
INSERT INTO [pizza_restaurant_ver_9].[dbo].[account] (e_mail, login, password, name, surname, account_creation_date, phone_number, role)
VALUES ('random@o2.pl','borubar4321','?E?r?O-?e??K?x?)','Fidol','Pêpenek', GETDATE(), '123654789', 'Client');

INSERT INTO pizza_restaurant_ver_9.dbo.client (points,address,account_id_account)
VALUES (0,'Zwyciêstwa 2', (	SELECT [pizza_restaurant_ver_9].[dbo].[account].[id_account] 
				FROM pizza_restaurant_ver_9.dbo.account 
				WHERE pizza_restaurant_ver_9.dbo.account.name = 'Fidol'));

-- Create new Manager
INSERT INTO [pizza_restaurant_ver_9].[dbo].[account] (e_mail, login, password, name, surname, account_creation_date, phone_number, role)
VALUES ('kowalski@o2.pl','Kowal','?E?r?O-?e??K?x?)','Janek','Kowalski', GETDATE(), '123654789', 'Manager');

INSERT INTO pizza_restaurant_ver_9.dbo.client (points, address, account_id_account)
VALUES (0,'Amarantowa 1', (	SELECT [pizza_restaurant_ver_9].[dbo].[account].[id_account] 
				FROM pizza_restaurant_ver_9.dbo.account 
				WHERE pizza_restaurant_ver_9.dbo.account.name = 'Janek'));

INSERT INTO pizza_restaurant_ver_9.dbo.manager (salary_netto,salary_brutto,account_id_account)
VALUES (4106.86, 5700, (SELECT [pizza_restaurant_ver_9].[dbo].[account].[id_account] 
			FROM pizza_restaurant_ver_9.dbo.account 
			WHERE pizza_restaurant_ver_9.dbo.account.name = 'Janek'));

-- Create new pizza type
INSERT INTO pizza_restaurant_ver_9.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (16, 5, 1, 'Margherita', 32, 0);

INSERT INTO pizza_restaurant_ver_9.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (18, 6, 1, 'Funghi', 32, 0);

INSERT INTO pizza_restaurant_ver_9.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (21, 7, 1, 'Capriciosa', 32, 1);

INSERT INTO pizza_restaurant_ver_9.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (21, 8, 1, 'Pepperoni', 32, 2);

INSERT INTO pizza_restaurant_ver_9.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (22, 8, 1, 'Roma', 32, 2);

INSERT INTO pizza_restaurant_ver_9.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (22, 9, 1, 'Capo', 32, 2);

INSERT INTO pizza_restaurant_ver_9.dbo.pizza (price, cost, isAvailable, type, size, points)
VALUES (28, 14, 1, 'Quattro formaggi', 32, 5);

-- Create new restaurats
INSERT INTO pizza_restaurant_ver_9.dbo.restaurant (address, phone_number, e_mail)
VALUES ('Konarskiego 13', '1293848576', 'galakPizzaK@gmail.com');

INSERT INTO pizza_restaurant_ver_9.dbo.restaurant (address, phone_number, e_mail)
VALUES ('Akademicka 16', '322371310', 'galakPizzaA@gmail.com');

INSERT INTO pizza_restaurant_ver_9.dbo.restaurant (address, phone_number, e_mail)
VALUES ('Chorzowska 23', '123654798', 'galakPizzaC@gmail.com');

-- Assign a manager to two restaurants
INSERT INTO pizza_restaurant_ver_9.dbo.manager_assignment (assignment_role, manager_id_manager, restaurant_id_restaurant)
VALUES	( 'Manager', 
		(	SELECT [pizza_restaurant_ver_9].[dbo].[manager].[id_manager] 
			FROM pizza_restaurant_ver_9.dbo.manager JOIN pizza_restaurant_ver_9.dbo.account 
			ON pizza_restaurant_ver_9.dbo.manager.account_id_account = pizza_restaurant_ver_9.dbo.account.id_account
			WHERE pizza_restaurant_ver_9.dbo.account.name = 'Janek'),
		(	SELECT [pizza_restaurant_ver_9].[dbo].[restaurant].[id_restaurant] 
			FROM pizza_restaurant_ver_9.dbo.restaurant 
			WHERE pizza_restaurant_ver_9.dbo.restaurant.address = 'Konarskiego 13'));

INSERT INTO pizza_restaurant_ver_9.dbo.manager_assignment (assignment_role, manager_id_manager, restaurant_id_restaurant)
VALUES	( 'Manager', 
		(	SELECT [pizza_restaurant_ver_9].[dbo].[manager].[id_manager] 
			FROM pizza_restaurant_ver_9.dbo.manager JOIN pizza_restaurant_ver_9.dbo.account 
			ON pizza_restaurant_ver_9.dbo.manager.account_id_account = pizza_restaurant_ver_9.dbo.account.id_account
			WHERE pizza_restaurant_ver_9.dbo.account.name = 'Janek'),
		(	SELECT [pizza_restaurant_ver_9].[dbo].[restaurant].[id_restaurant] 
			FROM pizza_restaurant_ver_9.dbo.restaurant 
			WHERE pizza_restaurant_ver_9.dbo.restaurant.address = 'Akademicka 16'));

-- Create tables in restaurants
-- Insert into Konarskiego 13
INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (2, 1);

INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (4, 1);

INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (6, 1);

-- Insert into Akademicka 16
INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (4, 2);

INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (5, 2);

INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (7, 2);

-- Insert into Chorzowska 23
INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (4, 3);

INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (6, 3);

INSERT INTO [pizza_restaurant_ver_9].[dbo].[table] (capacity, restaurant_id_restaurant)
VALUES (8, 3);
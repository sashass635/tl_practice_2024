IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Rooms')
 CREATE TABLE dbo.Rooms (
 	room_id INT IDENTITY(1, 1) NOT NULL,
	room_number INT NOT NULL, 
	room_type NVARCHAR(50) NOT NULL, 
	price_per_night DECIMAL(20,2) NOT NULL, 
	availability BIT NOT NULL, 
	
	CONSTRAINT PK_rooms_room_id PRIMARY KEY (room_id)
 )

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Customers')
 CREATE TABLE dbo.Customers (
 	customer_id INT IDENTITY(1, 1) NOT NULL,
	first_name NVARCHAR(50) NOT NULL, 
	last_name NVARCHAR(50) NOT NULL, 
	email NVARCHAR(100) NOT NULL, 
	phone_number NVARCHAR(20) NOT NULL, 
	
	CONSTRAINT PK_customers_customer_id PRIMARY KEY (customer_id)
 );

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Bookings')
 CREATE TABLE dbo.Bookings (
 	booking_id INT IDENTITY(1, 1) NOT NULL,
	customer_id INT NOT NULL, 
	room_id INT NOT NULL, 
	check_in_date DATE NOT NULL, 
	check_out_date DATE NOT NULL, 
	CONSTRAINT PK_bookings_booking_id PRIMARY KEY (booking_id),
	
	CONSTRAINT FK_bookings_customer_id 
		FOREIGN KEY (customer_id) REFERENCES dbo.Customers (customer_id),
	CONSTRAINT FK_bookings_room_id
		FOREIGN KEY (room_id) REFERENCES dbo.Rooms (room_id)
 );

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Facilities')
 CREATE TABLE dbo.Facilities (
 	facility_id INT IDENTITY(1, 1) NOT NULL,
	facility_name NVARCHAR(100) NOT NULL, 
		
	CONSTRAINT PK_facilities_facility_id PRIMARY KEY (facility_id)
 );

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'RoomsToFacilities')
 CREATE TABLE dbo.RoomsToFacilities (
 	room_id INT NOT NULL,
	facility_id INT NOT NULL, 
	CONSTRAINT PK_roomsToFacilities_room_id PRIMARY KEY (room_id),

	CONSTRAINT FK_roomsToFacilities_room_id
		FOREIGN KEY (room_id) REFERENCES dbo.Rooms (room_id),
	CONSTRAINT FK_roomsToFacilities_facility_id
		FOREIGN KEY (facility_id) REFERENCES dbo.Facilities (facility_id)
 );

INSERT INTO dbo.Rooms (room_number, room_type, price_per_night, availability)
VALUES	
		('100', 'Standard', 75.00, 1),
		('101', 'Superior', 105.50, 1),
		('102', 'Deluxe', 300.75, 0),
		('103', 'Suite', 120.20, 1);

INSERT INTO dbo.Customers (first_name, last_name, email, phone_number) 
VALUES	
		('Peter', 'Smith', 'p.smith@mail.com', '+72234567842'),
		('Julia', 'Jonson', 'jonson@gmail.com', '+447871234567'),
		('Alice', 'Mercury', 'mercury@list.com', '+79852569154'),
		('Tom', 'Smith', 'tom.smith@gmail.com', '+78524567890');

INSERT INTO dbo.Bookings (customer_id, room_id, check_in_date, check_out_date) 
VALUES	
		(1, 2, '2024-07-23', '2024-08-01'),
		(2, 3, '2025-08-18', '2025-09-12'),
		(3, 4, '2024-12-03', '2024-12-12');

INSERT INTO dbo.Facilities (facility_name) 
VALUES	
		('Wi-Fi'),
		('Air Conditioning'),
		('Mini Bar'),
		('TV'), 
		('Fridge');

INSERT INTO dbo.RoomsToFacilities (room_id, facility_id) 
VALUES 
		(1, 1),
		(2, 3),
		(3, 2),
		(4, 4);

--Найдите все доступные номера для бронирования сегодня.
SELECT * FROM dbo.Rooms
WHERE availability <> 0 AND room_id NOT IN(
 SELECT room_id FROM dbo.Bookings 
 WHERE GETDATE() BETWEEN check_in_date AND check_out_date
 );

 --Найдите всех клиентов, чьи фамилии начинаются с буквы "S".
SELECT * FROM dbo.Customers WHERE last_name like 'S%';

--Найдите все бронирования для определенного клиента (по имени или электронному адресу).
SELECT * FROM dbo.Bookings JOIN dbo.Customers 
	ON dbo.Bookings.customer_id = dbo.Customers.customer_id 
	WHERE dbo.Customers.first_name = 'Alice';

--Найдите все бронирования для определенного номера.
SELECT * FROM dbo.Bookings JOIN dbo.Rooms
	ON dbo.Bookings.room_id = dbo.Rooms.room_id
	WHERE dbo.Rooms.room_number = 103;

--Найдите все номера, которые не забронированы на определенную дату.
SELECT * FROM dbo.Rooms
WHERE  availability <> 0 AND room_id NOT IN (
 SELECT room_id FROM dbo.Bookings 
 WHERE '2024-12-10' BETWEEN check_in_date AND check_out_date
 );
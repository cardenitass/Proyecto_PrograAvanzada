CREATE DATABASE ProyectoFinal_KN_BD
DROP DATABASE ProyectoFinal_KN_BD
GO

USE ProyectoFinal_KN_BD

--TABLES--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Store (
    id_store int IDENTITY (1,1) NOT NULL CONSTRAINT pk_store PRIMARY KEY ,
	address varchar(500) NOT NULL,
	name varchar(250) NOT NULL,
);


CREATE TABLE Product(
	id_product int IDENTITY (1,1) NOT NULL CONSTRAINT pk_product PRIMARY KEY,
	name varchar(255) NOT NULL,
	stock int NOT NULL,
	price decimal NOT NULL,
	picture_url varchar(500) NOT NULL,
	id_store int NOT NULL,
);

CREATE TABLE User_Profile (
	id_userProfile int IDENTITY (1,1) CONSTRAINT pk_User_Profile PRIMARY KEY,
	user_type varchar (255) NOT NULL,
	description varchar (300) NOT NULL,
);


CREATE TABLE User_tb(
	id_user int IDENTITY (1,1) NOT NULL CONSTRAINT pk_User_tb PRIMARY KEY,
	first_name varchar(255) NOT NULL,
	last_name varchar(255) NOT NULL,
	email varchar(255) NOT NULL UNIQUE,
	password varchar(500) NOT NULL,
	active BINARY NOT NULL,
	id_userProfile int NOT NULL,
);


CREATE TABLE Invoice(
	id_invoice int IDENTITY (1,1) NOT NULL CONSTRAINT pk_invoice PRIMARY KEY,
	total decimal NOT NULL,
	date DATETIME NOT NULL,
	id_user int NOT NULL,
);


CREATE TABLE  Invoice_details (
	id_invoice_detail int IDENTITY (1,1) NOT NULL CONSTRAINT pk_invoice_details PRIMARY KEY,
	id_product int NOT NULL,
	cantidad decimal NOT NULL,
	precio decimal NOT NULL,
	id_invoice int NOT NULL,
);

CREATE TABLE  Binnacle (
	id_error int  NOT NULL CONSTRAINT pk_binnacle PRIMARY KEY,
	description varchar(500) NOT NULL,
	id_user int NOT NULL,
);


ALTER TABLE product ADD CONSTRAINT fk_store FOREIGN KEY (id_store)
REFERENCES store (id_store);

ALTER TABLE Invoice  ADD CONSTRAINT fk_user FOREIGN KEY (id_user) 
REFERENCES User_tb(id_user);

ALTER TABLE User_tb ADD CONSTRAINT fk_user_profile FOREIGN KEY (id_userProfile) 
REFERENCES User_Profile(id_userProfile);

ALTER TABLE Invoice_details  ADD CONSTRAINT fk_product FOREIGN KEY (id_product) 
REFERENCES Product(id_product);

ALTER TABLE Invoice_details  ADD CONSTRAINT fk_invoice FOREIGN KEY (id_invoice)
REFERENCES Invoice(id_invoice);

ALTER TABLE Binnacle  ADD CONSTRAINT fk_user2 FOREIGN KEY (id_user)
REFERENCES User_tb(id_user);

--INSERTS--------------------------------------------------------------------------------------------------------------------------------


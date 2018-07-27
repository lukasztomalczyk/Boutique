CREATE DATABASE IF NOT EXISTS `shop` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `shop`;

CREATE TABLE IF NOT EXISTS Users
(
  Id        varchar(50) not null
    constraint Users_Id_pk
    primary key,
  Login     varchar(25),
  Password  varchar(84),
  FirstName varchar(25),
  LastName  varchar(25),
  Role      varchar(25)
)
go

CREATE TABLE IF NOT EXISTS Products
(
  Id    varchar(50)  not null
    constraint PK_Product
    primary key,
  Name  varchar(500) not null,
  Color varchar(50),
  Price money
)
go

insert into dbo.Users (Id, Login, Password, FirstName, LastName, Role) values ('778bc60c-7284-42cc-a84c-17721ea7b7fc', 'Garib', 'mhGC3Dwn3VJxtovaiAXLWw==', 'Garib', 'Kumpel', 'User');
insert into dbo.Users (Id, Login, Password, FirstName, LastName, Role) values ('9ae049ad-16de-493b-ac45-4a98e539ef34', 'TestUser', 'X03MO1qnZdYdgyfeuILPmQ==', 'User Name', 'Last Name', 'Admin');
insert into dbo.Users (Id, Login, Password, FirstName, LastName, Role) values ('fbfbb542-1c20-4336-9385-2b3f6a25498c', 'lukasz', 'mhGC3Dwn3VJxtovaiAXLWw==', 'Lukasz', 'Tomalczyk', 'Admin');

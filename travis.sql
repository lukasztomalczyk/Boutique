CREATE DATABASE IF NOT EXISTS `shop` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `sklep`;

CREATE TABLE IF NOT EXISTS `Users`
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

CREATE TABLE IF NOT EXISTS `Products`
(
  Id    varchar(50)  not null
    constraint PK_Product
    primary key,
  Name  varchar(500) not null,
  Color varchar(50),
  Price money
)

INSERT INTO `Users` (`Id`, `Login`, `Password`, `FirstName`, `LastName`, `Role`)
VALUES (`1`, `TestUser`, `X03MO1qnZdYdgyfeuILPmQ==`, `UserName`, `UserLastName`, `Admin`)
GO

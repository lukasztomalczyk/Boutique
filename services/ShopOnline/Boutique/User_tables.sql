create table Users
(
  Id_Guid   int not null,
  Login     varchar(25),
  Password  varchar(32),
  FirstName varchar(25),
  LastName  varchar(25),
  Role      tinyint
)
go



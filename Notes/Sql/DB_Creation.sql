USE master
GO

IF EXISTS(SELECT * FROM sys.sysdatabases WHERE name = 'Notes') 
  DROP DATABASE Notes
GO

CREATE DATABASE Notes ON  ( 
  NAME = N'notes', FILENAME = N'D:\data\notes.mdf' , SIZE = 5120KB , FILEGROWTH = 1024KB )
LOG ON  (
  NAME = N'notes_log', FILENAME = N'D:\data\notes_log.ldf' , SIZE = 2048KB , FILEGROWTH = 10%)
GO

USE Notes
GO

CREATE TABLE dbo.Roles(
  Id int NOT NULL IDENTITY(1,1)
    CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED (Id),
  Name nvarchar(50) NOT NULL,
  IsAdmin bit,
  IsEditor bit)
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Roles TO Public
GO

CREATE TABLE dbo.Users(
  Id int NOT NULL IDENTITY(1,1)
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED(Id),
  RoleId int NOT NULL
    CONSTRAINT FK_Users_RoleId FOREIGN KEY (RoleId) REFERENCES dbo.Roles(Id),
  Username nvarchar(50) NOT NULL,
  Email nvarchar(100) NOT NULL,
  Password varchar(40) NOT NULL,
  Status int NOT NULL DEFAULT(0),
  CreatedOnDate  datetime NULL
    CONSTRAINT DF_Users_CreateOnDate DEFAULT(GETDATE()),
  ActivationInfo varchar(100))
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO Public
GO

SET IDENTITY_INSERT dbo.Roles ON
GO
INSERT INTO dbo.Roles (
  Id, Name, IsAdmin, IsEditor)
VALUES (
  1, 'Администраторы', 1, 1), (
  2, 'Редакторы', 0, 1), (
  3, 'Рядовые пользователи', 0, 0)
GO

SET IDENTITY_INSERT dbo.Roles OFF

DBCC CHECKIDENT(Roles,RESEED)
GO

SET IDENTITY_INSERT dbo.Users ON
GO

INSERT INTO dbo.Users (
  Id, UserName, 
  Password, Email, RoleId, 
  ActivationInfo)
VALUES (
  1, 'admin', 
  'D033E22AE348AEB5660FC2140AEC35850C4DA997', 'admin@domain.com', 1, 
  NULL), (
  2, 'editor', 
  'AB41949825606DA179DB7C89DDCEDCC167B64847', 'editor@domain.com', 2, 
  NULL), (
  3, 'user', 
  'DA39A3EE5E6B4B0D3255BFEF95601890AFD80709', 'user@domain.com', 3, 
  NULL)
GO

SET IDENTITY_INSERT dbo.Users OFF
GO

DBCC CHECKIDENT(Users,RESEED)
GO

CREATE TABLE dbo.Categories (
  Id int NOT NULL IDENTITY(1,1)
    CONSTRAINT PK_Categories PRIMARY KEY CLUSTERED (Id),
  Name nvarchar(1000)
)
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Categories TO Public
GO

CREATE TABLE dbo.Notes (
  Id int not NULL IDENTITY(1,1)
    CONSTRAINT PK_Notes PRIMARY KEY CLUSTERED(Id),
  Title nvarchar(1000),
  CreationDate datetime NOT NULL
    CONSTRAINT DF_Notes_CreationDate DEFAULT(GETDATE()),
  Description nvarchar(max),
  ActualTill datetime NULL,
  --References int NULL
  --  CONSTRAINT FK_Notes_Reference FOREIGN KEY (Reference) REFERENCES dbo.Notes(Id),
  CategoryId int NOT NULL
    CONSTRAINT FK_Notes_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(Id),
  OwnerId int NOT NULL
    CONSTRAINT FK_Notes_OwnerId FOREIGN KEY (OwnerId) REFERENCES dbo.Users(Id),
  Picture varbinary(max) NULL)
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Categories TO Public
GO

CREATE TABLE dbo.NoteReferences (
  Id int not NULL IDENTITY(1,1)
    CONSTRAINT PK_NoteReferences PRIMARY KEY CLUSTERED (Id),
  NoteId int NOT NULL
    CONSTRAINT FK_NoteReferences_NoteId FOREIGN KEY (NoteId) REFERENCES dbo.Notes(Id),
  ReferenceId int NOT NULL
    CONSTRAINT FK_NoteReferences_ReferenceId FOREIGN KEY (ReferenceId) REFERENCES dbo.Notes(Id))
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.NoteReferences TO Public
GO

CREATE VIEW dbo.GetRoles 
AS
  SELECT * FROM dbo.Roles
GO

GRANT SELECT ON dbo.GetRoles TO Public
GO

CREATE VIEW dbo.GetUsers 
AS
  SELECT 
    U.*, R.Name as RoleName
  FROM
    dbo.Users U
	  INNER JOIN dbo.Roles R ON U.RoleId = R.Id
GO

GRANT SELECT ON dbo.GetUsers TO Public
GO

CREATE VIEW dbo.GetCategories
AS
  SELECT * FROM dbo.Categories
GO

CREATE VIEW dbo.GetNotes
AS
  SELECT
    N.*, C.Name as CategoryTitle
  FROM
    dbo.Notes N
	  INNER JOIN dbo.Categories C ON N.CategoryId = C.Id
GO

CREATE VIEW dbo.GetNoteReferences
AS
  SELECT * FROM dbo.NoteReferences
GO

CREATE PROCEDURE dbo.SaveRole
  @Id int OUTPUT,
  @Name nvarchar(50),
  @IsAdmin bit,
  @IsEditor bit
AS
  BEGIN
    DECLARE @Result int
    IF EXISTS(SELECT * FROM dbo.Roles WHERE Id = @Id)
	  BEGIN
	    UPDATE dbo.Roles SET Name = @Name, IsAdmin = @IsAdmin, IsEditor = @IsEditor WHERE Id = @Id
		SELECT @Result = @@ROWCOUNT		
	  END
	ELSE
	  BEGIN
	    INSERT INTO dbo.Roles (
		  Name, IsAdmin, IsEditor)
        VALUES (
		  @Name, @IsAdmin, @IsEditor)
		SELECT @Id = SCOPE_IDENTITY(), @Result = @@ROWCOUNT
	  END
	RETURN @Result
  END
GO

GRANT EXECUTE ON dbo.SaveRole TO Public
GO

CREATE PROCEDURE dbo.DeleteRole
  @Id int
AS
  BEGIN
    DECLARE @Result int
	IF EXISTS(SELECT * FROM dbo.Users WHERE RoleId = @Id)
	  SET @Result = 0
	ELSE
	  BEGIN
	    DELETE FROM dbo.Roles WHERE Id = @Id
		SET @Result = @@ROWCOUNT
	  END
	RETURN @Result
  END
GO

GRANT EXECUTE ON dbo.DeleteRole TO Public
GO

CREATE PROCEDURE dbo.SaveUser
  @Id int OUTPUT,
  @RoleId int,
  @UserName nvarchar(50),
  @Email nvarchar(50),
  @Password varchar(40),
  @Status int
AS
  BEGIN
    DECLARE @Result int
    IF EXISTS(SELECT * FROM dbo.Users WHERE Id = @Id)
	  BEGIN
	    UPDATE dbo.Users SET 
		  RoleId = @RoleId, UserName = @UserName, Email = @Email,
		  Password = @Password, Status = @Status
		WHERE 
		  Id = @Id
		SET @Result = @@ROWCOUNT
	  END
	ELSE
	  BEGIN
	    INSERT INTO dbo.Users (
		  RoleId, UserName, Email,
		  Password, Status, CreatedOnDate)
		VALUES (
		  @RoleId, @UserName, @Email,
		  @Password, @Status, GETDATE())
		SELECT @Result = @@ROWCOUNT, @Id = SCOPE_IDENTITY()
	  END
	RETURN @Result
  END
GO

GRANT EXECUTE ON dbo.SaveUser TO Public
GO

CREATE PROCEDURE dbo.DeleteUser
  @Id int
AS
  BEGIN
    DECLARE @Result int
    IF EXISTS(SELECT * FROM dbo.Notes WHERE OwnerId = @Id)
	  SET @Result = 0
	ELSE
	  BEGIN
	    DELETE FROM dbo.Users WHERE Id = @Id
		SET @Result = @@ROWCOUNT
	  END
	RETURN @Result
  END
GO

GRANT EXECUTE ON dbo.DeleteUser TO Public
GO

CREATE PROCEDURE dbo.UpdatePasswordUser
  @Id int,
  @Password varchar(40)
AS
  BEGIN
    IF EXISTS(SELECT * FROM dbo.Users WHERE Id = @Id)
	  BEGIN
	    UPDATE dbo.Users SET Password = @Password WHERE Id = @Id		
		RETURN @@ROWCOUNT
	  END
	ELSE
	  RETURN -1
  END
GO

GRANT EXECUTE ON dbo.UpdatePasswordUser TO Public
GO

CREATE PROCEDURE dbo.SaveCategory
  @Id int OUTPUT,
  @Title nvarchar(1000)
AS
  BEGIN
    DECLARE @Result int
	IF EXISTS(SELECT * FROM dbo.Categories WHERE Id = @Id)
	  BEGIN
	    UPDATE dbo.Categories SET
		  Name = @Title 
		WHERE
		  Id = @Id
		SET @Result = @@ROWCOUNT
	  END
	ELSE
	  BEGIN
	    INSERT INTO dbo.Categories (
		  Name)
		VALUES (
		  @Title)
		SELECT @Result = @@ROWCOUNT, @Id = SCOPE_IDENTITY()
	  END
    RETURN @Result
  END
 GO
 
 GRANT EXECUTE ON dbo.SaveCategory TO Public
 GO
 
 CREATE PROCEDURE dbo.DeleteCategory
   @Id int
 AS
   BEGIN 
     DECLARE @Result int
	 IF EXISTS(SELECT * FROM dbo.Notes WHERE CategoryId = @Id)
	   BEGIN
	     SET @Result = 0
	   END
	 ELSE
	   BEGIN
	     DELETE FROM dbo.Categories WHERE Id = @Id
		 SET @Result = @@ROWCOUNT
	   END 
	 RETURN @Result 
   END
 GO
    
GRANT EXECUTE ON dbo.DeleteCategory TO Public
GO

CREATE PROCEDURE dbo.SaveNote 
  @Id int OUTPUT,
  @Title nvarchar(1000),
  @Description nvarchar(max),
  @CategoryId int,
  @OwnerId int,
  @Picture varbinary(max)
AS
  BEGIN
    DECLARE @Result int
	IF EXISTS(SELECT * FROM dbo.Notes WHERE Id = @Id)
	  BEGIN
	    UPDATE dbo.Notes SET 
		  Title = @Title, Description = @Description, CategoryId = @CategoryId,
		  OwnerId = @OwnerId, Picture = @Picture
		WHERE
		  Id  = @Id
		SET @Result = @@ROWCOUNT
	  END
	ELSE
	  BEGIN
	    INSERT INTO dbo.Notes (
		  Title, Description, CategoryId,
		  OwnerId, Picture, CreationDate)
		VALUES (
		  @Title, @Description, @CategoryId,
		  @OwnerId, @Picture, GETDATE())
		SELECT @Result = @@ROWCOUNT, @Id = SCOPE_IDENTITY()
	  END
	RETURN @Result 
  END
GO

GRANT EXECUTE ON dbo.SaveNote TO Public
GO

CREATE PROCEDURE dbo.DeleteNote
  @Id int
AS
  BEGIN
    DECLARE @Result int
	IF EXISTS(SELECT * FROM dbo.Notes WHERE Id = @Id)
	  BEGIN
	    DELETE FROM dbo.Notes WHERE Id = @Id
		SET @Result = @@ROWCOUNT
		DELETE FROM dbo.NoteRefereces WHERE (NoteId = @Id)OR(ReferenceId = @Id)
	  END
	ELSE
	  SET @Result = 0
	RETURN @Result
  END
GO

GRANT EXECUTE ON dbo.DeleteNote TO Public
GO

  
INSERT INTO dbo.Categories (
  Name)
VALUES (
  'Домашние заботы'), (
  'Здоровье'), (
  'Развлечение'), (
  'Спорт'), (
  'Работа'), (
  'Хобби'), (
  'Дети'), (
  'Родители')
GO

INSERT INTO dbo.Notes (
  Title, Description, CategoryId,
  OwnerId)
VALUES (
  'Просмотр фильма с ребенком',
  'Обещал посмотреть с ребенком последнюю часть Гарри Поттера. Надо сдерживать свои обещания', 7, 1), (
  'Собрать портал станка',
  'Заказать детали крепления стола станка, заказать конструкционный профиль', 6, 1), (
  'Командировка в Минск',
  '3 апреля 2020 надо съездить в Минск в головную организации с вопросами по мониторингу', 5, 1)
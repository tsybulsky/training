USE master
GO

IF EXISTS(SELECT * FROM sys.sysdatabases WHERE name = 'Notes')
  BEGIN
    EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Notes'
    ALTER DATABASE Notes SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE Notes
  END
GO

CREATE DATABASE Notes ON  ( 
  NAME = N'notes', FILENAME = N'D:\data\notes.mdf' , SIZE = 5120KB , FILEGROWTH = 1024KB )
LOG ON  (
  NAME = N'notes_log', FILENAME = N'D:\data\notes_log.ldf' , SIZE = 2048KB , FILEGROWTH = 10%)
GO

USE Notes
GO

CREATE TABLE dbo.ErrorMessages (
  Code int NOT NULL PRIMARY KEY CLUSTERED,
  Message nvarchar(1000))
GO
DENY SELECT, INSERT, UPDATE, DELETE ON dbo.ErrorMessages TO Public
GO

CREATE TABLE dbo.Roles(
  Id int NOT NULL IDENTITY(1,1)
    CONSTRAINT PK_Roles PRIMARY KEY CLUSTERED (Id),
  Name nvarchar(50) NOT NULL,
  LocalizedName nvarchar(50) NOT NULL)
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Roles TO Public
GO

CREATE TABLE dbo.Users(
  Id int NOT NULL IDENTITY(1,1)
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED(Id),
  Login nvarchar(50) NOT NULL,
  Password varchar(40) NOT NULL,
  Email nvarchar(100) NOT NULL,
  Name nvarchar(100) NULL,
  Status int NOT NULL DEFAULT(0),
  CreatedOnDate  datetime NULL
    CONSTRAINT DF_Users_CreateOnDate DEFAULT(GETDATE()),
  LastLogin datetime NULL,
  ActivationInfo varchar(100))
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO Public
GO

CREATE TABLE dbo.UserRoles (
  Id int NOT NULL IDENTITY(1,1)
    CONSTRAINT PK_UserRoles PRIMARY KEY CLUSTERED(Id),
  UserId int NOT NULL
    CONSTRAINT FK_UserRoles_UserId FOREIGN KEY (UserId) REFERENCES dbo.Users(Id),
  RoleId int NOT NULL
    CONSTRAINT FK_UserRoles_RoleId FOREIGN KEY (RoleId) REFERENCES dbo.Roles(Id))
GO

DENY SELECT, INSERT, UPDATE, DELETE ON dbo.Users TO Public
GO

SET IDENTITY_INSERT dbo.Roles ON
GO
INSERT INTO dbo.Roles (
  Id, Name, LocalizedName)
VALUES (
  1, 'admin', 'Администраторы'), (
  2, 'editor', 'Редакторы'), (
  3, 'user', 'Пользователи')
GO

SET IDENTITY_INSERT dbo.Roles OFF

DBCC CHECKIDENT(Roles,RESEED)
GO

SET IDENTITY_INSERT dbo.Users ON
GO

INSERT INTO dbo.Users (
  Id, Login, Password, 
  Email)
VALUES (
  1, 'admin',	--default password: admin
  'D033E22AE348AEB5660FC2140AEC35850C4DA997', 'admin@domain.com'), (
  2, 'editor',	--default password: editor
  'AB41949825606DA179DB7C89DDCEDCC167B64847', 'editor@domain.com'), (
  3, 'user',	--default password: <blank>
  'DA39A3EE5E6B4B0D3255BFEF95601890AFD80709', 'user@domain.com')
GO

SET IDENTITY_INSERT dbo.Users OFF
GO

INSERT INTO dbo.UserRoles (
  UserId, RoleId)
VALUES (
  1, 1), (
  1, 2), (
  1, 3), (
  2, 2), (
  2, 3), (
  3, 3)
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
  Picture varbinary(max) NULL,
  PictureMimeType nvarchar(100) NULL)
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
  SELECT *, CASE WHEN Name IS NULL THEN Login ELSE Name END As NameOrLogin FROM dbo.Users
GO

GRANT SELECT ON dbo.GetUsers TO Public
GO

CREATE VIEW dbo.GetUserRoles
AS
  SELECT
    A.*, R.Name as RoleName, R.LocalizedName as LocalizedRoleName
  FROM
    dbo.UserRoles A
	  INNER JOIN dbo.Roles R ON A.RoleId = R.Id
GO

GRANT SELECT ON dbo.GetUserRoles TO Public
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
  SELECT 
	A.*, b.Title as ReferenceTitle 
  FROM 
    dbo.NoteReferences A 
	  INNER JOIN dbo.Notes B ON A.ReferenceId = b.Id
GO

CREATE PROCEDURE dbo.SaveRole
  @Id int OUTPUT,
  @Name nvarchar(50),
  @LocalizedName nvarchar(50)
AS
  BEGIN
    DECLARE @Result int
    IF EXISTS(SELECT * FROM dbo.Roles WHERE Id = @Id)
	  BEGIN
	    IF EXISTS(SELECT * FROM dbo.Roles WHERE (Name = @Name)AND(Id <> @Id))
		  RETURN -1	
	    UPDATE dbo.Roles SET Name = @Name, LocalizedName = @LocalizedName
		SELECT @Result = @@ROWCOUNT		
	  END
	ELSE
	  BEGIN
	    IF EXISTS(SELECT * FROM dbo.Roles WHERE (Name = @Name))
		  RETURN -1
	    INSERT INTO dbo.Roles (
		  Name, LocalizedName)
        VALUES (
		  @Name, @LocalizedName)
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
	IF EXISTS(SELECT * FROM dbo.UserRoles WHERE RoleId = @Id)
	  RETURN -4
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
  @Login nvarchar(50),
  @Password varchar(40),
  @Email nvarchar(50),  
  @Name nvarchar(100),
  @Status int
AS
  BEGIN
    DECLARE @Result int
    IF EXISTS(SELECT * FROM dbo.Users WHERE Id = @Id)
	  BEGIN
	    IF EXISTS(SELECT * FROM dbo.Users WHERE (Login = @Login)AND(Id <> @Id))
		  RETURN -2
		IF EXISTS(SELECT * FROM dbo.Users WHERE (Email = @Email)AND(Id <> @Id))
		  RETURN -3
	    UPDATE dbo.Users SET 
		  Login = @Login, Email = @Email, Name = @Name,
		  Status = @Status
		WHERE 
		  Id = @Id
		SET @Result = @@ERROR
	  END
	ELSE
	  BEGIN
	    INSERT INTO dbo.Users (
		  Login, Password, Email,
		  Name, Status, CreatedOnDate)
		VALUES (
		  @Login, @Password, @Email,
		  @Name, @Status, GETDATE())
		SELECT @Result = @@ERROR, @Id = SCOPE_IDENTITY()		
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

CREATE PROCEDURE dbo.SaveUserRole
  @Id int OUTPUT,
  @UserId int,
  @RoleId int
AS
  BEGIN
    DECLARE @Result int = 0
    IF EXISTS(SELECT * FROM dbo.UserRoles WHERE Id = @Id)
	  BEGIN
	    IF EXISTS(SELECT * FROM dbo.UserRoles WHERE (Id <> @Id)AND(UserId = @UserId)AND(RoleId = @RoleId))
		  RETURN -5
		UPDATE dbo.UserRoles SET RoleId = @RoleId, UserId = @UserId WHERE Id = @Id
		SET @Result = @@ERROR
	  END
	ELSE
	  BEGIN
	    IF EXISTS(SELECT * FROM dbo.UserRoles WHERE (UserId = @UserId)AND(RoleId = @RoleId))
		  RETURN -5
		INSERT INTO dbo.UserRoles (
		  UserId, RoleId) 
		VALUES (
		  @UserId, @RoleId)
		SET @Result = @@ERROR
	  END
	RETURN @Result
  END
GO

GRANT EXECUTE ON dbo.SaveUserRole TO Public
GO

CREATE PROCEDURE dbo.DeleteUserRole
  @id int
AS
  BEGIN
    DECLARE @Result int, @RowCount int 
	DELETE FROM dbo.UserRoles WHERE Id = @Id
	SELECT @Result = @@ERROR, @RowCount = @@ROWCOUNT
	IF (@Result = 0)AND(@RowCount = 0)
	  SET @Result = -6
	RETURN @Result
  END
GO

GRANT EXECUTE ON dbo.DeleteUserRole TO Public
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

CREATE PROCEDURE dbo.UpdateLoginTime
  @Id int
AS
  BEGIN
    UPDATE dbo.Users SET LastLogin = GETDATE() WHERE Id = @Id
  END
GO

GRANT EXECUTE ON dbo.UpdateLoginTime TO Public
GO

CREATE PROCEDURE dbo.SaveCategory
  @Id int OUTPUT,
  @Name nvarchar(1000)
AS
  BEGIN
    DECLARE @Result int
	IF EXISTS(SELECT * FROM dbo.Categories WHERE Id = @Id)
	  BEGIN
	    UPDATE dbo.Categories SET
		  Name = @Name 
		WHERE
		  Id = @Id
		SET @Result = @@ROWCOUNT
	  END
	ELSE
	  BEGIN
	    INSERT INTO dbo.Categories (
		  Name)
		VALUES (
		  @Name)
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
	   RETURN -5
     DELETE FROM dbo.Categories WHERE Id = @Id
     RETURN @@ERROR
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
  @Picture varbinary(max),
  @PictureMimeType varchar(100)
AS
  BEGIN
    DECLARE @Result int
	IF EXISTS(SELECT * FROM dbo.Notes WHERE Id = @Id)
	  BEGIN
	    UPDATE dbo.Notes SET 
		  Title = @Title, Description = @Description, CategoryId = @CategoryId,
		  OwnerId = @OwnerId, Picture = @Picture, PictureMimeType = @PictureMimeType
		WHERE
		  Id  = @Id
		SET @Result = @@ROWCOUNT
	  END
	ELSE
	  BEGIN
	    INSERT INTO dbo.Notes (
		  Title, Description, CategoryId,
		  OwnerId, Picture, PictureMimeType,
		  CreationDate)
		VALUES (
		  @Title, @Description, @CategoryId,
		  @OwnerId, @Picture, @PictureMimeType,
		  GETDATE())
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

 INSERT INTO dbo.ErrorMessages (
   Code, Message)
VALUES (
  -1, 'Роль с таким именем уже существует в базе данных'), (
  -2, 'Пользователь с таким логином уже зарегистрирован'), (
  -3, 'Пользователь с таким email уже зарегистрирован'), (
  -4, 'Нельзя удалить роль пока есть пользователи этой роли'), (
  -5, 'У пользователя уже имеется данная роль'), (
  -6, 'Роль пользователя не найдена в базе данных')
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



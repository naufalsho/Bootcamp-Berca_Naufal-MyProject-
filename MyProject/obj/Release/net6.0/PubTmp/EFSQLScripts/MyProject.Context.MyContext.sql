IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230408103526_firstmigration')
BEGIN
    CREATE TABLE [Department] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Department] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230408103526_firstmigration')
BEGIN
    CREATE TABLE [TB_Employee] (
        [NIK] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Phone] nvarchar(max) NOT NULL,
        [BirthDate] datetimeoffset NOT NULL,
        [Salary] int NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Gender] int NOT NULL,
        [DepartmentId] int NULL,
        CONSTRAINT [PK_TB_Employee] PRIMARY KEY ([NIK]),
        CONSTRAINT [FK_TB_Employee_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230408103526_firstmigration')
BEGIN
    CREATE INDEX [IX_TB_Employee_DepartmentId] ON [TB_Employee] ([DepartmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230408103526_firstmigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230408103526_firstmigration', N'7.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411032143_add-Table-Account')
BEGIN
    CREATE TABLE [TB_Account] (
        [NIK] nvarchar(450) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [EmployeeNIK] nvarchar(450) NULL,
        CONSTRAINT [PK_TB_Account] PRIMARY KEY ([NIK]),
        CONSTRAINT [FK_TB_Account_TB_Employee_EmployeeNIK] FOREIGN KEY ([EmployeeNIK]) REFERENCES [TB_Employee] ([NIK])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411032143_add-Table-Account')
BEGIN
    CREATE INDEX [IX_TB_Account_EmployeeNIK] ON [TB_Account] ([EmployeeNIK]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411032143_add-Table-Account')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230411032143_add-Table-Account', N'7.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411035957_add-FKnik-Account')
BEGIN
    ALTER TABLE [TB_Account] DROP CONSTRAINT [FK_TB_Account_TB_Employee_EmployeeNIK];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411035957_add-FKnik-Account')
BEGIN
    DROP INDEX [IX_TB_Account_EmployeeNIK] ON [TB_Account];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411035957_add-FKnik-Account')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TB_Account]') AND [c].[name] = N'EmployeeNIK');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [TB_Account] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [TB_Account] DROP COLUMN [EmployeeNIK];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411035957_add-FKnik-Account')
BEGIN
    ALTER TABLE [TB_Account] ADD CONSTRAINT [FK_TB_Account_TB_Employee_NIK] FOREIGN KEY ([NIK]) REFERENCES [TB_Employee] ([NIK]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411035957_add-FKnik-Account')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230411035957_add-FKnik-Account', N'7.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411041231_add-FKnik-Account2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230411041231_add-FKnik-Account2', N'7.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411041429_add-FKnik-Account3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230411041429_add-FKnik-Account3', N'7.0.1');
END;
GO

COMMIT;
GO


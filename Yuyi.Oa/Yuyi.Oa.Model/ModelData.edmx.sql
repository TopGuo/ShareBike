
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2017 15:54:31
-- Generated from EDMX file: C:\Users\guo\Desktop\123\Yuyi.Oa\Yuyi.Oa.Model\ModelData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [YuYiDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserInfoOrderInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderInfo] DROP CONSTRAINT [FK_UserInfoOrderInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_User_Action]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_User_ActionInfo] DROP CONSTRAINT [FK_UserInfoR_User_Action];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoR_User_Action]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_User_ActionInfo] DROP CONSTRAINT [FK_ActionInfoR_User_Action];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[OrderInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[R_User_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_User_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[BikeInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BikeInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfoActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfoActionInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ShowName] nvarchar(50)  NULL,
    [Pwd] nvarchar(50)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifyOn] datetime  NOT NULL,
    [Remark] nvarchar(100)  NULL
);
GO

-- Creating table 'OrderInfo'
CREATE TABLE [dbo].[OrderInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [UserInfoID] int  NOT NULL,
    [Counts] int  NULL,
    [TotalPrice] decimal(18,0)  NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifyOn] datetime  NOT NULL,
    [Remark] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Url] nvarchar(512)  NOT NULL,
    [HttpMethod] nvarchar(32)  NULL,
    [ModifyOn] datetime  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ActionName] nvarchar(64)  NOT NULL,
    [Remark] nvarchar(100)  NULL,
    [IsMenu] bit  NULL,
    [DelFlag] smallint  NOT NULL,
    [Sort] int  NULL,
    [IsIcon] nvarchar(512)  NULL
);
GO

-- Creating table 'R_User_ActionInfo'
CREATE TABLE [dbo].[R_User_ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [HasPerssion] bit  NULL,
    [UserInfoID] int  NOT NULL,
    [ActionInfoID] int  NOT NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'BikeInfo'
CREATE TABLE [dbo].[BikeInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UID] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [DelFlag] smallint  NULL,
    [SubTime] datetime  NOT NULL,
    [ModifyOn] datetime  NULL,
    [AdminUser] nvarchar(50)  NOT NULL,
    [Counts] int  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [TotalPrice] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'AccountInfo'
CREATE TABLE [dbo].[AccountInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Money] decimal(18,0)  NOT NULL,
    [UserInfoID] int  NOT NULL,
    [Remark] nvarchar(500)  NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifyOn] datetime  NOT NULL
);
GO

-- Creating table 'YiquanInfo'
CREATE TABLE [dbo].[YiquanInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Counts] int  NOT NULL,
    [Remark] nvarchar(500)  NULL,
    [SubTime] datetime  NULL,
    [ModifyOn] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [UserInfoID] int  NOT NULL
);
GO

-- Creating table 'UserInfoRoleInfo'
CREATE TABLE [dbo].[UserInfoRoleInfo] (
    [UserInfo_ID] int  NOT NULL,
    [RoleInfo_ID] int  NOT NULL
);
GO

-- Creating table 'RoleInfoActionInfo'
CREATE TABLE [dbo].[RoleInfoActionInfo] (
    [RoleInfo_ID] int  NOT NULL,
    [ActionInfo_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [PK_OrderInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RoleInfo'
ALTER TABLE [dbo].[RoleInfo]
ADD CONSTRAINT [PK_RoleInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'R_User_ActionInfo'
ALTER TABLE [dbo].[R_User_ActionInfo]
ADD CONSTRAINT [PK_R_User_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BikeInfo'
ALTER TABLE [dbo].[BikeInfo]
ADD CONSTRAINT [PK_BikeInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AccountInfo'
ALTER TABLE [dbo].[AccountInfo]
ADD CONSTRAINT [PK_AccountInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'YiquanInfo'
ALTER TABLE [dbo].[YiquanInfo]
ADD CONSTRAINT [PK_YiquanInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserInfo_ID], [RoleInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [PK_UserInfoRoleInfo]
    PRIMARY KEY NONCLUSTERED ([UserInfo_ID], [RoleInfo_ID] ASC);
GO

-- Creating primary key on [RoleInfo_ID], [ActionInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [PK_RoleInfoActionInfo]
    PRIMARY KEY NONCLUSTERED ([RoleInfo_ID], [ActionInfo_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfoID] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [FK_UserInfoOrderInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoOrderInfo'
CREATE INDEX [IX_FK_UserInfoOrderInfo]
ON [dbo].[OrderInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [UserInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
    FOREIGN KEY ([UserInfo_ID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_ID])
    REFERENCES [dbo].[RoleInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRoleInfo_RoleInfo'
CREATE INDEX [IX_FK_UserInfoRoleInfo_RoleInfo]
ON [dbo].[UserInfoRoleInfo]
    ([RoleInfo_ID]);
GO

-- Creating foreign key on [RoleInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_ID])
    REFERENCES [dbo].[RoleInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_ID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoActionInfo_ActionInfo'
CREATE INDEX [IX_FK_RoleInfoActionInfo_ActionInfo]
ON [dbo].[RoleInfoActionInfo]
    ([ActionInfo_ID]);
GO

-- Creating foreign key on [UserInfoID] in table 'R_User_ActionInfo'
ALTER TABLE [dbo].[R_User_ActionInfo]
ADD CONSTRAINT [FK_UserInfoR_User_Action]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_User_Action'
CREATE INDEX [IX_FK_UserInfoR_User_Action]
ON [dbo].[R_User_ActionInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [ActionInfoID] in table 'R_User_ActionInfo'
ALTER TABLE [dbo].[R_User_ActionInfo]
ADD CONSTRAINT [FK_ActionInfoR_User_Action]
    FOREIGN KEY ([ActionInfoID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoR_User_Action'
CREATE INDEX [IX_FK_ActionInfoR_User_Action]
ON [dbo].[R_User_ActionInfo]
    ([ActionInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'AccountInfo'
ALTER TABLE [dbo].[AccountInfo]
ADD CONSTRAINT [FK_UserInfoAccountInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoAccountInfo'
CREATE INDEX [IX_FK_UserInfoAccountInfo]
ON [dbo].[AccountInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'YiquanInfo'
ALTER TABLE [dbo].[YiquanInfo]
ADD CONSTRAINT [FK_UserInfoYiquanInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoYiquanInfo'
CREATE INDEX [IX_FK_UserInfoYiquanInfo]
ON [dbo].[YiquanInfo]
    ([UserInfoID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
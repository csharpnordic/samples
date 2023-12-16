--
-- Создание триггеров для заполнения поля Modified 
--
-- [adm].[Colors]
CREATE OR ALTER TRIGGER [adm].[Colors_AfterUpdate] ON [adm].[Colors] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Colors]
    SET    [adm].[Colors].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Colors].[ID]
END
GO
ALTER TABLE [adm].[Colors] ENABLE TRIGGER [Colors_AfterUpdate]
GO
-- [adm].[Features]
CREATE OR ALTER TRIGGER [adm].[Features_AfterUpdate] ON [adm].[Features] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Features]
    SET    [adm].[Features].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Features].[ID]
END
GO
ALTER TABLE [adm].[Features] ENABLE TRIGGER [Features_AfterUpdate]
GO
-- [adm].[FeatureGroups]
CREATE OR ALTER TRIGGER [adm].[FeatureGroups_AfterUpdate] ON [adm].[FeatureGroups] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[FeatureGroups]
    SET    [adm].[FeatureGroups].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[FeatureGroups].[ID]
END
GO
ALTER TABLE [adm].[FeatureGroups] ENABLE TRIGGER [FeatureGroups_AfterUpdate]
GO
-- [adm].[FeaturesOfGroups]
CREATE OR ALTER TRIGGER [adm].[FeaturesOfGroups_AfterUpdate] ON [adm].[FeaturesOfGroups] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[FeaturesOfGroups]
    SET    [adm].[FeaturesOfGroups].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[FeaturesOfGroups].[ID]
END
GO
ALTER TABLE [adm].[FeaturesOfGroups] ENABLE TRIGGER [FeaturesOfGroups_AfterUpdate]
GO
-- [adm].[Icons]
CREATE OR ALTER TRIGGER [adm].[Icons_AfterUpdate] ON [adm].[Icons] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Icons]
    SET    [adm].[Icons].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Icons].[ID]
END
GO
ALTER TABLE [adm].[Icons] ENABLE TRIGGER [Icons_AfterUpdate]
GO
-- [adm].[Jobs]
CREATE OR ALTER TRIGGER [adm].[Jobs_AfterUpdate] ON [adm].[Jobs] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Jobs]
    SET    [adm].[Jobs].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Jobs].[ID]
END
GO
ALTER TABLE [adm].[Jobs] ENABLE TRIGGER [Jobs_AfterUpdate]
GO
-- [adm].[Languages]
CREATE OR ALTER TRIGGER [adm].[Languages_AfterUpdate] ON [adm].[Languages] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Languages]
    SET    [adm].[Languages].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Languages].[ID]
END
GO
ALTER TABLE [adm].[Languages] ENABLE TRIGGER [Languages_AfterUpdate]
GO
-- [adm].[MenuItems]
CREATE OR ALTER TRIGGER [adm].[MenuItems_AfterUpdate] ON [adm].[MenuItems] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[MenuItems]
    SET    [adm].[MenuItems].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[MenuItems].[ID]
END
GO
ALTER TABLE [adm].[MenuItems] ENABLE TRIGGER [MenuItems_AfterUpdate]
GO
-- [adm].[MenuItemOfGroups]
CREATE OR ALTER TRIGGER [adm].[MenuItemOfGroups_AfterUpdate] ON [adm].[MenuItemOfGroups] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[MenuItemOfGroups]
    SET    [adm].[MenuItemOfGroups].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[MenuItemOfGroups].[ID]
END
GO
ALTER TABLE [adm].[MenuItemOfGroups] ENABLE TRIGGER [MenuItemOfGroups_AfterUpdate]
GO
-- [adm].[Notifications]
CREATE OR ALTER TRIGGER [adm].[Notifications_AfterUpdate] ON [adm].[Notifications] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Notifications]
    SET    [adm].[Notifications].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Notifications].[ID]
END
GO
ALTER TABLE [adm].[Notifications] ENABLE TRIGGER [Notifications_AfterUpdate]
GO
-- [adm].[NotifiedUsers]
CREATE OR ALTER TRIGGER [adm].[NotifiedUsers_AfterUpdate] ON [adm].[NotifiedUsers] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[NotifiedUsers]
    SET    [adm].[NotifiedUsers].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[NotifiedUsers].[ID]
END
GO
ALTER TABLE [adm].[NotifiedUsers] ENABLE TRIGGER [NotifiedUsers_AfterUpdate]
GO
-- [adm].[OkeiUnits]
CREATE OR ALTER TRIGGER [adm].[OkeiUnits_AfterUpdate] ON [adm].[OkeiUnits] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[OkeiUnits]
    SET    [adm].[OkeiUnits].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[OkeiUnits].[ID]
END
GO
ALTER TABLE [adm].[OkeiUnits] ENABLE TRIGGER [OkeiUnits_AfterUpdate]
GO
-- [adm].[Plants]
CREATE OR ALTER TRIGGER [adm].[Plants_AfterUpdate] ON [adm].[Plants] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Plants]
    SET    [adm].[Plants].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Plants].[ID]
END
GO
ALTER TABLE [adm].[Plants] ENABLE TRIGGER [Plants_AfterUpdate]
GO
-- [adm].[PlantsOfRoles]
CREATE OR ALTER TRIGGER [adm].[PlantsOfRoles_AfterUpdate] ON [adm].[PlantsOfRoles] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[PlantsOfRoles]
    SET    [adm].[PlantsOfRoles].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[PlantsOfRoles].[ID]
END
GO
ALTER TABLE [adm].[PlantsOfRoles] ENABLE TRIGGER [PlantsOfRoles_AfterUpdate]
GO
-- [adm].[Roles]
CREATE OR ALTER TRIGGER [adm].[Roles_AfterUpdate] ON [adm].[Roles] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Roles]
    SET    [adm].[Roles].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Roles].[ID]
END
GO
ALTER TABLE [adm].[Roles] ENABLE TRIGGER [Roles_AfterUpdate]
GO
-- [adm].[RolesOfUsers]
CREATE OR ALTER TRIGGER [adm].[RolesOfUsers_AfterUpdate] ON [adm].[RolesOfUsers] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[RolesOfUsers]
    SET    [adm].[RolesOfUsers].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[RolesOfUsers].[ID]
END
GO
ALTER TABLE [adm].[RolesOfUsers] ENABLE TRIGGER [RolesOfUsers_AfterUpdate]
GO
-- [adm].[SubSystems]
CREATE OR ALTER TRIGGER [adm].[SubSystems_AfterUpdate] ON [adm].[SubSystems] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[SubSystems]
    SET    [adm].[SubSystems].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[SubSystems].[ID]
END
GO
ALTER TABLE [adm].[SubSystems] ENABLE TRIGGER [SubSystems_AfterUpdate]
GO
-- [adm].[SubSystemsOfPlants]
CREATE OR ALTER TRIGGER [adm].[SubSystemsOfPlants_AfterUpdate] ON [adm].[SubSystemsOfPlants] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[SubSystemsOfPlants]
    SET    [adm].[SubSystemsOfPlants].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[SubSystemsOfPlants].[ID]
END
GO
ALTER TABLE [adm].[SubSystemsOfPlants] ENABLE TRIGGER [SubSystemsOfPlants_AfterUpdate]
GO
-- [adm].[Users]
CREATE OR ALTER TRIGGER [adm].[Users_AfterUpdate] ON [adm].[Users] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[Users]
    SET    [adm].[Users].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[Users].[ID]
END
GO
ALTER TABLE [adm].[Users] ENABLE TRIGGER [Users_AfterUpdate]
GO
-- [adm].[UsersProfiles]
CREATE OR ALTER TRIGGER [adm].[UsersProfiles_AfterUpdate] ON [adm].[UsersProfiles] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[UsersProfiles]
    SET    [adm].[UsersProfiles].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[UsersProfiles].[ID]
END
GO
ALTER TABLE [adm].[UsersProfiles] ENABLE TRIGGER [UsersProfiles_AfterUpdate]
GO
-- [adm].[UserSettingsOfSubsystems]
CREATE OR ALTER TRIGGER [adm].[UserSettingsOfSubsystems_AfterUpdate] ON [adm].[UserSettingsOfSubsystems] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[UserSettingsOfSubsystems]
    SET    [adm].[UserSettingsOfSubsystems].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[UserSettingsOfSubsystems].[ID]
END
GO
ALTER TABLE [adm].[UserSettingsOfSubsystems] ENABLE TRIGGER [UserSettingsOfSubsystems_AfterUpdate]
GO
-- [adm].[UsersTables]
CREATE OR ALTER TRIGGER [adm].[UsersTables_AfterUpdate] ON [adm].[UsersTables] AFTER UPDATE AS
BEGIN
    UPDATE [adm].[UsersTables]
    SET    [adm].[UsersTables].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [adm].[UsersTables].[ID]
END
GO
ALTER TABLE [adm].[UsersTables] ENABLE TRIGGER [UsersTables_AfterUpdate]
GO

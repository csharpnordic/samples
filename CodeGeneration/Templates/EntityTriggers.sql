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
-- [gts].[Drivers]
CREATE OR ALTER TRIGGER [gts].[Drivers_AfterUpdate] ON [gts].[Drivers] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[Drivers]
    SET    [gts].[Drivers].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[Drivers].[ID]
END
GO
ALTER TABLE [gts].[Drivers] ENABLE TRIGGER [Drivers_AfterUpdate]
GO
-- [gts].[Instruments]
CREATE OR ALTER TRIGGER [gts].[Instruments_AfterUpdate] ON [gts].[Instruments] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[Instruments]
    SET    [gts].[Instruments].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[Instruments].[ID]
END
GO
ALTER TABLE [gts].[Instruments] ENABLE TRIGGER [Instruments_AfterUpdate]
GO
-- [gts].[InstrumentDetails]
CREATE OR ALTER TRIGGER [gts].[InstrumentDetails_AfterUpdate] ON [gts].[InstrumentDetails] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentDetails]
    SET    [gts].[InstrumentDetails].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentDetails].[ID]
END
GO
ALTER TABLE [gts].[InstrumentDetails] ENABLE TRIGGER [InstrumentDetails_AfterUpdate]
GO
-- [gts].[InstrumentMeasurements]
CREATE OR ALTER TRIGGER [gts].[InstrumentMeasurements_AfterUpdate] ON [gts].[InstrumentMeasurements] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentMeasurements]
    SET    [gts].[InstrumentMeasurements].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentMeasurements].[ID]
END
GO
ALTER TABLE [gts].[InstrumentMeasurements] ENABLE TRIGGER [InstrumentMeasurements_AfterUpdate]
GO
-- [gts].[InstrumentMeasurementDetails]
CREATE OR ALTER TRIGGER [gts].[InstrumentMeasurementDetails_AfterUpdate] ON [gts].[InstrumentMeasurementDetails] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentMeasurementDetails]
    SET    [gts].[InstrumentMeasurementDetails].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentMeasurementDetails].[ID]
END
GO
ALTER TABLE [gts].[InstrumentMeasurementDetails] ENABLE TRIGGER [InstrumentMeasurementDetails_AfterUpdate]
GO
-- [gts].[InstrumentParameters]
CREATE OR ALTER TRIGGER [gts].[InstrumentParameters_AfterUpdate] ON [gts].[InstrumentParameters] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentParameters]
    SET    [gts].[InstrumentParameters].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentParameters].[ID]
END
GO
ALTER TABLE [gts].[InstrumentParameters] ENABLE TRIGGER [InstrumentParameters_AfterUpdate]
GO
-- [gts].[ParameterHistory]
CREATE OR ALTER TRIGGER [gts].[ParameterHistory_AfterUpdate] ON [gts].[ParameterHistory] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[ParameterHistory]
    SET    [gts].[ParameterHistory].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[ParameterHistory].[ID]
END
GO
ALTER TABLE [gts].[ParameterHistory] ENABLE TRIGGER [ParameterHistory_AfterUpdate]
GO
-- [gts].[InstrumentParameterTypes]
CREATE OR ALTER TRIGGER [gts].[InstrumentParameterTypes_AfterUpdate] ON [gts].[InstrumentParameterTypes] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentParameterTypes]
    SET    [gts].[InstrumentParameterTypes].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentParameterTypes].[ID]
END
GO
ALTER TABLE [gts].[InstrumentParameterTypes] ENABLE TRIGGER [InstrumentParameterTypes_AfterUpdate]
GO
-- [gts].[InstrumentTypes]
CREATE OR ALTER TRIGGER [gts].[InstrumentTypes_AfterUpdate] ON [gts].[InstrumentTypes] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentTypes]
    SET    [gts].[InstrumentTypes].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentTypes].[ID]
END
GO
ALTER TABLE [gts].[InstrumentTypes] ENABLE TRIGGER [InstrumentTypes_AfterUpdate]
GO
-- [gts].[InstrumentTypeDetails]
CREATE OR ALTER TRIGGER [gts].[InstrumentTypeDetails_AfterUpdate] ON [gts].[InstrumentTypeDetails] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentTypeDetails]
    SET    [gts].[InstrumentTypeDetails].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentTypeDetails].[ID]
END
GO
ALTER TABLE [gts].[InstrumentTypeDetails] ENABLE TRIGGER [InstrumentTypeDetails_AfterUpdate]
GO
-- [gts].[InstrumentSafetyCriterias]
CREATE OR ALTER TRIGGER [gts].[InstrumentSafetyCriterias_AfterUpdate] ON [gts].[InstrumentSafetyCriterias] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[InstrumentSafetyCriterias]
    SET    [gts].[InstrumentSafetyCriterias].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[InstrumentSafetyCriterias].[ID]
END
GO
ALTER TABLE [gts].[InstrumentSafetyCriterias] ENABLE TRIGGER [InstrumentSafetyCriterias_AfterUpdate]
GO
-- [gts].[Locations]
CREATE OR ALTER TRIGGER [gts].[Locations_AfterUpdate] ON [gts].[Locations] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[Locations]
    SET    [gts].[Locations].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[Locations].[ID]
END
GO
ALTER TABLE [gts].[Locations] ENABLE TRIGGER [Locations_AfterUpdate]
GO
-- [gts].[MonitoringTypes]
CREATE OR ALTER TRIGGER [gts].[MonitoringTypes_AfterUpdate] ON [gts].[MonitoringTypes] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[MonitoringTypes]
    SET    [gts].[MonitoringTypes].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[MonitoringTypes].[ID]
END
GO
ALTER TABLE [gts].[MonitoringTypes] ENABLE TRIGGER [MonitoringTypes_AfterUpdate]
GO
-- [gts].[SafetyCriterias]
CREATE OR ALTER TRIGGER [gts].[SafetyCriterias_AfterUpdate] ON [gts].[SafetyCriterias] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[SafetyCriterias]
    SET    [gts].[SafetyCriterias].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[SafetyCriterias].[ID]
END
GO
ALTER TABLE [gts].[SafetyCriterias] ENABLE TRIGGER [SafetyCriterias_AfterUpdate]
GO
-- [gts].[SafetyStates]
CREATE OR ALTER TRIGGER [gts].[SafetyStates_AfterUpdate] ON [gts].[SafetyStates] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[SafetyStates]
    SET    [gts].[SafetyStates].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[SafetyStates].[ID]
END
GO
ALTER TABLE [gts].[SafetyStates] ENABLE TRIGGER [SafetyStates_AfterUpdate]
GO
-- [gts].[SourceSystems]
CREATE OR ALTER TRIGGER [gts].[SourceSystems_AfterUpdate] ON [gts].[SourceSystems] AFTER UPDATE AS
BEGIN
    UPDATE [gts].[SourceSystems]
    SET    [gts].[SourceSystems].Modified = GETDATE()           
    FROM   INSERTED
    WHERE  INSERTED.Id = [gts].[SourceSystems].[ID]
END
GO
ALTER TABLE [gts].[SourceSystems] ENABLE TRIGGER [SourceSystems_AfterUpdate]
GO

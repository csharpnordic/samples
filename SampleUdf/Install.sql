-- Выполняется однократно на SQL Server
sp_configure 'clr enabled', 1
GO
-- Выполняется однократно на SQL Server
RECONFIGURE
GO
-- Вариант для Microsoft SQL Server 2017
IF SERVERPROPERTY('ProductMajorVersion') >= '14' BEGIN
DECLARE @blob VARBINARY(128) 
SELECT @blob = HASHBYTES('SHA2_512', BulkColumn) FROM OPENROWSET(BULK N'C:\App\CalculationForSql.dll', SINGLE_BLOB) AS DLLFILE
-- Регистрация сборки как доверенной по hash-коду
BEGIN TRY
	EXEC sp_add_trusted_assembly @blob
END TRY
BEGIN CATCH
	IF ERROR_NUMBER() <> 10345 THROW -- Игнорируем ошибку повторной регистрации сборки как доверенной
	PRINT '(i) Сборка уже является доверенной'
END CATCH
END
-- Удаление старой версии сборки вместе с функциями
DROP FUNCTION IF EXISTS [dbo].Calculation
DROP ASSEMBLY IF EXISTS [CalculationForSql]
-- Выполняется на каждом экземпляре SQL-сервера (в случае кластера)
CREATE ASSEMBLY CalculationForSql FROM 'C:\App\CalculationForSql.dll' WITH PERMISSION_SET = UNSAFE
GO
-- Создание или обновление функции
CREATE OR ALTER FUNCTION [dbo].Calculation (
	@R NUMERIC(10,6) -- ... другие параметры ... 
)
RETURNS TABLE (
	M NUMERIC(18,6) A-- ... другие результаты ...
)
EXTERNAL NAME CalculationForSql.UserDefinedFunctions.Calculation
GO



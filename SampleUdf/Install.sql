-- ����������� ���������� �� SQL Server
sp_configure 'clr enabled', 1
GO
-- ����������� ���������� �� SQL Server
RECONFIGURE
GO
-- ������� ��� Microsoft SQL Server 2017
IF SERVERPROPERTY('ProductMajorVersion') >= '14' BEGIN
DECLARE @blob VARBINARY(128) 
SELECT @blob = HASHBYTES('SHA2_512', BulkColumn) FROM OPENROWSET(BULK N'C:\App\CalculationForSql.dll', SINGLE_BLOB) AS DLLFILE
-- ����������� ������ ��� ���������� �� hash-����
BEGIN TRY
	EXEC sp_add_trusted_assembly @blob
END TRY
BEGIN CATCH
	IF ERROR_NUMBER() <> 10345 THROW -- ���������� ������ ��������� ����������� ������ ��� ����������
	PRINT '(i) ������ ��� �������� ����������'
END CATCH
END
-- �������� ������ ������ ������ ������ � ���������
DROP FUNCTION IF EXISTS [dbo].Calculation
DROP ASSEMBLY IF EXISTS [CalculationForSql]
-- ����������� �� ������ ���������� SQL-������� (� ������ ��������)
CREATE ASSEMBLY CalculationForSql FROM 'C:\App\CalculationForSql.dll' WITH PERMISSION_SET = UNSAFE
GO
-- �������� ��� ���������� �������
CREATE OR ALTER FUNCTION [dbo].Calculation (
	@R NUMERIC(10,6) -- ... ������ ��������� ... 
)
RETURNS TABLE (
	M NUMERIC(18,6) A-- ... ������ ���������� ...
)
EXTERNAL NAME CalculationForSql.UserDefinedFunctions.Calculation
GO



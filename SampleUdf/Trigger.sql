
CREATE OR ALTER TRIGGER Measurement_Insert ON [dbo].[Measurement] FOR INSERT AS
BEGIN
SET NOCOUNT ON
BEGIN TRY
	-- Параметры
	DECLARE @ID BIGINT
	DECLARE @NormalVolume NUMERIC(10,6)
	DECLARE @NormalDensity NUMERIC(10,6)
	-- [...]
	-- Цикл по вставленным строкам
	DECLARE MY_CURSOR CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY
	FOR SELECT ID, Density, Volume, Mass, Temperature
	FROM INSERTED
	WHERE NormalVolume IS NULL AND NormalDensity IS NULL
		
	OPEN MY_CURSOR
	FETCH NEXT FROM MY_CURSOR INTO @ID -- [...]
	WHILE @@FETCH_STATUS = 0
	BEGIN 
		-- Выполнение пересчёта с вызовом функции [...]		
		UPDATE [Measurement] -- Обновление расчитанных полей
		SET NormalVolume  = @NormalVolume, NormalDensity = @NormalDensity			
		FROM INSERTED
		WHERE [OrderResultLogistic].iID = @ID		
		FETCH NEXT FROM MY_CURSOR INTO @ID -- [...]
	END
	CLOSE MY_CURSOR
	DEALLOCATE MY_CURSOR
END TRY
BEGIN CATCH
	-- Протоколирование ошибки
	INSERT INTO [dbo].[ErrorLog] ([ID], [TableName], [TimeStamp], [Warning] ,[Error])
		   VALUES (@ID, 'Measurement', GETDATE(), NULL, ERROR_MESSAGE())
END CATCH
END
GO


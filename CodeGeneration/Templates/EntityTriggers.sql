--
-- Создание функции для заполнения поля Modified 
--
CREATE OR REPLACE FUNCTION public.func_update_modified()
RETURNS trigger
LANGUAGE plpgsql
AS $BODY$
BEGIN
  new."Modified" := LOCALTIMESTAMP;
  RETURN NEW;
END;
$BODY$
;
--
-- Создание триггеров для заполнения поля Modified 
-- 

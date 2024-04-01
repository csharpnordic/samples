# Перечень проектов

* Целевая платформа: Microsoft.NET Framework 4.8 или .NET 8.0

## AcronymForm

* Microsoft.NET Framework, Windows Forms
* Поиск сокращений в текстовом файле, построение списка использованных сокращений
* Словарь сокращений расположен на внешнем веб-сайте в формате XML
* Для загрузки используется XML-десериализация (System.Xml.Serialization)
* Осуществляется копирование полученного списка в буфер обмена Windows в формате CSV для вставки в Microsoft Excel 

## BgWorker

* Microsoft.NET Framework, Windows Forms
* Демонстрация использования класса BackgroundWorker для многопоточных вычислений

## BlogDownload

* Microsoft.NET Framework
* Загрузка блога с сайта на базе wix.com и сохранение в базе данных

## Calculator

* .NET 8.0, Windows Forms
* Проект простого калькулятора, поддерживающего различные системы счисления (для целых чисел)

## CodeGeneration

* .NET 8.0, библиотека классов
* Генерация кода (C# и SQL) при помощи шаблонов T4 (*.tt)

## ConsoleThreads

* .NET 8.0, консольное приложение
* Многопоточное консольное приложение 
* Демонстрация блокировки потоков через lock
* Досрочное завершение потока при помощи CancellationTokenSource / CancellationToken

## ExcelSample

* Microsoft.NET Framework, Microsoft Excel VSTO add-in
* Пример работы с файлом Microsoft Excel

## GenericApi

* Microsoft.NET Framework 4.8, ASP.NET
* Пример обработки исключений в Web API при помощи атрибутов

## LogicForm

* Microsoft.NET Framework, Windows Forms
* Простой дизайнер логических схем
* Элементы добавляются при помощи кнопки на панели и перетаскиваются мышью
* Для соединения двух элементов необходимо два раза щелкнуть на первый элемент а потом два раза же - на второй

## MarsChallenge

* Решение задачи Mars

## Puzzle Solver

* Microsoft.NET Framework, Windows Forms
* Решение различных головоломок

## Piano

* .NET 8.0, Windows Forms, NAudio
* Электронное пианино (динамическая генерация клавиш, четыре октавы, многоголосное)

## Queens

* .NET 8.0
* Решение задачи размещения N ферзей на шахматной доске

## SampleUdf

* Microsoft.NET Framework, Microsoft SQL Server User Defined Function (CLR)
* Пример написания функции для SQL Server на C#

## SimpleConsole

* .NET 8.0
* Литералы и приведение типа данных

## SnmpReaderService

* Microsoft.NET Framework, служба операционной системы
* служба операционной системы, чтение данных по протоколу SNMP
* возможность установки и удаления службы (параметры командной строки install и delete)
* возможность запуска службы в консольном режиме (параметры командной строки console) 

## StarSky

* .NET 8.0, консольное приложение
* многопоточное звёздное небо

## Transfusion

* Решение задачи о переливаниях

## UserInformation

* Запрос информации о пользователях из Microsoft Active Directory

## WalkieTalkie

* .NET 8.0, Windows Forms
* Межпроцессная синхронизация с использованием Mutex и Semaphore
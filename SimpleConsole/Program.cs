// Целочисленные литералы
var integer1 = 1;   // int
var integer2 = 10000000000; // long, так как число очень большое
var integer3 = 1u;  // uint
var integer4 = 1l;  // long
var integer5 = 1ul; // ulong

// Вещественные литералы
var number1 = 1.2345;  // double
var number2 = 1.2345f; // float
var number3 = 1.2345d; // double
var number4 = 1.2345m; // decimal

// Индексация строк
string s = "ABCDEFGHIJKL";
Console.WriteLine(s);
Console.WriteLine(s[0..3]);
Console.WriteLine(s[1..4]);
Console.WriteLine(s[2..]);
Console.WriteLine(s[3..^1]);
Console.WriteLine(s[^3..^1]);
Console.WriteLine(s[..^2]);

// Демонстрация неочевидного приведения типа данных
int number = 0;
char ch = 'O';
string s0 = "";
string s1 = s0 + number;
string s2 = s0 + ch;
string s3 = s0 + (number > 0 ? number : ch);
Console.WriteLine($"s1 = {s1}"); // Выводит '0'
Console.WriteLine($"s2 = {s2}"); // Выводит 'O'
Console.WriteLine($"s3 = {s3}"); // Выводит 79 - код символа 'O'
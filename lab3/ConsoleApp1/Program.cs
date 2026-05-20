using System;
using System.Collections.Generic;
using System.Linq;

// Абстрактный класс Number
abstract class Number
{
    public abstract Number Add(Number other);
    public abstract Number Subtract(Number other);
    public abstract Number Multiply(Number other);
    public abstract Number Divide(Number other);
    public abstract void Print();
    public abstract string GetTypeName();
    public abstract double GetDoubleValue();
}

// Класс Integer (наследник Number)
class Integer : Number
{
    private int value;

    public Integer(int value)
    {
        this.value = value;
    }

    public int GetValue()
    {
        return value;
    }

    public override Number Add(Number other)
    {
        if (other is Integer intOther)
            return new Integer(value + intOther.value);
        else if (other is Real realOther)
            return new Real(value + realOther.GetValue());
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override Number Subtract(Number other)
    {
        if (other is Integer intOther)
            return new Integer(value - intOther.value);
        else if (other is Real realOther)
            return new Real(value - realOther.GetValue());
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override Number Multiply(Number other)
    {
        if (other is Integer intOther)
            return new Integer(value * intOther.value);
        else if (other is Real realOther)
            return new Real(value * realOther.GetValue());
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override Number Divide(Number other)
    {
        if (other is Integer intOther)
        {
            if (intOther.value == 0)
                throw new DivideByZeroException("Деление на ноль!");
            return new Real((double)value / intOther.value);
        }
        else if (other is Real realOther)
        {
            if (realOther.GetValue() == 0)
                throw new DivideByZeroException("Деление на ноль!");
            return new Real(value / realOther.GetValue());
        }
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override void Print()
    {
        Console.Write(value);
    }

    public override string GetTypeName()
    {
        return "Integer";
    }

    public override double GetDoubleValue()
    {
        return value;
    }
}

// Класс Real (наследник Number)
class Real : Number
{
    private double value;

    public Real(double value)
    {
        this.value = value;
    }

    public double GetValue()
    {
        return value;
    }

    public override Number Add(Number other)
    {
        if (other is Real realOther)
            return new Real(value + realOther.value);
        else if (other is Integer intOther)
            return new Real(value + intOther.GetValue());
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override Number Subtract(Number other)
    {
        if (other is Real realOther)
            return new Real(value - realOther.value);
        else if (other is Integer intOther)
            return new Real(value - intOther.GetValue());
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override Number Multiply(Number other)
    {
        if (other is Real realOther)
            return new Real(value * realOther.value);
        else if (other is Integer intOther)
            return new Real(value * intOther.GetValue());
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override Number Divide(Number other)
    {
        if (other is Real realOther)
        {
            if (realOther.value == 0)
                throw new DivideByZeroException("Деление на ноль!");
            return new Real(value / realOther.value);
        }
        else if (other is Integer intOther)
        {
            if (intOther.GetValue() == 0)
                throw new DivideByZeroException("Деление на ноль!");
            return new Real(value / intOther.GetValue());
        }
        else
            throw new ArgumentException("Неподдерживаемый тип");
    }

    public override void Print()
    {
        Console.Write(value);
    }

    public override string GetTypeName()
    {
        return "Real";
    }

    public override double GetDoubleValue()
    {
        return value;
    }
}

// Класс Series (набор чисел)
class Series
{
    private List<Number> numbers;
    private string name;

    public Series(string name)
    {
        this.name = name;
        numbers = new List<Number>();
    }

    public string Name => name;

    public void AddNumber(Number number)
    {
        numbers.Add(number);
        Console.Write($"Добавлено число в набор '{name}': ");
        number.Print();
        Console.WriteLine();
    }

    public bool RemoveNumber(int index)
    {
        if (index >= 0 && index < numbers.Count)
        {
            Console.Write($"Удалено число из набора '{name}': ");
            numbers[index].Print();
            Console.WriteLine();
            numbers.RemoveAt(index);
            return true;
        }
        return false;
    }

    public Number GetNumber(int index)
    {
        if (index >= 0 && index < numbers.Count)
            return numbers[index];
        return null;
    }

    public void PrintAllCharacteristics()
    {
        if (numbers.Count == 0)
        {
            Console.WriteLine($"Набор '{name}' пуст");
            return;
        }

        Console.WriteLine($"\n=== Характеристики набора '{name}' ===");
        Console.WriteLine($"Всего элементов: {numbers.Count}");
        Console.WriteLine("\nДетальная информация об элементах:");

        for (int i = 0; i < numbers.Count; i++)
        {
            Console.Write($"[{i}] Тип: {numbers[i].GetTypeName()}, Значение: ");
            numbers[i].Print();

            if (numbers[i] is Integer intNum)
            {
                Console.Write($", Квадрат: {intNum.GetValue() * intNum.GetValue()}");
                Console.Write($", Четность: {(intNum.GetValue() % 2 == 0 ? "четное" : "нечетное")}");
            }
            else if (numbers[i] is Real realNum)
            {
                Console.Write($", Округление: {Math.Round(realNum.GetValue(), 2)}");
                Console.Write($", Целая часть: {(int)realNum.GetValue()}");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\n=== Статистика ===");
        Console.WriteLine($"Сумма: {Sum()}");
        Console.WriteLine($"Среднее: {Average():F2}");
        Console.WriteLine($"Максимум: {Max():F2}");
        Console.WriteLine($"Минимум: {Min():F2}");
        Console.WriteLine($"Целых чисел: {CountByType("Integer")}");
        Console.WriteLine($"Вещественных чисел: {CountByType("Real")}");
        Console.WriteLine("===================\n");
    }

    public void PrintAll()
    {
        if (numbers.Count == 0)
        {
            Console.WriteLine($"Набор '{name}' пуст");
            return;
        }

        Console.Write($"Набор '{name}' [{numbers.Count}]: ");
        for (int i = 0; i < numbers.Count; i++)
        {
            numbers[i].Print();
            if (i < numbers.Count - 1)
                Console.Write(", ");
        }
        Console.WriteLine();
    }

    public double Sum() => numbers.Sum(n => n.GetDoubleValue());
    public double Average() => numbers.Count == 0 ? 0 : Sum() / numbers.Count;
    public double Max() => numbers.Count == 0 ? 0 : numbers.Max(n => n.GetDoubleValue());
    public double Min() => numbers.Count == 0 ? 0 : numbers.Min(n => n.GetDoubleValue());
    public int CountByType(string typeName) => numbers.Count(n => n.GetTypeName() == typeName);
    public int Size() => numbers.Count;

    public void SortAscending()
    {
        numbers = numbers.OrderBy(n => n.GetDoubleValue()).ToList();
        Console.WriteLine($"Набор '{name}' отсортирован по возрастанию");
    }

    public void SortDescending()
    {
        numbers = numbers.OrderByDescending(n => n.GetDoubleValue()).ToList();
        Console.WriteLine($"Набор '{name}' отсортирован по убыванию");
    }

    public void Clear()
    {
        numbers.Clear();
        Console.WriteLine($"Набор '{name}' очищен");
    }
}

// Основная программа с интерактивным меню
class Program
{
    private static Dictionary<string, Number> variables = new Dictionary<string, Number>();
    private static Dictionary<string, Series> serieses = new Dictionary<string, Series>();
    private static int varCounter = 1;
    private static int seriesCounter = 1;

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║     КАЛЬКУЛЯТОР ЧИСЕЛ И НАБОРОВ (Integer и Real)         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        ShowHelp();

        while (true)
        {
            Console.Write("\n> ");
            string input = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(input))
                continue;

            if (input == "exit" || input == "quit")
            {
                Console.WriteLine("До свидания!");
                break;
            }

            ProcessCommand(input);
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("\nДОСТУПНЫЕ КОМАНДЫ:");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        Console.WriteLine("СОЗДАНИЕ ЧИСЕЛ:");
        Console.WriteLine("  int <значение>              - создать целое число");
        Console.WriteLine("  real <значение>             - создать вещественное число");
        Console.WriteLine("  list                        - показать все созданные числа");
        Console.WriteLine();
        Console.WriteLine("АРИФМЕТИЧЕСКИЕ ОПЕРАЦИИ:");
        Console.WriteLine("  <имя1> + <имя2>              - сложение");
        Console.WriteLine("  <имя1> - <имя2>              - вычитание");
        Console.WriteLine("  <имя1> * <имя2>              - умножение");
        Console.WriteLine("  <имя1> / <имя2>              - деление");
        Console.WriteLine("  <имя1> <операция> <значение> - операция с числом");
        Console.WriteLine();
        Console.WriteLine("РАБОТА С НАБОРАМИ (SERIES):");
        Console.WriteLine("  series create <имя>         - создать новый набор");
        Console.WriteLine("  series list                  - показать все наборы");
        Console.WriteLine("  series add <набор> <число>   - добавить число в набор");
        Console.WriteLine("  series remove <набор> <индекс> - удалить число из набора");
        Console.WriteLine("  series print <набор>         - показать все числа набора");
        Console.WriteLine("  series info <набор>          - показать характеристики набора");
        Console.WriteLine("  series sort asc <набор>      - сортировка по возрастанию");
        Console.WriteLine("  series sort desc <набор>     - сортировка по убыванию");
        Console.WriteLine("  series clear <набор>         - очистить набор");
        Console.WriteLine("  series sum <набор>           - сумма чисел в наборе");
        Console.WriteLine("  series avg <набор>           - среднее арифметическое");
        Console.WriteLine();
        Console.WriteLine("ДРУГИЕ КОМАНДЫ:");
        Console.WriteLine("  help                        - показать эту справку");
        Console.WriteLine("  exit                        - выход");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    }

    static void ProcessCommand(string input)
    {
        string[] parts = input.Split(' ');
        string command = parts[0].ToLower();

        try
        {
            switch (command)
            {
                case "help":
                    ShowHelp();
                    break;

                case "int":
                    CreateInteger(parts);
                    break;

                case "real":
                    CreateReal(parts);
                    break;

                case "list":
                    ListVariables();
                    break;

                case "series":
                    ProcessSeriesCommand(parts);
                    break;

                default:
                    // Проверка на арифметическую операцию
                    if (parts.Length == 3 && (parts[1] == "+" || parts[1] == "-" || parts[1] == "*" || parts[1] == "/"))
                    {
                        PerformArithmetic(parts);
                    }
                    else
                    {
                        Console.WriteLine("Неизвестная команда. Введите 'help' для справки.");
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void CreateInteger(string[] parts)
    {
        if (parts.Length != 2)
        {
            Console.WriteLine("Использование: int <значение>");
            return;
        }

        if (int.TryParse(parts[1], out int value))
        {
            string name = "int" + varCounter++;
            variables[name] = new Integer(value);
            Console.WriteLine($"✓ Создано целое число '{name}' = {value}");
        }
        else
        {
            Console.WriteLine("Ошибка: неверное целое число");
        }
    }

    static void CreateReal(string[] parts)
    {
        if (parts.Length != 2)
        {
            Console.WriteLine("Использование: real <значение>");
            return;
        }

        if (double.TryParse(parts[1], out double value))
        {
            string name = "real" + varCounter++;
            variables[name] = new Real(value);
            Console.WriteLine($"✓ Создано вещественное число '{name}' = {value}");
        }
        else
        {
            Console.WriteLine("Ошибка: неверное вещественное число");
        }
    }

    static void ListVariables()
    {
        if (variables.Count == 0)
        {
            Console.WriteLine("Нет созданных чисел. Создайте их с помощью 'int' или 'real'");
            return;
        }

        Console.WriteLine("\nСозданные числа:");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        foreach (var var in variables)
        {
            Console.Write($"  {var.Key} = ");
            var.Value.Print();
            Console.WriteLine($" (тип: {var.Value.GetTypeName()})");
        }
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
    }

    static void PerformArithmetic(string[] parts)
    {
        string name1 = parts[0];
        string op = parts[1];
        string operand2 = parts[2];

        if (!variables.ContainsKey(name1))
        {
            Console.WriteLine($"Число '{name1}' не найдено");
            return;
        }

        Number num1 = variables[name1];
        Number num2;

        // Проверяем, является ли второй операнд именем переменной или числом
        if (variables.ContainsKey(operand2))
        {
            num2 = variables[operand2];
        }
        else
        {
            // Пробуем преобразовать в число
            if (int.TryParse(operand2, out int intVal))
                num2 = new Integer(intVal);
            else if (double.TryParse(operand2, out double realVal))
                num2 = new Real(realVal);
            else
            {
                Console.WriteLine($"Операнд '{operand2}' не является ни именем переменной, ни числом");
                return;
            }
        }

        Number result = null;
        string opName = "";

        switch (op)
        {
            case "+":
                result = num1.Add(num2);
                opName = "сложения";
                break;
            case "-":
                result = num1.Subtract(num2);
                opName = "вычитания";
                break;
            case "*":
                result = num1.Multiply(num2);
                opName = "умножения";
                break;
            case "/":
                result = num1.Divide(num2);
                opName = "деления";
                break;
        }

        string resultName = "res" + varCounter++;
        variables[resultName] = result;

        Console.Write($"Результат {opName}: ");
        num1.Print();
        Console.Write($" {op} ");
        if (variables.ContainsKey(operand2))
            Console.Write(operand2);
        else
            Console.Write(operand2);
        Console.Write(" = ");
        result.Print();
        Console.WriteLine($" (сохранено как '{resultName}')");
    }

    static void ProcessSeriesCommand(string[] parts)
    {
        if (parts.Length < 2)
        {
            Console.WriteLine("Использование: series <команда> [аргументы]");
            Console.WriteLine("Команды: create, list, add, remove, print, info, sort, clear, sum, avg");
            return;
        }

        string subCommand = parts[1].ToLower();

        switch (subCommand)
        {
            case "create":
                if (parts.Length != 3)
                {
                    Console.WriteLine("Использование: series create <имя_набора>");
                    return;
                }
                string seriesName = parts[2];
                if (serieses.ContainsKey(seriesName))
                {
                    Console.WriteLine($"Набор с именем '{seriesName}' уже существует");
                    return;
                }
                serieses[seriesName] = new Series(seriesName);
                Console.WriteLine($"✓ Создан новый набор '{seriesName}'");
                break;

            case "list":
                if (serieses.Count == 0)
                {
                    Console.WriteLine("Нет созданных наборов. Создайте с помощью 'series create <имя>'");
                    return;
                }
                Console.WriteLine("\nСозданные наборы:");
                foreach (var s in serieses)
                {
                    Console.WriteLine($"  • {s.Key} (элементов: {s.Value.Size()})");
                }
                break;

            case "add":
                if (parts.Length != 4)
                {
                    Console.WriteLine("Использование: series add <набор> <число>");
                    return;
                }
                string addSeries = parts[2];
                string numberName = parts[3];

                if (!serieses.ContainsKey(addSeries))
                {
                    Console.WriteLine($"Набор '{addSeries}' не найден");
                    return;
                }
                if (!variables.ContainsKey(numberName))
                {
                    Console.WriteLine($"Число '{numberName}' не найдено");
                    return;
                }
                serieses[addSeries].AddNumber(variables[numberName]);
                break;

            case "remove":
                if (parts.Length != 4)
                {
                    Console.WriteLine("Использование: series remove <набор> <индекс>");
                    return;
                }
                string removeSeries = parts[2];
                if (!serieses.ContainsKey(removeSeries))
                {
                    Console.WriteLine($"Набор '{removeSeries}' не найден");
                    return;
                }
                if (int.TryParse(parts[3], out int index))
                {
                    serieses[removeSeries].RemoveNumber(index);
                }
                else
                {
                    Console.WriteLine("Индекс должен быть числом");
                }
                break;

            case "print":
                if (parts.Length != 3)
                {
                    Console.WriteLine("Использование: series print <набор>");
                    return;
                }
                string printSeries = parts[2];
                if (!serieses.ContainsKey(printSeries))
                {
                    Console.WriteLine($"Набор '{printSeries}' не найден");
                    return;
                }
                serieses[printSeries].PrintAll();
                break;

            case "info":
                if (parts.Length != 3)
                {
                    Console.WriteLine("Использование: series info <набор>");
                    return;
                }
                string infoSeries = parts[2];
                if (!serieses.ContainsKey(infoSeries))
                {
                    Console.WriteLine($"Набор '{infoSeries}' не найден");
                    return;
                }
                serieses[infoSeries].PrintAllCharacteristics();
                break;

            case "sort":
                if (parts.Length != 4)
                {
                    Console.WriteLine("Использование: series sort <asc/desc> <набор>");
                    return;
                }
                string sortOrder = parts[2].ToLower();
                string sortSeries = parts[3];
                if (!serieses.ContainsKey(sortSeries))
                {
                    Console.WriteLine($"Набор '{sortSeries}' не найден");
                    return;
                }
                if (sortOrder == "asc")
                    serieses[sortSeries].SortAscending();
                else if (sortOrder == "desc")
                    serieses[sortSeries].SortDescending();
                else
                    Console.WriteLine("Используйте 'asc' или 'desc'");
                break;

            case "clear":
                if (parts.Length != 3)
                {
                    Console.WriteLine("Использование: series clear <набор>");
                    return;
                }
                string clearSeries = parts[2];
                if (!serieses.ContainsKey(clearSeries))
                {
                    Console.WriteLine($"Набор '{clearSeries}' не найден");
                    return;
                }
                serieses[clearSeries].Clear();
                break;

            case "sum":
                if (parts.Length != 3)
                {
                    Console.WriteLine("Использование: series sum <набор>");
                    return;
                }
                string sumSeries = parts[2];
                if (!serieses.ContainsKey(sumSeries))
                {
                    Console.WriteLine($"Набор '{sumSeries}' не найден");
                    return;
                }
                Console.WriteLine($"Сумма чисел в наборе '{sumSeries}': {serieses[sumSeries].Sum()}");
                break;

            case "avg":
                if (parts.Length != 3)
                {
                    Console.WriteLine("Использование: series avg <набор>");
                    return;
                }
                string avgSeries = parts[2];
                if (!serieses.ContainsKey(avgSeries))
                {
                    Console.WriteLine($"Набор '{avgSeries}' не найден");
                    return;
                }
                Console.WriteLine($"Среднее чисел в наборе '{avgSeries}': {serieses[avgSeries].Average():F2}");
                break;

            default:
                Console.WriteLine($"Неизвестная команда series: {subCommand}");
                Console.WriteLine("Доступные команды: create, list, add, remove, print, info, sort, clear, sum, avg");
                break;
        }
    }
}

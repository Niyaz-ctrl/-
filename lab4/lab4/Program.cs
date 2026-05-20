using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharSet : IEnumerable<char>
{
    private HashSet<char> _elements;

    // Конструктор по умолчанию
    public CharSet()
    {
        _elements = new HashSet<char>();
    }

    // Конструктор копирования
    public CharSet(CharSet other)
    {
        _elements = new HashSet<char>(other._elements);
    }

    // Оператор () - конструктор множества в стиле Паскаля
    // Использование: CharSet set = new CharSet('a','b','c');
    public CharSet(params char[] elements)
    {
        _elements = new HashSet<char>();
        foreach (char c in elements)
        {
            _elements.Add(c);
        }
    }

    // Конструктор из строки
    public CharSet(string elements)
    {
        _elements = new HashSet<char>();
        foreach (char c in elements)
        {
            if (!char.IsWhiteSpace(c))
                _elements.Add(c);
        }
    }

    // Добавление элемента
    public void Add(char c)
    {
        _elements.Add(c);
    }

    // Удаление элемента
    public bool Remove(char c)
    {
        return _elements.Remove(c);
    }

    // Проверка принадлежности
    public bool Contains(char c)
    {
        return _elements.Contains(c);
    }

    // Получение количества элементов
    public int Count => _elements.Count;

    // Оператор + - объединение множеств
    public static CharSet operator +(CharSet a, CharSet b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException("Множество не может быть null");

        CharSet result = new CharSet(a);
        foreach (char c in b._elements)
        {
            result.Add(c);
        }
        return result;
    }

    // Оператор <= - проверка на подмножество (все ли элементы b есть в a)
    public static bool operator <=(CharSet a, CharSet b)
    {
        if (a is null || b is null)
            throw new ArgumentNullException("Множество не может быть null");

        // Проверяем, является ли b подмножеством a
        foreach (char c in b._elements)
        {
            if (!a.Contains(c))
                return false;
        }
        return true;
    }

    // Оператор >= (нужен для пары с <= в C#)
    public static bool operator >=(CharSet a, CharSet b)
    {
        return b <= a;
    }

    // Пересечение множеств (дополнительная операция)
    public CharSet Intersect(CharSet other)
    {
        CharSet result = new CharSet();
        foreach (char c in _elements)
        {
            if (other.Contains(c))
                result.Add(c);
        }
        return result;
    }

    // Разность множеств (дополнительная операция)
    public CharSet Except(CharSet other)
    {
        CharSet result = new CharSet();
        foreach (char c in _elements)
        {
            if (!other.Contains(c))
                result.Add(c);
        }
        return result;
    }

    // Реализация IEnumerable<char>
    public IEnumerator<char> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Преобразование в строку
    public override string ToString()
    {
        return "{" + string.Join(", ", _elements.OrderBy(c => c)) + "}";
    }

    // Преобразование из строки
    public static CharSet Parse(string input)
    {
        // Ожидается формат {a,b,c} или a,b,c или abc
        input = input.Trim();
        if (input.StartsWith("{") && input.EndsWith("}"))
        {
            input = input.Substring(1, input.Length - 2);
        }

        char[] elements = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                               .SelectMany(s => s.ToCharArray())
                               .Where(c => !char.IsWhiteSpace(c))
                               .ToArray();

        return new CharSet(elements);
    }
}

class Program
{
    static void PrintHelp()
    {
        Console.WriteLine("\n=== Команды для работы с множествами ===");
        Console.WriteLine("help                    - показать эту справку");
        Console.WriteLine("exit                    - выход из программы");
        Console.WriteLine("new [setName] [элементы] - создать новое множество");
        Console.WriteLine("  пример: new A abc");
        Console.WriteLine("  пример: new B {a,b,c}");
        Console.WriteLine("  пример: new C a,b,c");
        Console.WriteLine("show [setName]          - показать множество");
        Console.WriteLine("add [setName] [элемент] - добавить элемент в множество");
        Console.WriteLine("remove [setName] [элемент] - удалить элемент");
        Console.WriteLine("union [set1] [set2] [result] - объединение множеств (set1+set2)");
        Console.WriteLine("subset [set1] [set2]   - проверить set2 ⊆ set1");
        Console.WriteLine("intersect [set1] [set2] [result] - пересечение множеств");
        Console.WriteLine("except [set1] [set2] [result] - разность множеств");
        Console.WriteLine("list                    - показать все множества");
        Console.WriteLine("\nПримеры использования операторов:");
        Console.WriteLine("  var A = new CharSet('a','b','c');");
        Console.WriteLine("  var B = new CharSet('b','c','d');");
        Console.WriteLine("  var C = A + B;  // объединение");
        Console.WriteLine("  bool isSubset = A <= B; // проверка");
        Console.WriteLine("=====================================\n");
    }

    static void Main(string[] args)
    {
        Dictionary<string, CharSet> sets = new Dictionary<string, CharSet>();
        string input;

        Console.WriteLine("АТД Множество (char)");
        Console.WriteLine("Введите 'help' для списка команд или 'exit' для выхода\n");

        if (args.Length > 0)
        {
            // Режим обработки аргументов командной строки
            ProcessCommandLine(args, sets);
            return;
        }

        // Интерактивный режим
        while (true)
        {
            Console.Write("> ");
            input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
                continue;

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("До свидания!");
                break;
            }

            if (input.ToLower() == "help")
            {
                PrintHelp();
                continue;
            }

            ProcessCommand(input, sets);
        }
    }

    static void ProcessCommandLine(string[] args, Dictionary<string, CharSet> sets)
    {
        try
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Использование: program.exe [имя_множества] [элементы]");
                Console.WriteLine("Пример: program.exe A abc");
                return;
            }

            string setName = args[0];
            string elements = args[1];

            CharSet set = new CharSet(elements);
            sets[setName] = set;

            Console.WriteLine($"Создано множество {setName} = {set}");

            // Демонстрация работы операторов
            Console.WriteLine("\n=== Демонстрация операций ===");
            CharSet set1 = new CharSet('a', 'b', 'c');
            CharSet set2 = new CharSet('b', 'c', 'd', 'e');

            Console.WriteLine($"Set1 = {set1}");
            Console.WriteLine($"Set2 = {set2}");

            // Оператор +
            CharSet union = set1 + set2;
            Console.WriteLine($"Set1 + Set2 = {union}");

            // Оператор <= (проверка подмножества)
            CharSet set3 = new CharSet('b', 'c');
            Console.WriteLine($"Set3 = {set3}");
            Console.WriteLine($"Set3 ⊆ Set1? {set3 <= set1}");
            Console.WriteLine($"Set3 ⊆ Set2? {set3 <= set2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void ProcessCommand(string command, Dictionary<string, CharSet> sets)
    {
        string[] parts = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            Console.WriteLine("Неизвестная команда. Введите 'help' для справки.");
            return;
        }

        try
        {
            switch (parts[0].ToLower())
            {
                case "new":
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Использование: new [имя] [элементы]");
                        return;
                    }
                    string name = parts[1];
                    string elements = string.Join(" ", parts.Skip(2));

                    // Удаляем возможные фигурные скобки и пробелы
                    elements = elements.Replace("{", "").Replace("}", "").Replace(",", "");
                    CharSet newSet = new CharSet(elements);
                    sets[name] = newSet;
                    Console.WriteLine($"Создано множество {name} = {newSet}");
                    break;

                case "show":
                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Использование: show [имя]");
                        return;
                    }
                    string showName = parts[1];
                    if (sets.ContainsKey(showName))
                        Console.WriteLine($"{showName} = {sets[showName]}");
                    else
                        Console.WriteLine($"Множество {showName} не найдено");
                    break;

                case "add":
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Использование: add [имя] [элемент]");
                        return;
                    }
                    string addName = parts[1];
                    char element = parts[2][0];
                    if (sets.ContainsKey(addName))
                    {
                        sets[addName].Add(element);
                        Console.WriteLine($"Добавлено '{element}' в {addName} = {sets[addName]}");
                    }
                    else
                        Console.WriteLine($"Множество {addName} не найдено");
                    break;

                case "remove":
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Использование: remove [имя] [элемент]");
                        return;
                    }
                    string removeName = parts[1];
                    char removeElement = parts[2][0];
                    if (sets.ContainsKey(removeName))
                    {
                        if (sets[removeName].Remove(removeElement))
                            Console.WriteLine($"Удалено '{removeElement}' из {removeName} = {sets[removeName]}");
                        else
                            Console.WriteLine($"Элемент '{removeElement}' не найден в {removeName}");
                    }
                    else
                        Console.WriteLine($"Множество {removeName} не найдено");
                    break;

                case "union":
                    if (parts.Length < 4)
                    {
                        Console.WriteLine("Использование: union [set1] [set2] [result]");
                        return;
                    }
                    string set1Name = parts[1];
                    string set2Name = parts[2];
                    string resultName = parts[3];

                    if (sets.ContainsKey(set1Name) && sets.ContainsKey(set2Name))
                    {
                        // Использование перегруженного оператора +
                        CharSet result = sets[set1Name] + sets[set2Name];
                        sets[resultName] = result;
                        Console.WriteLine($"{set1Name} + {set2Name} = {resultName} = {result}");
                    }
                    else
                        Console.WriteLine("Одно из множеств не найдено");
                    break;

                case "subset":
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Использование: subset [superset] [subset]");
                        return;
                    }
                    string superName = parts[1];
                    string subName = parts[2];

                    if (sets.ContainsKey(superName) && sets.ContainsKey(subName))
                    {
                        // Использование перегруженного оператора <=
                        bool isSubset = sets[superName] <= sets[subName];
                        Console.WriteLine($"{subName} ⊆ {superName}: {isSubset}");
                    }
                    else
                        Console.WriteLine("Одно из множеств не найдено");
                    break;

                case "intersect":
                    if (parts.Length < 4)
                    {
                        Console.WriteLine("Использование: intersect [set1] [set2] [result]");
                        return;
                    }
                    string inter1Name = parts[1];
                    string inter2Name = parts[2];
                    string interResultName = parts[3];

                    if (sets.ContainsKey(inter1Name) && sets.ContainsKey(inter2Name))
                    {
                        CharSet interResult = sets[inter1Name].Intersect(sets[inter2Name]);
                        sets[interResultName] = interResult;
                        Console.WriteLine($"Пересечение {inter1Name} ∩ {inter2Name} = {interResultName} = {interResult}");
                    }
                    else
                        Console.WriteLine("Одно из множеств не найдено");
                    break;

                case "except":
                    if (parts.Length < 4)
                    {
                        Console.WriteLine("Использование: except [set1] [set2] [result]");
                        return;
                    }
                    string except1Name = parts[1];
                    string except2Name = parts[2];
                    string exceptResultName = parts[3];

                    if (sets.ContainsKey(except1Name) && sets.ContainsKey(except2Name))
                    {
                        CharSet exceptResult = sets[except1Name].Except(sets[except2Name]);
                        sets[exceptResultName] = exceptResult;
                        Console.WriteLine($"{except1Name} \\ {except2Name} = {exceptResultName} = {exceptResult}");
                    }
                    else
                        Console.WriteLine("Одно из множеств не найдено");
                    break;

                case "list":
                    if (sets.Count == 0)
                        Console.WriteLine("Нет созданных множеств");
                    else
                    {
                        Console.WriteLine("Созданные множества:");
                        foreach (var kvp in sets)
                        {
                            Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Введите 'help' для справки.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
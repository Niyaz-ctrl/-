using System; // Подключаем пространство имён для работы с базовыми классами (Console, Array и др.)

// Класс, представляющий массив строк фиксированной длины
public class FixedStringArray
{
    private string[] array; // Приватное поле - внутренний массив строк
    private int length;     // Приватное поле - длина массива (хранится отдельно для удобства)

    // Конструктор: создаёт массив заданной длины
    public FixedStringArray(int length)
    {
        if (length <= 0) throw new ArgumentException("Длина должна быть >0"); // Проверка: длина не может быть 0 или отрицательной
        this.length = length;             // Сохраняем длину в поле класса
        array = new string[length];       // Выделяем память под массив строк указанной длины
    }

    // Конструктор копирования: создаёт массив на основе готового массива строк
    public FixedStringArray(string[] source)
    {
        length = source.Length;            // Берём длину из исходного массива
        array = new string[length];        // Создаём новый внутренний массив нужного размера
        Array.Copy(source, array, length); // Копируем все элементы из source в array
    }

    // Свойство только для чтения: возвращает длину массива
    public int Length => length;

    // Индексатор - позволяет обращаться к элементам через квадратные скобки [index]
    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= length) throw new IndexOutOfRangeException(); // Проверка границ
            return array[index]; // Возвращаем строку по указанному индексу
        }
        set
        {
            if (index < 0 || index >= length) throw new IndexOutOfRangeException(); // Проверка границ
            array[index] = value; // Присваиваем новое значение по указанному индексу
        }
    }

    // Метод для заполнения массива данными, введёнными с клавиатуры
    public void FillFromConsole()
    {
        for (int i = 0; i < length; i++) // Цикл по всем индексам массива
        {
            Console.Write($"Введите строку {i}: "); // Приглашение ко вводу
            array[i] = Console.ReadLine();          // Читаем строку и сохраняем в массив
        }
    }

    // Метод для вывода всего содержимого массива на экран
    public void PrintAll()
    {
        Console.WriteLine("Содержимое массива:"); // Заголовок
        for (int i = 0; i < length; i++)          // Цикл по всем элементам
            Console.WriteLine($"[{i}]: \"{array[i]}\""); // Выводим индекс и значение в кавычках
    }

    // Статический метод: поэлементное сцепление двух массивов (конкатенация строк с одинаковыми индексами)
    public static FixedStringArray Concatenate(FixedStringArray a, FixedStringArray b)
    {
        int newLength = Math.Max(a.length, b.length); // Длина результата = максимум из двух длин
        FixedStringArray result = new FixedStringArray(newLength); // Создаём массив-результат

        for (int i = 0; i < newLength; i++) // Цикл по всем индексам результата
        {
            string part1 = (i < a.length) ? a[i] : ""; // Если индекс есть в массиве a - берём элемент, иначе пустую строку
            string part2 = (i < b.length) ? b[i] : ""; // Если индекс есть в массиве b - берём элемент, иначе пустую строку
            result[i] = part1 + part2;                 // Сцепляем две строки и сохраняем в результат
        }
        return result; // Возвращаем новый массив
    }

    // Статический метод: слияние двух массивов без повторяющихся элементов (уникальные строки)
    public static FixedStringArray MergeUnique(FixedStringArray a, FixedStringArray b)
    {
        string[] temp = new string[a.length + b.length]; // Временный массив максимального размера (сумма длин)
        int count = 0; // Счётчик уникальных элементов (и следующий свободный индекс в temp)

        // Добавляем все уникальные элементы из первого массива
        for (int i = 0; i < a.length; i++)
            if (!Contains(temp, count, a[i])) // Если элемент ещё не добавлен
                temp[count++] = a[i];         // Добавляем его и увеличиваем счётчик

        // Добавляем все уникальные элементы из второго массива
        for (int i = 0; i < b.length; i++)
            if (!Contains(temp, count, b[i])) // Если элемент ещё не добавлен
                temp[count++] = b[i];         // Добавляем его и увеличиваем счётчик

        // Создаём результирующий массив точно под размер уникальных элементов
        FixedStringArray result = new FixedStringArray(count);
        for (int i = 0; i < count; i++) // Копируем уникальные элементы из temp в result
            result[i] = temp[i];

        return result; // Возвращаем массив уникальных строк
    }

    // Вспомогательный приватный статический метод: проверяет, содержится ли значение в первой части массива (до count)
    private static bool Contains(string[] arr, int count, string value)
    {
        for (int i = 0; i < count; i++)     // Проходим только по уже заполненной части (первые count элементов)
            if (arr[i] == value) return true; // Если нашли совпадение - возвращаем true
        return false; // Если не нашли - false
    }

    // Переопределяем метод ToString для удобного вывода информации об объекте
    public override string ToString() => $"StringArray[{length}]";
}

// Главный класс программы с точкой входа
class Program
{
    static FixedStringArray array1 = null; // Статическая переменная для хранения первого массива
    static FixedStringArray array2 = null; // Статическая переменная для хранения второго массива

    // Точка входа в программу
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Устанавливаем кодировку UTF-8 для корректного вывода русских букв

        // Бесконечный цикл для показа меню снова и снова
        while (true)
        {
            Console.WriteLine("\n--- Главное меню ---"); // Выводим заголовок меню
            Console.WriteLine("1. Создать массив 1");    // Пункт меню 1
            Console.WriteLine("2. Создать массив 2");    // Пункт меню 2
            Console.WriteLine("3. Показать оба массива"); // Пункт меню 3
            Console.WriteLine("4. Поэлементное сцепление массивов"); // Пункт меню 4
            Console.WriteLine("5. Слияние массивов с исключением повторений"); // Пункт меню 5
            Console.WriteLine("0. Выход");               // Пункт меню 0
            Console.Write("Выберите действие: ");        // Приглашение к вводу

            // Обрабатываем выбор пользователя через конструкцию switch
            switch (Console.ReadLine()) // Читаем строку, введённую пользователем
            {
                case "1": array1 = CreateArray("Массив 1"); break; // Создаём первый массив
                case "2": array2 = CreateArray("Массив 2"); break; // Создаём второй массив
                case "3": ShowArrays(); break;                     // Показываем оба массива
                case "4": // Поэлементное сцепление
                    if (array1 != null && array2 != null) // Проверяем, что оба массива созданы
                        FixedStringArray.Concatenate(array1, array2).PrintAll(); // Сцепляем и выводим результат
                    else
                        Console.WriteLine("Оба массива должны быть созданы!"); // Сообщение об ошибке
                    break;
                case "5": // Слияние с удалением повторений
                    if (array1 != null && array2 != null) // Проверяем, что оба массива созданы
                        FixedStringArray.MergeUnique(array1, array2).PrintAll(); // Сливаем и выводим результат
                    else
                        Console.WriteLine("Оба массива должны быть созданы!"); // Сообщение об ошибке
                    break;
                case "0": return; // Выход из программы (завершение Main)
                default: Console.WriteLine("Неверный ввод!"); break; // Если ввели не 0-5
            }
        }
    }

    // Метод для создания массива с интерактивным вводом от пользователя
    static FixedStringArray CreateArray(string name)
    {
        Console.Write($"Введите длину {name}: ");        // Запрашиваем длину массива
        int length = int.Parse(Console.ReadLine());     // Читаем строку и преобразуем в число
        FixedStringArray arr = new FixedStringArray(length); // Создаём массив указанной длины
        arr.FillFromConsole(); // Заполняем массив данными с клавиатуры
        return arr; // Возвращаем готовый массив
    }

    // Метод для вывода на экран содержимого обоих массивов
    static void ShowArrays()
    {
        Console.WriteLine("--- Массив 1 ---"); // Заголовок для первого массива
        if (array1 != null) array1.PrintAll(); // Если массив создан - выводим его содержимое
        else Console.WriteLine("Массив 1 не создан."); // Иначе сообщаем, что он не создан

        Console.WriteLine("--- Массив 2 ---"); // Заголовок для второго массива
        if (array2 != null) array2.PrintAll(); // Если массив создан - выводим его содержимое
        else Console.WriteLine("Массив 2 не создан."); // Иначе сообщаем, что он не создан
    }
}

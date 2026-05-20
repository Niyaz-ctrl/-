using System;

// Собственное исключение (аналог throw в задании)
public class NoRealRootsException : Exception
{
    public NoRealRootsException(string message) : base(message) { }
}

class QuadraticEquationSolver
{
    // Функция вычисления корней квадратного уравнения
    public static void Solve(double a, double b, double c)
    {
        if (a == 0)
            throw new ArgumentException("Коэффициент A не может быть равен 0 (это не квадратное уравнение).");

        double discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
            throw new NoRealRootsException("Дискриминант отрицателен: действительных корней нет.");

        double sqrtD = Math.Sqrt(discriminant);
        double x1 = (-b + sqrtD) / (2 * a);
        double x2 = (-b - sqrtD) / (2 * a);

        Console.WriteLine($"Корни уравнения: x1 = {x1:F4}, x2 = {x2:F4}");
    }

    // "Замена" unexpected() — вывод сообщения и завершение
    static void MyUnexpectedHandler()
    {
        Console.WriteLine("Непредвиденное исключение: отсутствует обработчик или исключение не перехвачено.");
        Environment.Exit(1);
    }

    static void Main()
    {
        // Эмуляция set_unexpected в стиле задания (в C# так не принято, но для демонстрации)
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Console.WriteLine("Unhandled exception caught:");
            MyUnexpectedHandler();
        };

        try
        {
            Console.WriteLine("Решение квадратного уравнения ax^2 + bx + c = 0");

            Console.Write("Введите a: ");
            double a = double.Parse(Console.ReadLine());

            Console.Write("Введите b: ");
            double b = double.Parse(Console.ReadLine());

            Console.Write("Введите c: ");
            double c = double.Parse(Console.ReadLine());

            Solve(a, b, c);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Ошибка формата: {ex.Message}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Арифметическое переполнение: {ex.Message}");
        }
        catch (NoRealRootsException ex)
        {
            Console.WriteLine($"Сгенерированное исключение: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка аргумента: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общее исключение: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Завершение блока обработки исключений.");
        }
    }
}
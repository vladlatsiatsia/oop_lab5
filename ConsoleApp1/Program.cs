using System;
using System.Linq;
using System.Text;

class Result
{
    protected string Subject;
    protected string Teacher;
    protected int Points;

    public Result()
    {
        Subject = "Невідомо";
        Teacher = "Невідомо";
        Points = 0;
    }

    public Result(string subject, string teacher, int points)
    {
        Subject = subject;
        Teacher = teacher;
        Points = points;
    }

    public Result(Result other)
    {
        Subject = other.Subject;
        Teacher = other.Teacher;
        Points = other.Points;
    }

    public string GetSubject() => Subject;
    public void SetSubject(string subject) => Subject = subject;

    public string GetTeacher() => Teacher;
    public void SetTeacher(string teacher) => Teacher = teacher;

    public int GetPoints() => Points;
    public void SetPoints(int points) => Points = points;
}

class Student
{
    protected string Name;
    protected string Surname;
    protected string Group;
    protected int Year;
    protected Result[] Results;

    public Student()
    {
        Name = "Невідомо";
        Surname = "Невідомо";
        Group = "XXX";
        Year = 1;
        Results = new Result[0];
    }

    public Student(string name, string surname, string group, int year, Result[] results)
    {
        Name = name;
        Surname = surname;
        Group = group;
        Year = year;
        Results = results;
    }

    public Student(Student other)
    {
        Name = other.Name;
        Surname = other.Surname;
        Group = other.Group;
        Year = other.Year;
        Results = other.Results.Select(r => new Result(r)).ToArray();
    }

    public string GetName() => Name;
    public void SetName(string name) => Name = name;

    public string GetSurname() => Surname;
    public void SetSurname(string surname) => Surname = surname;

    public string GetGroup() => Group;
    public void SetGroup(string group) => Group = group;

    public int GetYear() => Year;
    public void SetYear(int year) => Year = year;

    public Result[] GetResults() => Results;
    public void SetResults(Result[] results) => Results = results;

    public double GetAveragePoints()
    {
        if (Results.Length == 0) return 0;
        return Results.Average(r => r.GetPoints());
    }

    public string GetBestSubject()
    {
        if (Results.Length == 0) return "Немає";
        return Results.OrderByDescending(r => r.GetPoints()).First().GetSubject();
    }

    public string GetWorstSubject()
    {
        if (Results.Length == 0) return "Немає";
        return Results.OrderBy(r => r.GetPoints()).First().GetSubject();
    }
}

class Program
{
    static Student[] ReadStudentsArray()
    {
        Console.Write("Введіть кількість студентів: ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 1)
        {
            Console.WriteLine("Невірне значення. Встановлено 1.");
            n = 1;
        }

        Student[] students = new Student[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nСтудент #{i + 1}:");

            Console.Write("Ім'я: ");
            string name = Console.ReadLine().Trim();

            Console.Write("Прізвище: ");
            string surname = Console.ReadLine().Trim();

            Console.Write("Група: ");
            string group = Console.ReadLine().Trim();

            Console.Write("Курс: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Невірне значення. Встановлено 1.");
                year = 1;
            }

            Console.Write("Кількість предметів: ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m < 0)
            {
                Console.WriteLine("Невірне значення. Встановлено 0.");
                m = 0;
            }

            Result[] results = new Result[m];

            for (int j = 0; j < m; j++)
            {
                Console.WriteLine($"Предмет #{j + 1}:");

                Console.Write("Назва предмета: ");
                string subject = Console.ReadLine().Trim();

                Console.Write("Викладач: ");
                string teacher = Console.ReadLine().Trim();

                Console.Write("Бали (0-100): ");
                if (!int.TryParse(Console.ReadLine(), out int points) || points < 0 || points > 100)
                {
                    Console.WriteLine("Невірне значення. Встановлено 0.");
                    points = 0;
                }

                results[j] = new Result(subject, teacher, points);
            }

            students[i] = new Student(name, surname, group, year, results);
        }

        return students;
    }

    static void PrintStudent(Student student)
    {
        Console.WriteLine($"{student.GetSurname()} {student.GetName()}, Група: {student.GetGroup()}, Курс: {student.GetYear()}");

        foreach (var result in student.GetResults())
        {
            Console.WriteLine($"  Предмет: {result.GetSubject()}, Викладач: {result.GetTeacher()}, Бали: {result.GetPoints()}");
        }

        Console.WriteLine($"  Середній бал: {student.GetAveragePoints():F2}, Кращий предмет: {student.GetBestSubject()}, Гірший предмет: {student.GetWorstSubject()}");
    }

    static void PrintStudents(Student[] students)
    {
        foreach (var student in students)
        {
            PrintStudent(student);
            Console.WriteLine();
        }
    }

    static void GetStudentsInfo(Student[] students, out double highestAvg, out double lowestAvg)
    {
        if (students.Length == 0)
        {
            highestAvg = lowestAvg = 0;
            return;
        }

        highestAvg = students.Max(s => s.GetAveragePoints());
        lowestAvg = students.Min(s => s.GetAveragePoints());
    }

    static void SortStudentsByPoints(Student[] students)
    {
        Array.Sort(students, (a, b) => b.GetAveragePoints().CompareTo(a.GetAveragePoints()));
    }

    static void SortStudentsByName(Student[] students)
    {
        Array.Sort(students, (a, b) =>
        {
            int result = a.GetSurname().CompareTo(b.GetSurname());
            return result == 0 ? a.GetName().CompareTo(b.GetName()) : result;
        });
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Лацяця Владислав ІПЗ-24-2(2): ");
        Console.WriteLine("Лабораторна 5 Варіант 4 ");
        Console.WriteLine();

        Student[] students = ReadStudentsArray();

        Console.WriteLine("\n--- Список студентів ---");
        PrintStudents(students);

        GetStudentsInfo(students, out double maxAvg, out double minAvg);
        Console.WriteLine($"Максимальний середній бал: {maxAvg:F2}, Мінімальний: {minAvg:F2}");

        Console.WriteLine("\n--- Сортування за балами ---");
        SortStudentsByPoints(students);
        PrintStudents(students);

        Console.WriteLine("\n--- Сортування за ім’ям ---");
        SortStudentsByName(students);
        PrintStudents(students);
    }
}

using System;
using System.Text;

class Book
{
    public string Title { get; }
    public string Author { get; }
    public int Year { get; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public void Display()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Назва: ");
        Console.ResetColor();
        Console.WriteLine(Title);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Автор: ");
        Console.ResetColor();
        Console.WriteLine(Author);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Рік видання: ");
        Console.ResetColor();
        Console.WriteLine(Year);

        Console.WriteLine();
    }
}

class Program
{
    static Book[] books = new Book[0];

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Лацяця Владислав ІПЗ-24-2(2):");
        Console.WriteLine("Лабораторна 5 - Завдання з меню\n");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Меню:");
            Console.ResetColor();
            Console.WriteLine("1. Ввести книги");
            Console.WriteLine("2. Показати книги");
            Console.WriteLine("3. Сортувати за назвою");
            Console.WriteLine("4. Вихід");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    books = ReadBooks();
                    break;
                case "2":
                    ShowBooks();
                    break;
                case "3":
                    SortBooksByTitle();
                    break;
                case "4":
                    Console.WriteLine("Завершення програми...");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.\n");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static Book[] ReadBooks()
    {
        Console.Write("Скільки книг ви хочете ввести? ");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Некоректне число. Введіть ще раз: ");
            Console.ResetColor();
        }

        Book[] tempBooks = new Book[count];

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"\nКнига #{i + 1}");

            Console.Write("Назва: ");
            string title = Console.ReadLine().Trim();

            Console.Write("Автор: ");
            string author = Console.ReadLine().Trim();

            int year;
            Console.Write("Рік видання: ");
            while (!int.TryParse(Console.ReadLine(), out year) || year < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Некоректний рік. Введіть ще раз: ");
                Console.ResetColor();
            }

            tempBooks[i] = new Book(title, author, year);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nКниги успішно збережено!\n");
        Console.ResetColor();

        return tempBooks;
    }

    static void ShowBooks()
    {
        if (books.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Список книг порожній!\n");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Список книг:\n");
        Console.ResetColor();

        foreach (var book in books)
        {
            book.Display();
        }
    }

    static void SortBooksByTitle()
    {
        if (books.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Немає книг для сортування!\n");
            Console.ResetColor();
            return;
        }

        Array.Sort(books, (a, b) => a.Title.CompareTo(b.Title));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Сортування за назвою завершено!\n");
        Console.ResetColor();
    }
}

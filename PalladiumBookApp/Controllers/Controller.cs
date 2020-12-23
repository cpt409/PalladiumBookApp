using System;
using System.Collections.Generic;
using System.Text;
using PalladiumBookApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PalladiumBookApp.Controllers
{
    public class Controller
    {

        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }
        public List<Game> Games { get; set; }

        public Controller()
        {
            Books = LoadBooks();
            Categories = LoadCategories();
            Games = LoadGames();
        }

        private List<Book> LoadBooks()
        {
            using (var context = new PalladiumDBContext())
            {
                return context.Books.ToList();
            }
        }

        private List<Category> LoadCategories()
        {
            using (var context = new PalladiumDBContext())
            {
                return context.Categories.ToList();
            }
        }

        private List<Game> LoadGames()
        {
            using (var context = new PalladiumDBContext())
            {
                return context.Games.ToList();
            }
        }

        public void PrintBookList(List<Book> books)
        {
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.Id}\t{b.Name}\t{b.OwnedBool}");
            }
        }

        public void PrintCategoryList(List<Category> categories)
        {
            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.Id}\t{c.Name}");
            }
        }

        public void PrintGameList(List<Game> games)
        {
            foreach (Game g in games)
            {
                Console.WriteLine($"{g.Id}\t{g.Name}");
            }
        }

        public void AddBook(List<Book> books, List<Category> categories, List<Game> games)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nFull Book List");
            Console.ResetColor();
            PrintBookList(books);

            using (var context = new PalladiumDBContext())
            {

                // set the game id
                int gameValue = 0;
                string gameInput = string.Empty;
                PrintGameList(games);
                do
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Please enter a Game ID: ");
                    gameInput = Console.ReadLine();
                    Console.ResetColor();
                } while (!int.TryParse(gameInput, out gameValue));

                Console.WriteLine("\n\n");

                // set the category id
                int catValue = 0;
                string catInput = string.Empty;
                PrintCategoryList(categories);
                do
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Please enter a Category ID: ");
                    catInput = Console.ReadLine();
                    Console.ResetColor();
                } while (!int.TryParse(catInput, out catValue));


                string bookname = string.Empty;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Please enter name of new Book: ");
                bookname = Console.ReadLine();
                Console.ResetColor();

                context.Books.Add(new Book
                {
                    Gameid = gameValue,
                    Categoryid = catValue,
                    Name = bookname
                });

                context.SaveChanges();

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Book List has been updated!");
                Console.ResetColor();
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.ResetColor();
            }

        }

        public void AddCategory(List<Category> categories, List<Game> games)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nFull Category List");
            Console.ResetColor();

            PrintCategoryList(categories);

            using (var context = new PalladiumDBContext())
            {
                string catName = string.Empty;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Please enter name of new Category: ");
                catName = Console.ReadLine();
                Console.ResetColor();

                context.Categories.Add(new Category
                {
                    Name = catName
                });

                context.SaveChanges();

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Category List has been updated!");
                Console.ResetColor();
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        public void AddGame(List<Game> games)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nFull Game List");
            Console.ResetColor();

            PrintGameList(games);

            using (var context = new PalladiumDBContext())
            {
                int value = 0;
                string input = string.Empty;
                PrintGameList(games);

                string gameName = string.Empty;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Please enter name of new Game: ");
                gameName = Console.ReadLine();
                Console.ResetColor();

                context.Games.Add(new Game
                {
                    Name = gameName
                });

                context.SaveChanges();

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Game List has been updated!");
                Console.ResetColor();
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.ResetColor();
            }

        }

        public void UpdateBook(List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("\nFull Book List");
            PrintBookList(books);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\nEnter ID of the record that you want to update.\n\n");
            Console.ResetColor();

            bool validId = false;
            int value = 0;
            do
            {
                using (var context = new PalladiumDBContext())
                {
                    value = GetInputInt();
                    var item = context.Books.First(f => f.Id == value);

                    if (item != null)
                    {
                        Console.Write("Please enter the new name: ");
                        var newName = Console.ReadLine();
                        item.Name = newName;

                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{item.Name} has been updated!");
                        Console.ResetColor();
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        context.SaveChanges();
                        validId = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n\n{value} is an invalid selection.  Please try again.");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        validId = false;
                    }
                }
            } while (validId == false);
        }




        /// <summary>
        /// Menu Related Functions
        /// </summary>

        static void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n\nWelcome to the Palladium Book App\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Main Menu");
            Console.ResetColor();
            Console.WriteLine($"1) View Book List");
            Console.WriteLine($"2) View Category List");
            Console.WriteLine($"3) View Game List");
            Console.WriteLine($"4) Add Book");
            Console.WriteLine($"5) Add Category");
            Console.WriteLine($"6) Add Game");
            Console.WriteLine($"7) Update Book Name");
            Console.WriteLine($"8) Test Find Book");
            Console.WriteLine($"9) Exit App");
            Console.WriteLine();
        }

        public int GetInputInt()
        {
            int value = 0;
            string input = string.Empty;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Please enter a selection: ");
                input = Console.ReadLine();
                Console.ResetColor();
            } while (!int.TryParse(input, out value));

            return value;
        }

        public int GetInputInt(int min, int max)
        {
            int val = 0;
            bool valid = true;

            do
            {
                val = GetInputInt();
                if (val >= min && val <= max)
                {
                    valid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{val} is an invalid pick.\n\n");
                    Console.ResetColor();
                    valid = false;
                }

            } while (valid == false);

            return val;
        }



        public void Start()
        {
            int menuSelection;

            do
            {
                PrintMenu();
                menuSelection = GetInputInt();

                var bookList = LoadBooks();
                var categoryList = LoadCategories();
                var gameList = LoadGames();

                switch (menuSelection)
                {
                    case 1:
                        PrintBookList(bookList);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\nPress enter to go back to the main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    case 2:
                        PrintCategoryList(categoryList);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\nPress enter to go back to the main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    case 3:
                        PrintGameList(gameList);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\nPress enter to go back to the main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    case 4:
                        AddBook(bookList, categoryList, gameList);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\nPress enter to go back to the main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    case 5:
                        AddCategory(categoryList, gameList);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\nPress enter to go back to the main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    case 6:
                        AddGame(gameList);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\nPress enter to go back to the main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    case 7:
                        UpdateBook(bookList);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n\nPress enter to go back to the main menu");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                    case 8:
                    //TestFind2();
                    //Console.ForegroundColor = ConsoleColor.Cyan;
                    //Console.Write("\n\nPress enter to go back to the main menu");
                    //Console.ResetColor();
                    //Console.ReadLine();
                    //break;
                    case 9:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\nThank you for using the Palladium Book App");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n\nInvalid selection.  Please try again\n\n");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                }

            } while (menuSelection != 9);
        }
    }

}



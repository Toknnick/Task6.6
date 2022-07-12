using System;

namespace Task6._6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;
            Player player = new Player();
            Console.WriteLine(GetRandomPhrase());

            while (isWork)
            {
                Console.WriteLine("1.Купить товар. \n2.Просмотреть свой инвентарь.\n3.Покинуть лавку. \nВыберите вариант действия:");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        player.BuyProduct();
                        break;
                    case "2":
                        player.ShowInventory();
                        break;
                    case "3":
                        isWork = false;
                        break;
                    default:
                        player.WriteError();
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу:");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static string GetRandomPhrase()
        {
            Random r = new Random();
            string[] phrases = new string[4] {"Добро пожаловать, путник. Что желаешь?", "Дешевые зелья! дешевые зелья! О, путник, что желаешь?", "Только у меня ты можешь купить камень силы. Что желаешь?", "Хм... Что тебе нужно?"};
            string randomPhrase = phrases[r.Next(0,phrases.Length)];
            return randomPhrase;
        }
    }

    class Trader
    {
        public Product[] Products = new Product[]
        {
            new Product("Еда", 5),
            new Product("Вода", 1),
            new Product("Лук", 60),
            new Product("Меч", 10),
            new Product("Сумка", 25),
            new Product("Зелье", 5),
            new Product("Зелье", 5),
            new Product("Доспехи", 100),
            new Product("Стрелы х60", 80),
            new Product("Двуручный меч", 40),
            new Product("Камень силы", 500)
        };

        public void ShowProducts()
        {
            Console.Clear();

            for (int i = 0; i < Products.Length; i++)
            {
                Products[i].ShowInfo((i + 1));
            }

            Console.WriteLine();
        }
    }

    class Player
    {
        private Trader _trader = new Trader();
        private Product[] _inventory = new Product[0];
        private int _money = 500;

        public void BuyProduct()
        {
            Console.Clear();
            ChooseItem();
        }

        public void ShowInventory()
        {
            Console.Clear();

            if (_inventory.Length > 0)
            {
                for (int i = 0; i < _inventory.Length; i++)
                {
                    _inventory[i].ShowInfo((i + 1));
                }
            }
            else
            {
                Console.WriteLine("Инветарь пуст!");
            }

            Console.WriteLine("Количество золотых в сумке: " + _money);
        }

        public void WriteError()
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Введите корректные данные.");
            Console.ForegroundColor = defaultColor;
        }

        private void ChooseItem()
        {
            _trader.ShowProducts();
            Console.WriteLine("Чтобы купить товар, введите его порядковый номер:");
            string userInput = Console.ReadLine();
            CheckExistingItem(userInput);
        }

        private void CheckExistingItem(string userInput)
        {
            bool isRepeating = true;

            while (isRepeating)
            {
                if (int.TryParse(userInput, out int number))
                {
                    if (number > _trader.Products.Length || number < 0)
                    {
                        Console.WriteLine("Такого товара у продавца!");
                    }
                    else
                    {
                        number -= 1;

                        if (CheckMoney(number))
                        {
                            if (number <= _trader.Products.Length && number >= 0)
                            {
                                BuyItem(number);
                                Console.Clear();
                                Console.WriteLine("Товар успешно куплен!");
                                Console.WriteLine("У вас осталось золота:" + _money);
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    WriteError();
                }

                Console.WriteLine("Чтобы купить товар, введите его порядковый номер:");
                userInput = Console.ReadLine();
            }
        }


        private void BuyItem(int number)
        {
            _money -= _trader.Products[number].Price;
            AddAt(number);
            RemoveAt(number);
        }

        private void AddAt(int number)
        {
            Array.Resize(ref _inventory, _inventory.Length + 1);
            _inventory[_inventory.Length - 1] = _trader.Products[number];
        }

        private void RemoveAt(int number)
        {
            Product[] newArray = new Product[_trader.Products.Length - 1];

            for (int i = 0; i < number; i++)
            {
                newArray[i] = _trader.Products[i];
            }

            for (int i = number + 1; i < _trader.Products.Length; i++)
            {
                newArray[i - 1] = _trader.Products[i];
            }

            _trader.Products = newArray;
        }

        private bool CheckMoney(int number)
        {
            if (_money < _trader.Products[number].Price)
            {
                Console.WriteLine("Недостаточно золотых для покупки товара.");
                return false;
            }

            return true;
        }
    }

    class Product
    {
        private string _name;

        public int Price { get; private set; }

        public Product(string name, int price)
        {
            _name = name;
            Price = price;
        }

        public void ShowInfo(int id)
        {
            Console.WriteLine($"{id}. {_name}, стоймость: {Price}.");
        }
    }
}

using System;
using System.Collections.Generic;

namespace Task6._6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;
            Player player = new Player();
            Trader trader = new Trader();
            Console.WriteLine(GetRandomPhrase());

            while (isWork)
            {
                Console.WriteLine("1.Купить товар. \n2.Просмотреть свой инвентарь. \n3.Просмотреть лавку. \n4.Покинуть лавку. \nВыберите вариант действия:");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        trader.SellProduct(player);
                        break;
                    case "2":
                        player.ShowInventory();
                        break;
                    case "3":
                        trader.ShowProducts();
                        break;
                    case "4":
                        isWork = false;
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу:");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static string GetRandomPhrase()
        {
            Random random = new Random();
            string[] phrases = new string[4] {
                "Добро пожаловать, путник. Что желаешь?",
                "Дешевые зелья! дешевые зелья! О, путник, что желаешь?",
                "Только у меня ты можешь купить камень силы. Что желаешь?",
                "Хм... Что тебе нужно?" };
            string randomPhrase = phrases[random.Next(0, phrases.Length)];
            return randomPhrase;
        }
    }

    class Trader
    {
        private List<Product> _products = new List<Product>();

        public Trader()
        {
            AddItems();
        }

        public void ShowProducts()
        {
            Console.Clear();

            for (int i = 0; i < _products.Count; i++)
            {
                _products[i].ShowInfo((i + 1));
            }

            Console.WriteLine();
        }

        public void SellProduct(Player player)
        {
            Console.Clear();
            ShowProducts();

            if (IsExistingItem(ChooseItem(), out int number) == true)
            {
                number -= 1;

                if (CheckMoneyForBuy(number, player))
                {
                    if (number <= _products.Count && number >= 0)
                    {
                        SellItem(number, player);
                        Console.Clear();
                        Console.WriteLine("Товар успешно куплен!");
                        Console.WriteLine("У вас осталось золота:" + player.Money);
                    }
                }
            }
        }

        private void AddItems()
        {
            _products.Add(new Product("Еда", 5));
            _products.Add(new Product("Вода", 1));
            _products.Add(new Product("Лук", 60));
            _products.Add(new Product("Меч", 10));
            _products.Add(new Product("Сумка", 25));
            _products.Add(new Product("Зелье", 5));
            _products.Add(new Product("Зелье", 5));
            _products.Add(new Product("Доспехи", 100));
            _products.Add(new Product("Стрелы х60", 80));
            _products.Add(new Product("Двуручный меч", 40));
            _products.Add(new Product("Камень силы", 500));
        }

        private string ChooseItem()
        {
            Console.WriteLine("Чтобы купить товар, введите его порядковый номер:");
            string userInput = Console.ReadLine();
            return userInput;
        }

        private bool IsExistingItem(string userInput, out int number)
        {
            bool isFound = false;

            if (int.TryParse(userInput, out number))
            {
                if (number > _products.Count || number < 0)
                {
                    Console.WriteLine("Такого товара у продавца!");
                }
                else
                {
                    isFound = true;
                }
            }
            else
            {
                Console.WriteLine("Данные некорректны");
            }

            return isFound;
        }

        private void SellItem(int number, Player player)
        {
            player.TakeOffMoney(_products, number);
            player.AddInventoryItem(_products, number);
            _products.RemoveAt(number);
        }
        private bool CheckMoneyForBuy(int number, Player player)
        {
            if (player.Money < _products[number].Price)
            {
                Console.WriteLine("Недостаточно золотых для покупки товара.");
                return false;
            }

            return true;
        }
    }

    class Player
    {
        private List<Product> _inventory = new List<Product>();

        public int Money { get; private set; }

        public Player()
        {
            Money = 500;
        }

        public void ShowInventory()
        {
            Console.Clear();

            if (_inventory.Count > 0)
            {
                for (int i = 0; i < _inventory.Count; i++)
                {
                    _inventory[i].ShowInfo((i + 1));
                }
            }
            else
            {
                Console.WriteLine("Инветарь пуст!");
            }

            Console.WriteLine("Количество золотых в сумке: " + Money);
        }

        public void AddInventoryItem(List<Product> _products, int number)
        {
            _inventory.Add(_products[number]);
        }

        public void TakeOffMoney(List<Product> items, int number)
        {
            Money -= items[number].Price;
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

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

    class Trader : Person
    {
        public Trader()
        {
            AddItems();
        }

        public void SellProduct(Player player)
        {
            Console.Clear();

            if (IsExistingItem(ChooseItem(), out int number) == true)
            {
                number -= 1;

                if (IsMoneyForBuy(number, player))
                {
                    if (number <= Inventory.Count && number >= 0)
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
            Inventory.Add(new Product("Еда", 5));
            Inventory.Add(new Product("Вода", 1));
            Inventory.Add(new Product("Лук", 60));
            Inventory.Add(new Product("Меч", 10));
            Inventory.Add(new Product("Сумка", 25));
            Inventory.Add(new Product("Зелье", 5));
            Inventory.Add(new Product("Зелье", 5));
            Inventory.Add(new Product("Доспехи", 100));
            Inventory.Add(new Product("Стрелы х60", 80));
            Inventory.Add(new Product("Двуручный меч", 40));
            Inventory.Add(new Product("Камень силы", 500));
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
                if (number > Inventory.Count || number < 0)
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
            player.BuyItem(Inventory, number);
            Inventory.RemoveAt(number);
        }

        private bool IsMoneyForBuy(int number, Player player)
        {
            if (player.Money < Inventory[number].Price)
            {
                Console.WriteLine("Недостаточно золотых для покупки товара.");
                return false;
            }

            return true;
        }
    }

    class Player : Person
    {
        public int Money { get; private set; }

        public Player()
        {
            Money = 500;
        }

        public void ShowInventory()
        {
            Console.Clear();

            if (Inventory.Count > 0)
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Inventory[i].ShowInfo((i + 1));
                }
            }
            else
            {
                Console.WriteLine("Инветарь пуст!");
            }

            Console.WriteLine("Количество золотых в сумке: " + Money);
        }

        public void BuyItem(List<Product> _products, int number)
        {
            Inventory.Add(_products[number]);
            Money -= _products[number].Price;
        }
    }

    class Person
    {
        protected List<Product> Inventory = new List<Product>();

        public void ShowProducts()
        {
            Console.Clear();

            for (int i = 0; i < Inventory.Count; i++)
            {
                Inventory[i].ShowInfo((i + 1));
            }

            Console.WriteLine();
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

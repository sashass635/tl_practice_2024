using System;
using System.Xml.Linq;

namespace OrderManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string product, name, address;
            int count;

            dataRequest(out product, out count, out name, out address);
            if (orderСonfirmation(product, count, name, address))
            {
                successfulMessage(product, count, name, address);
            }
            else
            {
                Console.WriteLine("Заказ отменен. Попробуйте снова.");
            }
        }
        static string orderDetails(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Поле не может быть пустым. Попробуйте снова.");
                Console.Write(message);
                input = Console.ReadLine();
            }
            return input;
        }

        static int productsAmount(string message)
        {
            Console.Write(message);
            string number = Console.ReadLine();
            int count;
            while (!int.TryParse(number, out count) || count <= 0)
            {
                Console.WriteLine("Неверные данные. Попробуйте снова.");
                Console.Write(message);
                number = Console.ReadLine();
            }
            return count;
        }

        static void dataRequest(out string product, out int count, out string name, out string address)
        {
            product = orderDetails("Название товара: ");
            count = productsAmount("Количество товара: ");
            name = orderDetails("Имя пользователя: ");
            address = orderDetails("Адрес доставки: ");
        }

        static bool orderСonfirmation(string product, int count, string name, string address)
        {
            Console.WriteLine($"Здравствуйте, {name}, вы заказали {count} {product} на адрес {address}, все верно? (да/нет)");
            string confirmation = Console.ReadLine().ToLower();
            return (confirmation == "да");
        }

        static void successfulMessage(string product, int count, string name, string address)
        {
            DateTime deliveryDate = DateTime.Today.AddDays(3);
            Console.WriteLine($"{name}! Ваш заказ {product} в количестве {count} оформлен! Ожидайте доставку по адресу {address} к {deliveryDate.ToShortDateString()}.");
        }
    }
}

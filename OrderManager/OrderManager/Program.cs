namespace OrderManager
{
    class Program
    {
        static void Main( string[] args )
        {
            string product = ReadUserInput( "Название товара: " );
            int count = ReadProductCount( "Количество товара: " );
            string name = ReadUserInput( "Имя пользователя: " );
            string address = ReadUserInput( "Адрес доставки: " );

            if ( ConfirmOrder( product, count, name, address ) )
            {
                ShowSuccessfulMessage( product, count, name, address );
            }
            else
            {
                Console.WriteLine( "Заказ отменен. Попробуйте снова." );
            }
        }

        static string ReadUserInput( string message )
        {
            Console.Write( message );
            string input = Console.ReadLine();
            while ( string.IsNullOrEmpty( input ) )
            {
                Console.WriteLine( "Поле не может быть пустым. Попробуйте снова." );
                Console.Write( message );
                input = Console.ReadLine();
            }
            return input;
        }

        static int ReadProductCount( string message )
        {
            Console.Write( message );
            string number = Console.ReadLine();
            int count;
            while ( !int.TryParse( number, out count ) || count <= 0 )
            {
                Console.WriteLine( "Неверные данные. Попробуйте снова." );
                Console.Write( message );
                number = Console.ReadLine();
            }
            return count;
        }

        static bool ConfirmOrder( string product, int count, string name, string address )
        {
            Console.WriteLine( $"Здравствуйте, {name}, вы заказали {count} {product} на адрес {address}, все верно? (да/нет)" );
            string confirmation = Console.ReadLine().ToLower();
            return ( confirmation == "да" );
        }

        static void ShowSuccessfulMessage( string product, int count, string name, string address )
        {
            DateTime deliveryDate = DateTime.Today.AddDays( 3 );
            Console.WriteLine( $"{name}! Ваш заказ {product} в количестве {count} оформлен! Ожидайте доставку по адресу {address} к {deliveryDate.ToShortDateString()}." );
        }
    }
}

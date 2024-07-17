namespace Dictionary
{
    class Program
    {
        const string DictionaryFilePath = "dictionary.txt";
        const string YesResponse = "да";
        static Dictionary<string, string> dictionary = new Dictionary<string, string>();

        static void Main( string[] args )
        {
            int choice;

            LoadDictionary();

            do
            {
                PrintMenu();

                Console.WriteLine( "Выберите команду: " );
                string input = Console.ReadLine();

                if ( !int.TryParse( input, out choice ) )
                {
                    Console.WriteLine( "Команда должна быть целым числом. Повторите ввод.\n" );
                    continue;
                }

                switch ( choice )
                {
                    case 1:
                        InsertWord();
                        break;
                    case 2:
                        PrintDictionary();
                        break;
                    case 3:
                        TranslateWord();
                        break;
                    case 4:
                        ClearDictionary();
                        break;
                    case 5:
                        Console.WriteLine( "Программа завершена." );
                        break;
                    default:
                        HandleInvalidInput();
                        break;
                }
            } while ( choice != 5 );
        }

        static void LoadDictionary()
        {
            try
            {
                if ( File.Exists( DictionaryFilePath ) )
                {
                    string[] lines = File.ReadAllLines( DictionaryFilePath );
                    foreach ( string line in lines )
                    {
                        string[] parts = line.Split( ':' );
                        if ( parts.Length == 2 )
                        {
                            dictionary[ parts[ 0 ].Trim() ] = parts[ 1 ].Trim();
                        }
                    }
                }
            }
            catch ( Exception message )
            {
                Console.WriteLine( $"Произошла ошибка: {message.Message}" );
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine( "1 - Добавление слова" );
            Console.WriteLine( "2 - Печать словаря" );
            Console.WriteLine( "3 - Перевод слова" );
            Console.WriteLine( "4 - Удаление словаря" );
            Console.WriteLine( "5 - Выход" );
        }

        static void InsertWord()
        {
            Console.Write( "Введите слово:" );
            string word = Console.ReadLine();

            Console.Write( "Введите перевод:" );
            string translation = Console.ReadLine();

            if ( !dictionary.ContainsKey( word ) && !dictionary.ContainsKey( translation ) )
            {
                dictionary[ word ] = translation;
                dictionary[ translation ] = word;
                SaveToFile();
                Console.WriteLine( "Слово добавлено в словарь." );
            }
            else
            {
                Console.WriteLine( "Слово уже существует. Хотите заменить перевод? (да/нет)" );
                if ( Console.ReadLine().ToLower() == YesResponse )
                {
                    if ( dictionary.ContainsKey( word ) )
                    {
                        dictionary[ word ] = translation;
                    }
                    if ( dictionary.ContainsKey( translation ) )
                    {
                        dictionary[ translation ] = word;
                    }
                    SaveToFile();
                    Console.WriteLine( "Перевод заменен." );
                }
            }
        }

        static void PrintDictionary()
        {
            if ( dictionary.Count == 0 )
            {
                Console.WriteLine( "Словарь пуст." );
            }
            else
            {
                foreach ( KeyValuePair<string, string> word in dictionary )
                {
                    Console.WriteLine( $"{word.Key}: {word.Value}" );
                }
            }
        }

        static void TranslateWord()
        {
            Console.Write( "Введите слово для перевода: " );
            string word = Console.ReadLine().ToLower();

            if ( dictionary.ContainsKey( word ) )
            {
                Console.WriteLine( $"Перевод: {dictionary[ word ]}" );
            }
            else
            {
                Console.WriteLine( "Слово не найдено в словаре. Хотите добавить новое слово? (да/нет)" );
                if ( Console.ReadLine().ToLower() == YesResponse )
                {
                    InsertWord();
                }
            }
        }

        static void ClearDictionary()
        {
            dictionary.Clear();
            SaveToFile();
            Console.WriteLine( "Словарь пуст." );
        }

        static void SaveToFile()
        {
            try
            {
                using ( StreamWriter writer = new StreamWriter( DictionaryFilePath ) )
                {
                    foreach ( KeyValuePair<string, string> word in dictionary )
                    {
                        writer.WriteLine( $"{word.Key}:{word.Value}" );
                    }
                }
            }
            catch ( Exception message )
            {
                Console.WriteLine( $"Ошибка при записи в файл: {message.Message}" );
            }
        }

        static void HandleInvalidInput()
        {
            Console.WriteLine( "Неверная команда. Повторите ввод.\n" );
        }
    }
}

using CarFactory.Models.Cars;

namespace CarFactory
{
    public class CarManager
    {
        private List<ICar> cars = new List<ICar>();

        public void Run()
        {
            PrintMenu();

            int choice;

            do
            {
                Console.Write( "\nВыберите команду: " );
                string input = Console.ReadLine();

                if ( !int.TryParse( input, out choice ) )
                {
                    Console.WriteLine( "Команда должна быть целым числом. Повторите ввод.\n" );
                    continue;
                }

                switch ( choice )
                {
                    case 1:
                        Car newCar = Models.CarFactory.CarFactory.CreateNewCar();
                        CreateNewCar( newCar );
                        break;
                    case 2:
                        ShowAllCar();
                        break;
                    case 3:
                        DeleteCar();
                        break;
                    case 4:
                        Console.WriteLine( "Программа завершена." );
                        break;
                    default:
                        HandleInvalidInput();
                        break;
                }
            } while ( choice != 4 );
        }

        private static void HandleInvalidInput()
        {
            Console.WriteLine( "Неверная команда. Повторите ввод.\n" );
        }

        private static void PrintMenu()
        {
            Console.WriteLine( "1 - Cоздать автомобиль " );
            Console.WriteLine( "2 - Печать всех автомобилей" );
            Console.WriteLine( "3 - Удалить автомобиль" );
            Console.WriteLine( "4 - Выход" );
        }

        private void CreateNewCar( ICar car )
        {
            if ( cars.Any( existCar => existCar.Name == car.Name ) )
            {
                Console.WriteLine( $"Автомобиль с названием '{car.Name}' уже существует. Попробуйте другое название." );
                return;
            }

            cars.Add( car );
            Console.WriteLine( "Машина создана." );
        }

        private void ShowAllCar()
        {
            if ( cars.Count == 0 )
            {
                Console.WriteLine( "Нет созданных автомобилей." );
            }
            else
            {
                foreach ( ICar car in cars )
                {
                    car.ShowCarConfigurations();
                }
            }
        }

        private void DeleteCar()
        {
            Console.Write( "Введите название автомобиля: " );
            string name = Console.ReadLine();
            ICar carToDelete = cars.FirstOrDefault( car => car.Name == name );

            if ( carToDelete != null )
            {
                cars.Remove( carToDelete );
                Console.WriteLine( $"Автомобиль '{name}'  удален." );
            }
            else
            {
                Console.WriteLine( $"Автомобиль с названием '{name}' не найден." );
            }
        }
    }
}
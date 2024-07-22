using CarFactory.Models.BodyShape;
using CarFactory.Models.CarColor;
using CarFactory.Models.Engine;
using CarFactory.Models.Gearboxes;
using CarFactory.Models.SteeringPosition;
using CarFactory.Models.Cars;

namespace CarFactory.Models.CarProfile
{
    public class CarProfile
    {
        public static Car CreateNewCar()
        {
            Console.Write( "Введите название автомобиля: " );
            string carName = Console.ReadLine();

            SetOfCharacteristics characteristics = new SetOfCharacteristics();

            IBodyShape bodyShape = SelectOption( "Выберите тип кузова:", characteristics.GetBodyShape() );
            ICarColor carColor = SelectOption( "Выберите цвет кузова:", characteristics.GetCarColor() );
            IEngine carEngine = SelectOption( "Выберите тип двигателя:", characteristics.GetEngine() );
            IGearboxes carGearbox = SelectOption( "Выберите тип коробки передач:", characteristics.GetGearboxes() );
            ISteeringPosition steeringPosition = SelectOption( "Выберите позицию руля:", characteristics.GetSteeringPositione() );

            return new Car( carName, bodyShape, carColor, carEngine, carGearbox, steeringPosition );
        }

        public static T SelectOption<T>( string message, List<T> options )
        {
            Console.WriteLine( message );
            for ( int i = 0; i < options.Count; i++ )
            {
                Console.WriteLine( $"{i} - {options[ i ].GetType().Name}" );
            }

            int choice = GetValidChoice( "Выберите вариант:", options.Count );
            return options[ choice ];
        }

        public static int GetValidChoice( string v, int maxChoice )
        {
            while ( true )
            {
                Console.Write( v );
                if ( int.TryParse( Console.ReadLine(), out int choice ) && choice >= 0 && choice < maxChoice )
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine( $"Введите корректное число от 0 до {maxChoice - 1}." );
                }
            }
        }
    }
}
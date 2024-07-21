using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterType;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.FightersProfile
{
    public class FightersProfile
    {
        public static Fighter AddNewFigher()
        {
            Console.Write( "Введите имя персонажа: " );
            string fighterName = Console.ReadLine();

            SetOfCharacteristics characteristics = new SetOfCharacteristics();

            IRace fighterRace = SelectOption( "Выберите расу:", characteristics.GetRaces() );
            IWeapon fighterWeapon = SelectOption( "Выберите оружие:", characteristics.GetWeapons() );
            IArmor fighterArmor = SelectOption( "Выберите броню:", characteristics.GetArmors() );
            IFighterType fighterType = SelectOption( "Выберите тип бойца:", characteristics.GetFighterTypes() );

            return new Fighter( fighterName, fighterRace, fighterWeapon, fighterArmor, fighterType );
        }

        private static T SelectOption<T>( string message, List<T> options )
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
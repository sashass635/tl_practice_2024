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

            List<IRace> race = new() { new Human(), new Orc() };
            IRace fighterRace = SelectOption( "Выберите расу:", race );

            List<IWeapon> weapon = new() { new Fists(), new Sword() };
            IWeapon fighterWeapon = SelectOption( "Выберите оружие:", weapon );

            List<IArmor> armor = new() { new NoArmor(), new IronArmor() };
            IArmor fighterArmor = SelectOption( "Выберите броню:", armor );

            List<IFighterType> type = new() { new Knight(), new Mercenary() };
            IFighterType fighterType = SelectOption( "Выберете бойца: ", type );

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

using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.FighterType;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.FighterFactory
{
    public class FighterFactory : FighterFactoryValidChoice
    {
        public static Fighter AddNewFigher()
        {
            Console.Write( "Введите имя персонажа: " );
            string fighterName = Console.ReadLine();

            FightersConfiguration characteristics = new FightersConfiguration();

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
    }
}
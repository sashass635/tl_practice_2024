using Fighters.Models.Armors;
using Fighters.Models.FighterType;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using static System.Net.Mime.MediaTypeNames;

namespace Fighters.Models.Fighters
{
    public class Fighter : IFighter
    {
        private IRace _race;
        private IArmor _armor;
        private IWeapon _weapon;
        private IFighterType _fighterType;

        private static Random rnd = new Random();
        private static readonly double MinMultiplierDamage = 0.80;
        private static readonly double MaxMultiplierDamage = 1.10;
        private static readonly int CriticalPercentChance = 10;

        private int _currentHealth;

        public string Name { get; private set; }
        public int Initiative { get; private set; }

        public Fighter( string name, IRace race, IWeapon weapon, IArmor armor, IFighterType fighterType )
        {
            Name = name;
            _race = race;
            _armor = armor;
            _weapon = weapon;
            _fighterType = fighterType;
            _currentHealth = _race.Health + _fighterType.Health;
            Initiative = CalculateInitiative();
        }

        public int CalculateArmor() => _armor.Armor + _race.Armor;

        public int GetCurrentHealth() => _currentHealth;

        public int TakeDamage( int damage )
        {
            int startHealth = GetCurrentHealth();
            int newHealth = _currentHealth - Math.Max( damage - CalculateArmor(), 0 );

            if ( newHealth < 0 )
            {
                newHealth = 0;
            }
            _currentHealth = newHealth;
            int damageDone = startHealth - _currentHealth;

            return damageDone;
        }

        public int CalculateDamage()
        {
            int originalDamage = _race.Damage + _fighterType.Damage + _weapon.Damage;
            double attackMultiplier = rnd.Next( ( int )( MinMultiplierDamage * 100 ), ( int )( MaxMultiplierDamage * 100 + 1 ) ) / 100.0;
            int modifiedDamage = ( int )( originalDamage * attackMultiplier );
            bool isCriticialDamage = rnd.Next( 1, 101 ) < CriticalPercentChance;

            if ( isCriticialDamage )
            {
                modifiedDamage *= 2;
            }

            return modifiedDamage;
        }

        private int CalculateInitiative() => _fighterType.Initiative + rnd.Next( 1, 16 );

    }
}
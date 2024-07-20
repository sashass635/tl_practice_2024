using Fighters.Models.Armors;
using Fighters.Models.FighterType;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Tests
{
    [TestFixture]
    public class FighterTests
    {

        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange
            TestRace race = new TestRace();
            TestWeapon weapon = new TestWeapon();
            TestArmor armor = new TestArmor();
            TestFighterType fighterType = new TestFighterType();

            // Act
            Fighter fighter = new Fighter( "Fighter 1", race, weapon, armor, fighterType );

            // Assert
            Assert.AreEqual( "Fighter 1", fighter.Name );
            Assert.AreEqual( race.Health + fighterType.Health, fighter.GetCurrentHealth() );
        }

        [Test]
        public void TakeDamage_ShouldReduceHealthCorrectly()
        {
            // Arrange
            TestRace race = new TestRace();
            TestWeapon weapon = new TestWeapon();
            TestArmor armor = new TestArmor();
            TestFighterType fighterType = new TestFighterType();
            Fighter fighter = new Fighter( "Fighter 1", race, weapon, armor, fighterType );

            int initialHealth = fighter.GetCurrentHealth();

            int damage = 15;

            // Act
            int damageTaken = fighter.TakeDamage( damage );

            // Assert
            int expectedDamage = damage - ( armor.Armor + race.Armor );
            Assert.AreEqual( expectedDamage, damageTaken );
            Assert.AreEqual( initialHealth - expectedDamage, fighter.GetCurrentHealth() );
        }

        [Test]
        public void CalculateDamage_ShouldReturnCorrectDamage()
        {
            // Arrange
            TestRace race = new TestRace();
            TestWeapon weapon = new TestWeapon();
            TestArmor armor = new TestArmor();
            TestFighterType fighterType = new TestFighterType();
            Fighter fighter = new Fighter( "Fighter 1", race, weapon, armor, fighterType );

            // Act
            int damage = fighter.CalculateDamage();

            // Assert
            int minExpectedDamage = ( int )( ( race.Damage + fighterType.Damage + weapon.Damage ) * 0.80 );
            int maxExpectedDamage = ( int )( ( race.Damage + fighterType.Damage + weapon.Damage ) * 2.20 );
            Assert.GreaterOrEqual( damage, minExpectedDamage );
            Assert.LessOrEqual( damage, maxExpectedDamage );
        }
    }

    public class TestRace : IRace
    {
        public string Name => "Test Race";
        public int Damage => 10;
        public int Health => 100;
        public int Armor => 5;
    }

    public class TestWeapon : IWeapon
    {
        public string Name => "Test Weapon";
        public int Damage => 15;
    }

    public class TestArmor : IArmor
    {
        public string Name => "Test Armor";
        public int Armor => 10;
    }

    public class TestFighterType : IFighterType
    {
        public string Name => "Test Fighter Type";
        public int Damage => 20;
        public int Health => 50;
        public int Initiative => 10;
    }

}

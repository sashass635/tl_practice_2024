namespace Fighters.Models.Fighters
{
    public interface IFighter
    {
        public string Name { get; }
        public int Initiative { get; }

        public int CalculateArmor();

        public int GetCurrentHealth();

        public int TakeDamage( int damage );

        public int CalculateDamage();
    }
}
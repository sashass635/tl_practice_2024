namespace Fighters.Models.FighterType
{
    public interface IFighterType
    {
        public string Name { get; }
        public int Health { get; }
        public int Damage { get; }
        public int Initiative { get; }
    }
}
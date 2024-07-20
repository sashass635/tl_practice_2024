namespace Fighters.Models.FighterType
{
    public interface IFighterType
    {
        string Name { get; }
        int Health { get; }
        int Damage { get; }
        int Initiative { get; }
    }
}

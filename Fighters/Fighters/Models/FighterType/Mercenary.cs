namespace Fighters.Models.FighterType
{
    public class Mercenary : IFighterType
    {
        public string Name => "Наемник";
        public int Health => 35;
        public int Damage => 15;
        public int Initiative => 10;
    }
}
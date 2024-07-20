namespace Fighters.Models.FighterType
{
    public class Knight : IFighterType
    {
        public string Name => "Рыцарь";
        public int Health => 50;
        public int Damage => 20;
        public int Initiative { get; } = 5;
    }
}

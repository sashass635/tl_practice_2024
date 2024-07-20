namespace Fighters.Models.Races
{
    public class Orc : IRace
    {
        public string Name => "Orc";
        public int Damage => 2;
        public int Health => 100;
        public int Armor => 10;
    }
}

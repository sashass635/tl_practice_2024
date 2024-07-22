using Fighters.Models.Armors;
using Fighters.Models.FighterType;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.FightersProfile
{
    public class SetOfCharacteristics
    {
        private List<IRace> _races;
        private List<IWeapon> _weapons;
        private List<IArmor> _armors;
        private List<IFighterType> _fighterTypes;

        public SetOfCharacteristics()
        {
            _races = new List<IRace> { new Human(), new Orc() };
            _weapons = new List<IWeapon> { new Fists(), new Sword() };
            _armors = new List<IArmor> { new NoArmor(), new IronArmor() };
            _fighterTypes = new List<IFighterType> { new Knight(), new Mercenary() };
        }

        public List<IRace> GetRaces() => _races;
        public List<IWeapon> GetWeapons() => _weapons;
        public List<IArmor> GetArmors() => _armors;
        public List<IFighterType> GetFighterTypes() => _fighterTypes;
    }
}
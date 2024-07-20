using Fighters.Models.Fighters;

namespace Fighters.Extensions
{
    public static class IFighterExtensions
    {
        public static bool IsAlive( this IFighter fighter ) => fighter.GetCurrentHealth() > 0;

        public static bool IsCorrect( this List<IFighter> fighters )
        {
            if ( fighters.Count < 2 )
            {
                Console.WriteLine( "Недостаточно бойцов для начала боя." );
                return false;
            }
            return true;
        }
    }
}
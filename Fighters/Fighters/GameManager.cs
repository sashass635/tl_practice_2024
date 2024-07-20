using Fighters.Extensions;
using Fighters.Models.Fighters;
using Fighters.Models.FightersProfile;

namespace Fighters
{
    public class GameManager
    {
        private List<IFighter> fighters = new List<IFighter>();
        private int round = 1;

        public void Run()
        {
            string choice;

            PrintMenu();

            do
            {
                Console.Write( "\nВыберите команду: " );

                choice = Console.ReadLine().ToLower();
                switch ( choice )
                {
                    case "add-fighter":
                        Fighter fighter = FightersProfile.AddNewFigher();
                        AddFighter( fighter );
                        break;

                    case "play":
                        Play();
                        break;

                    case "fighters":
                        ShowFighters();
                        break;

                    case "delete":
                        fighters.Clear();
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine( "Неизвестная команда." );
                        break;
                }
            } while ( choice != "exit" );
        }

        public void PrintMenu()
        {
            Console.WriteLine( "Доступные команды:" );
            Console.WriteLine( "add-fighter - Добавить нового бойца на арену" );
            Console.WriteLine( "play - Начать битву" );
            Console.WriteLine( "fighters - Показать список бойцов" );
            Console.WriteLine( "delete - Удаление бойцов с арены" );
            Console.WriteLine( "exit - Выйти" );
        }

        public void AddFighter( IFighter fighter )
        {
            fighters.Add( fighter );
            Console.WriteLine( "Боец добавлен." );
        }

        public void Play()
        {
            if ( !fighters.IsCorrect() )
            {
                return;
            }

            List<IFighter> sortedFighters = fighters.OrderByDescending( f => f.Initiative ).ToList();

            while ( fighters.Count() > 1 )
            {
                Console.WriteLine( $"\nРаунд {round++}" );

                for ( int i = 0; i < sortedFighters.Count(); i++ )
                {
                    IFighter firstFighter = sortedFighters[ i ];
                    if ( !firstFighter.IsAlive() ) continue;

                    IFighter secondFighter = sortedFighters.FirstOrDefault( f => f != firstFighter && f.IsAlive() );

                    int damageDone = firstFighter.CalculateDamage();
                    int damageTaken = secondFighter.TakeDamage( damageDone );
                    Console.WriteLine( firstFighter.GetDamageInformation( secondFighter, damageTaken ) );

                    if ( !secondFighter.IsAlive() )
                    {
                        Console.WriteLine( $"{secondFighter.Name} погибает." );
                        fighters.Remove( secondFighter );
                    }
                }
            }
            if ( fighters.Count == 1 )
            {
                IFighter winner = sortedFighters.FirstOrDefault( f => f.IsAlive() );
                Console.WriteLine( $"Победитель: {winner.Name}" );
            }
            else
            {
                Console.WriteLine( "Все бойцы погибли. Ничья." );
            }
        }

        public void ShowFighters()
        {
            if ( fighters.Count == 0 )
            {
                Console.WriteLine( "Список бойцов пуст." );
                return;
            }
            else
            {
                foreach ( IFighter fighter in fighters )
                {
                    Console.WriteLine( $"Имя: {fighter.Name}" );
                    Console.WriteLine( $"Здоровье: {fighter.GetCurrentHealth()}" );
                    Console.WriteLine( $"Инициатива: {fighter.Initiative}" );
                }
            }
        }

        public List<IFighter> GetFighters()
        {
            return fighters;
        }
    }
}

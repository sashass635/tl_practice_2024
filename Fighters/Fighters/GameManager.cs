using Fighters.Extensions;
using Fighters.Models.FighterFactory;
using Fighters.Models.Fighters;

namespace Fighters
{
    public class GameManager
    {
        private List<IFighter> fighters = new List<IFighter>();
        private int round = 1;

        public void Run()
        {
            PrintMenu();

            string choice;

            do
            {
                Console.Write( "\nВыберите команду: " );

                choice = Console.ReadLine().ToLower().Trim();
                switch ( choice )
                {
                    case "add-fighter":
                        Fighter fighter = FighterFactory.AddNewFigher();
                        AddFighter( fighter );
                        break;
                    case "play":
                        Play();
                        break;
                    case "list":
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

        private void PrintMenu()
        {
            Console.WriteLine( "Доступные команды:" );
            Console.WriteLine( "add-fighter - Добавить нового бойца на арену" );
            Console.WriteLine( "play - Начать битву" );
            Console.WriteLine( "list - Показать список бойцов" );
            Console.WriteLine( "delete - Удаление бойцов с арены" );
            Console.WriteLine( "exit - Выйти" );
        }

        private void AddFighter( IFighter fighter )
        {
            fighters.Add( fighter );
            Console.WriteLine( "Боец добавлен." );
        }

        private void Play()
        {
            if ( !HasEnoughFighters() )
            {
                Console.WriteLine( "Недостаточно бойцов для начала боя." );
                return;
            }

            List<IFighter> sortedFighters = fighters.OrderByDescending( f => f.Initiative ).ToList();

            while ( fighters.Count() > 1 )
            {
                Console.WriteLine( $"\nРаунд {round++}" );

                for ( int i = 0; i < sortedFighters.Count(); i++ )
                {
                    IFighter firstFighter = sortedFighters[ i ];
                    if ( !firstFighter.IsAlive() )
                    {
                        continue;
                    }

                    IFighter secondFighter = sortedFighters.FirstOrDefault( f => f != firstFighter && f.IsAlive() );

                    int damageDone = firstFighter.CalculateDamage();
                    int damageTaken = secondFighter.TakeDamage( damageDone );
                    Console.WriteLine( $"{firstFighter.Name} наносит {damageDone} урона {secondFighter.Name}. {secondFighter.Name} теперь имеет {secondFighter.GetCurrentHealth()} здоровья." );

                    if ( !secondFighter.IsAlive() )
                    {
                        Console.WriteLine( $"{secondFighter.Name} погибает." );
                        fighters.Remove( secondFighter );
                        sortedFighters = fighters.Where( f => f.IsAlive() ).OrderByDescending( f => f.Initiative ).ToList();
                    }
                }
            }

            IFighter winner = fighters.FirstOrDefault( f => f.IsAlive() );
            Console.WriteLine( $"Победитель: {winner.Name}" );
        }

        private bool HasEnoughFighters()
        {
            if ( fighters.Count < 2 )
            {
                return false;
            }

            return true;
        }

        private void ShowFighters()
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
                    Console.WriteLine( $"Имя: {fighter.Name}, Здоровье: {fighter.GetCurrentHealth()}, Инициатива: {fighter.Initiative}" );
                }
            }
        }

        private List<IFighter> GetFighters()
        {
            return fighters;
        }
    }
}
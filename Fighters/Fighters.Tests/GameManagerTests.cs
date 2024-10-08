﻿using Fighters.Models.Fighters;
using FluentAssertions;

namespace Fighters.Tests
{
    [TestFixture]
    public class GameManagerTests
    {
        [Test]
        public void Play_NotEnoughFighters_ShouldNotStart()
        {
            // Arrange
            GameManager gameManager = new GameManager();
            Fighter fighter = new Fighter( "Fighter 1", new TestRace(), new TestWeapon(), new TestArmor(), new TestFighterType() );

            gameManager.AddFighter( fighter );

            StringWriter stringWriter = new StringWriter();
            Console.SetOut( stringWriter );

            // Act
            gameManager.Play();

            // Assert
            string output = stringWriter.ToString();
            output.Should().Be( "Недостаточно бойцов для начала боя.\r\n" );
        }


        [Test]
        public void Play_ShouldDetermineOneWinner()
        {
            // Arrange
            GameManager gameManager = new GameManager();
            Fighter fighter1 = new Fighter( "Fighter 1", new TestRace(), new TestWeapon(), new TestArmor(), new TestFighterType() );
            Fighter fighter2 = new Fighter( "Fighter 2", new TestRace(), new TestWeapon(), new TestArmor(), new TestFighterType() );

            gameManager.AddFighter( fighter1 );
            gameManager.AddFighter( fighter2 );

            // Act
            gameManager.Play();

            // Assert
            List<IFighter> winner = gameManager.GetFighters();
            winner.Should().NotBeNull();
            winner.Count.Should().Be( 1 ); ;
        }
    }
}
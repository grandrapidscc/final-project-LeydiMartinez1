using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    internal class Dungeon : IGameObject
    {
        public string Name => "Dungeon";
        private IGameObject[,] playArea;
        private readonly Wizert player;
        private Location playerLocation;
        private readonly Location exitLocation;

        private const int NUM_ENEMIES = 5;
        private const int NUM_POWERUPS = 5;

        private const int NUM_PLAYAREA_ROWS = 5;
        private const int NUM_PLAYAREA_COLS = 5;

        private readonly Random random = new Random();

        public enum MoveDirection
        {
            NORTH,EAST,SOUTH,WEST
        }

        public Dungeon()
        {
            // Play area setup
            playArea = new IGameObject[NUM_PLAYAREA_ROWS, NUM_PLAYAREA_COLS];

            // Player initialisation
            player = new Wizert();
            playerLocation = new Location(random.Next(NUM_PLAYAREA_ROWS), random.Next(NUM_PLAYAREA_COLS));

            // Find approriate Exit location
            while (true) {
                int exitRowLocation = random.Next(NUM_PLAYAREA_ROWS);
                int exitColLocation = random.Next(NUM_PLAYAREA_COLS);

                if (playerLocation.Row != exitRowLocation && playerLocation.Col != exitColLocation)
                {
                    exitLocation = new Location(exitRowLocation, exitColLocation);
                    break;
                }
            }

            // Populate the rest of the play area
            InitEnemies();
            InitPowerUps();
        }

        private void InitEnemies()
        {
            // Place enemies in random areas
            for (int i = 0; i < NUM_ENEMIES; i++)
            {
                int randomRow = random.Next(NUM_PLAYAREA_ROWS);
                int randomCol = random.Next(NUM_PLAYAREA_COLS);
                int randomEnemyType = random.Next(3);

                // Repeat the same loop if the location is already occupied
                if (playArea[randomRow, randomCol] != null || (playerLocation.Row == randomRow && playerLocation.Col == randomCol) || (exitLocation.Row == randomRow && exitLocation.Col == randomCol))
                {
                    i--;
                    continue;
                }

                if (randomEnemyType == 0)
                {
                    playArea[randomRow, randomCol] = new Goblin();
                    continue;
                }
                if (randomEnemyType == 1)
                {
                    playArea[randomRow, randomCol] = new Orc();
                    continue;
                }
                if (randomEnemyType == 2)
                {
                    playArea[randomRow, randomCol] = new Banshee();
                    continue;
                }
            }
        }
        private void InitPowerUps()
        {
            // Place power ups in random areas
            for (int i = 0; i < NUM_POWERUPS; i++)
            {
                int randomRow = random.Next(NUM_PLAYAREA_ROWS);
                int randomCol = random.Next(NUM_PLAYAREA_COLS);
                int randomEnemyType = random.Next(2);

                // Repeat the same loop if the location is already occupied
                if (playArea[randomRow, randomCol] != null || (playerLocation.Row == randomRow && playerLocation.Col == randomCol) || (exitLocation.Row == randomRow && exitLocation.Col == randomCol))
                {
                    i--;
                    continue;
                }

                if (randomEnemyType == 0)
                {
                    playArea[randomRow, randomCol] = new HealthPotion();
                    continue;
                }
                if (randomEnemyType == 1)
                {
                    playArea[randomRow, randomCol] = new MagickaPotion();
                    continue;
                }
            }
        }

        public IGameObject GetAreaContents()
        {
            return playArea[playerLocation.Row, playerLocation.Col];
        }

        public bool IsDungeonExit()
        {
            return playerLocation.Row == exitLocation.Row && playerLocation.Col == exitLocation.Col;
        }

        public ICharacter GetWizert()
        {
            return player;
        }

        public void WizertAttackAction(ICharacter enemy)
        {
            player.Attack(enemy);
        }

        public bool MovePlayer(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.NORTH:
                    return Move(playerLocation.Row - 1, playerLocation.Col);
                case MoveDirection.EAST:
                    return Move(playerLocation.Row, playerLocation.Col + 1);
                case MoveDirection.SOUTH:
                    return Move(playerLocation.Row + 1, playerLocation.Col);
                case MoveDirection.WEST:
                    return Move(playerLocation.Row, playerLocation.Col - 1);
                default:
                    return false;
            }
        }

        private bool Move(int newRow, int newCol)
        {
            if (newRow < 0 || newRow >= NUM_PLAYAREA_ROWS)
            {
                Console.WriteLine("There is a wall in that direction!");
                return false;
            }

            if (newCol < 0 || newCol >= NUM_PLAYAREA_COLS)
            {
                Console.WriteLine("There is a wall in that direction!");
                return false;
            }

            playerLocation.Row = newRow;
            playerLocation.Col = newCol;

            return true;
        }
    }
}

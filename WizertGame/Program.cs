using System;

namespace WizertGame
{
    public class Program
    {
        private Dungeon dungeon;
        private char? userInput;

        public Program()
        {
            this.dungeon = new Dungeon();
        }
        
        public void run()
        {
            while (true)
            {
                Console.WriteLine("\n\nWelcome to the Wizert Game\n");
                while (dungeon.GetWizert().IsAlive && !dungeon.IsDungeonExit())
                {
                    MovePlayer();
                    AnalysePlayerLocation();
                }

                if (dungeon.GetWizert().IsAlive)
                {
                    Console.WriteLine("\nCONGRATULATIONS, YOU WIN. You escaped the dungeon successfully");
                }
                else
                {
                    Console.WriteLine("\nYOU LOSE. You were killed before you could escaped the dungeon");
                }

                Console.WriteLine("\nDo you want to replay?\nPress...\n1 to Restart the Game\n2 to Exit the Game\n");
                userInput = Console.ReadKey(true).KeyChar;

                if(userInput == null || userInput != '1')
                {
                    break;
                }

                this.dungeon = new Dungeon();
            }
        }

        private void AnalysePlayerLocation()
        {
            IGameObject gameObject = dungeon.GetAreaContents();
            if(gameObject != null)
            {
                if (gameObject is ICharacter enemy && enemy.IsAlive)
                {
                    Console.WriteLine("\nYou have encountered an " + enemy.Name);
                    while (true)
                    {
                        Console.WriteLine("Your HP = " + dungeon.GetWizert().HealthPoints + ", " + enemy.Name + " HP = " + enemy.HealthPoints);
                        Console.WriteLine("Press...\n1 to Attack\n2 to Heal\n3 to Attempt to Flee\n");
                        userInput = Console.ReadKey(true).KeyChar;

                        if (userInput != null && int.TryParse(userInput.ToString().Trim(), out int option))
                        {
                            if (option > 0 && option < 4)
                            {
                                if (option == 1 && ProceedAfterAttack(enemy))
                                    break;
                                if (option == 2 && ProceedAfterHealing())
                                    break;
                                if (option == 3 && ProceedAfterFleeAttempt(enemy))
                                    break;
                            }
                            else { Console.WriteLine("Invalid input!\n"); }
                        }else
                        { Console.WriteLine("Invalid input!\n"); }
                    }
                }else if(gameObject is IPowerUp powerUp)
                {
                    Console.WriteLine("\nYou have found an " + powerUp.Name);
                    Console.WriteLine("Consuming the power up...");
                    powerUp.Consume(dungeon.GetWizert());
                    if (dungeon.GetWizert() is Wizert player)
                        Console.WriteLine("Your HP = " + player.HealthPoints + ", MP = " + player.MagickaPoints + "\n");
                }
            }
            else
            {
                Console.WriteLine("You have moved into a new room\n");
            }
        }

        private bool ProceedAfterFleeAttempt(ICharacter enemy)
        {
            Console.WriteLine("\nAttempting to flee...");
            bool isFleeAttemptSuccessful = new Random().NextDouble() > 0.5;

            if (isFleeAttemptSuccessful)
            {
                Console.WriteLine("You were able to flee successfully! No MP or HP were used");
                return true;
            }

            Console.WriteLine(enemy.Name + " attacked back with " + enemy.AttackName + ".\nDamage caused = " + enemy.DamagePerAttack);
            enemy.Attack(dungeon.GetWizert());

            return false;
        }

        private bool ProceedAfterHealing()
        {
            Console.WriteLine("\nCasting a healing spell...");
            if(dungeon.GetWizert() is Wizert player && player.Heal())
            {
                Console.WriteLine("Healed successfully! 5 Magicka points consumed!");
            }
            else
            {
                Console.WriteLine("You need atleast 5 Magicka points to heal!");
            }

            return false;
        }

        private bool ProceedAfterAttack(ICharacter enemy)
        {
            Console.WriteLine("\nYou attacked the " + enemy.Name + " with " + dungeon.GetWizert().AttackName + ".\nDamage caused = " + dungeon.GetWizert().DamagePerAttack);
            dungeon.GetWizert().Attack(enemy);
            if (enemy.IsAlive)
            {
                Console.WriteLine("\n"+enemy.Name + " attacked back with " + enemy.AttackName + ".\nDamage caused = " + enemy.DamagePerAttack);
                enemy.Attack(dungeon.GetWizert());

                return false;
            }

            Console.WriteLine("\nYou successfully killed the " + enemy.Name);

            return true;
        }

        private void MovePlayer()
        {
            while (true)
            {
                Console.WriteLine("You are in an empty room. Press...");
                Console.WriteLine("1 to go north\n2 to go east\n3 to go south\n4 to go west\n");
                userInput = Console.ReadKey(true).KeyChar;

                if (userInput != null && int.TryParse(userInput.ToString().Trim(), out int option))
                {
                    if (option > 0 && option < 5)
                    {
                        if (option == 1 && dungeon.MovePlayer(Dungeon.MoveDirection.NORTH))
                            break;
                        if (option == 2 && dungeon.MovePlayer(Dungeon.MoveDirection.EAST))
                            break;
                        if (option == 3 && dungeon.MovePlayer(Dungeon.MoveDirection.SOUTH))
                            break;
                        if (option == 4 && dungeon.MovePlayer(Dungeon.MoveDirection.WEST))
                            break;
                    }
                }

                Console.WriteLine("Invalid input.\n");
            }
        }

        public static void Main(String[] args)
        {
            new Program().run();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public class Wizert : ICharacter
    {
        public int MagickaPoints { get; private set; }
        public override string Name { get => "Wizert"; }
        public override string AttackName { get => "FireBall"; }

        public override int DamagePerAttack => 5;
        public const int HEALING_SPELL_HEALTH_POINTS = 3;
        public const int HEALING_MAGICKA_POINTS_CONSUMED = 5;

        public Wizert() : base(100)
        {
            MagickaPoints = 200;
        }

        public override void Attack(ICharacter other)
        {
            if (IsAlive && MagickaPoints >= 3)
            {
                MagickaPoints -= 3;
                CastFireBall(other);
            }
            else
            {
                Console.WriteLine("You don't have enough Magicka points to attack");
            }
        }

        private void CastFireBall(ICharacter other)
        {
            Console.WriteLine("Casting fireball");
            other.TakeDamage(DamagePerAttack);
        }

        public override void TakeDamage(int damage)
        {
            HealthPoints -= damage;
            if(HealthPoints <= 0)
            {
                IsAlive = false;
            }
        }

        public bool Heal()
        {
            if (MagickaPoints < HEALING_MAGICKA_POINTS_CONSUMED)
                return false;

            MagickaPoints -= HEALING_MAGICKA_POINTS_CONSUMED;
            HealthPoints += HEALING_SPELL_HEALTH_POINTS;
            
            return true;
        }

        public void Flee()
        {
            Console.WriteLine(this.Name + " attempting to flee!");
        }

        public void RestoreMagickaPoints(int points)
        {
            MagickaPoints += points;
        }
    }
}

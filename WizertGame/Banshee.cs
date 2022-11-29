using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public class Banshee : ICharacter
    {
        public override string Name => "Banshee";
        public override string AttackName { get => "Screech"; }

        public override int DamagePerAttack { get => 5; }

        public Banshee() : base(8)
        {
        }

        public override void Attack(ICharacter other)
        {
            if(IsAlive)
                Screech(other);
        }

        public override void TakeDamage(int damage)
        {
            HealthPoints -= damage;
            if (HealthPoints <= 0)
                IsAlive = false;
        }

        private void Screech(ICharacter other)
        {
            Console.WriteLine(this.Name + " screeching");
            other.TakeDamage(DamagePerAttack);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public class Orc : ICharacter
    {
        public override int DamagePerAttack => 3;

        public override string Name => "Orc";
        public override string AttackName { get => "Cleave"; }

        public Orc() : base(5)
        {
        }

        public override void Attack(ICharacter other)
        {
            if(IsAlive)
                Cleave(other);
        }

        public override void TakeDamage(int damage)
        {
            HealthPoints -= damage;
            if (HealthPoints <= 0)
                IsAlive = false;
        }

        private void Cleave(ICharacter other)
        {
            other.TakeDamage(DamagePerAttack);
        }
    }
}

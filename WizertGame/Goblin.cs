using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public class Goblin : ICharacter
    {
        public override int DamagePerAttack => 2;

        public override string Name => "Goblin";
        public override string AttackName { get => "BodySlam"; }

        public Goblin() : base(3)
        {
        }

        public override void Attack(ICharacter other)
        {
            if(IsAlive)
                BodySlam(other);
        }

        public override void TakeDamage(int damage)
        {
            HealthPoints -= damage;
            if (HealthPoints <= 0)
                IsAlive = false;
        }

        private void BodySlam(ICharacter other)
        {
            other.TakeDamage(DamagePerAttack);
        }
    }
}

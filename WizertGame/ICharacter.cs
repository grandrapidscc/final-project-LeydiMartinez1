using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public abstract class ICharacter : IGameObject
    {
        private int healthPoints;

        public int HealthPoints {
            get {return healthPoints;}
            protected set { healthPoints = value; }
        }

        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            protected set { isAlive = value; }
        }
        public abstract int DamagePerAttack { get; }

        public abstract string Name { get; }

        public abstract string AttackName { get; }

        protected ICharacter(int healthPoints)
        {
            HealthPoints = healthPoints;
            IsAlive = true;
        }

        public abstract void Attack(ICharacter other);
        public abstract void TakeDamage(int damage);

        public virtual void RestoreHealthPoints(int points)
        {
            HealthPoints += points;
        }
    }
}

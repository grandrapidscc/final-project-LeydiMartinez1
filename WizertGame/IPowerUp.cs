using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public abstract class IPowerUp : IGameObject
    {
        private bool isConsumed;
        public virtual bool IsConsumed { get { return isConsumed; } protected set { isConsumed = value; } }

        public abstract string Name { get; }

        public abstract void Consume(ICharacter other);
    }
}

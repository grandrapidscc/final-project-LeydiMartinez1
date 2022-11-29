using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public class HealthPotion : IPowerUp
    {
        private const int HEALTH_POINTS_TO_RESTORE = 10;
        public override string Name => "HealthPotion";

        public override void Consume(ICharacter other)
        {
            other.RestoreHealthPoints(HEALTH_POINTS_TO_RESTORE);
        }
    }
}

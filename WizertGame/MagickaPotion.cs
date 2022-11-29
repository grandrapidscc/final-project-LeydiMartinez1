using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizertGame
{
    public class MagickaPotion : IPowerUp
    {
        private const int MAGICKA_POINTS_TO_RESTORE = 10;
        public override string Name => "MagickaPotion";

        public override void Consume(ICharacter other)
        {
            if(other is Wizert player)
                player.RestoreMagickaPoints(MAGICKA_POINTS_TO_RESTORE);
        }
    }
}

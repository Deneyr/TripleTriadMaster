using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataTypes;

namespace Core.Commands
{
    public class PlayCardCommand : ACommand
    {
        public int CardIndex
        {
            get;
            set;
        }
        public int YPosition
        {
            get;
            set;
        }
        public int XPosition
        {
            get;
            set;
        }

        public PlayCardCommand(Player pPuppet, int pCardIndex, int pYPosition, int pXPosition) : base(pPuppet)
        {
            this.CardIndex = pCardIndex;

            this.YPosition = pYPosition;

            this.XPosition = pXPosition;
        }

        public override void RunCommand()
        {
            this.mPuppet.PlayCard(this.CardIndex, this.YPosition, this.XPosition);
        }

        public override string ToString()
        {
            return XPosition.ToString() + " - " + (3 - YPosition).ToString();
        }
    }
}

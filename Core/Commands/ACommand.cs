using Core.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands
{
    public abstract class ACommand
    {
        protected Player mPuppet;

        public ACommand(Player pPuppet)
        {
            this.mPuppet = pPuppet;
        }

        public abstract void RunCommand();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Commands;

namespace AI.MoveTree
{
    public class MoveRoot : MoveComposite
    {
        public MoveRoot() : base(null, null)
        {
        }
    }
}

using ReLogic.Graphics;

using System;
using System.Collections.Generic;

namespace OdeMod.Utils
{
    internal class MasterPiece
    {
        internal int[,] bow = new int[,]
        {
            {1,0,1,1,1},
            {0,1,0,1,0},
            {1,0,1,0,0},
            {1,1,0,1,0},
            {1,0,0,0,1}
        };
        internal int[,] sword = new int[,]
        {
            {0,0,0,1,1},
            {0,0,1,0,1},
            {1,1,0,1,0},
            {0,1,1,0,0},
            {1,0,1,0,0}
        };
        
    }
}
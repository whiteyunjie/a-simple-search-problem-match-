using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace match
{
    class myoperator
    {
        public int[] keep1= { -1};
        public int[] plus1= { -1};
        public int[] minus1= { -1};
        public int[] pltr = { -1 };
        public int[] mitr = { -1 }; 
        public int plus2=-1;
        public int minus2=-1;
        public void exten(int type,params int[] arr)
        {
            if (type == 0) keep1 = arr;
            if (type == 1) plus1 = arr;
            if (type == 2) minus1 = arr;
            if (type == 3) pltr = arr;
            if (type == 4) mitr = arr;

        }
    }
    
}

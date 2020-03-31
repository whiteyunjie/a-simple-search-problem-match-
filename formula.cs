using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace match
{
    class formula
    {
        int num1;
        int num2;
        int result;
        int oper;    //运算类型，0,1,2分别代表加、减、乘
        int opnum;   //初始值为1则移动一根火柴，为2则移动两根火柴
        

        public void setformula(int n1,int n2,int re, int op, int times)
        {
            num1 = n1;
            num2 = n2;
            result = re;
            oper = op;
            opnum = times;
        }

        public bool Isequal()
        {
            if (oper == 0)
            {
                if (num1 + num2 == result) return true;
                else return false;
            }
            else if (oper == 1)
            {
                if (num1 - num2 == result) return true;
                else return false;
            }
            else
            {
                if (num1 * num2 == result) return true;
                else return false;
            }
        }

        public void search()
        {

        }
    }
}

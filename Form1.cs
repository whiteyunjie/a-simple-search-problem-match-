using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace match
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button3.Enabled = false;
            chart();
            creatbtns();
        }
        myoperator[] myop = new myoperator[10];
        int num1 = 0;
        int num2 = 0;
        int result = 0;
        int oper = 0;    //运算类型，0,1,2分别代表加、减、乘
        int ornum1;
        int ornum2;
        int orresult;
        int oroper;
        int solvenum = 0;  //解决方案数目
        int cursolve = 0;  //当前页面所展示的解的索引
        int mapl = 0;   //加了火柴时的操作数
        int mami = 0;   //减了火柴的操作数
        int movenum = 0;//当前题目要求移动的火柴数
        List<int> num1k = new List<int>() { -1 };
        List<int> num1p = new List<int>() { -1 };
        List<int> num1m = new List<int>() { -1 };
        List<int> num2k = new List<int>() { -1 };
        List<int> num2p = new List<int>() { -1 };
        List<int> num2m = new List<int>() { -1 };
        List<int> resultk = new List<int>() { -1 };
        List<int> resultp = new List<int>() { -1 };
        List<int> resultm = new List<int>() { -1 };
        List<int> n1keep = new List<int>() { -1 };
        List<int> n1p = new List<int>() { -1 };
        List<int> n1m = new List<int>() { -1 };
        List<int> n2keep = new List<int>() { -1 };
        List<int> n2p = new List<int>() { -1 };
        List<int> n2m = new List<int>() { -1 };
        List<int> rekeep = new List<int>() { -1 };
        List<int> rep = new List<int>() { -1 };
        List<int> rem = new List<int>() { -1 };
        List<int> pltr1 = new List<int>() { -1 };
        List<int> pltr2 = new List<int>() { -1 };
        List<int> pltr3 = new List<int>() { -1 };
        List<int> mitr1 = new List<int>() { -1 };
        List<int> mitr2 = new List<int>() { -1 };
        List<int> mitr3 = new List<int>() { -1 };
        List<int> solveset = new List<int>();     //可能的解集
        int[,] questionbank = new int[20, 4];
        int[] questionlevel = new int[20];        //题库对应的题目难度
        int depth = 0;               //搜索深度
        //数字对应火柴的组合
        int[,] matchnum = new int[10,7]{
                          {1,1,1,0,1,1,1},
                          {0,0,1,0,0,1,0},
                          {1,0,1,1,1,0,1},
                          {1,0,1,1,0,1,1},
                          {0,1,1,1,0,1,0 },
                          {1,1,0,1,0,1,1 },
                          {1,1,0,1,1,1,1 },
                          {1,0,1,0,0,1,0 },
                          {1,1,1,1,1,1,1 },
                          {1,1,1,1,0,1,1 }};
        int[,] currnum = new int[6, 7];
        Button[,] btar;
        Point orp;
        private int matchtonum(int a, int b, int c, int d, int e, int f, int g)
        {
            if (a == 1 && b == 1 && c == 1 && d == 0 && e == 1 && f == 1 && g == 1) return 0;
            if (a == 0 && b == 0 && c == 1 && d == 0 && e == 0 && f == 1 && g == 0) return 1;
            if (a == 1 && b == 0 && c == 1 && d == 1 && e == 1 && f == 0 && g == 1) return 2;
            if (a == 1 && b == 0 && c == 1 && d == 1 && e == 0 && f == 1 && g == 1) return 3;
            if (a == 0 && b == 1 && c == 1 && d == 1 && e == 0 && f == 1 && g == 0) return 4;
            if (a == 1 && b == 1 && c == 0 && d == 1 && e == 0 && f == 1 && g == 1) return 5;
            if (a == 1 && b == 1 && c == 0 && d == 1 && e == 1 && f == 1 && g == 1) return 6;
            if (a == 1 && b == 0 && c == 1 && d == 0 && e == 0 && f == 1 && g == 0) return 7;
            if (a == 1 && b == 1 && c == 1 && d == 1 && e == 1 && f == 1 && g == 1) return 8;
            if (a == 1 && b == 1 && c == 1 && d == 1 && e == 0 && f == 1 && g == 1) return 9;
            return -1;
        }
        public void chart()
        {
            for (int i = 0; i < 10; i++)
            {
                myop[i] = new myoperator();
            }
            int[] arr00 = new int[2];
            arr00[0] = 6;
            arr00[1] = 9;

            //减移：3-4  4-7 5-4 6-2 9-2 加移：2-6 2-9  4-3 4-5 7-4
            myop[0].exten(0, arr00);
            int[] arr01 = { 8 };
            myop[0].exten(1, arr01);
            int[] arr02 = { -1 };        //没有则扔-1进去
            myop[0].exten(2, arr02);
            int[] arr03 = { 2 };
            myop[0].exten(4, arr03);
            int[] arr10 = { -1 };
            myop[1].exten(0, arr10);
            int[] arr11 = { 7 };
            myop[1].exten(1, arr11);
            int[] arr12 = { -1 };
            myop[1].exten(2, arr12);
            int[] arr20 = { 3 };
            myop[2].exten(0, arr20);
            int[] arr21 = { -1 };
            myop[2].exten(1, arr21);
            int[] arr22 = { -1 };
            myop[2].exten(2, arr22);
            int[] arr23 = { 6, 9 };
            myop[2].exten(3, arr23);
            int[] arr30 = { 2, 5 };
            myop[3].exten(0, arr30);
            int[] arr31 = { 9 };
            myop[3].exten(1, arr31);
            int[] arr32 = { -1 };
            myop[3].exten(2, arr32);
            int[] arr33 = { 4 };
            myop[3].exten(4, arr33);
            int[] arr40 = { -1 };
            myop[4].exten(0, arr40);
            int[] arr41 = { -1 };
            myop[4].exten(1, arr41);
            int[] arr42 = { -1 };
            myop[4].exten(2, arr42);
            int[] arr43 = { 3, 5 };
            myop[4].exten(3, arr43);
            int[] arr45 = { 7 };
            myop[4].exten(4, arr45);
            int[] arr50 = { 3 };
            myop[5].exten(0, arr50);
            int[] arr51 = { 6, 9 };
            myop[5].exten(1, arr51);
            int[] arr52 = { -1 };
            myop[5].exten(2, arr52);
            int[] arr53 = { 4 };
            myop[5].exten(4, arr53);
            int[] arr60 = { 0, 9 };
            myop[6].exten(0, arr60);
            int[] arr61 = { 8 };
            myop[6].exten(1, arr61);
            int[] arr62 = { 5 };
            myop[6].exten(2, arr62);
            int[] arr63 = { 2 };
            myop[6].exten(4, arr63);
            int[] arr70 = { -1 };
            myop[7].exten(0, arr70);
            int[] arr71 = { -1 };
            myop[7].exten(1, arr71);
            int[] arr72 = { 1 };
            myop[7].exten(2, arr72);
            int[] arr73 = { 4 };
            myop[7].exten(3, arr73);
            int[] arr80 = { -1 };
            myop[8].exten(0, arr80);
            int[] arr81 = { -1 };
            myop[8].exten(1, arr81);
            int[] arr82 = { 0, 6, 9 };
            myop[8].exten(2, arr82);
            int[] arr90 = { 0, 6 };
            myop[9].exten(0, arr90);
            int[] arr91 = { 8 };
            myop[9].exten(1, arr91);
            int[] arr92 = { 3, 5 };
            myop[9].exten(2, arr92);
            int[] arr93 = { 2 };
            myop[9].exten(4, arr93);

            myop[1].plus2 = 4;
            myop[2].plus2 = 8;
            myop[3].minus2 = 7;
            myop[4].plus2 = 9;
            myop[4].minus2 = 1;
            myop[7].plus2 = 3;
            myop[8].minus2 = 2;
            myop[9].minus2 = 4;
        }
        public void createlib()
        {
            if (num1 < 10)
            {
                num1k = new List<int>(myop[num1].keep1);
                num1p = new List<int>(myop[num1].plus1);
                num1m = new List<int>(myop[num1].minus1);
            }
            else
            {
                int c1 = num1 % 10;              //个位
                int c2 = num1 / 10;              //十位
                List<int> arr1 = new List<int>();
                List<int> arr2 = new List<int>();
                List<int> arr3 = new List<int>();
                for (int i = 0; i < myop[c1].keep1.Length; i++)
                {
                    if (myop[c1].keep1[i] != -1) arr1.Add(c2 * 10 + myop[c1].keep1[i]);
                }
                for (int i = 0; i < myop[c2].keep1.Length; i++)
                {
                    if (myop[c2].keep1[i] != -1) arr1.Add(myop[c2].keep1[i] * 10 + c1);
                }
                for (int i = 0; i < myop[c1].plus1.Length; i++)
                {
                    if (myop[c1].plus1[i] != -1) arr2.Add(c2 * 10 + myop[c1].plus1[i]);
                    for (int j = 0; j < myop[c2].minus1.Length; j++)
                        if (myop[c1].plus1[i] != -1 && myop[c2].minus1[j] != -1) arr1.Add(myop[c2].minus1[j] * 10 + myop[c1].plus1[i]);
                }
                for (int i = 0; i < myop[c2].plus1.Length; i++)
                {
                    if (myop[c2].plus1[i] != -1) arr2.Add(myop[c2].plus1[i] * 10 + c1);
                }
                for (int i = 0; i < myop[c1].minus1.Length; i++)
                {
                    if (myop[c1].minus1[i] != -1) arr3.Add(c2 * 10 + myop[c1].minus1[i]);
                    for (int j = 0; j < myop[c2].plus1.Length; j++)
                        if (myop[c1].minus1[i] != -1 && myop[c2].plus1[j] != -1) arr1.Add(myop[c2].plus1[j] * 10 + myop[c1].minus1[i]);
                }
                for (int i = 0; i < myop[c2].minus1.Length; i++)
                {
                    if (myop[c2].minus1[i] != -1) arr3.Add(myop[c2].minus1[i] * 10 + c1);
                }
                if (arr1.Count != 0) num1k = arr1;
                if (arr2.Count != 0) num1p = arr2;
                if (arr3.Count != 0) num1m = arr3;
            }
            if (num2 < 10)
            {
                num2k = new List<int>(myop[num2].keep1);
                num2p = new List<int>(myop[num2].plus1);
                num2m = new List<int>(myop[num2].minus1);
            }
            else
            {
                int c1 = num2 % 10;              //个位
                int c2 = num2 / 10;              //十位
                List<int> arr1 = new List<int>();
                List<int> arr2 = new List<int>();
                List<int> arr3 = new List<int>();
                for (int i = 0; i < myop[c1].keep1.Length; i++)
                {
                    if (myop[c1].keep1[i] != -1) arr1.Add(c2 * 10 + myop[c1].keep1[i]);
                }
                for (int i = 0; i < myop[c2].keep1.Length; i++)
                {
                    if (myop[c2].keep1[i] != -1) arr1.Add(myop[c2].keep1[i] * 10 + c1);
                }
                for (int i = 0; i < myop[c1].plus1.Length; i++)
                {
                    if (myop[c1].plus1[i] != -1) arr2.Add(c2 * 10 + myop[c1].plus1[i]);
                    for (int j = 0; j < myop[c2].minus1.Length; j++)
                        if (myop[c1].plus1[i] != -1 && myop[c2].minus1[j] != -1) arr1.Add(myop[c2].minus1[j] * 10 + myop[c1].plus1[i]);
                }
                for (int i = 0; i < myop[c2].plus1.Length; i++)
                {
                    if (myop[c2].plus1[i] != -1) arr2.Add(myop[c2].plus1[i] * 10 + c1);
                }
                for (int i = 0; i < myop[c1].minus1.Length; i++)
                {
                    if (myop[c1].minus1[i] != -1) arr3.Add(c2 * 10 + myop[c1].minus1[i]);
                    for (int j = 0; j < myop[c2].plus1.Length; j++)
                        if (myop[c1].minus1[i] != -1 && myop[c2].plus1[j] != -1) arr1.Add(myop[c2].plus1[j] * 10 + myop[c1].minus1[i]);
                }
                for (int i = 0; i < myop[c2].minus1.Length; i++)
                {
                    if (myop[c2].minus1[i] != -1) arr3.Add(myop[c2].minus1[i] * 10 + c1);
                }
                if (arr1.Count != 0) num2k = arr1;
                if (arr2.Count != 0) num2p = arr2;
                if (arr3.Count != 0) num2m = arr3;
            }
            if (result < 10)
            {
                resultk = new List<int>(myop[result].keep1);
                resultp = new List<int>(myop[result].plus1);
                resultm = new List<int>(myop[result].minus1);
            }
            else
            {
                int c1 = result % 10;              //个位
                int c2 = result / 10;              //十位
                List<int> arr1 = new List<int>();
                List<int> arr2 = new List<int>();
                List<int> arr3 = new List<int>();
                for (int i = 0; i < myop[c1].keep1.Length; i++)
                {
                    if (myop[c1].keep1[i] != -1) arr1.Add(c2 * 10 + myop[c1].keep1[i]);
                }
                for (int i = 0; i < myop[c2].keep1.Length; i++)
                {
                    if (myop[c2].keep1[i] != -1) arr1.Add(myop[c2].keep1[i] * 10 + c1);
                }
                for (int i = 0; i < myop[c1].plus1.Length; i++)
                {
                    if (myop[c1].plus1[i] != -1) arr2.Add(c2 * 10 + myop[c1].plus1[i]);
                    for (int j = 0; j < myop[c2].minus1.Length; j++)
                        if (myop[c1].plus1[i] != -1 && myop[c2].minus1[j] != -1) arr1.Add(myop[c2].minus1[j] * 10 + myop[c1].plus1[i]);
                }
                for (int i = 0; i < myop[c2].plus1.Length; i++)
                {
                    if (myop[c2].plus1[i] != -1) arr2.Add(myop[c2].plus1[i] * 10 + c1);
                }
                for (int i = 0; i < myop[c1].minus1.Length; i++)
                {
                    if (myop[c1].minus1[i] != -1) arr3.Add(c2 * 10 + myop[c1].minus1[i]);
                    for (int j = 0; j < myop[c2].plus1.Length; j++)
                        if (myop[c1].minus1[i] != -1 && myop[c2].plus1[j] != -1) arr1.Add(myop[c2].plus1[j] * 10 + myop[c1].minus1[i]);
                }
                for (int i = 0; i < myop[c2].minus1.Length; i++)
                {
                    if (myop[c2].minus1[i] != -1) arr3.Add(myop[c2].minus1[i] * 10 + c1);
                }
                if (arr1.Count != 0) resultk = arr1;
                if (arr2.Count != 0) resultp = arr2;
                if (arr3.Count != 0) resultm = arr3;
            }

        }

        //加两根火柴直接补位成个位或十位，或减两根
        public void movetwo()
        {
            if (num1 < 10)
            {
                List<int> arr = new List<int>() { -1 };
                List<int> a1 = new List<int>();
                List<int> a2 = new List<int>();
                pltr1 = new List<int>(myop[num1].pltr);
                mitr1 = new List<int>(myop[num1].mitr);
                n1keep = arr;
                if (num1 == 1) a1.Add(4);
                else if (num1 == 2) a1.Add(8);
                else if (num1 == 4) a1.Add(9);
                else if (num1 == 7) a1.Add(3);
                else a1.Add(-1);
                n1p = a1;
                if (num1 == 3) a2.Add(7);
                else if (num1 == 4) a2.Add(1);
                else if (num1 == 9) a2.Add(4);
                else if (num1 == 8) a2.Add(2);
                else a2.Add(-1);
                n1m = a2;
            }
            else
            {
                List<int> arr = new List<int>();
                List<int> pltrset = new List<int>();
                List<int> mitrset = new List<int>();
                int c1 = num1 % 10;
                int c2 = num1 / 10;
                if (myop[c1].pltr[0] != -1)
                    for (int i = 0; i < myop[c1].pltr.Length; i++) pltrset.Add(myop[c1].pltr[i] + c2 * 10);
                if (myop[c2].pltr[0] != -1)
                    for (int i = 0; i < myop[c2].pltr.Length; i++) pltrset.Add(myop[c2].pltr[i] * 10 + c1);
                if (myop[c1].mitr[0] != -1)
                    for (int i = 0; i < myop[c1].mitr.Length; i++) mitrset.Add(myop[c1].mitr[i] + c2 * 10);
                if (myop[c2].pltr[0] != -1)
                    for (int i = 0; i < myop[c2].mitr.Length; i++) mitrset.Add(myop[c2].mitr[i] * 10 + c1);
                if (pltrset.Count != 0) pltr1 = pltrset;
                if (mitrset.Count != 0) mitr1 = mitrset;

                if (myop[c1].plus2 != -1 && myop[c2].minus2 != -1) arr.Add(myop[c1].plus2 + myop[c2].minus2 * 10);
                //移一个，变一个，减移：4-7，加移：2-6,9 4-3,5
                if (c1 == 4)
                {
                    for (int i = 0; i < myop[c2].plus1.Length; i++)
                    {
                        if (myop[c2].plus1[i] != -1) arr.Add(myop[c2].plus1[i] * 10 + 7);
                    }
                    for (int i = 0; i < myop[c2].minus1.Length; i++)
                        if (myop[c2].minus1[i] != -1)
                        {
                            arr.Add(myop[c2].minus1[i] * 10 + 3);
                            arr.Add(myop[c2].minus1[i] * 10 + 5);
                        }
                }
                if (c2 == 4)
                {
                    for (int i = 0; i < myop[c1].plus1.Length; i++)
                    {
                        if (myop[c1].plus1[i] != -1) arr.Add(myop[c1].plus1[i] + 70);
                    }
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        if (myop[c1].minus1[i] != -1)
                        {
                            arr.Add(myop[c1].minus1[i] + 30);
                            arr.Add(myop[c1].minus1[i] + 50);
                        }
                }
                if (c1 == 2)
                {
                    for (int i = 0; i < myop[c2].minus1.Length; i++)
                        if (myop[c2].minus1[i] != -1)
                        {
                            arr.Add(myop[c2].minus1[i] * 10 + 6);
                            arr.Add(myop[c2].minus1[i] * 10 + 9);
                        }
                }
                if (c2 == 2)
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        if (myop[c1].minus1[i] != -1)
                        {
                            arr.Add(myop[c1].minus1[i] + 60);
                            arr.Add(myop[c1].minus1[i] + 90);
                        }
                if (arr.Count != 0) n1keep = arr;
                List<int> a1 = new List<int>();
                List<int> a2 = new List<int>();
                if (myop[c1].plus2 != -1) a1.Add(myop[c1].plus2 + c2 * 10);
                if (myop[c2].plus2 != -1) a1.Add(myop[c2].plus2 * 10 + c1);
                if (myop[c1].plus1[0] != -1 && myop[c2].plus1[0] != -1)
                    for (int i = 0; i < myop[c1].plus1.Length; i++)
                        for (int j = 0; j < myop[c2].plus1.Length; j++)
                            a1.Add(myop[c1].plus1[i] + myop[c2].plus1[j] * 10);
                if (myop[c1].minus2 != -1) a2.Add(myop[c1].minus2 + c2 * 10);
                if (myop[c2].minus2 != -1) a2.Add(myop[c2].minus2 * 10 + c1);
                if (myop[c1].minus1[0] != -1 && myop[c2].minus1[0] != -1)
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        for (int j = 0; j < myop[c2].minus1.Length; j++)
                            a2.Add(myop[c1].minus1[i] + myop[c2].minus1[j] * 10);
                if (a1.Count != 0) n1p = a1;
                if (a2.Count != 0) n1m = a2;
            }
            if (num2 < 10)
            {
                List<int> arr = new List<int>() { -1 };
                List<int> a1 = new List<int>();
                List<int> a2 = new List<int>();
                pltr2 = new List<int>(myop[num2].pltr);
                mitr2 = new List<int>(myop[num2].mitr);
                n2keep = arr;
                if (num2 == 1) a1.Add(4);
                else if (num2 == 2) a1.Add(8);
                else if (num2 == 4) a1.Add(9);
                else if (num2 == 7) a1.Add(3);
                else a1.Add(-1);
                n2p = a1;
                if (num2 == 3) a2.Add(7);
                else if (num2 == 4) a2.Add(1);
                else if (num2 == 9) a2.Add(4);
                else if (num2 == 8) a2.Add(2);
                else a2.Add(-1);
                n2m = a2;
            }
            else
            {
                List<int> arr = new List<int>();
                List<int> pltrset = new List<int>();
                List<int> mitrset = new List<int>();
                int c1 = num2 % 10;
                int c2 = num2 / 10;
                if (myop[c1].pltr[0] != -1)
                    for (int i = 0; i < myop[c1].pltr.Length; i++) pltrset.Add(myop[c1].pltr[i] + c2 * 10);
                if (myop[c2].pltr[0] != -1)
                    for (int i = 0; i < myop[c2].pltr.Length; i++) pltrset.Add(myop[c2].pltr[i] * 10 + c1);
                if (myop[c1].mitr[0] != -1)
                    for (int i = 0; i < myop[c1].mitr.Length; i++) mitrset.Add(myop[c1].mitr[i] + c2 * 10);
                if (myop[c2].pltr[0] != -1)
                    for (int i = 0; i < myop[c2].mitr.Length; i++) mitrset.Add(myop[c2].mitr[i] * 10 + c1);
                if (pltrset.Count != 0) pltr2 = pltrset;
                if (mitrset.Count != 0) mitr2 = mitrset;
                if (myop[c1].plus2 != -1 && myop[c2].minus2 != -1) arr.Add(myop[c1].plus2 + myop[c2].minus2 * 10);
                //移一个，变一个，减移：4-7，加移：2-6,9 4-3,5
                if (c1 == 4)
                {
                    for (int i = 0; i < myop[c2].plus1.Length; i++)
                    {
                        if (myop[c2].plus1[i] != -1) arr.Add(myop[c2].plus1[i] * 10 + 7);
                    }
                    for (int i = 0; i < myop[c2].minus1.Length; i++)
                        if (myop[c2].minus1[i] != -1)
                        {
                            arr.Add(myop[c2].minus1[i] * 10 + 3);
                            arr.Add(myop[c2].minus1[i] * 10 + 5);
                        }
                }
                if (c2 == 4)
                {
                    for (int i = 0; i < myop[c1].plus1.Length; i++)
                    {
                        if (myop[c1].plus1[i] != -1) arr.Add(myop[c1].plus1[i] + 70);
                    }
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        if (myop[c1].minus1[i] != -1)
                        {
                            arr.Add(myop[c1].minus1[i] + 30);
                            arr.Add(myop[c1].minus1[i] + 50);
                        }
                }
                if (c1 == 2)
                {
                    for (int i = 0; i < myop[c2].minus1.Length; i++)
                        if (myop[c2].minus1[i] != -1)
                        {
                            arr.Add(myop[c2].minus1[i] * 10 + 6);
                            arr.Add(myop[c2].minus1[i] * 10 + 9);
                        }
                }
                if (c2 == 2)
                {
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        if (myop[c1].minus1[i] != -1)
                        {
                            arr.Add(myop[c1].minus1[i] + 60);
                            arr.Add(myop[c1].minus1[i] + 90);
                        }
                }
                if (arr.Count != 0) n2keep = arr;
                List<int> a1 = new List<int>();
                List<int> a2 = new List<int>();
                if (myop[c1].plus2 != -1) a1.Add(myop[c1].plus2 + c2 * 10);
                if (myop[c2].plus2 != -1) a1.Add(myop[c2].plus2 * 10 + c1);
                if (myop[c1].plus1[0] != -1 && myop[c2].plus1[0] != -1)
                    for (int i = 0; i < myop[c1].plus1.Length; i++)
                        for (int j = 0; j < myop[c2].plus1.Length; j++)
                            a1.Add(myop[c1].plus1[i] + myop[c2].plus1[j] * 10);
                if (myop[c1].minus2 != -1) a2.Add(myop[c1].minus2 + c2 * 10);
                if (myop[c2].minus2 != -1) a2.Add(myop[c2].minus2 * 10 + c1);
                if (myop[c1].minus1[0] != -1 && myop[c2].minus1[0] != -1)
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        for (int j = 0; j < myop[c2].minus1.Length; j++)
                            a2.Add(myop[c1].minus1[i] + myop[c2].minus1[j] * 10);
                if (a1.Count != 0) n2p = a1;
                if (a2.Count != 0) n2m = a2;
            }
            if (result < 10)
            {
                List<int> arr = new List<int>() { -1 };
                List<int> a1 = new List<int>();
                List<int> a2 = new List<int>();
                pltr3 = new List<int>(myop[result].pltr);
                mitr3 = new List<int>(myop[result].mitr);
                rekeep = arr;
                if (result == 1) a1.Add(4);
                else if (result == 2) a1.Add(8);
                else if (result == 4) a1.Add(9);
                else if (result == 7) a1.Add(3);
                else a1.Add(-1);
                rep = a1;
                if (result == 3) a2.Add(7);
                else if (result == 4) a2.Add(1);
                else if (result == 9) a2.Add(4);
                else if (result == 8) a2.Add(2);
                else a2.Add(-1);
                rem = a2;
            }
            else
            {
                List<int> arr = new List<int>();
                List<int> pltrset = new List<int>();
                List<int> mitrset = new List<int>();
                int c1 = result % 10;
                int c2 = result / 10;
                if (myop[c1].pltr[0] != -1)
                    for (int i = 0; i < myop[c1].pltr.Length; i++) pltrset.Add(myop[c1].pltr[i] + c2 * 10);
                if (myop[c2].pltr[0] != -1)
                    for (int i = 0; i < myop[c2].pltr.Length; i++) pltrset.Add(myop[c2].pltr[i] * 10 + c1);
                if (myop[c1].mitr[0] != -1)
                    for (int i = 0; i < myop[c1].mitr.Length; i++) mitrset.Add(myop[c1].mitr[i] + c2 * 10);
                if (myop[c2].pltr[0] != -1)
                    for (int i = 0; i < myop[c2].mitr.Length; i++) mitrset.Add(myop[c2].mitr[i] * 10 + c1);
                if (pltrset.Count != 0) pltr3 = pltrset;
                if (mitrset.Count != 0) mitr3 = mitrset;
                if (myop[c1].plus2 != -1 && myop[c2].minus2 != -1) arr.Add(myop[c1].plus2 + myop[c2].minus2 * 10);
                //移一个，变一个，减移：4-7，加移：2-6,9 4-3,5
                if (c1 == 4)
                {
                    for (int i = 0; i < myop[c2].plus1.Length; i++)
                    {
                        if (myop[c2].plus1[i] != -1) arr.Add(myop[c2].plus1[i] * 10 + 7);
                    }
                    for (int i = 0; i < myop[c2].minus1.Length; i++)
                        if (myop[c2].minus1[i] != -1)
                        {
                            arr.Add(myop[c2].minus1[i] * 10 + 3);
                            arr.Add(myop[c2].minus1[i] * 10 + 5);
                        }
                }
                if (c2 == 4)
                {
                    for (int i = 0; i < myop[c1].plus1.Length; i++)
                    {
                        if (myop[c1].plus1[i] != -1) arr.Add(myop[c1].plus1[i] + 70);
                    }
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        if (myop[c1].minus1[i] != -1)
                        {
                            arr.Add(myop[c1].minus1[i] + 30);
                            arr.Add(myop[c1].minus1[i] + 50);
                        }
                }
                if (c1 == 2)
                {
                    for (int i = 0; i < myop[c2].minus1.Length; i++)
                        if (myop[c2].minus1[i] != -1)
                        {
                            arr.Add(myop[c2].minus1[i] * 10 + 6);
                            arr.Add(myop[c2].minus1[i] * 10 + 9);
                        }
                }
                if (c2 == 2)
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        if (myop[c1].minus1[i] != -1)
                        {
                            arr.Add(myop[c1].minus1[i] + 60);
                            arr.Add(myop[c1].minus1[i] + 90);
                        }
                if (arr.Count != 0) rekeep = arr;
                List<int> a1 = new List<int>();
                List<int> a2 = new List<int>();
                if (myop[c1].plus2 != -1) a1.Add(myop[c1].plus2 + c2 * 10);
                if (myop[c2].plus2 != -1) a1.Add(myop[c2].plus2 * 10 + c1);
                if (myop[c1].plus1[0] != -1 && myop[c2].plus1[0] != -1)
                    for (int i = 0; i < myop[c1].plus1.Length; i++)
                        for (int j = 0; j < myop[c2].plus1.Length; j++)
                            a1.Add(myop[c1].plus1[i] + myop[c2].plus1[j] * 10);
                if (myop[c1].minus2 != -1) a2.Add(myop[c1].minus2 + c2 * 10);
                if (myop[c2].minus2 != -1) a2.Add(myop[c2].minus2 * 10 + c1);
                if (myop[c1].minus1[0] != -1 && myop[c2].minus1[0] != -1)
                    for (int i = 0; i < myop[c1].minus1.Length; i++)
                        for (int j = 0; j < myop[c2].minus1.Length; j++)
                            a2.Add(myop[c1].minus1[i] + myop[c2].minus1[j] * 10);
                if (a1.Count != 0) rep = a1;
                if (a2.Count != 0) rem = a2;
            }

        }
        public bool Isequal(int n1, int n2, int re, int type)
        {
            depth++;
            if (type == 0)
            {
                if (n1 + n2 == re) return true;
                else return false;
            }
            else if (type == 1)
            {
                if (n1 - n2 == re) return true;
                else return false;
            }
            else
            {
                if (n1 * n2 == re) return true;
                else return false;
            }
        }
        public void addset(int n1, int n2, int re, int op)
        {
            //应当考虑答案重复问题
            int flag = 1;
            for (int i = 0; i < solvenum - 1; i++)
            {
                if (solveset[4 * i] == n1 && solveset[4 * i + 1] == n2 && solveset[4 * i + 2] == re && solveset[4 * i + 3] == op)
                {
                    flag = 0;
                    solvenum--;
                    break;
                }
            }
            if (flag == 1)
            {
                solveset.Add(n1);
                solveset.Add(n2);
                solveset.Add(re);
                solveset.Add(op);
            }
        }
        public bool search1()           //一根火柴的移动搜索
        {
            int answers = 0;
            //内部移动
            for (int i = 0; i < num1k.Count; i++)
                if (num1k[i] != -1 && Isequal(num1k[i], num2, result, oper))
                {
                    solvenum++;
                    addset(num1k[i], num2, result, oper);
                }
            for (int i = 0; i < num2k.Count; i++)
                if (num2k[i] != -1 && Isequal(num1, num2k[i], result, oper))
                {
                    solvenum++;
                    addset(num1, num2k[i], result, oper);
                }
            for (int i = 0; i < resultk.Count; i++)
                if (resultk[i] != -1 && Isequal(num1, num2, resultk[i], oper))
                {
                    solvenum++;
                    addset(num1, num2, resultk[i], oper);
                }
            //移动火柴
            if (num1m[0] != -1)
            {
                for (int i = 0; i < num1m.Count; i++)
                {
                    for (int j = 0; j < num2p.Count; j++)
                        if (num2p[0] != -1 && Isequal(num1m[i], num2p[j], result, oper))
                        {
                            solvenum++;
                            addset(num1m[i], num2p[j], result, oper);
                        }
                    for (int j = 0; j < resultp.Count; j++)
                        if (num2p[0] != -1 && Isequal(num1m[i], num2, resultp[j], oper))
                        {
                            solvenum++;
                            addset(num1m[i], num2, resultp[j], oper);
                        }

                }
            }
            if (oper == 0)                //加法可以去掉一个火柴变减法
            {
                for (int i = 0; i < num1p.Count; i++)
                    if (num1p[0] != -1 && Isequal(num1p[i], num2, result, oper + 1))
                    {
                        solvenum++;
                        addset(num1p[i], num2, result, oper + 1);
                    }
                for (int i = 0; i < num2p.Count; i++)
                    if (num2p[0] != -1 && Isequal(num1, num2p[i], result, oper + 1))
                    {
                        solvenum++;
                        addset(num1, num2p[i], result, oper + 1);
                    }
                for (int i = 0; i < resultp.Count; i++)
                    if (resultp[0] != -1 && Isequal(num1, num2, resultp[i], oper + 1))
                    {
                        solvenum++;
                        addset(num1, num2, resultp[i], oper + 1);
                    }
            }
            if (oper == 1)                //减法加一根火柴变加法
            {
                for (int i = 0; i < num1m.Count; i++)
                    if (num1m[0] != -1 && Isequal(num1m[i], num2, result, oper - 1))
                    {
                        solvenum++;
                        addset(num1m[i], num2, result, oper - 1);
                    }
                for (int i = 0; i < num2m.Count; i++)
                    if (num2m[0] != -1 && Isequal(num1, num2m[i], result, oper - 1))
                    {
                        solvenum++;
                        addset(num1, num2m[i], result, oper - 1);
                    }
                for (int i = 0; i < resultm.Count; i++)
                    if (resultm[0] != -1 && Isequal(num1, num2, resultm[i], oper - 1))
                    {
                        solvenum++;
                        addset(num1, num2, resultm[i], oper - 1);
                    }
            }
            if (num2m[0] != -1)
            {
                for (int i = 0; i < num2m.Count; i++)
                {
                    for (int j = 0; j < num1p.Count; j++)
                        if (num1p[0] != -1 && Isequal(num1p[j], num2m[i], result, oper))
                        {
                            solvenum++;
                            addset(num1p[j], num2m[i], result, oper);
                        }
                    for (int j = 0; j < resultp.Count; j++)
                        if (resultp[0] != -1 && Isequal(num1, num2m[i], resultp[j], oper))
                        {
                            solvenum++;
                            addset(num1, num2m[i], resultp[j], oper);
                        }
                }
            }
            if (resultm[0] != -1)
            {
                for (int i = 0; i < resultm.Count; i++)
                {
                    for (int j = 0; j < num1p.Count; j++)
                        if (num1p[0] != -1 && Isequal(num1p[j], num2, resultm[i], oper))
                        {
                            solvenum++;
                            addset(num1p[j], num2, resultm[i], oper);
                        }
                    for (int j = 0; j < num2p.Count; j++)
                        if (num2p[0] != -1 && Isequal(num1, num2p[j], resultm[i], oper))
                        {
                            solvenum++;
                            addset(num1, num2p[j], resultm[i], oper);
                        }
                }
            }
            answers = solvenum;
            if (answers != 0) return true;
            else return false;
        }
        public bool search2()          //两根火柴的移动搜索
        {
            //加一变一以及减一变一
            if (pltr1[0] != -1)
            {
                for (int i = 0; i < pltr1.Count; i++)
                {
                    if (oper == 0 && Isequal(pltr1[i], num2, result, 1))
                    {
                        solvenum++;
                        addset(pltr1[i], num2, result, 1);
                    }
                    for (int j = 0; j < num2m.Count; j++)
                    {
                        if (num2m[0] != -1 && Isequal(pltr1[i], num2m[j], result, oper))
                        {
                            solvenum++;
                            addset(pltr1[i], num2m[j], result, oper);
                        }
                    }
                    for (int j = 0; j < resultm.Count; j++)
                    {
                        if (resultm[0] != -1 && Isequal(pltr1[i], num2, resultm[j], oper))
                        {
                            solvenum++;
                            addset(pltr1[i], num2, resultm[j], oper);
                        }
                    }
                }
            }
            if (mitr1[0] != -1)
            {
                for (int i = 0; i < mitr1.Count; i++)
                {
                    if (oper == 1 && Isequal(mitr1[i], num2, result, 0))
                    {
                        solvenum++;
                        addset(mitr1[i], num2, result, 0);
                    }
                    for (int j = 0; j < num2p.Count; j++)
                    {
                        if (num2p[0] != -1 && Isequal(mitr1[i], num2p[j], result, oper))
                        {
                            solvenum++;
                            addset(mitr1[i], num2p[j], result, oper);
                        }
                    }
                    for (int j = 0; j < resultp.Count; j++)
                    {
                        if (resultp[0] != -1 && Isequal(mitr1[i], num2, resultp[j], oper))
                        {
                            solvenum++;
                            addset(mitr1[i], num2, resultp[j], oper);
                        }
                    }
                }

            }
            if (pltr2[0] != -1)
            {
                for (int i = 0; i < pltr2.Count; i++)
                {
                    if (oper == 0 && Isequal(num1, pltr2[i], result, 1))
                    {
                        solvenum++;
                        addset(num1, pltr2[i], result, 1);
                    }
                    for (int j = 0; j < num1m.Count; j++)
                    {
                        if (num2m[0] != -1 && Isequal(num1m[j], pltr2[i], result, oper))
                        {
                            solvenum++;
                            addset(num1m[j], pltr2[i], result, oper);
                        }
                    }
                    for (int j = 0; j < resultm.Count; j++)
                    {
                        if (resultm[0] != -1 && Isequal(num1, pltr2[i], resultm[j], oper))
                        {
                            solvenum++;
                            addset(num1, pltr2[i], resultm[j], oper);
                        }
                    }
                }
            }
            if (mitr2[0] != -1)
            {
                for (int i = 0; i < mitr2.Count; i++)
                {
                    if (oper == 1 && Isequal(num1, mitr2[i], result, 0))
                    {
                        solvenum++;
                        addset(num1, mitr2[i], result, 0);
                    }
                    for (int j = 0; j < num1p.Count; j++)
                    {
                        if (num1p[0] != -1 && Isequal(num1p[j], mitr2[i], result, oper))
                        {
                            solvenum++;
                            addset(num1p[j], mitr2[i], result, oper);
                        }
                    }
                    for (int j = 0; j < resultp.Count; j++)
                    {
                        if (resultp[0] != -1 && Isequal(num1, mitr2[i], resultp[j], oper))
                        {
                            solvenum++;
                            addset(num1, mitr2[i], resultp[j], oper);
                        }
                    }
                }

            }
            if (pltr3[0] != -1)
            {
                for (int i = 0; i < pltr3.Count; i++)
                {
                    if (oper == 0 && Isequal(num1, num2, pltr3[i], 1))
                    {
                        solvenum++;
                        addset(num1, num2, pltr3[i], 1);
                    }
                    for (int j = 0; j < num2m.Count; j++)
                    {
                        if (num2m[0] != -1 && Isequal(num1, num2m[j], pltr3[i], oper))
                        {
                            solvenum++;
                            addset(num1, num2m[j], pltr3[i], oper);
                        }
                    }
                    for (int j = 0; j < num1m.Count; j++)
                    {
                        if (num1m[0] != -1 && Isequal(num1m[j], num2, pltr3[i], oper))
                        {
                            solvenum++;
                            addset(num1m[j], num2, pltr3[i], oper);
                        }
                    }
                }
            }
            if (mitr3[0] != -1)
            {
                for (int i = 0; i < mitr3.Count; i++)
                {
                    if (oper == 1 && Isequal(num1, num2, mitr3[i], 0))
                    {
                        solvenum++;
                        addset(num1, num2, mitr3[i], 0);
                    }
                    for (int j = 0; j < num2p.Count; j++)
                    {
                        if (num2p[0] != -1 && Isequal(num1, num2p[j], mitr3[i], oper))
                        {
                            solvenum++;
                            addset(num1, num2p[j], mitr3[i], oper);
                        }
                    }
                    for (int j = 0; j < num1p.Count; j++)
                    {
                        if (num1p[0] != -1 && Isequal(num1p[j], num2, mitr3[i], oper))
                        {
                            solvenum++;
                            addset(num1p[j], num2, mitr3[i], oper);
                        }
                    }
                }

            }
            //特殊情况，加号变乘号以及1-4,2-8,3-7,4-9(无法通过变换一根转移过去)
            //同时考虑乘号变减号与减号变乘号两种特殊情况
            if (oper == 2)
            {
                if (Isequal(num1, num2, result, oper - 2))
                {
                    solvenum++;
                    addset(num1, num2, result, oper - 2);
                }
                if (num1p[0] != -1)
                {
                    for (int i = 0; i < num1p.Count; i++)
                    {
                        if (Isequal(num1p[i], num2, result, 1))
                        {
                            solvenum++;
                            addset(num1p[i], num2, result, 1);
                        }
                    }
                }
                if (num2p[0] != -1)
                {
                    for (int i = 0; i < num2p.Count; i++)
                    {
                        if (Isequal(num1, num2p[i], result, 1))
                        {
                            solvenum++;
                            addset(num1, num2p[i], result, 1);
                        }
                    }

                }
                if (resultp[0] != -1)
                {
                    for (int i = 0; i < resultp.Count; i++)
                    {
                        if (Isequal(num1, num2, resultp[i], 1))
                        {
                            solvenum++;
                            addset(num1, num2, resultp[i], 1);
                        }
                    }
                }
            }
            if (oper == 1)
            {
                if (num1m[0] != -1)
                {
                    for (int i = 0; i < num1m.Count; i++)
                    {
                        if (Isequal(num1m[i], num2, result, 2))
                        {
                            solvenum++;
                            addset(num1m[i], num2, result, 2);
                        }
                    }
                }
                if (num2m[0] != -1)
                {
                    for (int i = 0; i < num2m.Count; i++)
                    {
                        if (Isequal(num1, num2m[i], result, 2))
                        {
                            solvenum++;
                            addset(num1, num2m[i], result, 2);
                        }
                    }
                }
                if (resultm[0] != -1)
                {
                    for (int i = 0; i < resultm.Count; i++)
                    {
                        if (Isequal(num1, num2, resultm[i], 2))
                        {
                            solvenum++;
                            addset(num1, num2, resultm[i], 2);
                        }
                    }
                }
            }
            if (oper == 0)
            {
                if (Isequal(num1, num2, result, oper + 2))
                {
                    solvenum++;
                    addset(num1, num2, result, oper + 2);
                }
                if (num1p[0] != -1)
                {
                    for (int i = 0; i < num1p.Count; i++)
                    {
                        num1 = num1p[i];
                        oper = 1;
                        createlib();
                        search1();
                        recover();
                        createlib();
                    }
                }
                if (num2p[0] != -1)
                {
                    for (int i = 0; i < num2p.Count; i++)
                    {
                        num2 = num2p[i];
                        oper = 1;
                        createlib();
                        search1();
                        recover();
                        createlib();
                    }
                }
                if (resultp[0] != -1)
                {
                    for (int i = 0; i < resultp.Count; i++)
                    {
                        result = resultp[i];
                        oper = 1;
                        createlib();
                        search1();
                        recover();
                        createlib();
                    }
                }

            }
            if (n1keep[0] != -1)
            {
                for (int i = 0; i < n1keep.Count; i++)
                {
                    if (Isequal(n1keep[i], num2, result, oper))
                    {
                        solvenum++;
                        addset(n1keep[i], num2, result, oper);
                    }
                }
            }
            if (n2keep[0] != -1)
            {
                for (int i = 0; i < n2keep.Count; i++)
                {
                    if (Isequal(num1, n2keep[i], result, oper))
                    {
                        solvenum++;
                        addset(num1, n2keep[i], result, oper);
                    }
                }
            }
            if (rekeep[0] != -1)
            {
                for (int i = 0; i < rekeep.Count; i++)
                {
                    if (Isequal(num1, num2, rekeep[i], oper))
                    {
                        solvenum++;
                        addset(num1, num2, rekeep[i], oper);
                    }
                }
            }
            if (n1m[0] != -1)
            {

                for (int i = 0; i < n1m.Count; i++)
                {
                    for (int j = 0; j < n2p.Count; j++)
                    {
                        if (n2p[j] != -1 && Isequal(n1m[i], n2p[j], result, oper))
                        {
                            solvenum++;
                            addset(n1m[i], n2p[j], result, oper);
                        }
                    }
                    for (int j = 0; j < rep.Count; j++)
                    {
                        if (rep[j] != -1 && Isequal(n1m[i], num2, rep[j], oper))
                        {
                            solvenum++;
                            addset(n1m[i], num2, rep[j], oper);
                        }
                    }
                    for (int j = 0; j < num2p.Count; j++)
                    {
                        for (int k = 0; k < resultp.Count; k++)
                        {
                            if (num2p[j] != -1 && resultp[k] != -1 && Isequal(n1m[i], num2p[j], resultp[k], oper))
                            {
                                solvenum++;
                                addset(n1m[i], num2p[j], resultp[k], oper);
                            }
                            if (oper == 1 && num2p[j] != -1 && Isequal(n1m[i], num2p[j], result, oper - 1))
                            {
                                solvenum++;
                                addset(n1m[i], num2p[j], result, oper - 1);
                            }
                            if (oper == 1 && resultp[k] != -1 && Isequal(n1m[i], num2, resultp[k], oper - 1))
                            {
                                solvenum++;
                                addset(n1m[i], num2, resultp[k], oper - 1);
                            }
                        }
                    }
                    //或变符号

                }
            }
            if (n2m[0] != -1)
            {

                for (int i = 0; i < n2m.Count; i++)
                {
                    for (int j = 0; j < n1p.Count; j++)
                    {
                        if (n1p[j] != -1 && Isequal(n1p[j], n2m[i], result, oper))
                        {
                            solvenum++;
                            addset(n1p[j], n2m[i], result, oper);
                        }
                    }
                    for (int j = 0; j < rep.Count; j++)
                    {
                        if (rep[j] != -1 && Isequal(num1, n2m[i], rep[j], oper))
                        {
                            solvenum++;
                            addset(num1, n2m[i], rep[j], oper);
                        }
                    }
                    for (int j = 0; j < num1p.Count; j++)
                    {
                        for (int k = 0; k < resultp.Count; k++)
                        {
                            if (num1p[j] != -1 && resultp[k] != -1 && Isequal(num1p[j], n2m[i], resultp[k], oper))
                            {
                                solvenum++;
                                addset(num1p[j], n2m[i], resultp[k], oper);
                            }
                            if (oper == 1 && num1p[j] != -1 && Isequal(num1p[j], n2m[i], result, oper - 1))
                            {
                                solvenum++;
                                addset(num1p[j], n2m[i], result, oper - 1);
                            }
                            if (oper == 1 && resultp[k] != -1 && Isequal(num1, n2m[i], resultp[k], oper - 1))
                            {
                                solvenum++;
                                addset(num1, n2m[i], resultp[k], oper - 1);
                            }
                        }
                    }

                }
            }
            if (rem[0] != -1)
            {

                for (int i = 0; i < rem.Count; i++)
                {
                    for (int j = 0; j < n1p.Count; j++)
                    {
                        if (n1p[0] != -1 && Isequal(n1p[j], num2, rem[i], oper))
                        {
                            solvenum++;
                            addset(n1p[j], num2, rem[i], oper);
                        }
                    }
                    for (int j = 0; j < n2p.Count; j++)
                    {
                        if (n2p[0] != -1 && Isequal(num1, n2p[j], rem[i], oper))
                        {
                            solvenum++;
                            addset(num1, n2p[j], rem[i], oper);
                        }
                    }
                    for (int j = 0; j < num1p.Count; j++)
                    {
                        for (int k = 0; k < num2p.Count; k++)
                        {
                            if (num1p[0] != -1 && num2p[0] != -1 && Isequal(num1p[j], num2p[k], rem[i], oper))
                            {
                                solvenum++;
                                addset(num1p[j], num2p[k], rem[i], oper);
                            }
                            if (oper == 1 && num1p[0] != -1 && Isequal(num1p[j], num2, rem[i], oper - 1))
                            {
                                solvenum++;
                                addset(num1p[j], num2, rem[i], oper - 1);
                            }
                            if (oper == 1 && num2p[0] != -1 && Isequal(num1, num2p[k], rem[i], oper - 1))
                            {
                                solvenum++;
                                addset(num1, num2p[k], rem[i], oper - 1);
                            }
                        }
                    }

                }
            }
            if (n1p[0] != -1)
            {
                for (int i = 0; i < n1p.Count; i++)
                    for (int j = 0; j < num2m.Count; j++)
                        for (int k = 0; k < resultm.Count; k++)
                        {
                            if (num2m[j] != -1 && resultm[k] != -1 && Isequal(n1p[i], num2m[j], resultm[k], oper))
                            {
                                solvenum++;
                                addset(n1p[i], num2m[j], resultm[k], oper); ;
                            }
                            if (oper == 0 && num2m[j] != -1 && Isequal(n1p[i], num2m[j], result, oper + 1))
                            {
                                solvenum++;
                                addset(n1p[i], num2m[j], result, oper + 1);
                            }
                            if (oper == 0 && resultm[k] != -1 && Isequal(n1p[i], num2, resultm[k], oper + 1))
                            {
                                solvenum++;
                                addset(n1p[i], num2, resultm[k], oper + 1);
                            }
                        }
            }
            if (n2p[0] != -1)
            {
                for (int i = 0; i < n2p.Count; i++)
                    for (int j = 0; j < num1m.Count; j++)
                        for (int k = 0; k < resultm.Count; k++)
                        {
                            if (num1m[j] != -1 && resultm[k] != -1 && Isequal(num1m[j], n2p[i], resultm[k], oper))
                            {
                                solvenum++;
                                addset(num1m[j], n2p[i], resultm[k], oper); ;
                            }
                            if (oper == 0 && num1m[j] != -1 && Isequal(num1m[j], n2p[i], result, oper + 1))
                            {
                                solvenum++;
                                addset(num1m[j], n2p[i], result, oper + 1);
                            }
                            if (oper == 0 && resultm[k] != -1 && Isequal(num1, n2p[i], resultm[k], oper + 1))
                            {
                                solvenum++;
                                addset(num1, n2p[i], resultm[k], oper + 1);
                            }
                        }
            }
            if (rep[0] != -1)
            {
                for (int i = 0; i < rep.Count; i++)
                    for (int j = 0; j < num2m.Count; j++)
                        for (int k = 0; k < num1m.Count; k++)
                        {
                            if (num2m[j] != -1 && num1m[k] != -1 && Isequal(num1m[k], num2m[j], rep[i], oper))
                            {
                                solvenum++;
                                addset(num1m[k], num2m[j], rep[i], oper); ;
                            }
                            if (oper == 0 && num1m[k] != -1 && Isequal(num1m[k], num2, rep[i], oper + 1))
                            {
                                solvenum++;
                                addset(num1m[k], num2, rep[i], oper + 1);
                            }
                            if (oper == 0 && num2m[j] != -1 && Isequal(num1, num2m[j], rep[i], oper + 1))
                            {
                                solvenum++;
                                addset(num1, num2m[k], rep[i], oper + 1);
                            }
                        }
            }

            if (num1k[0] != -1)
            {
                for (int i = 0; i < num1k.Count; i++)
                {
                    num1 = num1k[i];
                    createlib();
                    search1();
                    recover();
                    createlib();
                }
            }
            if (num2k[0] != -1)
            {
                for (int i = 0; i < num2k.Count; i++)
                {
                    num2 = num2k[i];
                    createlib();
                    search1();
                    recover();
                    createlib();
                }
            }
            if (resultk[0] != -1)
            {
                for (int i = 0; i < resultk.Count; i++)
                {
                    result = resultk[i];
                    createlib();
                    search1();
                    recover();
                    createlib();
                }
            }
            if (num1m[0] != -1)
            {
                for (int i = 0; i < num1m.Count; i++)
                {
                    if (num2p[0] != -1)
                    {
                        for (int j = 0; j < num2p.Count; j++)
                        {
                            num1 = num1m[i];
                            num2 = num2p[j];
                            createlib();
                            search1();
                            recover();
                            createlib();
                        }
                    }
                    if (resultp[0] != -1)
                    {
                        for (int j = 0; j < resultp.Count; j++)
                        {
                            num1 = num1m[i];
                            result = resultp[j];
                            createlib();
                            search1();
                            recover();
                            createlib();
                        }
                    }
                    if (oper == 1)
                    {
                        num1 = num1m[i];
                        oper = 0;
                        createlib();
                        search1();
                        recover();
                        createlib();
                    }
                }

            }
            if (num2m[0] != -1)
            {
                for (int i = 0; i < num2m.Count; i++)
                {
                    if (num1p[0] != -1)
                    {
                        for (int j = 0; j < num1p.Count; j++)
                        {
                            num1 = num1p[j];
                            num2 = num2m[i];
                            createlib();
                            search1();
                            recover();
                            createlib();
                        }
                    }
                    if (resultp[0] != -1)
                    {
                        for (int j = 0; j < resultp.Count; j++)
                        {
                            num2 = num2m[i];
                            result = resultp[j];
                            createlib();
                            search1();
                            recover();
                            createlib();
                        }
                    }
                    if (oper == 1)
                    {
                        num2 = num2m[i];
                        oper = 0;
                        createlib();
                        search1();
                        recover();
                        createlib();
                    }
                }
            }
            if (resultm[0] != -1)
            {
                for (int i = 0; i < resultm.Count; i++)
                {
                    if (num2p[0] != -1)
                    {
                        for (int j = 0; j < num2p.Count; j++)
                        {
                            result = resultm[i];
                            num2 = num2p[j];
                            createlib();
                            search1();
                            recover();
                            createlib();
                        }
                    }
                    if (num1p[0] != -1)
                    {
                        for (int j = 0; j < num1p.Count; j++)
                        {
                            num1 = num1p[j];
                            result = resultm[i];
                            createlib();
                            search1();
                            recover();
                            createlib();
                        }
                    }
                    if (oper == 1)
                    {
                        result = resultm[i];
                        oper = 0;
                        createlib();
                        search1();
                        recover();
                        createlib();
                    }
                }
            }
            if (solvenum != 0) return true;
            else return false;
        }

        public void recover()                     //恢复到原来的式子
        {
            num1 = ornum1;
            num2 = ornum2;
            oper = oroper;
            result = orresult;
            num1k = new List<int>() { -1 };
            num1p = new List<int>() { -1 };
            num1m = new List<int>() { -1 };
            num2k = new List<int>() { -1 };
            num2p = new List<int>() { -1 };
            num2m = new List<int>() { -1 };
            resultk = new List<int>() { -1 };
            resultp = new List<int>() { -1 };
            resultm = new List<int>() { -1 };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
            readnum();
            chart();
            createlib();
            if (!search1())
            {
                MessageBox.Show("无解", "结果", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("有" + solvenum + "种解\n"+"搜索深度为"+depth+","+difficulty(depth), "结果", MessageBoxButtons.OK);
                shownum(solveset[0], solveset[1], solveset[2], solveset[3]);
                this.button3.Enabled = true;
                string op = "+-*";
                listBox1.Items.Clear();
                for (int i = 0; i < solvenum; i++)
                {
                    string exps = solveset[4 * i].ToString() + op[solveset[4 * i + 3]] + solveset[4 * i + 1].ToString() + '=' + solveset[4 * i + 2].ToString();
                    listBox1.Items.Add(exps);
                }
                this.label3.Text = null;
            }

        }
        public void init()
        {
            num1 = 0;
            num2 = 0;
            result = 0;
            oper = 0;    //运算类型，0,1,2分别代表加、减、乘
            ornum1 = 0;
            ornum2 = 0;
            orresult = 0;
            oroper = 0;
            solvenum = 0;  //解决方案数目
            cursolve = 0;
            mami = 0;
            mapl = 0;
            depth = 0;
            num1k = new List<int>() { -1 };
            num1p = new List<int>() { -1 };
            num1m = new List<int>() { -1 };
            num2k = new List<int>() { -1 };
            num2p = new List<int>() { -1 };
            num2m = new List<int>() { -1 };
            resultk = new List<int>() { -1 };
            resultp = new List<int>() { -1 };
            resultm = new List<int>() { -1 };
            n1keep = new List<int>() { -1 };
            n1p = new List<int>() { -1 };
            n1m = new List<int>() { -1 };
            n2keep = new List<int>() { -1 };
            n2p = new List<int>() { -1 };
            n2m = new List<int>() { -1 };
            rekeep = new List<int>() { -1 };
            rep = new List<int>() { -1 };
            rem = new List<int>() { -1 };
            pltr1 = new List<int>() { -1 };
            pltr2 = new List<int>() { -1 };
            pltr3 = new List<int>() { -1 };
            mitr1 = new List<int>() { -1 };
            mitr2 = new List<int>() { -1 };
            mitr3 = new List<int>() { -1 };
            solveset = new List<int>();     //可能的解集     
        }
        public void showanswer()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            init();
            readnum();
            ornum1 = num1;
            ornum2 = num2;
            oroper = oper;
            orresult = result;
            chart();
            createlib();
            movetwo();
            if (!search2())
            {
                MessageBox.Show("无解", "结果", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("有" + solvenum + "种解\n" + "搜索深度为" + depth + "," + difficulty(depth), "结果", MessageBoxButtons.OK);
                this.button3.Enabled = true;
                shownum(solveset[0], solveset[1],solveset[2],solveset[3]);
                string op = "+-*";
                listBox1.Items.Clear();
                for (int i = 0; i < solvenum; i++)
                {
                string exps = solveset[4 * i].ToString() + op[solveset[4 * i + 3]] + solveset[4 * i + 1].ToString() + '=' + solveset[4 * i + 2].ToString();
                listBox1.Items.Add(exps);
                }
                this.label3.Text = null;
            }
            
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.ShowDialog();
            num1 = fm2.getn1();
            num2 = fm2.getn2();
            result = fm2.getre();
            oper = fm2.getop();
            creatbtns();
            shownum(num1, num2, result, oper);
            this.button4.Enabled = false;
            movenum = 0;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            orp = e.Location;
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.btn14.Location = new Point(this.btn14.Left + (e.X - orp.X), this.btn14.Top + (e.Y - orp.Y));
            this.btn14.Visible = true;
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (e.Button == MouseButtons.Left)
            {
                if (btn.BackgroundImage != null)
                {
                    btn.BackgroundImage = null;
                    mami++;
                    if (checkmove() == false)
                    {
                        if (btn.Size == this.btn04.Size)
                            btn.BackgroundImage = Properties.Resources.matchstick3;
                        else
                            btn.BackgroundImage = Properties.Resources.matchstickho;
                    }
                }
                else
                {
                    if (btn.Size == this.btn04.Size)
                        btn.BackgroundImage = Properties.Resources.matchstick3;
                    else
                        btn.BackgroundImage = Properties.Resources.matchstickho;
                    mapl++;
                    if (checkmove()==false)
                    {
                        btn.BackgroundImage = null;
                    }
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //处理加号与乘号变化
            if (e.Button == MouseButtons.Left)
            {
                if (this.btnop1.BackgroundImage != null && this.btnop2.BackgroundImage != null)
                {
                    //加号变乘号
                    this.pictureBox1.BackgroundImage = Properties.Resources.matchstickmul_jpg;
                    this.btnop1.BackgroundImage = null;
                    this.btnop2.BackgroundImage = null;
                    this.btnop2.Parent = this.pictureBox1;
                    this.btnop1.Parent = this.pictureBox1;
                    this.btnop2.BackColor = Color.Transparent;
                    this.btnop1.BackColor = Color.Transparent;
                    mami = 2;
                    mapl = 2;
                    checkmove();

                }
                else
                {
                    if (this.pictureBox1.BackgroundImage != null)
                    {
                        //乘号变回去时变成减号，模仿减一移一操作
                        this.btnop1.Parent = this;
                        this.btnop2.Parent = this;
                        this.btnop1.BackColor = Color.White;
                        this.btnop2.BackColor = Color.White;
                        this.pictureBox1.SendToBack();
                        this.pictureBox1.BackgroundImage = null;
                        this.btnop1.BackgroundImage = Properties.Resources.matchstickho;
                        this.btnop2.BackgroundImage = null;
                        mami = 2;
                        mapl = 1;
                    }
                    if (this.btnop1.BackgroundImage != null&&mami==0)
                    {
                        //考虑减号变乘号的情况
                        this.pictureBox1.BackgroundImage = Properties.Resources.matchstickmul_jpg;
                        this.btnop1.BackgroundImage = null;
                        this.btnop2.BackgroundImage = null;
                        this.btnop2.Parent = this.pictureBox1;
                        this.btnop1.Parent = this.pictureBox1;
                        this.btnop2.BackColor = Color.Transparent;
                        this.btnop1.BackColor = Color.Transparent;
                        mapl = 2;
                        mami = 1;
                    }
                }
            }
        }
        private void creatbtns()
        {
            //将按钮存入按钮数组中方便调用
            btar = new Button[6,7];
            btar[0, 0] = this.btn00;
            btar[0, 1] = this.btn01;
            btar[0, 2] = this.btn02;
            btar[0, 3] = this.btn03;
            btar[0, 4] = this.btn04;
            btar[0, 5] = this.btn05;
            btar[0, 6] = this.btn06;
            btar[1, 0] = this.btn10;
            btar[1, 1] = this.btn11;
            btar[1, 2] = this.btn12;
            btar[1, 3] = this.btn13;
            btar[1, 4] = this.btn14;
            btar[1, 5] = this.btn15;
            btar[1, 6] = this.btn16;
            btar[2, 0] = this.btn20;
            btar[2, 1] = this.btn21;
            btar[2, 2] = this.btn22;
            btar[2, 3] = this.btn23;
            btar[2, 4] = this.btn24;
            btar[2, 5] = this.btn25;
            btar[2, 6] = this.btn26;
            btar[3, 0] = this.btn30;
            btar[3, 1] = this.btn31;
            btar[3, 2] = this.btn32;
            btar[3, 3] = this.btn33;
            btar[3, 4] = this.btn34;
            btar[3, 5] = this.btn35;
            btar[3, 6] = this.btn36;
            btar[4, 0] = this.btn40;
            btar[4, 1] = this.btn41;
            btar[4, 2] = this.btn42;
            btar[4, 3] = this.btn43;
            btar[4, 4] = this.btn44;
            btar[4, 5] = this.btn45;
            btar[4, 6] = this.btn46;
            btar[5, 0] = this.btn50;
            btar[5, 1] = this.btn51;
            btar[5, 2] = this.btn52;
            btar[5, 3] = this.btn53;
            btar[5, 4] = this.btn54;
            btar[5, 5] = this.btn55;
            btar[5, 6] = this.btn56;

        }
        private bool readnum()
        {
            //读取当前所代表的数字
            int[] nua = new int[6] {-1,-1,-1,-1,-1,-1};
            currnum = new int[6, 7];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                {
                    if (btar[i, j].BackgroundImage != null)
                        currnum[i, j] = 1;
                }
            for (int i = 0; i < 6; i++)
            {
                nua[i] = matchtonum(currnum[i, 0], currnum[i, 1], currnum[i, 2], currnum[i, 3], currnum[i, 4], currnum[i, 5], currnum[i, 6]);
                if (nua[i] == -1) return false;
            }
            if (this.btnop1.BackgroundImage != null)
            {
                if (this.btnop2.BackgroundImage != null) oper = 0;
                else oper = 1;
            }
            else
            {
                if (this.btnop2.BackgroundImage != null) return false;
                else oper = 2;
            }
            num1 = nua[0] * 10 + nua[1];
            num2 = nua[2] * 10 + nua[3];
            result = nua[4] * 10 + nua[5];
            return true;
        }
        private void shownum(int n1,int n2,int re,int op)
        {
            //用火柴棍显示数字
            int[] cur = new int[6];
            cur[0] = n1 / 10;
            cur[1] = n1 % 10;
            cur[2] = n2 / 10;
            cur[3] = n2 % 10;
            cur[4] = re / 10;
            cur[5] = re % 10;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (matchnum[cur[i], j] == 1)
                    {
                        if (btar[i, j].Size == this.btn04.Size)
                            btar[i, j].BackgroundImage = Properties.Resources.matchstick3;
                        else
                            btar[i, j].BackgroundImage = Properties.Resources.matchstickho;
                    }
                    else
                    {
                        btar[i, j].BackgroundImage = null;
                    }
                }
            }
            if (op == 2)
            {
                this.pictureBox1.BackgroundImage = Properties.Resources.matchstickmul_jpg;
                this.btnop1.BackgroundImage = null;
                this.btnop2.BackgroundImage = null;
                this.btnop2.Parent = this.pictureBox1;
                this.btnop1.Parent = this.pictureBox1;
                this.btnop2.BackColor = Color.Transparent;
                this.btnop1.BackColor = Color.Transparent;
            }
            else
            {
                this.btnop1.Parent = this;
                this.btnop2.Parent = this;
                this.btnop1.BackColor = Color.White;
                this.btnop2.BackColor = Color.White;
                this.pictureBox1.SendToBack();
                this.pictureBox1.BackgroundImage = null;
                if (op == 0) this.btnop2.BackgroundImage = Properties.Resources.matchstick3;
                else this.btnop2.BackgroundImage = null;
                this.btnop1.BackgroundImage = Properties.Resources.matchstickho;
            }
        }

        private void helpform_Click(object sender, EventArgs e)
        {
            Form3 fm = new Form3();
            fm.ShowDialog();
        }
        private string difficulty(int dep)
        {
            if (dep <= 500) return "简单";
            else if (dep <= 800) return "普通";
            else return "困难";
        }
        private string diffc(int level)
        {
            if (level ==0) return "简单";
            else if (level ==1) return "普通";
            else return "困难";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (solvenum > 0)
            {
                if (solvenum == 1)
                    MessageBox.Show("只有一种解哦。", "提示", MessageBoxButtons.OK);
                else
                {
                    cursolve++;
                    if (cursolve >= solvenum) cursolve = 0;
                    shownum(solveset[4*cursolve], solveset[4 * cursolve+1], solveset[4 * cursolve+2], solveset[4 * cursolve+3]);
                }
            }
        }
        private void produceproblem(int level, int num)
        {
            //两个参数，分别是难度和移动火柴的数量
            int n1,n2,re,op;
            int count = -1;
            Random r = new Random();
            while (count < 19)
            {
                init();
                n1 = r.Next(0,100);
                n2 = r.Next(0, 100);
                re = r.Next(0, 100);
                op = r.Next(0, 3);
                num1 = n1;
                num2 = n2;
                result = re;
                oper = op;
                ornum1 = num1;
                ornum2 = num2;
                oroper = oper;
                orresult = result;
                createlib();
                if (num == 1)
                {
                    if (search1())
                    {                        
                        count++;
                        questionbank[count, 0] = n1;
                        questionbank[count, 1] = n2;
                        questionbank[count, 2] = re;
                        questionbank[count, 3] = op;
                        if (depth < 500) questionlevel[count] = 0;
                        if (depth < 800&&depth>=500) questionlevel[count] = 1;
                        if (depth >= 800) questionlevel[count] = 2;
                    }
                }
                if (num == 2)
                {
                    movetwo();
                    if (search2())
                    {
                        count++;
                        questionbank[count, 0] = n1;
                        questionbank[count, 1] = n2;
                        questionbank[count, 2] = re;
                        questionbank[count, 3] = op;
                        if (depth < 500) questionlevel[count] = 0;
                        if (depth < 800 && depth >= 500) questionlevel[count] = 1;
                        if (depth >= 800) questionlevel[count] = 2;
                    }
                }
            }
        }

        private void ea1_Click(object sender, EventArgs e)
        {
            produceproblem(0,1);
            movenum = 1;
            shownum(questionbank[0,0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            cursolve = 0;
        }

        private void ea2_Click(object sender, EventArgs e)
        {
            produceproblem(0, 2);
            movenum = 2;
            shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            cursolve = 0;
        }

        private void si1_Click(object sender, EventArgs e)
        {
            produceproblem(1, 1);
            movenum = 1;
            shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            cursolve = 0;
        }

        private void si2_Click(object sender, EventArgs e)
        {
            produceproblem(1, 2);
            movenum = 2;
            shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            cursolve = 0;
        }

        private void di1_Click(object sender, EventArgs e)
        {
            produceproblem(2, 1);
            movenum = 1;
            shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            cursolve = 0;
        }

        private void di2_Click(object sender, EventArgs e)
        {
            produceproblem(2, 2);
            movenum = 2;
            shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            cursolve = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mami = 0;//还原火柴移动数
            mapl = 0;
            cursolve++;
            if (cursolve >= 20)
            {
                shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
                this.label3.Text = diffc(questionlevel[0]);
                cursolve = 0;
            }
            else
            {
                shownum(questionbank[cursolve, 0], questionbank[cursolve, 1], questionbank[cursolve, 2], questionbank[cursolve, 3]);
                this.label3.Text = diffc(questionlevel[cursolve]);
            }
        }
        private bool checkmove()
        {
            //检查目前移动了火柴的根数
            if (movenum == 1)
            {
                if (mami == 1 && mapl == 1)
                {
                    if (readnum()==false)
                    {
                        MessageBox.Show("答案不对哦。", "消息", MessageBoxButtons.OK);
                        shownum(ornum1, ornum2, orresult, oroper);
                    }
                    else
                    {
                        if (Isequal(num1, num2, result, oper))
                        {
                            MessageBox.Show("恭喜你，做对了哦！", "消息", MessageBoxButtons.OK);
                            shownum(ornum1, ornum2, orresult, oroper);
                        }
                        else
                        {
                            MessageBox.Show("遗憾，你没有做对啊。", "消息", MessageBoxButtons.OK);
                            shownum(ornum1, ornum2, orresult, oroper);
                        }
                    }
                    mapl = 0;
                    mami = 0;
                }
                if (mami > 1)
                {
                    MessageBox.Show("不能再移动火柴了！", "消息", MessageBoxButtons.OK);
                    mami = 1;
                    return false;
                }
                if (mapl > 1)
                {
                    MessageBox.Show("不能再移动火柴了！", "消息", MessageBoxButtons.OK);
                    mapl = 1;
                    return false;
                }
            }
            if (movenum == 2)
            {
                if (mami == 2 && mapl == 2)
                {
                    if (readnum() == false)
                    {
                        MessageBox.Show("答案不对哦。", "消息", MessageBoxButtons.OK);
                        shownum(ornum1, ornum2, orresult, oroper);
                    }
                    else
                    {
                        if (Isequal(num1, num2, result, oper))
                        {
                            MessageBox.Show("恭喜你，做对了哦！", "消息", MessageBoxButtons.OK);
                            shownum(ornum1, ornum2, orresult, oroper);
                        }
                        else
                        {
                            MessageBox.Show("遗憾，你没有做对啊。", "消息", MessageBoxButtons.OK);
                            shownum(ornum1, ornum2, orresult, oroper);
                        }
                    }
                    mapl = 0;
                    mami = 0;
                }
                if (mami > 2)
                {
                    MessageBox.Show("不能再移动火柴了！", "消息", MessageBoxButtons.OK);
                    mami = 2;
                    return false;
                }
                if (mapl > 2)
                {
                    MessageBox.Show("不能再移动火柴了！", "消息", MessageBoxButtons.OK);
                    mapl = 2;
                    return false;
                }
            }
            return true;
        }

        private void easy_Click(object sender, EventArgs e)
        {
            this.button4.Enabled = true;
            produceproblem(0, 1);
            movenum = 1;
            shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            this.label3.Text = diffc(questionlevel[0]);
            cursolve = 0;
        }

        private void simple_Click(object sender, EventArgs e)
        {
            this.button4.Enabled = true;
            produceproblem(1, 2);
            movenum = 2;
            shownum(questionbank[0, 0], questionbank[0, 1], questionbank[0, 2], questionbank[0, 3]);
            this.label3.Text = diffc(questionlevel[0]);
            cursolve = 0;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

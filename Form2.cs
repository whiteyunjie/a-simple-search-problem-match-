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
    public partial class Form2 : Form
    {
        int num1 = 0;
        int num2 = 0;
        int result = 0;
        int oper = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string formu = textBox1.Text;
            if (!checkform())
            {
                MessageBox.Show("格式错误，请重新输入！", "提示", MessageBoxButtons.OK);
                textBox1.Clear();
            }
            else
            {
                this.Close();
            }
        }
        public bool checkform()
        {
            string func = textBox1.Text;
            string oper1 = "0";
            string oper2 = "0";
            string oper3 = "0";
            int pro = 0;
            if (func.Length < 5) return false;
            if (func[0] < 48 || func[0] > 57) return false;
            for (int i = 1; i < func.Length; i++)
            {
                if (pro == 0 && (func[i] == 42 || func[i] == 43 || func[i] == 45))
                {
                    if (func[i] == 42) oper = 2;
                    if (func[i] == 43) oper = 0;
                    if (func[i] == 45) oper = 1;
                    oper1 = func.Substring(0, i);
                    pro++;
                }
                else if (pro == 1 && func[i] == 61)
                {
                    oper2 = func.Substring(oper1.Length + 1, i - oper1.Length - 1);
                    oper3 = func.Substring(i + 1, func.Length - i - 1);
                }
                else if (func[i] < 48 || func[i] > 57) return false;
            }
            for (int i = 0; i < oper1.Length; i++)
            {
                num1 = num1 * 10 + oper1[i] - '0';
            }
            for (int i = 0; i < oper2.Length; i++)
            {
                num2 = num2 * 10 + oper2[i] - '0';
            }
            for (int i = 0; i < oper3.Length; i++)
            {
                result = result * 10 + oper3[i] - '0';
            }
            return true;
        }
        public int getn1()
        {
            return num1;
        }
        public int getn2()
        {
            return num2;
        }
        public int getre()
        {
            return result;
        }
        public int getop()
        {
            return oper;
        }
    }
}

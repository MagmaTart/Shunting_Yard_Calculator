using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shunting_Yard_Algorithm
{
    public partial class Form1 : Form
    {
        private String Equation = "";
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            formulaText.Text = Equation;
        }

        public void Form1_KeyPress(Object Sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                if (!Equation.Equals(""))
                    Equation = Equation.Remove(Equation.Length - 1, 1);
            }
            else if ((Keys)e.KeyChar == Keys.Enter)
            {
                //Press Enter to Calculate
                Formula formula = new Formula(Equation);

                decimal Result = formula.Calculate();
                answerText.Text = "Result : " + Result.ToString();
            }
            else
                Equation += e.KeyChar.ToString();
            formulaText.Text = Equation;
        }
    }
}

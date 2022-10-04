using System;
using FunctionParser;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DichotomyMethod
{
    public partial class Form1 : Form
    {
        string[] idsNames = new string[10];
        double[] idsValues = new double[10];
        List<string> listNames = new List<string>();
        List<double> listValues = new List<double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        double Function(double x)
        {
            listNames.Clear();
            listValues.Clear();
            listNames.Add("x");
            idsNames = listNames.ToArray();
            listValues.Add(x);
            idsValues = listValues.ToArray();
            string Formula = FormulaField.Text;
            if (Expression.IsExpression(Formula, idsNames))
            {
                Expression expression = new Expression(Formula, idsNames, null);
                double result = expression.CalculateValue(idsValues);
                return result;
            }
            return 0;
        }

        void MethodDichotomy(double a, double b, double e)
        {
            double c;
            while (b - a > e)
            {
                c = (a + b) / 2;
                if (Function(b) * Function(c) < 0)
                {
                    a = c;
                }
                else
                {
                    b = c;
                }
            }
            textBox1.Text = Math.Round((a + b) / 2, 5).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //переменные
            double a = Convert.ToDouble(ValueA.Text);
            double b = Convert.ToDouble(ValueB.Text);
            double eps = Convert.ToDouble(ValueE.Text);

            MethodDichotomy(a, b, eps);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart.Series[0].Points.Clear();
            double a = Convert.ToDouble(ValueA.Text);
            double b = Convert.ToDouble(ValueB.Text);
            double h = Convert.ToDouble(ValueE.Text);
            double x;
            x = a;
            while (x <= b)
            {
                listNames.Add("x");
                idsNames = listNames.ToArray();
                listValues.Add(x);
                idsValues = listValues.ToArray();
                string Formula = FormulaField.Text;
                if (Expression.IsExpression(Formula, idsNames))
                {
                    Expression expression = new Expression(Formula, idsNames, null);
                    double y = expression.CalculateValue(idsValues);
                    chart.Series[0].Points.AddXY(x, y);
                    x += h;
                    listNames.Clear();
                    listValues.Clear();
                }
            }
        }
    }
}


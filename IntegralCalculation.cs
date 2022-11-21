using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegralCalculation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private double Function(double value)
        {
            Argument x = new Argument("x", value);
            Expression e = new Expression(FunctionField.Text, x);
            double result = e.calculate();
            return result;
        }

        private double Rectangle(double n, double a, double b)
        {
            double x, h, s, y;
            h = (b - a) / n; //шаг
            s = 0;
            for (x = a + h / 2; x < b; x += h)
            {
                y = Function(x);// подинтегральная функция
                s += y * h; // Элементарное приращение
            }
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double n = Convert.ToDouble(nField.Text);
            double a = Convert.ToDouble(FromField.Text);
            double b = Convert.ToDouble(ToField.Text);

            MessageBox.Show(Rectangle(n, a, b).ToString());
        }
    }
}

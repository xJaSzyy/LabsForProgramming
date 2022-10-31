using System;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;

namespace DichotomyMethodv2
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

        bool Check()
        {
            if (IntFrom.Text == "")
            {
                MessageBox.Show("Начальная точка интервала не задана");
                return false;
            }
            else if (IntTo.Text == "")
            {
                MessageBox.Show("Конечная точка интервала не задана");
                return false;
            }
            else if (Formula.Text == "")
            {
                MessageBox.Show("Формула не задана");
                return false;
            }
            else if (Convert.ToDouble(Accuracy.Text) == 0)
            {
                MessageBox.Show("Точность не задана");
                return false;
            }
            return true;
        }

        double Function(double value)
        {
            Argument x = new Argument("x = 1");
            x.setArgumentValue(value);
            Expression e = new Expression(Formula.Text, x);
            double result = e.calculate();
            return result;
        }

        void MethodDichotomy(double a, double b, double eps)
        {
            double c;
            while (b - a > eps)
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
            Result.Text = Math.Round((a + b) / 2, 5).ToString();
        }

        private void построитьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                chart.Series[0].Points.Clear();

                double a = Convert.ToDouble(IntFrom.Text);
                double b = Convert.ToDouble(IntTo.Text);
                double h = Convert.ToDouble(Accuracy.Text);

                Argument argx = new Argument($"x = {a}");
                double x = a;
                while (x <= b)
                {
                    Expression expy = new Expression(Formula.Text, argx);
                    double y = expy.calculate();
                    chart.Series[0].Points.AddXY(x, y);
                    x += h;
                    argx.setArgumentValue(x);
                }
            }
        }

        private void найтиМинимумToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                double a = Convert.ToDouble(IntFrom.Text);
                double b = Convert.ToDouble(IntTo.Text);
                double eps = Convert.ToDouble(Accuracy.Text);
                MethodDichotomy(a, b, eps);
            }
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IntFrom.Text = "";
            IntTo.Text = "";
            Accuracy.Text = "";
            Formula.Text = "";
            Result.Text = "";
        }

        private void IntFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 45; //только цифры
        }

        private void IntTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 45;//только цифры
        }
    }
}
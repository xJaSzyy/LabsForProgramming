using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using org.mariuszgromada.math.mxparser;
using static System.Collections.Specialized.BitVector32;

namespace GoldenSection
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

        private void построитьГрафикToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Check())
            {
                chart.Series[0].Points.Clear();
                chart.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "0.00";
                double a = Convert.ToDouble(IntFrom.Text);
                double b = Convert.ToDouble(IntTo.Text);
                double eps = Convert.ToDouble(Accuracy.Text);
                double h = 0.1;
                double y;

                GSS(a, b, eps);

                Argument argx = new Argument($"x = {a}");
                double x = a;
                while (x <= b)
                {
                    Expression expy = new Expression(Formula.Text, argx);
                    y = expy.calculate();
                    chart.Series[0].Points.AddXY(x, y);
                    x += h;
                    argx.setArgumentValue(x);
                }
                SetBoundaries();
            }
        }

        private void очиститьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            IntFrom.Text = "";
            IntTo.Text = "";
            Accuracy.Text = "";
            Formula.Text = "";
            ResultY.Text = "";
            ResultX.Text = "";
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
        }

        private void SetBoundaries()
        {
            double xmin = chart.Series[0].Points.FindMinByValue().XValue;
            double xmax = chart.Series[0].Points.FindMaxByValue().XValue;
            Argument argMin = new Argument("x", xmin);
            Argument argMax = new Argument("x", xmax);
            Expression expMin = new Expression(Formula.Text, argMin);
            Expression expMax = new Expression(Formula.Text, argMax);
            double ymin = expMin.calculate();
            double ymax = expMax.calculate();
            chart.ChartAreas[0].AxisY.Minimum = ymin - 0.5;
            chart.ChartAreas[0].AxisY.Maximum = ymax + 0.5;
        }

        private bool Check()
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
            else if (Convert.ToDouble(IntFrom.Text) > Convert.ToDouble(IntTo.Text))
            {
                MessageBox.Show("Интервал задан неверно");
                return false;
            }
            return true;
        }

        private double Function(double value)
        {
            Argument x = new Argument("x", value);
            Expression e = new Expression(Formula.Text, x);
            double result = e.calculate();
            return result;
        }

        private void DisplayExtremes(double x)
        {
            Argument argument = new Argument("x", x);
            Expression expression = new Expression(Formula.Text, argument);
            double y = expression.calculate();
            ResultY.Text = Math.Round(y, 2).ToString();
            chart.Series[1].Points.AddXY(Convert.ToDouble(ResultX.Text), Convert.ToDouble(ResultY.Text));
        }

        private void GoldenSectionSearch(double a, double b, double eps)
        {
            if (Check())
            {
                Random rnd = new Random();
                double x = rnd.Next((int)a, (int)b);
                if (Function(x) < Function(x + eps))
                {
                    x = x - eps;
                    while (Function(x) > Function(x - eps))
                    {
                        x = x - eps;
                    }
                }
                else
                {
                    x = x + eps;
                    while (Function(x) > Function(x + eps))
                    {
                        x = x + eps;
                    }
                }
                if (x < a || x > b)
                {
                    GoldenSectionSearch(a, b, eps);
                }
                else
                {
                    ResultX.Text = Math.Round(x, 2).ToString();
                    DisplayExtremes(Math.Round(x, 2));
                }
            }
        }

        private void GSS(double a, double b, double eps)
        {
            for (int i = 0; b - a > eps; i++)
            {
                double z = (3 - Math.Sqrt(5)) / 2;
                double x1 = a + z * (b - a), x2 = b - z * (b - a);
                if (Function(x1) <= Function(x2))
                {
                    b = x2;
                    x2 = x1;
                    x1 = a + b - x2;
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    x2 = a + b - x1;
                }
            }
            double x = (a + b) / 2;
            ResultX.Text = Math.Round(x, 2).ToString();
            ResultY.Text = Math.Round(Function(x), 2).ToString();
            DisplayExtremes(Math.Round(x, 2));
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

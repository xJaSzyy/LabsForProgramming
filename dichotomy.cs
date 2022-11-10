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
            else if (Math.Sign(Convert.ToDouble(IntFrom.Text)) == 1 && Math.Sign(Convert.ToDouble(IntTo.Text)) == -1)
            {
                MessageBox.Show("Интервал задан неверно");
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
            try
            {
                do
                {
                    if (Function(a) > Function(b))
                    {
                        a = a + (b - a) / 2;
                    }
                    else
                    {
                        b = a + (b - a) / 2;
                    }
                }
                while (Math.Abs(Function(b) - Function(a)) / 2 >= eps);
                Result.Text = Math.Round(Function(Function(b - a) / 2), (int)(eps * 100)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void построитьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                chart.Series[0].Points.Clear();

                double a = Convert.ToDouble(IntFrom.Text);
                double b = Convert.ToDouble(IntTo.Text);
                double eps = Convert.ToDouble(Accuracy.Text);
                double h = 0.1;
                double y, ymax = 0,ymin = 9999;

                MethodDichotomy(a, b, eps);

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
                ResultX.Text = chart.Series[0].Points.FindMinByValue().XValue.ToString();
                argx.setArgumentValue(Convert.ToDouble(ResultX.Text));
                Expression min = new Expression(Formula.Text, argx);
                ymin = min.calculate();
                Result.Text = Math.Round(ymin, (int)(eps * 100)).ToString();
                chart.Series[1].Points.Clear();
                chart.Series[1].Points.AddXY(Convert.ToDouble(ResultX.Text), Convert.ToDouble(Result.Text));
                argx.setArgumentValue(chart.Series[0].Points.FindMaxByValue().XValue);
                Expression max = new Expression(Formula.Text, argx);
                ymax = max.calculate();

                chart.ChartAreas[0].AxisY.Minimum = ymin;
                chart.ChartAreas[0].AxisY.Maximum = ymax;
            }
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IntFrom.Text = "";
            IntTo.Text = "";
            Accuracy.Text = "";
            Formula.Text = "";
            Result.Text = "";
            ResultX.Text = "";
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
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

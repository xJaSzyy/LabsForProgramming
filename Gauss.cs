using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumericalMethods
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

        private void button1_Click(object sender, EventArgs e)
        {  
            double[,] array = new double[dataGridView1.RowCount, dataGridView1.ColumnCount];
            double[] y = new double[dataGridView2.RowCount];

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    array[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
            }

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                y[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value);
            }

            double[] x = GaussMethod(array, y);

            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                dataGridView3.Rows[i].Cells[0].Value = x[i];
            }
        }

        private double[] GaussMethod(double[,] array, double[] y)
        {
            double[] x = new double[dataGridView1.RowCount];
            double max;
            double eps = 1e-9;
            int index, k = 0;
            int n = dataGridView1.RowCount;
            while (k < n)
            {
                max = Math.Abs(array[k, k]);
                index = k;
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(array[i, k]) > max)
                    {
                        max = Math.Abs(array[i, k]);
                        index = i;
                    }
                }

                if (max < eps)
                {
                    MessageBox.Show("Получить решение невозможно из-за нулевого столбца");
                }

                for (int j = 0; j < n; j++)
                {
                    double temp = array[k, j];
                    array[k, j] = array[index, j];
                    array[index, j] = temp;
                }
                double temp1 = y[k];
                y[k] = y[index];
                y[index] = temp1;

                for (int i = k; i < n; i++)
                {
                    double temp2 = array[i, k];
                    if (Math.Abs(temp2) < eps)
                    {
                        continue;
                    }
                    for (int j = 0; j < n; j++)
                    {
                        array[i, j] = array[i, j] / temp2;
                    }
                    y[i] = y[i] / temp2;
                    if (i == k)
                    {
                        continue;
                    }
                    for (int j = 0; j < n; j++)
                    {
                        array[i, j] = array[i, j] - array[k, j];
                    }
                    y[i] = y[i] - y[k];
                }
                k++;
            }
            for (k = n - 1; k >= 0; k--)
            {
                x[k] = y[k];
                for (int i = 0; i < k; i++)
                {
                    y[i] = y[i] - array[i, k] * x[k];
                }
            }
            return x;
        }

        private void RowCount_TextChanged(object sender, EventArgs e)
        {
            if (RowCount.Text != "")
            {
                dataGridView1.RowCount = int.Parse(RowCount.Text);
                dataGridView2.RowCount = int.Parse(RowCount.Text);
                dataGridView3.RowCount = int.Parse(RowCount.Text);
            }
        }

        private void ColumnCount_TextChanged(object sender, EventArgs e)
        {
            if (ColumnCount.Text != "")
            {
                dataGridView1.ColumnCount = int.Parse(ColumnCount.Text);
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Name = "x" + (i+1).ToString();
                }
            }
        }
    }
}

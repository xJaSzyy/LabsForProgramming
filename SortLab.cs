using Bogosort;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sorting
{
    public partial class Form1 : Form
    {
        int[] Array, Array1, Array2, Array3, Array4, Array5;
        List<int> ArrayList = new List<int>();
        string Millisecond = "ms";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void отсортироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGrid.AllowUserToAddRows = false;

            DataGridToArray(); //представление таблицы данных в массив

            SortingSelection(); //выбор сортировки

            if (!IsAll.Checked)
            {
                ArrayToDataGrid(Array); //представление массива в таблицу данных
            }

            DataGrid.AllowUserToAddRows = true;
        }
        
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable Table = new DataTable();
            Table.Columns.Add("Элементы массива", typeof(int));
            DataGrid.Columns.Clear();
            DataGrid.DataSource = null;
            DataGrid.DataSource = Table;
        }

        private void Swap(int[] Array, int i, int j)
        {
            int Temp = Array[i];
            Array[i] = Array[j];
            Array[j] = Temp;
        }

        private bool IsSorted(int[] Array)
        {
            int Count = Array.Length;
            while (--Count >= 1)
            {
                if (Array[Count] < Array[Count - 1])
                {
                    return false;
                }
            }
            return true;
        }

        private void Check()
        {
            if (label1.Text != "")
            {
                ms1.Text = Millisecond;
            }
            if (label2.Text != "")
            {
                ms2.Text = Millisecond;
            }
            if (label3.Text != "")
            {
                ms3.Text = Millisecond;
            }
            if (label4.Text != "")
            {
                ms4.Text = Millisecond;
            }
            if (label5.Text != "")
            {
                ms5.Text = Millisecond;
            }
        }

        private void Visualization(int[] Array, Stopwatch SW)
        {
            SW.Stop();
            chart.Series[0].Points.Clear();
            for (int i = 0; i < Array.Length; i++)
            {
                chart.Series[0].Points.AddY(Array[i]);
            }
            Wait(1.0);
            SW.Start();
        }

        private void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }

        public void SortingSelection()
        {
            if (!IsAll.Checked)
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked)
                {
                    if (checkBox1.Checked)
                    {
                        BubbleSort(Array);
                    }
                    if (checkBox2.Checked)
                    {
                        InsertionSort(Array);
                    }
                    if (checkBox3.Checked)
                    {
                        CoctailSort(Array);
                    }
                    if (checkBox4.Checked)
                    {
                        Stopwatch SW = new Stopwatch();
                        QuickSort(Array, 0, Array.Length - 1, SW);
                    }
                    if (checkBox5.Checked)
                    {
                        BogoSort(Array);
                    }

                }
                else
                {
                    MessageBox.Show("Сортировка не выбрана");
                }
            }
            else
            {
                if (!IsVisualized.Checked)
                {
                    Stopwatch SW = new Stopwatch();
                    BubbleSort(Array1);
                    InsertionSort(Array2);
                    CoctailSort(Array3);
                    QuickSort(Array4, 0, Array4.Length - 1, SW);
                    BogoSort(Array5);
                    ArrayToDataGrid(Array1);
                }
                else
                {
                    MessageBox.Show("Отключите визуализацию");
                }
            }
        }

        private void DataGridToArray()
        {
            if (ArrayList.Count != 0)
            {
                ArrayList.Clear();
            }
            for (int i = 0; i < DataGrid.RowCount; i++)
            {
                ArrayList.Add(Convert.ToInt32(DataGrid[0, i].Value));
            }
            Array = ArrayList.ToArray();
            Array1 = ArrayList.ToArray();
            Array2 = ArrayList.ToArray();
            Array3 = ArrayList.ToArray();
            Array4 = ArrayList.ToArray();
            Array5 = ArrayList.ToArray();
            Stopwatch SW = new Stopwatch();
            Visualization(Array, SW);

        }

        private void ArrayToDataGrid(int[] Array)
        {
            Check();
            DataTable Table = new DataTable();
            Table.Columns.Add("Элементы массива", typeof(int));
            for (int i = 0; i < Array.Length; i++)
            {
                Table.Rows.Add(Array[i]);
            }
            DataGrid.Columns.Clear();
            DataGrid.DataSource = null;
            DataGrid.DataSource = Table;
        }

        #region Sorting
        public int[] BubbleSort(int[] Array)
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();
            int temp;
            for (int i = 0; i < Array.Length; i++)
            {
                for (int j = i + 1; j < Array.Length; j++)
                {
                    if (Array[i] > Array[j])
                    {
                        temp = Array[i];
                        Array[i] = Array[j];
                        Array[j] = temp;
                        if (IsVisualized.Checked)
                        {
                            Visualization(Array, SW);
                        }
                        Thread.Sleep(1);
                    }
                }
            }
            SW.Stop();
            label1.Text = SW.ElapsedMilliseconds.ToString();
            return Array;
        }

        public int[] InsertionSort(int[] Array)
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();
            for (int i = 1; i < Array.Length; i++)
            {
                int k = Array[i];
                int j = i - 1;

                while (j >= 0 && Array[j] > k)
                {
                    Array[j + 1] = Array[j];
                    Array[j] = k;
                    j--;
                    if (IsVisualized.Checked)
                    {
                        Visualization(Array, SW);
                    }
                    Thread.Sleep(1);
                }
            }
            SW.Stop();
            label2.Text = SW.ElapsedMilliseconds.ToString();
            return Array;
        }

        public int[] CoctailSort(int[] Array)
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();
            int left = 0,
            right = Array.Length - 1;
            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (Array[i] > Array[i + 1])
                    {
                        Swap(Array, i, i + 1);
                        if (IsVisualized.Checked)
                        {
                            Visualization(Array, SW);
                        }
                        Thread.Sleep(1);
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (Array[i - 1] > Array[i])
                    {
                        Swap(Array, i - 1, i);
                        if (IsVisualized.Checked)
                        {
                            Visualization(Array, SW);
                        }
                        Thread.Sleep(1);
                    }
                }
                left++;
            }
            SW.Stop();
            label3.Text = SW.ElapsedMilliseconds.ToString();
            return Array;
        }

        public int[] QuickSort(int[] Array, int Low, int High, Stopwatch SW)
        {
            SW.Start();

            var i = Low;
            var j = High;

            var Pivot = Array[Low];
            while (i <= j)
            {
                while (Array[i] < Pivot)
                {
                    i++;
                }

                while (Array[j] > Pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    Swap(Array, i, j);
                    if (IsVisualized.Checked)
                    {
                        Visualization(Array, SW);
                    }
                    Thread.Sleep(1);
                    i++;
                    j--;
                }
            }
            if (Low < j)
            {
                QuickSort(Array, Low, j, SW);
            }
            if (i < High)
            {
                QuickSort(Array, i, High, SW);
            }
            SW.Stop();
            label4.Text = (SW.ElapsedMilliseconds*3).ToString();
            return Array;
        }

        public int[] BogoSort(int[] Array)
        {
            int Temp, Rnd;
            Stopwatch SW = new Stopwatch();
            Random Random = new Random();
            SW.Start();
            while (!IsSorted(Array))
            {
                for (int i = 0; i < Array.Length; ++i)
                {
                    Rnd = Random.Next(Array.Length);
                    Temp = Array[i];
                    Array[i] = Array[Rnd];
                    Array[Rnd] = Temp;
                    if (IsVisualized.Checked)
                    {
                        Visualization(Array, SW);
                    }
                    Thread.Sleep(1);
                }
            }
            SW.Stop();
            label5.Text = SW.ElapsedMilliseconds.ToString();
            return Array;
        }
        #endregion Sorting

    }
}

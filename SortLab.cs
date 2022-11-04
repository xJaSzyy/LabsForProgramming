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
        int[] Array;
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
 
            ArrayToDataGrid(); //представление массива в таблицу данных

            DataGrid.AllowUserToAddRows = true;
        }
        
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable Table = new DataTable();
            Table.Columns.Add("Элементы массива", typeof(int));
            DataGrid.Columns.Clear();
            DataGrid.DataSource = null;
            DataGrid.DataSource = Table;

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            ms1.Text = "";
            ms2.Text = "";
            ms3.Text = "";
            ms4.Text = "";
            ms5.Text = "";
        }

        static void Swap(int[] Array, int i, int j)
        {
            int Temp = Array[i];
            Array[i] = Array[j];
            Array[j] = Temp;
        }

        void Reverse(int[] Array)
        {
            for (int i = 0; i < Array.Length / 2; i++)
            {
                Swap(Array, i, Array.Length - i - 1);
            }
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
            if (Decrease.Checked)
            {
                Reverse(Array);
            }
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

        private void Visualization(int[] Array)
        {
            chart.Series[0].Points.Clear();
            for (int i = 0; i < Array.Length; i++)
            {
                chart.Series[0].Points.AddY(Array[i]);
            }
            Wait(1.0);
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
                    QuickSort(Array, 0, Array.Length - 1);
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

        private void DataGridToArray()
        {
            ArrayList.Clear();
            for (int i = 0; i < DataGrid.RowCount; i++)
            {
                ArrayList.Add(Convert.ToInt32(DataGrid[0, i].Value));
            }
            Array = ArrayList.ToArray();
            Visualization(Array);
        }

        private void ArrayToDataGrid()
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
                        SW.Stop();
                        Visualization(Array);
                        SW.Start();
                        Thread.Sleep(10);
                    }
                }
            }
            SW.Stop();
            label1.Text = $"{SW.ElapsedMilliseconds}";
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
                    SW.Stop();
                    Visualization(Array);
                    SW.Start();
                    Thread.Sleep(10);
                }
            }
            SW.Stop();
            label2.Text = $"{SW.ElapsedMilliseconds}";
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
                        SW.Stop();
                        Visualization(Array);
                        SW.Start();
                        Thread.Sleep(10);
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (Array[i - 1] > Array[i])
                    {
                        Swap(Array, i - 1, i);
                        SW.Stop();
                        Visualization(Array);
                        SW.Start();
                        Thread.Sleep(10);
                    }
                }
                left++;
            }
            SW.Stop();
            label3.Text = $"{SW.ElapsedMilliseconds}";
            return Array;
        }

        public int[] QuickSort(int[] Array, int LeftIndex, int RightIndex)
        {
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
                    SW.Stop();
                    Visualization(Array);
                    SW.Start();
                    Thread.Sleep(10);
                }
            }
            SW.Stop();
            label5.Text = $"{SW.ElapsedMilliseconds}";
            return Array;
        }

        #endregion Sorting


    }
}

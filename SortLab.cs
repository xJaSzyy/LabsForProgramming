using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sorting
{
    public partial class Form1 : Form
    {
        int[] Array;
        List<int> ArrayList = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public int[] BubbleSort(int[] Array)
        {
            long start = Stopwatch.GetTimestamp();
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
                    }
                }
            }
            long end = Stopwatch.GetTimestamp();
            label1.Text = $"{end-start}";
            return Array;
        }

        public int[] InsertionSort(int[] Array)
        {
            long start = Stopwatch.GetTimestamp();
            for (int i = 1; i < Array.Length; i++)
            {
                int k = Array[i];
                int j = i - 1;

                while (j >= 0 && Array[j] > k)
                {
                    Array[j + 1] = Array[j];
                    Array[j] = k;
                    j--;
                }
            }
            long end = Stopwatch.GetTimestamp();
            label2.Text = $"{end - start}";
            return Array;
        }

        public int[] CoctailSort(int[] Array)
        {
            long start = Stopwatch.GetTimestamp();
            int left = 0,
            right = Array.Length - 1;
            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (Array[i] > Array[i + 1])
                        Swap(Array, i, i + 1);
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (Array[i - 1] > Array[i])
                        Swap(Array, i - 1, i);
                }
                left++;
            }
            long end = Stopwatch.GetTimestamp();
            label3.Text = $"{end - start}";
            return Array;
        }

        static void Swap(int[] Array, int i, int j)
        {
            int temp = Array[i];
            Array[i] = Array[j];
            Array[j] = temp;
        }

        public int[] QuickSort(int[] Array, int LeftIndex, int RightIndex)
        {
            long start = Stopwatch.GetTimestamp();
            var i = LeftIndex;
            var j = RightIndex;
            var Pivot = Array[LeftIndex];

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
                    int temp = Array[i];
                    Array[i] = Array[j];
                    Array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (LeftIndex < j)
            {
                QuickSort(Array, LeftIndex, j);
            }
            if (i < RightIndex)
            {
                QuickSort(Array, i, RightIndex);
            }

            long end = Stopwatch.GetTimestamp();
            label4.Text = $"{end - start}";
            return Array;
        }

        public void SortingSelection()
        {
            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
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
            }
            else
            {
                MessageBox.Show("Сортировка не выбрана");
            }
        }

        private void отсортироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGrid.AllowUserToAddRows = false;

            DataGridToArray(); //представление таблицы данных в массив

            SortingSelection(); //выбор сортировки

            ArrayToDataGrid(); //представление массива в таблицу данных

            DataGrid.AllowUserToAddRows = true;
        }

        private void DataGridToArray()
        {
            for (int i = 0; i < DataGrid.RowCount; i++)
            {
                ArrayList.Add(Convert.ToInt32(DataGrid[0, i].Value));
            }
            Array = ArrayList.ToArray();
            ArrayList.Clear();
        }

        private void ArrayToDataGrid()
        {
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
    }
}

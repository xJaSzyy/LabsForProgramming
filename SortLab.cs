using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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

        private void заполнитьМассивToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random Random = new Random();
            int[] RandomArray = new int[500];
            for (int i = 0; i < RandomArray.Length; i++)
            {
                RandomArray[i] = Random.Next(1, 500);
            }
            ArrayToDataGrid(RandomArray);
        }

        private void Swap(int[] Array, int i, int j)
        {
            int Temp = Array[i];
            Array[i] = Array[j];
            Array[j] = Temp;
            if (IsVisualized.Checked)
            {
                Visualization(Array);
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

        public void Visualization(int[] Array)
        {
            chart.Series[0].Points.Clear();
            for (int i = 0; i < Array.Length; i++)
            {
                chart.Series[0].Points.AddY(Array[i]);
            }
            Wait(0.001);
        }

        private void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }

        public async Task SortingSelection()
        {
            if (!IsAll.Checked)
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked)
                {
                    if (checkBox1.Checked)
                    {
                        BubbleSort(Array);
                        Visualization(Array);
                    }
                    if (checkBox2.Checked)
                    {
                        InsertionSort(Array);
                        Visualization(Array);
                    }
                    if (checkBox3.Checked)
                    {
                        CoctailSort(Array);
                        Visualization(Array);
                    }
                    if (checkBox4.Checked)
                    {
                        QuickSort(Array);
                        Visualization(Array);
                    }
                    if (checkBox5.Checked)
                    {
                        BogoSort(Array);
                        Visualization(Array);
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
                    await Task.Run(() => BubbleSort(Array1));
                    ArrayToDataGrid(Array1);
                    await Task.Run(() => InsertionSort(Array2));
                    await Task.Run(() => CoctailSort(Array3));
                    await Task.Run(() => QuickSort(Array4));
                    //await Task.Run(() => BogoSort(Array5));
                    Check();
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
            Visualization(Array);

        }

        public void ArrayToDataGrid(int[] Array)
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
        
        public static void ToLabel(Label label, string text)
        {
            label.Invoke(new Action(() => label.Text = text));
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
                            SW.Stop();
                            Visualization(Array);
                            SW.Start();
                        }
                    }
                }
            }
            Wait(1);
            SW.Stop();
            ToLabel(label1, SW.ElapsedMilliseconds.ToString());
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
                        SW.Stop();
                        Visualization(Array);
                        SW.Start();
                    }
                }
            }
            Wait(1);
            SW.Stop();
            ToLabel(label2, SW.ElapsedMilliseconds.ToString());
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
                        SW.Stop();
                        Swap(Array, i, i + 1);
                        SW.Start();
                        if (IsVisualized.Checked)
                        {
                            SW.Stop();
                            Visualization(Array);
                            SW.Start();
                        }
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (Array[i - 1] > Array[i])
                    {
                        SW.Stop();
                        Swap(Array, i - 1, i);
                        SW.Start();
                        if (IsVisualized.Checked)
                        {
                            SW.Stop();
                            Visualization(Array);
                            SW.Start();
                        }
                    }
                }
                left++;
            }
            Wait(1);
            SW.Stop();
            ToLabel(label3, SW.ElapsedMilliseconds.ToString());
            return Array;
        }

        public int[] QuickSort(int[] Array)
        {
            Stopwatch SW = new Stopwatch();
            SW.Start();
            var stack = new Stack<int>();

            int pivot;
            int pivotIndex = 0;
            int leftIndex = pivotIndex + 1;
            int rightIndex = Array.Length - 1;

            stack.Push(pivotIndex);
            stack.Push(rightIndex);

            int leftIndexOfSubSet, rightIndexOfSubset;

            while (stack.Count > 0)
            {
                if (IsVisualized.Checked)
                {
                    SW.Stop();
                    Visualization(Array);
                    SW.Start();
                }
                rightIndexOfSubset = stack.Pop();
                leftIndexOfSubSet = stack.Pop();

                leftIndex = leftIndexOfSubSet + 1;
                pivotIndex = leftIndexOfSubSet;
                rightIndex = rightIndexOfSubset;

                pivot = Array[pivotIndex];

                if (leftIndex > rightIndex)
                {
                    continue;
                }

                while (leftIndex < rightIndex)
                {
                    while ((leftIndex <= rightIndex) && (Array[leftIndex] <= pivot))
                    {
                        leftIndex++;
                    }

                    while ((leftIndex <= rightIndex) && (Array[rightIndex] >= pivot))
                    {
                        rightIndex--;
                    }

                    if (rightIndex >= leftIndex)
                    {
                        SW.Stop();
                        Swap(Array, leftIndex, rightIndex);
                        SW.Start();
                    }
                }

                if (pivotIndex <= rightIndex)
                {
                    if (Array[pivotIndex] > Array[rightIndex])
                    {
                        SW.Stop();
                        Swap(Array, pivotIndex, rightIndex);
                        SW.Start();
                    }
                }

                if (leftIndexOfSubSet < rightIndex)
                {
                    stack.Push(leftIndexOfSubSet);
                    stack.Push(rightIndex - 1);
                }

                if (rightIndexOfSubset > rightIndex)
                {
                    stack.Push(rightIndex + 1);
                    stack.Push(rightIndexOfSubset);
                }
            }
            Wait(1);
            SW.Stop();
            ToLabel(label4, SW.ElapsedMilliseconds.ToString());
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
                        Visualization(Array);
                    }
                }
            }
            SW.Stop();
            ToLabel(label5, SW.ElapsedMilliseconds.ToString());
            return Array;
        }
        #endregion Sorting
    }
}

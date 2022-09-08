using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int BarLength = int.Parse(BarLengthText.Text);
                int EdgeA = int.Parse(EdgeAText.Text);
                int EdgeB = int.Parse(EdgeBText.Text);
                int CylinderLength = int.Parse(CylinderLengthText.Text);
                int Radius = int.Parse(RadiusText.Text);

                if (CylinderLength <= BarLength)
                {
                    if (Radius <= EdgeA / 2 && Radius <= EdgeB / 2)
                    {
                        //Подсчёт
                        int BarVolume = Math.Abs(BarLength * EdgeA * EdgeB);
                        double CylinderVolume = Math.Abs(Math.Round(Math.PI * Math.Pow(Radius, 2) * CylinderLength, 6));
                        double PercentWaste = Math.Abs(Math.Round(100 - (CylinderVolume / BarVolume * 100), 6));

                        //Вывод
                        Size = new Size(420, 450);
                        BarVolumeText.Text = Convert.ToString(BarVolume);
                        CylinderVolumeText.Text = Convert.ToString(CylinderVolume);
                        PercentWasteText.Text = Convert.ToString(PercentWaste);
                    }
                    else
                    {
                        MessageBox.Show("Радиус цилиндра слишком большой");
                    }
                }
                else
                {
                    MessageBox.Show("Длина цилиндра слишком большая");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка ввода данных");
            }
        }

    }
}

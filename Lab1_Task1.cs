using System;
using System.Windows.Forms;

namespace Lab00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                    int BarVolume = BarLength * EdgeA * EdgeB;
                    double CylinderVolume = Math.Round(Math.PI * Math.Pow(Radius, 2) * CylinderLength, 3);
                    double PercentWaste = Math.Round(100 - (CylinderVolume / BarVolume * 100), 3);

                    //Вывод
                    BarVolumeText.Text = Convert.ToString(BarVolume);
                    CylinderVolumeText.Text = Convert.ToString(CylinderVolume);
                    PercentWasteText.Text = Convert.ToString(PercentWaste);
                }
                else
                {
                    MessageBox.Show("Радиус цилиндра слишком большая");
                }
            }
            else
            {
                MessageBox.Show("Длина цилиндра слишком большая");
            }
        }
    }
}

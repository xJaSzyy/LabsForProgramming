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

        private void Form1_Load(object sender, EventArgs e)
        {
            BarLengthText.Text = "Длина бруска";
            BarLengthText.ForeColor = Color.Gray;
            EdgeAText.Text = "Ребро 1";
            EdgeAText.ForeColor = Color.Gray;
            EdgeBText.Text = "Ребро 2";
            EdgeBText.ForeColor = Color.Gray;
            CylinderLengthText.Text = "Длина цилиндра";
            CylinderLengthText.ForeColor = Color.Gray;
            RadiusText.Text = "Радиус цилиндра";
            RadiusText.ForeColor = Color.Gray;
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
                        Size = new Size(420, 281);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BarLengthText_Enter(object sender, EventArgs e)
        {
            if (BarLengthText.Text == "Длина бруска")
            {
                BarLengthText.Text = "";
                BarLengthText.ForeColor = Color.Black;
            }
        }
        
        private void BarLengthText_Leave(object sender, EventArgs e)
        {
            if (BarLengthText.Text == "")
            {
                BarLengthText.Text = "Длина бруска";
                BarLengthText.ForeColor = Color.Gray;
            }
        }

        private void EdgeAText_Enter(object sender, EventArgs e)
        {
            if (EdgeAText.Text == "Ребро 1")
            {
                EdgeAText.Text = "";
                EdgeAText.ForeColor = Color.Black;
            }
        }

        private void EdgeAText_Leave(object sender, EventArgs e)
        {
            if (EdgeAText.Text == "")
            {
                EdgeAText.Text = "Ребро 1";
                EdgeAText.ForeColor = Color.Gray;
            }
        }

        private void EdgeBText_Enter(object sender, EventArgs e)
        {
            if (EdgeBText.Text == "Ребро 2")
            {
                EdgeBText.Text = "";
                EdgeBText.ForeColor = Color.Black;
            }
        }

        private void EdgeBText_Leave(object sender, EventArgs e)
        {
            if (EdgeBText.Text == "")
            {
                EdgeBText.Text = "Ребро 2";
                EdgeBText.ForeColor = Color.Gray;
            }
        }

        private void CylinderLengthText_Enter(object sender, EventArgs e)
        {
            if (CylinderLengthText.Text == "Длина цилиндра")
            {
                CylinderLengthText.Text = "";
                CylinderLengthText.ForeColor = Color.Black;
            }
        }

        private void CylinderLengthText_Leave(object sender, EventArgs e)
        {
            if (CylinderLengthText.Text == "")
            {
                CylinderLengthText.Text = "Длина цилиндра";
                CylinderLengthText.ForeColor = Color.Gray;
            }
        }

        private void RadiusText_Enter(object sender, EventArgs e)
        {
            if (RadiusText.Text == "Радиус цилиндра")
            {
                RadiusText.Text = "";
                RadiusText.ForeColor = Color.Black;
            }
        }

        private void RadiusText_Leave(object sender, EventArgs e)
        {
            if (RadiusText.Text == "")
            {
                RadiusText.Text = "Радиус цилиндра";
                RadiusText.ForeColor = Color.Gray;
            }
        }

    }
}

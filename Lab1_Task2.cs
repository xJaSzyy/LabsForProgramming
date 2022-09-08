using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using System.Globalization;

namespace WindowsFormsApp2
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
        
        List<Animal> animals = new List<Animal> { };

        public class Animal
        {
            public string Name { get; set; }
            public string Weight { get; set; }
            public string FoodWeightPerDay { get; set; }
            public string FoodType { get; set; }
        }

        string PathCsvFile = "D:/Users/student/Desktop/animals.csv";

        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            ShouldQuote = args => args.Row.Index == 1
        };

        private void button1_Click(object sender, EventArgs e)
        {
            animals.Add(new Animal
            {
                Name = NameText.Text,
                Weight = WeightText.Text,
                FoodWeightPerDay = FoodWeightPerDayText.Text,
                FoodType = FoodTypeText.Text
            });

            //очищаем textbox
            NameText.Text = null;
            WeightText.Text = null;
            FoodWeightPerDayText.Text = null;
            FoodTypeText.Text = null;

            using (StreamWriter streamReader = new StreamWriter(PathCsvFile))
            {
                using (CsvWriter csvReader = new CsvWriter(streamReader, csvConfig))
                {
                    // записываем данные в csv файл
                    csvReader.WriteRecords(animals);
                }
            }
        }
            
    }
}

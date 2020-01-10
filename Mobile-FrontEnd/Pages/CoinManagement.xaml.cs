using System;
using System.Collections.Generic;
using SkiaSharp;
using Xamarin.Forms;
using Microcharts;

namespace MobileFrontEnd.Pages
{
    public partial class CoinManagement : ContentPage
    {
        public CoinManagement()
        {
            InitializeComponent();

            InitChart();
        }

        void InitChart()
        {
            // Earn chart view
            var entries = new[]
            {
                new Microcharts.Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#266489")
                },
                new Microcharts.Entry(400)
                {
                    Label = "February",
                    ValueLabel = "400",
                    Color = SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(-100)
                {
                    Label = "March",
                    ValueLabel = "-100",
                    Color = SKColor.Parse("#90D585")
                }
            };
            var chart = new BarChart() { Entries = entries };


            this.chartViewEarn.Chart = chart;

            // Donate chart view
            var donatedEntries = new[]
            {
                new Microcharts.Entry(10)
                {
                    Label= "one",
                    ValueLabel="300",
                    Color = SKColor.Parse("#450973")
                },
                new Microcharts.Entry(10)
                {
                    Label= "two",
                    ValueLabel="300",
                    Color = SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(10)
                {
                    Label= "one",
                    ValueLabel="300",
                    Color = SKColor.Parse("#002aff")
                },
                new Microcharts.Entry(10)
                {
                    Label= "one",
                    ValueLabel="300",
                    Color = SKColor.Parse("#77ff00")
                },new Microcharts.Entry(10)
                {
                    Label= "one",
                    ValueLabel="300",
                    Color = SKColor.Parse("#04c2b5")
                }
            };
            
            this.chartViewDonate.Chart = new DonutChart(){Entries = donatedEntries};
        }
    }
}

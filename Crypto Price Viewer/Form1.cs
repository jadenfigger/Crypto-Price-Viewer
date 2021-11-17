// Jaden Figger
// Crypto Price Tracker
// 11/16/21

using System;
using System.Windows.Forms;

namespace Crypto_Price_Viewer
{
    public partial class Form1 : Form
    {
        static Scraper scraper;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Initializing the scraper class
            scraper = new Scraper();

            // Scraping the data from the cryptowatch website
            scraper.ScrapeData("https://cryptowat.ch/assets");

            // Updating the list view once data has been scraped
            UpdateListView();
        }

        public void UpdateListView()
        {
            lvMain.Items.Clear();

            // Looping through each crypto in entries list
            foreach (var entry in scraper.entries)
            {
                // Creating a string that stores all the info about the crypto
                var row = new string[] { entry.Name, entry.Price, entry.MarketCap, entry.OneMonthChange, entry.OneYearChange };
                var lvi = new ListViewItem(row);

                lvi.Tag = entry;

                // Adding the row to the list view 
                lvMain.Items.Add(lvi);
            }
        }
        // Called when the refresh button is clicked
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            scraper.entries.Clear();
            scraper.ScrapeData("https://cryptowat.ch/assets");

            UpdateListView();
        }

    }
}

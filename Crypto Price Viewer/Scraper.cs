using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Web;

namespace Crypto_Price_Viewer
{
    class Scraper
    {
        public List<EntryModel> entries = new List<EntryModel>();
        static List<string> IncludedCryptos = new List<string>() { "Bitcoin", "Ethereum", "Solana", "Litecoin", "Chainlink" };

        public void ScrapeData(string page)
        {
            // Creating a connection with the web page
            var web = new HtmlWeb();
            var doc = web.Load(@page);

            // Selecting all crypto's via class search
            var cryptos = doc.DocumentNode.SelectNodes("//a[contains(@class, '_2mHoLKk1EmQ90Hj2VxwVKC _2cEwIPLxNCKyZSBxk7MyUQ Q0Cxwokka8qzW-qAyjdq6 pointer')]");

            // Looping through each crypto found
            foreach (var crypto in cryptos)
            {
                // Selecting the name from each crypto
                var name = HttpUtility.HtmlDecode(crypto.SelectSingleNode(".//div[contains(@class, 'text-left truncate _1hazOxgsUXq0rb-UgDZwNp _1GdBC6rgsSADLryaaGeEuX w8u1-Ks6zzfWwPQ23ywUj _36FIyjphKz71izCg1N-Uks')]").InnerText);
                
                // If the crypto is one that we are interested in
                if (IncludedCryptos.Contains(name)) {

                    // Fetching all the different info about the crypto and storing them in the corresponding variables
                    var price = HttpUtility.HtmlDecode(crypto.SelectSingleNode(".//span[contains(@class, 'price')]").InnerText);
                    var marketCap = HttpUtility.HtmlDecode(crypto.SelectNodes(".//div[contains(@class, 'text-right _1hazOxgsUXq0rb-UgDZwNp LNc8C7U5Q_4hVq8G7HQHa _36FIyjphKz71izCg1N-Uks')]")[2].InnerText);
                    var mChange = HttpUtility.HtmlDecode(crypto.SelectNodes(".//div[contains(@class, 'text-right _1hazOxgsUXq0rb-UgDZwNp _2FulmH3iQ5Kp8yQh0HAxic _36FIyjphKz71izCg1N-Uks')]")[1].InnerText);
                    var yChange = HttpUtility.HtmlDecode(crypto.SelectSingleNode(".//div[contains(@class, 'text-right _1hazOxgsUXq0rb-UgDZwNp _2FulmH3iQ5Kp8yQh0HAxic _3NvcQsdOpz_YW0qNx_nkRA _36FIyjphKz71izCg1N-Uks')]").InnerText);

                    var nEntry = new EntryModel();

                    nEntry.Name = name;
                    nEntry.Price = price;
                    nEntry.MarketCap = marketCap;
                    nEntry.OneMonthChange = mChange;
                    nEntry.OneYearChange = yChange;

                    // Adding the new crytpo entry to the entries list
                    entries.Add(nEntry);
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ReleaseAnalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            string page = wc.DownloadString("http://www.nyaa.se/?page=search&cats=1_37&filter=0&term=" + textBox1.Text.Replace(' ', '+'));
            HtmlAgilityPack.HtmlDocument d = new HtmlAgilityPack.HtmlDocument();
            d.LoadHtml(page);
            var document = d.DocumentNode;
            var result = document.QuerySelectorAll(".tlistname a");

            List<NyaaResultRecord> records = new List<NyaaResultRecord>();
            foreach (var item in result)
            {
                try
                {
                    page = wc.DownloadString(System.Net.WebUtility.HtmlDecode(item.Attributes["href"].Value));
                    d = new HtmlAgilityPack.HtmlDocument();
                    d.LoadHtml(page);
                    document = d.DocumentNode;
                    string date = document.QuerySelectorAll(".thead").Where(el => el.InnerText == "Date:").First().NextSibling.InnerText;
                    //                                                                                                               Oct 14 2013, 22:02 UTC
                    records.Add(new NyaaResultRecord() { FileName = System.Net.WebUtility.HtmlDecode(item.FirstChild.InnerText), Timestamp = DateTime.ParseExact(date, "MMM d yyyy, HH:mm UTC", CultureInfo.InvariantCulture) });

                }
                catch { }
            }

            var episodesList = records.Select(rc => new EpisodeRecord() { FileName = rc.FileName, Timestamp = rc.Timestamp }).ToList();

            ////////////////////// end of filename fetching
            //starting cleanup

            foreach (var item in records)
            {
                item.FileName = item.FileName.Replace('_', ' ');
            }

            Regex r = new Regex("\\[.+?\\]");//metadata
            foreach (var item in records)
            {
                item.FileName= r.Replace(item.FileName,"");
            }

            
            r = new Regex("\\(.+?\\)");//metadata
            foreach (var item in records)
            {
                item.FileName = r.Replace(item.FileName, "");
            }

            r = new Regex("\\..{3,4}$");//removing extention
            foreach (var item in records)
            {
                item.FileName = r.Replace(item.FileName, "");
            }

            r = new Regex("[^\\s\\d\\w-.]");//removing all remaining non digit/letter/space/- charters to cleanup the output
            foreach (var item in records)
            {
                item.FileName = r.Replace(item.FileName, "");
            }


            r= new Regex(textBox1.Text.Replace(" ",".{0,4}"),RegexOptions.IgnoreCase);
            foreach (var item in records)
            {
                item.FileName = r.Replace(item.FileName, "");
            }

            //end of cleanup
            //starting to parse episde 
            Regex[] Patterns = new Regex[] { new Regex("-\\s{0,2}(\\d{1,3}(\\.\\d)?)(\\s?([vV]|[vV]ersion)\\d)?"), new Regex("(\\d{1,3}(\\.\\d)?})(\\s?([vV]|[vV]ersion)\\d)?") };
            for (int i = 0; i < records.Count; i++)
            {
                foreach (var rx in Patterns)
                {
                    var rxMatch = rx.Match(records[i].FileName);
                    if (rxMatch.Success)
                    {
                        episodesList[i].EpisodeNumber = double.Parse(rxMatch.Groups[1].Value);
                        episodesList[i].Ready=true;
                        break;
                    }
                }

            }

            var unparsedEpisodes = episodesList.Where(ep => !ep.Ready).ToList();
            episodesList = episodesList.Where(ep => ep.Ready).OrderBy(ep=>ep.Timestamp).ToList();

            //end of episode parsing

            var tmp = episodesList.GroupBy(ep=>ep.EpisodeNumber);
            episodesList = tmp.Select(g => g.OrderBy(ep => ep.Timestamp).First()).ToList();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = episodesList;
            
            
        }
    }
    public class NyaaResultRecord
    {
        public string FileName { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString()
        {
            return string.Format("{0} || {1}", FileName, Timestamp.ToString());
        }
    }

    public class EpisodeRecord
    {
        public bool Ready { get; set; }
        public string FileName { get; set; }
        public DateTime Timestamp { get; set; }
        public string DisplayTime { get { return Timestamp.ToString("dddd HH:mm"); } }

        public string Group { get; set; }
        public string Codec { get; set; }
        public string Resolution { get; set; }
        public string version { get; set; }
        public double EpisodeNumber { get; set; }

        public EpisodeRecord()
        {
            Ready = false;
        }
        public override string ToString()
        {
            return string.Format("{0} || {1} || {2}", EpisodeNumber, Timestamp.ToString(), FileName);
        }
    }
}

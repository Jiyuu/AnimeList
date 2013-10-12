using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;

namespace AnimesList
{
    public partial class Form1 : ApplicationDesktopToolbar
    {
        public static List<AnimeSchedule> getAnimeSchedules()
        {
            List<AnimeSchedule> animes = new List<AnimeSchedule>();

            Data.AnimeDataTable t= new Data.AnimeDataTable();
            DataTableAdapters.AnimeTableAdapter dt = new DataTableAdapters.AnimeTableAdapter();
            dt.Fill(t);
            foreach (Data.AnimeRow row in t.Rows)
            {
                animes.Add(new AnimeSchedule(row["name"].ToString(), row["Time"].ToString(), row.TimeZone,row.SearchTerm,row.Enabled));
            }
            animes = animes.Where(a => a.Enabled).ToList();
            return animes;
        }
        public Form1()
        {
            InitializeComponent();
        }
        public void ResizeGrid(DataGridView dataGrid, int prevWidth)
        {
            if (prevWidth == 0)
                prevWidth = dataGrid.Width;
            if (prevWidth == dataGrid.Width)
                return;

            int fixedWidth = SystemInformation.VerticalScrollBarWidth +
               dataGrid.RowHeadersWidth + 2;
            int mul = 100 * (dataGrid.Width - fixedWidth) /
            (prevWidth - fixedWidth);
            int columnWidth;
            int total = 0;
            DataGridViewColumn lastVisibleCol = null;

            for (int i = 0; i < dataGrid.ColumnCount; i++)
                if (dataGrid.Columns[i].Visible)
                {
                    columnWidth = (dataGrid.Columns[i].Width * mul + 50) / 100;
                    dataGrid.Columns[i].Width =
                          Math.Max(columnWidth, dataGrid.Columns[i].MinimumWidth);
                    total += dataGrid.Columns[i].Width;
                    lastVisibleCol = dataGrid.Columns[i];
                }
            if (lastVisibleCol == null)
                return;
            columnWidth = dataGrid.Width - total +
               lastVisibleCol.Width - fixedWidth;
            lastVisibleCol.Width =
               Math.Max(columnWidth, lastVisibleCol.MinimumWidth);
            prevWidth = dataGrid.Width;
        }

        private int GetDgvMinWidth(DataGridView dgv)
        {
            // Add two pixels for the border for BorderStyles other than None.
            var controlBorderWidth = (dgv.BorderStyle == BorderStyle.None) ? 0 : 2;

            // Return the width of all columns plus the row header, and adjusted for the DGV's BorderStyle.
            return dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) /*+ dgv.RowHeadersWidth */+ controlBorderWidth;
        }

        List<AnimeSchedule> animes;
        private void Form1_Load(object sender, EventArgs e)
        {
            //RegisterBar();
            animes = getAnimeSchedules();



            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Time", "Time");
            dataGridView1.Columns.Add("Eta", "Eta");
            dataGridView1.Columns.Add("SearchTerm", "SearchTerm");
            dataGridView1.Columns[3].Visible = false;
            redraw();

            Timer t = new Timer();
            t.Tick += t_Tick;
            t.Interval = 60000;
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            redraw();

        }
        void redraw()
        {
            List<AnimeSchedule> animestocome = animes.Where(a => a.CompareTo(DateTime.UtcNow) > 0).ToList();
            List<AnimeSchedule> animesbygone = animes.Where(a => a.CompareTo(DateTime.UtcNow) <= 0).ToList();

            animestocome.Sort();
            animesbygone.Sort();

            dataGridView1.Rows.Clear();
            foreach (var item in animestocome)
            {
                dataGridView1.Rows.Add(item.Name, DateTime.UtcNow.Date.AddDays(item.DOW - DateTime.UtcNow.DayOfWeek).Add(item.TOD).ToLocalTime().ToString("dddd HH:mm"), TimeSpan.FromMinutes((int)DateTime.UtcNow.Date.AddDays(item.DOW - DateTime.UtcNow.DayOfWeek).Add(item.TOD).Subtract(DateTime.UtcNow).TotalMinutes).ToString(@"d\d\ hh\:mm"),item.SearchTerm);
            }
            foreach (var item in animesbygone)
            {
                dataGridView1.Rows.Add(item.Name, DateTime.UtcNow.Date.AddDays(item.DOW - DateTime.UtcNow.DayOfWeek).Add(item.TOD).AddHours(168).ToLocalTime().ToString("dddd HH:mm"), TimeSpan.FromMinutes((int)DateTime.UtcNow.Date.AddDays(item.DOW - DateTime.UtcNow.DayOfWeek).Add(item.TOD).Subtract(DateTime.UtcNow).Add(new TimeSpan(168, 0, 0)).TotalMinutes).ToString(@"d\d\ hh\:mm"),item.SearchTerm);
            }
            dataGridView1.Height = (int)(dataGridView1.Rows.Count * 20.8);
            this.Height = (int)(dataGridView1.Rows.Count * 20.8) + 40;
            dataGridView1.Width = GetDgvMinWidth(dataGridView1);
            this.Width = GetDgvMinWidth(dataGridView1) + 16;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            
            if (e.ColumnIndex == 0)
            {
                Process.Start("http://tokyotosho.info/search.php?terms=" + dg.Rows[e.RowIndex].Cells[3].Value.ToString());


            }
        }
    }
    public class AnimeSchedule : IComparable
    {
        public string Name { get; set; }
        public TimeSpan TOD { get; set; }
        public DayOfWeek DOW { get; set; }
        public string SearchTerm { get; set; }
        public bool Enabled { get; set; }
        //public DateTime BroadcastStart { get; set; }
        //public DateTime BroadcastEnd { get; set; }
        public AnimeSchedule(string name, string time,int? timeOffset,string searchTerm,bool isEnabled)
        {
            int offset = 9;
            if (timeOffset.HasValue)
                offset = timeOffset.Value;
            Name = name;
            DOW = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), time.Split(' ')[0], true);
            if (DateTime.Parse(time.Split(' ')[1]).TimeOfDay.TotalHours < 9)
                if (DOW == DayOfWeek.Sunday)
                    DOW = DayOfWeek.Saturday;
                else
                    DOW -= 1;
            TOD = DateTime.Parse(time.Split(' ')[1]).AddHours(-9).TimeOfDay;

            if (string.IsNullOrEmpty(searchTerm))
                SearchTerm = Name;
            else
                SearchTerm = searchTerm;

            Enabled = isEnabled;
            //BroadcastStart = bs;
            //BroadcastEnd = be;
        }
        public override string ToString()
        {
            return (Name == null ? "" : Name) + ": " + DOW.ToString() + " " + (TOD == null ? "" : TOD.ToString());
        }


        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;
            AnimeSchedule converted = obj as AnimeSchedule;
            if (converted == null)
            {
                if (obj is DateTime)
                {
                    DateTime time = (DateTime)obj;
                    if (time.DayOfWeek > this.DOW)
                        return -1;
                    else if (time.DayOfWeek < this.DOW)
                        return 1;
                    else
                    {
                        if (time.TimeOfDay > this.TOD)
                            return -1;
                        else if (time.TimeOfDay < this.TOD)
                            return 1;
                        else
                            return 0;
                    }
                }
                else
                    return 0;
            }
            else
            {
                if (converted.DOW > this.DOW)
                    return -1;
                else if (converted.DOW < this.DOW)
                    return 1;
                else
                {
                    if (converted.TOD > this.TOD)
                        return -1;
                    else if (converted.TOD < this.TOD)
                        return 1;
                    else
                        return 0;
                }
            }
        }
    }
}

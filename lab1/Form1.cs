using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace lab1
{
    public partial class Form1 : Form
    {
        private string TestStringLine = string.Empty;
        private List<string> Test = new List<string>();
        private List<float> Numbers = new List<float>();
        private List<string> Letters = new List<string>();

        static int PlayPause = 0;
      //  WMPLib.WindowsMediaPlayer pl = new WMPLib.WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearAll();
            InitializeTestList();
            InitializeLists();
            UpdateLablesText();
        }

        private void InitializeLists()
        {
            foreach (string s in Test)
            {
                float.TryParse(s, out float result);
                if (result != 0)
                {
                    Numbers.Add(result);
                }
                else
                {
                    Letters.Add(s);
                }
            }
        }

        private void UpdateLablesText()
        {
            label3.Text = Numbers.Count.ToString();
            label4.Text = Letters.Count.ToString();

            foreach (float f in Numbers.OrderByDescending(n => n))
            {
                label5.Text += f.ToString() + "\n";
            }

            foreach (string s in Letters.OrderByDescending(n => n.Length))
            {
                label6.Text += s.ToString() + "\n";
            }
        }

        private void InitializeTestList()
        {
            string[] words = TestStringLine.Split(new char[] { ';' });
            foreach (string s in words)
            {
                Test.Add(s);
            }
        }

        private void ClearAll()
        {
            Test.Clear();
            Numbers.Clear();
            Letters.Clear();
            label3.Text = string.Empty;
            label4.Text = string.Empty;
            label5.Text = string.Empty;
            label6.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TestStringLine = textBox1.Text;
        }

        WMPLib.WindowsMediaPlayer p = new WMPLib.WindowsMediaPlayer();

        private void button2_Click(object sender, EventArgs e)
        {
            string b = Path.GetFullPath(@"..\..\..\music.mp3");
            p.URL = b;
            
            // p.controls.play()
            if (PlayPause>2)
            {
                PlayPause = 0;
            }

            if (PlayPause == 0)
            {
                p.controls.play();
                PlayPause++;
                timer1.Start();
            }

            else if (PlayPause == 1)
            {
                p.controls.pause();
                PlayPause++;
                timer1.Stop();
            }

            else if (PlayPause == 2)
            {

                p.controls.play();
                PlayPause ++;
                timer1.Start();
            }
           // PlayPause = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double dlina = p.currentMedia.duration;
            trackBar1.Maximum = (int)dlina;
            double tekPosition = p.controls.currentPosition;
            trackBar1.Value = (int)tekPosition;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000; //Таймер для плеера
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            p.controls.currentPosition = trackBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            p.controls.pause();
        }
    }
}

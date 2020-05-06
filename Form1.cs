using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PremierePictureBoxApp
{
    public partial class Form1 : Form
    {
        private const int width = 383;
        private const int startX = 291;
        private const int startY = 503;
        private const int ACCEPTED_INTERVAL = 3;
        private const int ACCEPTED_AVERAGE_INTERVAL = 2;
        private const int MOUSE_CLICK_LIMIT = 1;

        static Timer myTimer = new Timer();

        // number of clicks
        // must be inferior or equal to MOUSE_CLICK_LIMIT
        static int numberOfClicks = 0;

        // Mouse control
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        private const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention= CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        // Clicker at position x,y
        public void Clicker(int x, int y)
        {
            SetCursorPos(x, y);
            //this.Refresh();
            //Application.DoEvents();
            mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
            numberOfClicks++;
        }

        public Form1()
        {
            InitializeComponent();

            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 100;
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            Bitmap destBitmap = new Bitmap(width, width);

            using (Graphics g = Graphics.FromImage(destBitmap))
            {
                g.CopyFromScreen(new Point(startX, startY), Point.Empty, new Size(width, width));
            }
            pictureBox1.Image = destBitmap;
            //textBox1.Text = Convert.ToString(getSizeOfTable(destBitmap));
            imageProcessing(destBitmap);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="level"></param>
        private void imageProcessing(Bitmap bitmap)
        {
            int sizeOfTable = getSizeOfTable(bitmap);
            if (sizeOfTable != 0)
            {
                int widthZone = bitmap.Width / sizeOfTable;
                Color backgroundColor = bitmap.GetPixel(0, 0);
                
                double S = widthZone * widthZone;
                int[] averageRTable = new int[sizeOfTable * sizeOfTable];
                int[] averageGTable = new int[sizeOfTable * sizeOfTable];
                int[] averageBTable = new int[sizeOfTable * sizeOfTable];

                for (int i = 0; i < sizeOfTable; i++)
                {
                    for (int j = 0; j < sizeOfTable; j++)
                    {
                        // for each zone, we calculate the average color
                        double averageR = 0;
                        double averageG = 0;
                        double averageB = 0;

                        for (int l = i * widthZone; l < (i + 1) * widthZone && l < bitmap.Width; l++)
                        {
                            for (int k = j * widthZone; k < (j + 1) * widthZone && k < bitmap.Height; k++)
                            {
                                Color currentColor = bitmap.GetPixel(l, k);
                                averageR += currentColor.R / S;
                                averageG += currentColor.G / S;
                                averageB += currentColor.B / S;
                            }
                        }
                        averageRTable[sizeOfTable * i + j] = (int)averageR;
                        averageGTable[sizeOfTable * i + j] = (int)averageG;
                        averageBTable[sizeOfTable * i + j] = (int)averageB;
                    }
                }
                // Display all the average case in console
                //displayAllAverageCase(averageRTable, averageGTable, averageBTable, sizeOfTable * sizeOfTable);

                // Find the different case
                int X = -1;
                int Y = -1;
                findTheDifferentCase(averageRTable, averageGTable, averageBTable, sizeOfTable, ref X, ref Y);
                
                // Get the center of the different case in the bitmap
                Point p = centerOfCase(X, Y, sizeOfTable, width);

                // Modify the picture bitmap by adding the center point
                addCenterPointToCaseAndRefresh(p, bitmap);

                // Get the center of the different case in the game screen
                //if (numberOfClicks < MOUSE_CLICK_LIMIT)
                //{
                //    int xCenterInGame = startX + X;
                //    int yCenterInGame = startY + Y;
                //    Clicker(xCenterInGame, yCenterInGame);
                    
                //}
                

                textBox1.Text = Convert.ToString(X) + ", " + Convert.ToString(Y);
            }
        }

        private bool findTheDifferentCase(int[] averageRTable, int[] averageGTable, int[] averageBTable, int sizeOfTable, ref int X, ref int Y)
        {
            bool isFound = false;

            for (int i = 1; i < sizeOfTable * sizeOfTable; i++)
            {
                if (!isSimilar(averageRTable[i], averageRTable[i - 1]))
                {
                    if (i == sizeOfTable * sizeOfTable - 1)
                    {
                        X = (i) / sizeOfTable;
                        Y = (i) % sizeOfTable;
                    }
                    else if (!isSimilar(averageRTable[i + 1], averageRTable[i - 1]))
                    {
                        X = (i - 1) / sizeOfTable;
                        Y = (i - 1) % sizeOfTable;
                    }
                    else
                    {
                        X = (i) / sizeOfTable;
                        Y = (i) % sizeOfTable;
                    }
                    isFound = true;
                    break;
                }
                if (i == sizeOfTable * sizeOfTable-1)
                {
                    isFound = findTheDifferentCase(averageGTable, averageBTable, sizeOfTable, ref X, ref Y);
                }
            }
            return isFound;
        }

        private bool findTheDifferentCase(int[] averageGTable, int[] averageBTable, int sizeOfTable, ref int X, ref int Y)
        {
            bool isFound = false;
            for (int i = 1; i < sizeOfTable * sizeOfTable; i++)
            {
                if (!isSimilar(averageGTable[i], averageGTable[i - 1]))
                {
                    if (i == sizeOfTable * sizeOfTable - 1)
                    {
                        X = (i) / sizeOfTable;
                        Y = (i) % sizeOfTable;
                    }
                    else if (!isSimilar(averageGTable[i + 1], averageGTable[i - 1]))
                    {
                        X = (i - 1) / sizeOfTable;
                        Y = (i - 1) % sizeOfTable;
                    }
                    else
                    {
                        X = (i) / sizeOfTable;
                        Y = (i) % sizeOfTable;
                    }
                    isFound = true;
                    break;
                }
                if (i == sizeOfTable * sizeOfTable - 1)
                {
                    isFound = findTheDifferentCase(averageBTable, sizeOfTable, ref X, ref Y);
                }
            }
            return isFound;
        }

        private bool findTheDifferentCase(int[] averageBTable, int sizeOfTable, ref int X, ref int Y)
        {
            bool isFound = false;

            for (int i = 1; i < sizeOfTable * sizeOfTable; i++)
            {
                if (!isSimilar(averageBTable[i], averageBTable[i - 1]))
                {
                    if (i == sizeOfTable * sizeOfTable - 1)
                    {
                        X = (i) / sizeOfTable;
                        Y = (i) % sizeOfTable;
                    }
                    else if (!isSimilar(averageBTable[i + 1], averageBTable[i - 1]))
                    {
                        X = (i - 1) / sizeOfTable;
                        Y = (i - 1) % sizeOfTable;
                    }
                    else
                    {
                        X = (i) / sizeOfTable;
                        Y = (i) % sizeOfTable;
                    }
                    isFound = true;
                    break;
                }
                if (i == sizeOfTable * sizeOfTable - 1)
                {
                    Console.WriteLine("Not found");
                }
            }
            return isFound;
        }

        private void displayAllAverageCase(int[] RTable, int[] GTable, int[] BTable, int size)
        {
            for (int i=0; i<size; i++)
            {
                Console.WriteLine(RTable[i] + "," + GTable[i] + "," + BTable[i]);
            }
            
        }

        /// <summary>
        /// return size m of the table m x m
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private int getSizeOfTable(Bitmap bitmap)
        {
            int zoneNumber = 1;
            int temp = 1;
            for (int i = 1, j = 1; i < bitmap.Width && j < bitmap.Height; i=i+1, j=j+1)
            {
                Console.WriteLine(bitmap.GetPixel(i, j).ToString());

                if (!similarColors(bitmap.GetPixel(i, j),bitmap.GetPixel(i - 1, j - 1)))
                {
                    temp++;
                }
                else
                {
                    if (temp > 1)
                    {
                        zoneNumber++;
                        //Console.WriteLine(i + "," + j + ":" + bitmap.GetPixel(i, j));
                    }
                    temp = 1;
                }
            }
            return (zoneNumber-1) / 2;
        }

        private bool similarColors(Color c1, Color c2)
        {
            if (absolute(c1.R, c2.R) <= ACCEPTED_INTERVAL
                && absolute(c1.G, c2.G) <= ACCEPTED_INTERVAL
                && absolute(c1.B, c2.B) <= ACCEPTED_INTERVAL)
                return true;
            return false;
        }

        private bool isSimilar(int x1, int x2)
        {
            if (absolute(x1, x2) <= ACCEPTED_AVERAGE_INTERVAL)
                return true;
            return false;
        }

        private int absolute(int x, int y)
        {
            if (x >= y) return x - y;
            return y - x;
        }


        // Run the app with test images 
        private void btn_Test_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Fichiers JPG|*.jpg|Fichers PNG|*.png|Tous les fichiers|*.*";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.InitialDirectory = @"C:\Users\Nhon\workspace\c_sharp\PremierePictureBoxApp\PremierePictureBoxApp\test";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }
        /// <summary>
        /// Get the center of the case
        /// 
        /// </summary>
        /// <param name="a">width</param>
        /// <param name="b">height</param>
        /// <param name="dim">dimension of the table: eg. m</param>
        /// <param name="width">width of the bitmap in pixel</param>
        /// <returns></returns>
        private static Point centerOfCase(int a, int b, int dim, int width)
        {
            try
            {
                Point p = new Point();
                p.X = width / dim * a + width / dim / 2;
                p.Y = width / dim * b + width / dim / 2;
                return p;
            } catch
            {
                throw new Exception("Some error occurs");
            }
        }

        private void addCenterPointToCaseAndRefresh(Point center, Bitmap bm)
        {

            if (center.X > 0 && center.X < bm.Width && center.Y > 0 && center.Y < bm.Height)
            {
                Bitmap m_copyBm = bm;
                // Draw a line Y
                for (int i = 0; i < bm.Width; i++)
                {
                    m_copyBm.SetPixel(i, center.Y, Color.White);
                }

                // draw a line X
                for (int j = 0; j < bm.Height; j++)
                {
                    m_copyBm.SetPixel(center.X, j, Color.White);
                }
                pictureBox1.Image = m_copyBm;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            imageProcessing(bitmap);
        }

        private void btn_live_Click(object sender, EventArgs e)
        {
            
            myTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myTimer.Stop();
        }
    }


}

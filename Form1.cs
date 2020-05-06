using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        private const int MOUSE_CLICK_LIMIT = 10;

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

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
            Thread.Sleep(100);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
            numberOfClicks++;
        }

        public Form1()
        {
            InitializeComponent();

            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 45;
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
                bool draw = true;
                if (sizeOfTable == 2) draw = false;
                addCenterPointToCaseAndRefresh(p, bitmap, widthZone / 2, draw);
                // Draw a circle at point p with R = widthZone/2

                // Get the center of the different case in the game screen
                if (numberOfClicks < MOUSE_CLICK_LIMIT)
                {
                    int xCenterInGame = startX + p.X;
                    int yCenterInGame = startY + p.Y;
                    //Clicker(xCenterInGame, yCenterInGame);

                }


                textBox1.Text = Convert.ToString(X) + ", " + Convert.ToString(Y);
            }
        }

        #region Find the different case

        /// <summary>
        /// Find the different case from RTable then GTable then BTable
        /// </summary>
        /// <param name="averageRTable"></param>
        /// <param name="averageGTable"></param>
        /// <param name="averageBTable"></param>
        /// <param name="sizeOfTable"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find the different case from GTable and BTable if not found in RTable
        /// </summary>
        /// <param name="averageGTable"></param>
        /// <param name="averageBTable"></param>
        /// <param name="sizeOfTable"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find the different case from BTable if not found from RTable and GTable
        /// </summary>
        /// <param name="averageBTable"></param>
        /// <param name="sizeOfTable"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
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

        #endregion

        /// <summary>
        /// Display case averages to console
        /// </summary>
        /// <param name="RTable"></param>
        /// <param name="GTable"></param>
        /// <param name="BTable"></param>
        /// <param name="size"></param>
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

        /// <summary>
        /// Add the center point to the found case then refresh the bitmap
        /// Also add a circle around this point of rayon R
        /// </summary>
        /// <param name="center"></param>
        /// <param name="bm"></param>
        /// <param name="r"></param>
        /// <param name="drawCircle"></param>
        private void addCenterPointToCaseAndRefresh(Point center, Bitmap bm, int r, bool drawCircle)
        {
            
            int xCenter = center.X;
            int yCenter = center.Y;
            int rr = r * r;

            if (xCenter > 0 && xCenter < bm.Width && yCenter > 0 && yCenter < bm.Height)
            {
                Bitmap m_copyBm = bm;
                // Draw a line Y
                for (int i = 0; i < bm.Width; i++)
                {
                    m_copyBm.SetPixel(i, yCenter, Color.White);
                }

                // draw a line X
                for (int j = 0; j < bm.Height; j++)
                {
                    m_copyBm.SetPixel(xCenter, j, Color.White);
                }
                if (drawCircle)
                {
                    // Draw a circle at point p with R = widthZone/2
                    for (int i = xCenter - (int)r; i <= xCenter + r && i < bm.Width; i++)
                        for (int j = yCenter - (int)r; j <= yCenter + r && j < bm.Height; j++)
                            if (Math.Abs(Math.Pow(i - xCenter, 2) + Math.Pow(j - yCenter, 2) - rr) <= r)
                                m_copyBm.SetPixel(i, j, Color.White);
                    pictureBox1.Image = m_copyBm;
                }
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

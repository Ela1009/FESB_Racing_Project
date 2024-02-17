using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1

{
    public partial class Form1 : Form
    {
        private const int numRows = 6;
        private const int numCols = 10;
        private const int cellSize = 50;
        private const int minTemp = 20;
        private const int maxTemp = 50;

        private Label[,] cellLabels = new Label[numRows, numCols];
        private int[,] temperatures = new int[numRows, numCols];
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            StartSimulation();
        }

        private void InitializeGrid()
        {
            Panel panel = new Panel(); 
            panel.Size = new Size(numCols * cellSize, numRows * cellSize);
            panel.Location = new Point(10, 10); 
            this.Controls.Add(panel); 

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    temperatures[i, j] = minTemp; 
                    Label label = new Label();
                    label.Size = new Size(cellSize, cellSize);
                    label.Location = new Point(j * cellSize, i * cellSize);
                    label.BorderStyle = BorderStyle.FixedSingle;
                    label.BackColor = GetColorForTemperature(minTemp); 
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.Text = minTemp.ToString(); 
                    cellLabels[i, j] = label;
                    panel.Controls.Add(label);
                }
            }
        }

        private void StartSimulation()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(500); 

                    for (int i = 0; i < numRows; i++)
                    {
                        for (int j = 0; j < numCols; j++)
                        {
                            temperatures[i, j] += random.Next(-1, 2); 
                            temperatures[i, j] = Math.Min(maxTemp, Math.Max(minTemp, temperatures[i, j])); 

                            this.Invoke((MethodInvoker)delegate
                            {
                                cellLabels[i, j].BackColor = GetColorForTemperature(temperatures[i, j]); 
                                cellLabels[i, j].Text = temperatures[i, j].ToString(); 
                            });
                        }
                    }
                }
            });
        }

        private Color GetColorForTemperature(int temperature)
        {
            int r = Math.Min(255, (temperature - minTemp) * 255 / (maxTemp - minTemp));
            int g = Math.Max(0, 255 - (temperature - minTemp) * 255 / (maxTemp - minTemp));
            int b = 0;
            return Color.FromArgb(r, g, b);
        }

    }
}
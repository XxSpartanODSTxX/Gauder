using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DarcMonitor
{

   


    public partial class GrahControl : UserControl
    {



        double[] xval;
        double[][] yval;
        int index = 0;

        bool _bMagnet;
        public bool bMagnet
        {
            get => _bMagnet;
            set => _bMagnet = value;
        }

        bool _bEncoder = false;
        public bool bEncoder
        {
            get => _bEncoder;
            set => _bEncoder = value;
        }

        public void setValue(int n, int index, double dvalue)
        {
            yval[n][index] = dvalue;
        }

        public GrahControl()
        {
            InitializeComponent();
        }


        public void clearData()
        {
            if(yval != null)
            for (int i = 0; i < 1024; i++)
            {
                for (int j = 0; j < 16; j++)
                    yval[j][i] = 0;
                xval[i] = i;
            }
        }
        public void initData()
        {

            index = 0;
            xval = new double[1024];
            yval = new double[16][];
            for (int i = 0; i < 16; i++)
            {
                yval[i] = new double[1024];
            }
            for (int i = 0; i < 1024; i++)
            {
                xval[i] = i;

            }
        }


        public void updateChart()
        {
            listBoxChanells.Items.Clear();
            this.chart.Series.Clear();
            if (xval == null)
                return;
            clearData();

            //   this.chart.Titles.Add("DARC1000 Positions");
            Series series1 = this.chart.Series.Add("theta1");
            series1.ChartType = SeriesChartType.Spline;
            series1.Points.DataBindXY(xval, yval[0]);
            listBoxChanells.Items.Add(series1);
            listBoxChanells.SetSelected(0, true);

            Series series2 = this.chart.Series.Add("theta2");
            listBoxChanells.Items.Add(series2);
            series2.ChartType = SeriesChartType.Spline;
            series2.Points.DataBindXY(xval, yval[1]);
            listBoxChanells.SetSelected(1, true);
            if (_bEncoder)
            {
                Series series3 = this.chart.Series.Add("Encoder1");
                listBoxChanells.Items.Add(series3);
                series3.ChartType = SeriesChartType.Spline;
                series3.Points.DataBindXY(xval, yval[4]);
                listBoxChanells.SetSelected(2, true);


                Series series4 = this.chart.Series.Add("Encoder2");
                listBoxChanells.Items.Add(series4);
                series4.ChartType = SeriesChartType.Spline;
                series4.Points.DataBindXY(xval, yval[5]);
                listBoxChanells.SetSelected(3, true);
            }
            else
            {
                listBoxChanells.Items.Add("Encoder1");
                listBoxChanells.Items.Add("Encoder2");
            }


            if (_bMagnet)
            {
                Series series5 = this.chart.Series.Add("Magnet1");
                listBoxChanells.Items.Add(series5);
                series5.ChartType = SeriesChartType.Spline;
                series5.Points.DataBindXY(xval, yval[6]);
                listBoxChanells.SetSelected(4, true);

                Series series6 = this.chart.Series.Add("Magnet2");
                listBoxChanells.Items.Add(series6);
                series6.ChartType = SeriesChartType.Spline;
                series6.Points.DataBindXY(xval, yval[7]);
                listBoxChanells.SetSelected(5, true);

            }
            else
            {
                listBoxChanells.Items.Add("Magnet1");
                listBoxChanells.Items.Add("Magnet2");
            }

        }


        private void listBoxChanells_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bEncoder = listBoxChanells.GetSelected(2);
                bMagnet = listBoxChanells.GetSelected(4);
            }
            catch (Exception ex) { }

        }


        public void timerUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (index >= (1024 - 20))
                {
                    for (int k = 0; k < 1024; k++)
                    {
                        xval[k] += 20;
                        for (int n = 0; n < 8; n++)
                        {
                            if (k < (1024 - 20))
                                yval[n][k] = yval[n][k + 20];
                            else
                                yval[n][k] = 0;
                        }
                    }
                    index -= 20;
                }
                this.chart.Series["theta1"].Points.DataBindXY(xval, yval[0]);
                this.chart.Series["theta2"].Points.DataBindXY(xval, yval[1]);
                if (_bEncoder)
                {
                    this.chart.Series["Encoder1"].Points.DataBindXY(xval, yval[4]);
                    this.chart.Series["Encoder2"].Points.DataBindXY(xval, yval[5]);
                }
                if (_bMagnet)
                {
                    this.chart.Series["Magnet1"].Points.DataBindXY(xval, yval[6]);
                    this.chart.Series["Magnet2"].Points.DataBindXY(xval, yval[7]);
                }
                /*
                for (int i = index; i < 1024; i++)
                {
                    this.chart.Series["theta1"].Points[i].IsEmpty = true;
                    this.chart.Series["theta2"].Points[i].IsEmpty = true;
                    this.chart.Series["theta1a"].Points[i].IsEmpty = true;
                    this.chart.Series["theta2a"].Points[i].IsEmpty = true;
                    this.chart.Series["theta1b"].Points[i].IsEmpty = true;
                    this.chart.Series["theta2b"].Points[i].IsEmpty = true;
                }*/
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            updateChart();
        }
    }
}

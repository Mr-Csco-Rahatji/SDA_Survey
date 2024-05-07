using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDA_Survey
{
    public partial class frmResults : Form
    {
        public frmResults()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void lblFillOutSurvey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Thread t2 = new Thread(() =>
            {
                Application.Run(new frmSdaSurvey());
            });
            t2.Start();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void frmResults_Load(object sender, EventArgs e)
        {
            Connection conn = new Connection();
            lblnSurveys.Text = conn.getNumberOfSurveys().ToString();
            lblAvgSurveys.Text=conn.getAvgAge().ToString("F2");
            lblMaxAge.Text = conn.getOld().ToString();
            lblMinAge.Text = conn.getYoung().ToString();
            lblPizza.Text= conn.getPizza().ToString("F1")+"%";
            lblPasta.Text= conn.getPasta().ToString("F1") + "%";
            lblPap.Text= conn.getPap().ToString("F1") + "%";
            lblAvgMovies.Text = conn.getMovies().ToString("F1");
            lblAvgRadio.Text = conn.getRadio().ToString("F1");
            lblAvgEat.Text = conn.getEatOut().ToString("F1");
            lblAvgTv.Text = conn.getWatchTv().ToString("F1");
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SDA_Survey
{
    public partial class frmSdaSurvey : Form
    {
        public frmSdaSurvey()
        {
            InitializeComponent();
        }


        private void frmSdaSurvey_Load(object sender, EventArgs e)
        {

        }


        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            connection conn = new connection();
            conn.addUser();
        }
    }
}

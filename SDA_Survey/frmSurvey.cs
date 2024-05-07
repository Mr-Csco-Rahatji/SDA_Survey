using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Cache;
using System.Threading;

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
            List<User> users = new List<User>();
            Connection conn = new Connection();
            users = conn.getAllUsers();

            Console.WriteLine("You have " + users.Count + " users sir");

            for(int a=0;a<users.Count; a++)
            {
                Console.WriteLine("Name is " + users[a].full_names);
                for(int b = 0; b < users[a].likes.Count; b++)
                {
                   // Console.WriteLine( "I like  " + users[a].likes[b].type +" by this much : "+ users[a].likes[b].rating);
                }
                Console.WriteLine("I am " + (DateTime.Now.Year - users[a].dob.Year));
            }
        }


        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            if (validateInput())
            {
                lblNullsExist.Visible = false;
                Connection conn = new Connection();
                if (conn.addUser(getInformation()))
                {
                    MessageBox.Show("Survey Recorded");
                }
                else
                {
                    MessageBox.Show("User Already exists");
                }
            }
            else
            {
                lblNullsExist.Visible = true;
            }
        }

        public bool validateInput()
        {
            bool status=true;
            if(DateTime.Now.Year-dtpDob.Value.Date.Year<5 || DateTime.Now.Year - dtpDob.Value.Date.Year > 120)
            {
                status = false;
                lbldToErr.Visible = true;
            }
            else
            {
                lbldToErr.Visible=false;
            }
            if (txtFull_Names.Text.Length < 3)
            { 
                lblFullErr.Visible = true;
                status = false;
            } else 
            { 
                lblFullErr.Visible = false;
            }

            if(txtEmail.Text.Length < 3)
            {
                lblEmailErr.Visible = true;
                status = false;
            }
            else {
                for(int a=0;a<txtEmail.Text.Length;a++)
                {
                    if (txtEmail.Text[a] == '@')
                    {
                        lblEmailErr.Visible = false;
                        break;
                    }
                }
               
            }

            if (txtContact.Text.Length == 10)
            {
                lblContactErr.Visible = false ;
            }
            else {
                lblContactErr.Visible = true;
                status = false;
            }

            if (rbMovies1.Checked)
            {
                lblMovieErr.Visible = false;
            }else if (rbMovies2.Checked)
            {
                lblMovieErr.Visible = false;
            }else if (rbMovies3.Checked)
            {
                lblMovieErr.Visible = false;
            }else if (rbMovies4.Checked)
            {
                lblMovieErr.Visible = false;
            }else if (rbMovies5.Checked)
            {
                lblMovieErr.Visible = false;
            }
            else
            {
                status = false;
                lblMovieErr.Visible = true;
            }


            if (rbRadio1.Checked)
            {
                lblRadioErr.Visible = false;
            }else if (rbRadio2.Checked)
            {
                    lblRadioErr.Visible = false;
            }else if (rbRadio3.Checked)
            {
                    lblRadioErr.Visible = false;
            }else if (rbRadio4.Checked)
            {
                lblRadioErr.Visible = false;
            }else if (rbRadio5.Checked)
            {
                lblRadioErr.Visible = false;
            }
            else
            {
                status = false;
                lblRadioErr.Visible = true;
            }

            if (rbEat1.Checked)
            {
                lblEatErr.Visible=false;
            }else if (rbEat2.Checked)
            {
                lblEatErr.Visible=false;
            }else if (rbEat3.Checked)
            {
                lblEatErr.Visible=false;
            }else if (rbEat4.Checked)
            {
                lblEatErr.Visible=false;
            }else if (rbEat5.Checked)
            {
                lblEatErr.Visible=false;
            }else
            {
                status = false;
                lblEatErr.Visible = true;
            }

            if (rbTV1.Checked)
            {
                lblTVErr.Visible=false;
            }else if (rbTV2.Checked)
            {
                lblTVErr.Visible=false;
            }else if(rbTV3.Checked)
            {
                lblTVErr.Visible=false;
            }else if(rbTV4.Checked)
            {
                lblTVErr.Visible=false;
            }else if(rbTV5.Checked)
            {
                lblTVErr.Visible=false;
            }else
            {
                status=false;
                lblTVErr.Visible = true;
            }

            Console.WriteLine(status);
            return status;
        }

        private User getInformation()
        {
            User user = new User();
            user.full_names=txtFull_Names.Text; 
            user.email=txtEmail.Text;
            user.dob = Convert.ToDateTime(dtpDob.Value); //remeber to convert to shortdatestring
            user.contact=txtContact.Text;
            user.foodList = new List<FavFood>();

            if(chkPizza.Checked ) {
                user.foodList.Add(new FavFood(Type.PIZZA));
            }
            if (chkPasta.Checked)
            {
                user.foodList.Add(new FavFood(Type.PASTA));
            }
            if (chkPapandWors.Checked)
            {
                user.foodList.Add(new FavFood(Type.PAP_AND_WORS));
            }
            if (chkOther.Checked)
            {
                user.foodList.Add(new FavFood(Type.OTHER));
            }

            user.likes = new List<Like>();
            user.likes.Add(new Like());
            user.likes.Add(new Like());
            user.likes.Add(new Like());
            user.likes.Add(new Like());

            //Get User Rating
            if (rbMovies1.Checked) { user.likes[0].rating = 1; }
            if (rbMovies2.Checked) { user.likes[0].rating = 2; }
            if (rbMovies3.Checked) { user.likes[0].rating = 3; }    //Revise and make shorter if there is still timne
            if (rbMovies4.Checked) { user.likes[0].rating = 4; }
            if (rbMovies5.Checked) { user.likes[0].rating = 5; }

            if (rbRadio1.Checked) { user.likes[1].rating = 1; }
            if (rbRadio2.Checked) { user.likes[1].rating = 2; }
            if (rbRadio3.Checked) { user.likes[1].rating = 3; }
            if (rbRadio4.Checked) { user.likes[1].rating = 4; }
            if (rbRadio5.Checked) { user.likes[1].rating = 5; }

            if (rbEat1.Checked) { user.likes[2].rating = 1; }
            if (rbEat2.Checked) { user.likes[2].rating = 2; }
            if (rbEat3.Checked) { user.likes[2].rating = 3; }
            if (rbEat4.Checked) { user.likes[2].rating = 4; }
            if (rbEat5.Checked) { user.likes[2].rating = 5; }

            if (rbTV1.Checked) { user.likes[3].rating = 1; }
            if (rbTV2.Checked) { user.likes[3].rating = 2; }
            if (rbTV3.Checked) { user.likes[3].rating = 3; }
            if (rbTV4.Checked) { user.likes[3].rating = 4; }
            if (rbTV5.Checked) { user.likes[3].rating = 5; }
            return user;
        }

        private void lblViewSurveyResults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Thread t2 = new Thread(() =>
            {
                Application.Run(new frmResults());
            });
            t2.Start();
            this.Close();
        }

        private void lblFillOutSurvey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        /*
        public void ShowUsers(User user)
        {
            Console.WriteLine("users name is "+user.full_names);
            Console.WriteLine("email is "+user.email);
            Console.WriteLine("dob is "+user.dob.ToShortDateString());
            Console.WriteLine("contact is "+user.contact);
            Console.WriteLine("Food list :");
            for (int a = 0; a < user.foodList.Count; a++)
            {
                Console.WriteLine(user.foodList[a].type);         
            }
            Console.WriteLine("Rating :");
            Console.WriteLine("Movies " + user.likes[0].rating);
            Console.WriteLine("Radio " + user.likes[1].rating);
            Console.WriteLine("Eat Out " + user.likes[2].rating);
            Console.WriteLine("TV " + user.likes[3].rating);
            
        }*/

    }
}

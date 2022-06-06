using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Liabry__Assignment_
{
    public partial class Library : Form
    {
        Database db = new Database();

        string startupPath = Directory.GetCurrentDirectory();
        string path;
        public Library()
        {
            InitializeComponent();
            MaximizeBox = false;
            string newPath = Path.GetFullPath(Path.Combine(startupPath, @"..\..\.."));
            path = newPath;

            try
            {
                pictureBox2.Image = Image.FromFile(path + @"\Images\Buttons\Untitled-2.png");
                pictureBox3.Image = Image.FromFile(path + @"\Images\Buttons\CreateAccount.png");
                userButton.Image = Image.FromFile(path + @"\Images\Buttons\userClicked1.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        public void tryLogin()
        {
            if (label5.Visible == true)
            {
                userDetails userFound = db.userLogin(usernameTXT.Text, passwordTXT.Text);
                if (userFound != null && userFound.password.Equals(passwordTXT.Text))
                {
                    this.Hide();
                    MyBooks MyBooksForm = new MyBooks(ref db, ref userFound);
                    MyBooksForm.Show(this);
                }
                else
                {
                    MessageBox.Show("Invalid username and/or password entered");
                }
            }
            else if (label1.Visible == true)
            {
                AdminDetails adminFound = db.adminLogin(usernameTXT.Text, passwordTXT.Text);
                
                if (adminFound != null && adminFound.password.Equals(passwordTXT.Text))
                {
                    this.Hide();
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show(this);
                }
                else
                {
                    MessageBox.Show("Invalid username and/or password entered");
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            tryLogin();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            CreateAccount CAForm = new CreateAccount();
            CAForm.Show();
            this.Hide();
        }

        private void pictureBox2_MouseEnter_1(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = Image.FromFile(path + @"\Images\Buttons\login.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        private void pictureBox2_MouseLeave_1(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = Image.FromFile(path + @"\Images\Buttons\Untitled-2.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                pictureBox3.Image = Image.FromFile(path + @"\Images\Buttons\CreateAccountHover.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                pictureBox3.Image = Image.FromFile(path + @"\Images\\Buttons\\CreateAccount.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        private void passwordTXT_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && string.IsNullOrWhiteSpace(usernameTXT.Text))
                usernameTXT.Focus();
            else if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(usernameTXT.Text))
                tryLogin();
        }

        private void usernameTXT_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && string.IsNullOrWhiteSpace(passwordTXT.Text))
                passwordTXT.Focus();
            else if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(passwordTXT.Text))
                tryLogin();
        }

        private void userButton_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                userButton.Image = Image.FromFile(path + @"\Images\Buttons\userHover.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        private void userButton_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (label5.Visible == true)
                {
                    userButton.Image = Image.FromFile(path + @"\Images\Buttons\userClicked1.png");
                }
                else
                {
                    userButton.Image = Image.FromFile(path + @"\Images\Buttons\userButton.png");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            try
            {
                string newPath = Path.GetFullPath(Path.Combine(startupPath, @"..\..\.."));
                adminButton.Image = Image.FromFile(path + @"\Images\Buttons\admin.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
            label5.Visible = true;
            label1.Visible = false;
            pictureBox3.Visible = true;
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            userButton.Image = Image.FromFile(path + @"\Images\Buttons\userButton.png");
            label5.Visible = false;
            label1.Visible = true;
            pictureBox3.Visible = false;
        }

        private void adminButton_MouseEnter(object sender, EventArgs e)
        {
            string newPath = Path.GetFullPath(Path.Combine(startupPath, @"..\..\.."));
            adminButton.Image = Image.FromFile(path + @"\Images\Buttons\adminHover.png");
        }

        private void adminButton_MouseLeave(object sender, EventArgs e)
        {
            if (label5.Visible == true)
            {
                adminButton.Image = Image.FromFile(path + @"\Images\Buttons\admin.png");
            }
            else
            {
                adminButton.Image = Image.FromFile(path + @"\Images\Buttons\adminClicked.png");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            HelpMenu_ help = new HelpMenu_("loginForm");
            help.Show();
            this.Hide();
        }
    }
}

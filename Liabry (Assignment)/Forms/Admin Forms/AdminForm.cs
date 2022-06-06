using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liabry__Assignment_
{
    public partial class AdminForm : Form
    {
        Database runDatabase = new Database();
        public AdminForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                userDetails UD = runDatabase.searchUserRecord(textBox1.Text);
                comboBox1.Text = UD.title.ToString();
                firstName_txtBx.Text = UD.firstName;
                lastName_txtBx.Text = UD.lastName;
                firstLineAddress_txtBx.Text = UD.firstLineAddress;
                secondLineAddress_txtBx.Text = UD.secondLineAddress;
                postCode_txtBx.Text = UD.postCode;
                dateTimePicker1.Value = UD.DOB;
                email_txtBx.Text = UD.email;
                userName_txtBx.Text = UD.userName;
                password_txtBx.Text = UD.password;
                phoneNumber_txtBx.Text = UD.phoneNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong\n" + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            runDatabase.deleteUser(userName_txtBx.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddBooks addBookForm = new AddBooks();
            addBookForm.Show();
            this.Close();
        }

        //Edit books button
        private void button8_Click(object sender, EventArgs e)
        {
            EditBooks edit = new EditBooks();
            edit.Show();
            this.Close();
        }

        //Edit users button
        private void button9_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.Show();
            this.Close();
        }

        //Logout button
        private void button11_Click(object sender, EventArgs e)
        {
            Library f1 = new Library();
            f1.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddBooks addBookForm = new AddBooks();
            addBookForm.Show();
            this.Close();
        }

        //Rental info button
        private void button2_Click_1(object sender, EventArgs e)
        {
            RentalInfo retnalInfoForm = new RentalInfo();
            retnalInfoForm.Show();
            this.Close();
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }
    }
}

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
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            populateTitleComboBox();
        }

        public void populateTitleComboBox()
        {
            comboBox1.Items.Add(title.Mr);
            comboBox1.Items.Add(title.Mrs);
            comboBox1.Items.Add(title.Miss);
            comboBox1.Items.Add(title.Ms);
            comboBox1.Items.Add(title.Mx);
            comboBox1.Items.Add(title.Master);
            comboBox1.Items.Add(title.Professor);
            comboBox1.Items.Add(title.Sir);
            comboBox1.Items.Add(title.Doctor);
            comboBox1.Items.Add(title.Madam);
            comboBox1.Items.Add(title.Dame);
            comboBox1.Items.Add(title.Lady);
            comboBox1.Items.Add(title.Lord);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            closeForm();
        }

        private void CreateAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void error(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        public void closeForm()
        {
            Library F1 = new Library();
            F1.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            closeForm();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            userDetails newUser = new userDetails();

            newUser.title = (title)comboBox1.SelectedIndex;
            newUser.firstName = firstName_txtBx.Text;
            newUser.lastName = lastName_txtBx.Text;
            newUser.firstLineAddress = firstLineAddress_txtBx.Text;
            newUser.secondLineAddress = secondLineAddress_txtBx.Text;
            newUser.postCode = postCode_txtBx.Text;
            newUser.DOB = dateTimePicker1.Value;

            if (email_txtBx.Text == reTypeEmail_txtBx.Text)
            {
                newUser.email = email_txtBx.Text;
            }
            else
            {
                error("The emails don't match");
            }

            newUser.userName = userName_txtBx.Text;

            if (password_txtBx.Text == reTypePassword_txtBx.Text)
            {
                newUser.password = password_txtBx.Text;
            }
            else
            {
                error("The passwords don't match");
            }

            newUser.phoneNumber = phoneNumber_txtBx.Text;

            Database runDatabase = new Database();
            runDatabase.createAccount(newUser);

            firstName_txtBx.Clear();
            lastName_txtBx.Clear();
            lastName_txtBx.Clear();
            firstLineAddress_txtBx.Clear();
            userName_txtBx.Clear();
            reTypePassword_txtBx.Clear();
            reTypeEmail_txtBx.Clear();
            phoneNumber_txtBx.Clear();
            secondLineAddress_txtBx.Clear(); ;
            postCode_txtBx.Clear();
            dateTimePicker1.Value = DateTime.Now;
            email_txtBx.Clear();
            password_txtBx.Clear();
        }

        private void adminBTN_Click(object sender, EventArgs e)
        {
            closeForm();
        }
    }
}

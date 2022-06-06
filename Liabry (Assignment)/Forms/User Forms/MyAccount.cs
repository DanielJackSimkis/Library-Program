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
    public partial class MyAccount : Form
    {
        userDetails currentUser = new userDetails();
        Database runDatabase = new Database();

        public MyAccount(ref Database runDatabase, ref userDetails currentUser)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            this.currentUser = currentUser;
            populateTitleComboBox();
        }

        public void populateTitleComboBox()
        {
            titleComboBox.Items.Add(title.Mr);
            titleComboBox.Items.Add(title.Mrs);
            titleComboBox.Items.Add(title.Miss);
            titleComboBox.Items.Add(title.Ms);
            titleComboBox.Items.Add(title.Mx);
            titleComboBox.Items.Add(title.Master);
            titleComboBox.Items.Add(title.Professor);
            titleComboBox.Items.Add(title.Sir);
            titleComboBox.Items.Add(title.Doctor);
            titleComboBox.Items.Add(title.Madam);
            titleComboBox.Items.Add(title.Dame);
            titleComboBox.Items.Add(title.Lady);
            titleComboBox.Items.Add(title.Lord);

            titleComboBox.SelectedIndex = 0;
        }

        public void populateTextBoxes()
        {
            clearText();
            try
            {

                disableTextBoxes();
                dateTimePicker1.Value = currentUser.DOB;
                title_txtBx.Text = currentUser.title.ToString();
                fname_txtBx.Text = currentUser.firstName;
                lname_txtBx.Text = currentUser.lastName;
                FLAddress_txtBx.Text = currentUser.firstLineAddress;
                SLAddress_txtBx.Text = currentUser.secondLineAddress;
                postCode_txtBx.Text = currentUser.postCode;
                DOB_txtBx.Text = currentUser.DOB.ToString("dd/MM/yyyy");
                email_txtBx.Text = currentUser.email;
                userName_txtBx.Text = currentUser.userName;
                password_txtBx.Text = currentUser.password;
                phoneNumber_txtBx.Text = currentUser.phoneNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong\n" + ex.Message);
            }
        }

        //Edit Button
        private void button4_Click_1(object sender, EventArgs e)
        {
            enableTextBoxes();
            titleComboBox.Visible = true;
            title_txtBx.Visible = false;
            DOB_txtBx.Hide();
            dateTimePicker1.Show();
        }

        public void clearText()
        {
            title_txtBx.Clear();
            fname_txtBx.Clear();
            lname_txtBx.Clear();
            FLAddress_txtBx.Clear();
            SLAddress_txtBx.Clear();
            postCode_txtBx.Clear();
            DOB_txtBx.Clear();
            email_txtBx.Clear();
            userName_txtBx.Clear();
            password_txtBx.Clear();
            phoneNumber_txtBx.Clear();
        }

        public void enableTextBoxes()
        {
            title_txtBx.Enabled = true;
            fname_txtBx.Enabled = true;
            lname_txtBx.Enabled = true;
            FLAddress_txtBx.Enabled = true;
            SLAddress_txtBx.Enabled = true;
            postCode_txtBx.Enabled = true;
            DOB_txtBx.Enabled = true;
            email_txtBx.Enabled = true;
            userName_txtBx.Enabled = true;
            password_txtBx.Enabled = true;
            phoneNumber_txtBx.Enabled = true;
        }

        public void disableTextBoxes()
        {
            title_txtBx.Enabled = false;
            fname_txtBx.Enabled = false;
            lname_txtBx.Enabled = false;
            FLAddress_txtBx.Enabled = false;
            SLAddress_txtBx.Enabled = false;
            postCode_txtBx.Enabled = false;
            DOB_txtBx.Enabled = false;
            email_txtBx.Enabled = false;
            userName_txtBx.Enabled = false;
            password_txtBx.Enabled = false;
            phoneNumber_txtBx.Enabled = false;
        }

        //Search button (takes the user to the search page)
        private void button2_Click(object sender, EventArgs e)
        {
            SearchBooks s = new SearchBooks(ref runDatabase, ref currentUser);
            this.Hide();
            s.Show();
        }

        //My books (takes the user to the my books page)
        private void button6_Click(object sender, EventArgs e)
        {
            MyBooks myBooksForm = new MyBooks(ref runDatabase, ref currentUser);
            this.Hide();
            myBooksForm.Show();
        }

        //My account (takes the user to the my account page)
        private void button1_Click(object sender, EventArgs e)
        {
            MyAccount account = new MyAccount(ref runDatabase, ref currentUser);
            account.populateTextBoxes();
            this.Hide();
            account.Show();
        }

        //logout button
        private void button5_Click(object sender, EventArgs e)
        {
            Library F1 = new Library();
            F1.Show();
            this.Hide();
        }

        //Save changes button
        private void button8_Click_1(object sender, EventArgs e)
        {
            DOB_txtBx.Show();
            string title = titleComboBox.SelectedItem.ToString();
            currentUser.title = runDatabase.convert(title);
            currentUser.firstName = fname_txtBx.Text;
            currentUser.lastName = lname_txtBx.Text;
            currentUser.firstLineAddress = FLAddress_txtBx.Text;
            currentUser.secondLineAddress = SLAddress_txtBx.Text;
            currentUser.postCode = postCode_txtBx.Text;
            currentUser.DOB = dateTimePicker1.Value;
            currentUser.email = email_txtBx.Text;
            currentUser.userName = userName_txtBx.Text;
            currentUser.password = password_txtBx.Text;
            currentUser.phoneNumber = phoneNumber_txtBx.Text;
            //
            runDatabase.updateUserRecord(currentUser);
            //
            populateTextBoxes();


            titleComboBox.Visible = false;
            title_txtBx.Visible = true;
        }

        //cancel button
        private void button3_Click_1(object sender, EventArgs e)
        {
            disableTextBoxes();
            DOB_txtBx.Show();
            dateTimePicker1.Hide();
            titleComboBox.Visible = false;
            title_txtBx.Visible = true;
        }
    }
}

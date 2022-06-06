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
    public partial class RentalInfo : Form
    {
        Database runDatabase = new Database();
        List<BookDetails> bookInfoList = new List<BookDetails>();
        List<userDetails> userList = new List<userDetails>();

        public RentalInfo()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            populateListBox();
        }

        public void populateListBox()
        {
            try
            {
                bookInfoList = runDatabase.getLateBooks();
                bookInfoList.OrderBy(o => o.bookReference);
                int index = 0;

                while (index < bookInfoList.Count - 1)
                {
                    if (bookInfoList[index].bookReference == bookInfoList[index + 1].bookReference)
                        bookInfoList.RemoveAt(index);
                    else
                        index++;
                }

                foreach (BookDetails item in bookInfoList)
                {
                    listBox1.Items.Add("#" + item.bookReference + "\t" + item.bookTitle);
                }

                listBox1.SelectedIndex = 0;

                bookInfoList.Clear();
                bookInfoList = runDatabase.allRentals();
                bookInfoList.OrderBy(o => o.bookReference);
                index = 0;

                while (index < bookInfoList.Count - 1)
                {
                    if (bookInfoList[index].bookReference == bookInfoList[index + 1].bookReference)
                        bookInfoList.RemoveAt(index);
                    else
                        index++;
                }


                foreach (BookDetails item in bookInfoList)
                {
                    listBox3.Items.Add("#" + item.bookReference + "\t" + item.bookTitle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong\n" + ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            populateListBox();
        }

        //Edit users button
        private void button3_Click(object sender, EventArgs e)
        {
            EditBooks eb = new EditBooks();
            eb.Show();
            this.Close();
        }

        //Add books button
        private void button4_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.Show();
            this.Close();
        }

        //Edit books button
        private void button6_Click(object sender, EventArgs e)
        {
            EditBooks eb = new EditBooks();
            eb.Show();
            this.Close();
        }

        //Rental info button
        private void button5_Click(object sender, EventArgs e)
        {
            RentalInfo ri = new RentalInfo();
            ri.Show();
            this.Close();
        }

        //Logout button
        private void button7_Click(object sender, EventArgs e)
        {
            Library f1 = new Library();
            f1.Show();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Clear();
                userList.Clear();
                pictureBox1.Image = bookInfoList[listBox1.SelectedIndex].bookCover;

                userList = runDatabase.getLateRentalsUserDetails(bookInfoList[listBox1.SelectedIndex].bookReference);

                foreach (userDetails item in userList)
                {
                    listBox2.Items.Add(" #" + item.userID + "\t" + item.firstName + "\t" + item.lastName);
                }

                listBox2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong\n" + ex.Message);
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                listBox4.Items.Clear();
                userList.Clear();
                pictureBox3.Image = bookInfoList[listBox3.SelectedIndex].bookCover;

                userList = runDatabase.getLateRentalsUserDetails(bookInfoList[listBox3.SelectedIndex].bookReference);

                foreach (userDetails item in userList)
                {
                    listBox4.Items.Add(" #" + item.userID + "\t" + item.firstName + "\t" + item.lastName);
                }

                listBox4.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong\n" + ex.Message);
            }
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = userList[listBox2.SelectedIndex].userPhoto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox4.Image = userList[listBox2.SelectedIndex].userPhoto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Could not find image\n" + ex.Message);
            }
        }
    }
}

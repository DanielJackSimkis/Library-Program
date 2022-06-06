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
    public partial class AddBooks : Form
    {
        Database runDatabase = new Database();
        public AddBooks()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        BookDetails newBook = new BookDetails();
        //Add book
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                newBook.bookReference = int.Parse(bookRefTXT.Text);
                newBook.bookTitle = titleTXT.Text;
                newBook.author = authorTXT.Text;
                newBook.publisher = publisherTXT.Text;
                newBook.ISBN = long.Parse(ISBNTXT.Text);
                newBook.genre = genreTXT.Text;
                newBook.rentalDurationInWeeks = int.Parse(rentalDurationTXT.Text);
                newBook.isleNumber = int.Parse(isleNumberTXT.Text);
                newBook.rowNumber = int.Parse(rowNumberTXT.Text);

                runDatabase.addBook(newBook);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong.. make sure all fields are filled out\n" + ex.Message);
            }
        }

        //Logout Button
        private void button7_Click(object sender, EventArgs e)
        {
            Library F1 = new Library();
            F1.Show();
            this.Hide();
        }

        private void helpBTN_Click(object sender, EventArgs e)
        {
            //To be completed
        }

        // Edit books button
        private void button6_Click(object sender, EventArgs e)
        {
            EditBooks eb = new EditBooks();
            eb.Show();
            this.Hide();
        }

        //settings button
        private void button1_Click(object sender, EventArgs e)
        {

        }

        //rental info button
        private void button5_Click(object sender, EventArgs e)
        {
            RentalInfo ri = new RentalInfo();
            ri.Show();
            this.Hide();
        }

        //Account button
        private void button2_Click(object sender, EventArgs e)
        {
            //Might be removed revisit later
        }

        //Edit users button
        private void button4_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.Show();
            this.Hide();
        }

        //Add books Button 
        private void button11_Click(object sender, EventArgs e)
        {
            AddBooks ab = new AddBooks();
            ab.Show();
            this.Hide();
        }

        //Browse file Button
        private void button3_Click_1(object sender, EventArgs e)
        {
            getImage();
        }

        public void getImage()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "image Files (*.png, .jpg, .bmp)|*.png; *.bmp; *.jpg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string strfilename = openFileDialog1.FileName;
                            imageTXT.Text = strfilename;
                            newBook.bookDirectory = strfilename;
                            bookImageBox.ImageLocation = openFileDialog1.FileName;
                            newBook.bookCover = bookImageBox.Image;
                            MessageBox.Show(strfilename);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}

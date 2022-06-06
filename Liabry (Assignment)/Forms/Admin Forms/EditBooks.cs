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
    public partial class EditBooks : Form
    {
        Database runDataBase = new Database();
        BookDetails book = new BookDetails();
        userDetails ud = new userDetails();

        public EditBooks(ref BookDetails book)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        public EditBooks()
        {
            InitializeComponent();
        }

        //Search button
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                book = runDataBase.searchBookReference(int.Parse(bookReferenceTXT.Text));

                titleTXT.Text = book.bookTitle;
                authotTXT.Text = book.author;
                publisherTXT.Text = book.publisher;
                pictureBox1.Image = book.bookCover;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong\n" + ex.Message);
            }
        }

        //Save Chnages button
        private void button7_Click(object sender, EventArgs e)
        {
            book.bookTitle = titleTXT.Text;
            book.author = authotTXT.Text;
            book.publisher = publisherTXT.Text;
            book.bookCover = pictureBox1.Image;

            runDataBase.updateBook(book, int.Parse(bookReferenceTXT.Text));
        }

        //Edit Users Button
        private void button11_Click(object sender, EventArgs e)
        {
            AdminForm admin = new AdminForm();
            admin.Show();
            this.Hide();
        }

        //Add Books Button
        private void button4_Click(object sender, EventArgs e)
        {
            AddBooks ab = new AddBooks();
            ab.Show();
            this.Hide();
        }

        //rental info button
        private void button2_Click(object sender, EventArgs e)
        {
            RentalInfo ri = new RentalInfo();
            ri.Show();
            this.Hide();
        }

        //My Account button
        private void button10_Click(object sender, EventArgs e)
        {
            //To be Completed
        }

        //Edit books button
        private void button8_Click(object sender, EventArgs e)
        {
            EditBooks eb = new EditBooks();
            eb.Show();
            this.Hide();
        }

        //Logout Button
        private void button12_Click(object sender, EventArgs e)
        {
            Library f1 = new Library();
            f1.Show();
            this.Hide();
        }
    }
}

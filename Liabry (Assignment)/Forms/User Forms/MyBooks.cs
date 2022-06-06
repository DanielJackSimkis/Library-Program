using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Liabry__Assignment_
{
    public partial class MyBooks : Form
    {
        List<BookDetails> listOfBooks = new List<BookDetails>();
        List<Rentals> listOfRentalInfo = new List<Rentals>();
        Database runDatabase = new Database();
        userDetails currentUser = new userDetails();

        public MyBooks(ref Database db, ref userDetails UD)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            currentUser = UD;
            runDatabase = db;
            loadUserBooks();
        }

        //OverLoaded Constructer (Used so help Form can create class object)
        public MyBooks()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            loadUserBooks();
        }


        private void MyBooks_Load(object sender, EventArgs e)
        {
            userName.Text = currentUser.userName;
            loadUserBooks();
        }

        public void loadUserBooks()
        {
            listBox1.Items.Clear();
            listOfBooks.Clear();
            listOfRentalInfo.Clear();
            listOfBooks = runDatabase.getBooks(currentUser.userID);
            listOfRentalInfo = runDatabase.getRentals(currentUser.userID);

            foreach (BookDetails item in listOfBooks)
            {
                listBox1.Items.Add("#" + item.bookReference + "\t" + item.bookTitle);
            }

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = listOfBooks[listBox1.SelectedIndex].bookCover;
                author_txt.Text = listOfBooks[listBox1.SelectedIndex].author;
                title_txt.Text = listOfBooks[listBox1.SelectedIndex].bookTitle;
                rentalDate_txt.Text = listOfRentalInfo[listBox1.SelectedIndex].rental_date.ToString("dd - MM - yyyy");
                returnDate_txt.Text = listOfRentalInfo[listBox1.SelectedIndex].return_date.ToString();
                rowNumberTXT.Text = listOfBooks[listBox1.SelectedIndex].rowNumber.ToString();
                islenumberTXT.Text = listOfBooks[listBox1.SelectedIndex].isleNumber.ToString();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Oops Somthing went wrong... Image could not be found \n" + ex.Message);
            }          
        }

        private void MyBooks_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public bool canRentBook()
        {
            if (listBox1.Items.Count >= 4)
            {
                return false;
            }
            else
            {
                return true;
            }           
        }

        //Search Button
        private void button3_Click(object sender, EventArgs e)
        {
            SearchBooks s = new SearchBooks(ref runDatabase, ref currentUser);
            this.Hide();
            s.Show();
        }

        //My account (Returns the user to the my account page)
        private void button4_Click(object sender, EventArgs e)
        {
            MyAccount account = new MyAccount(ref runDatabase, ref currentUser);
            account.populateTextBoxes();
            this.Hide();
            account.Show();
        }

        //Logout button
        private void button5_Click(object sender, EventArgs e)
        {
            Library F1 = new Library();
            F1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            runDatabase.returnBook(listOfRentalInfo[listBox1.SelectedIndex].rental_ID, listOfBooks[listBox1.SelectedIndex].stockNumber + 1);
            loadUserBooks();
        }
        private void helpBTN_Click(object sender, EventArgs e)
        {
            HelpMenu_ hm = new HelpMenu_("myBooksForm");
            hm.Show();
            this.Hide();
        }
    }
}
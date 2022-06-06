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
    public partial class SearchBooks : Form
    {
        Database runDatabase = new Database();
        userDetails UD = new userDetails();
        Rentals rental = new Rentals();
        string path = "D:\\HNC\\Joe\\Library (Assignment)";

        List<BookDetails> listOfSearchedBooks = new List<BookDetails>();
        MyBooks books;

        public SearchBooks(ref Database db, ref userDetails UD)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            this.UD = UD;
            runDatabase = db;
            
            books = new MyBooks(ref runDatabase, ref this.UD);

            rentalDurationComboBox.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            searchBook();
        }

        public void searchBook()
        {
            listBox1.Items.Clear();

            if (comboBox1.SelectedIndex == 0)
            {
                listOfSearchedBooks = runDatabase.searchBookTitle(textBox1.Text);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                listOfSearchedBooks = runDatabase.searchBookAuthor(textBox1.Text);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                listOfSearchedBooks = runDatabase.searchBookPublisher(textBox1.Text);
            }

            foreach (BookDetails item in listOfSearchedBooks)
            {
                listBox1.Items.Add("#" + item.bookReference + " - " + item.bookTitle + " - " + item.author + " - " + item.publisher);
            }

            if(listBox1.Items.Count > 0)
            {
               listBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No books match!!! make sure the spelling is correct and please try again");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(path + listOfSearchedBooks[listBox1.SelectedIndex].bookDirectory);
                ISBNTXT.Text = listOfSearchedBooks[listBox1.SelectedIndex].isleNumber.ToString();
                author_txt.Text = listOfSearchedBooks[listBox1.SelectedIndex].author;
                title_txt.Text = listOfSearchedBooks[listBox1.SelectedIndex].bookTitle;
                PublisherTXT.Text = listOfSearchedBooks[listBox1.SelectedIndex].publisher;
                genreTXT.Text = listOfSearchedBooks[listBox1.SelectedIndex].genre;
                rentalDurationTXT.Text = listOfSearchedBooks[listBox1.SelectedIndex].rentalDurationInWeeks.ToString();
                isleNumberTXT.Text = listOfSearchedBooks[listBox1.SelectedIndex].isleNumber.ToString();
                rowNumberTXT.Text = listOfSearchedBooks[listBox1.SelectedIndex].rowNumber.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops Somthing went wrong\n" + ex.Message);
            }
        }

        private void SearchBooks_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (books.canRentBook() == true && int.Parse(rentalDurationComboBox.SelectedItem.ToString()) <= listOfSearchedBooks[listBox1.SelectedIndex].rentalDurationInWeeks && listOfSearchedBooks[listBox1.SelectedIndex].stockNumber > 0)
            {
                runDatabase.rentBook(listOfSearchedBooks[listBox1.SelectedIndex].bookReference.ToString(), UD.userID, dateTimePicker1.Value.AddDays(rentalDurationValue()), listOfSearchedBooks[listBox1.SelectedIndex].stockNumber - 1);
                searchBook();
            }
            else if (int.Parse(rentalDurationComboBox.SelectedItem.ToString()) > listOfSearchedBooks[listBox1.SelectedIndex].rentalDurationInWeeks)
            {
                MessageBox.Show("OOps the maximum number of weeks this book can be rented is: " + listOfSearchedBooks[listBox1.SelectedIndex].rentalDurationInWeeks);
            }
            else if (listOfSearchedBooks[listBox1.SelectedIndex].stockNumber < 1)
            {
                MessageBox.Show("We're sorry but this book is currently out of stock please check back later to see if the book has been returned");
            }
            else
            {
                MessageBox.Show("You have already rented 4 books please return one ion order to rent another");
            }
        }

        public int rentalDurationValue()
        {
            switch (rentalDurationComboBox.SelectedIndex)
            {
                case 0: return 7;
                case 1: return 14;
                case 2: return 21;
                case 3: return 28;
                case 4: return 35;
                default: return 7;
            }
      
        }
       //My books button (Returns the user to the my books page)
        private void button2_Click(object sender, EventArgs e)
        {
            books.loadUserBooks();
            books.Show();
            this.Hide();
        }

        //My account (Returns the user to the my account page)
        private void button4_Click(object sender, EventArgs e)
        {
            MyAccount account = new MyAccount(ref runDatabase, ref UD);
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

        private void SearchBooks_Load(object sender, EventArgs e)
        {
            addItemsToComboBox();
        }

        public void addItemsToComboBox()
        {
            comboBox1.Items.Add("Title");
            comboBox1.Items.Add("Author");
            comboBox1.Items.Add("Publisher");
            comboBox1.Items.Add("Barcode");
            comboBox1.Items.Add("ISBN");
            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 3 && textBox1.Text.Length == 4)
            {
                listOfSearchedBooks = runDatabase.searchBookID(int.Parse(textBox1.Text));
                searchBook();
                listBox1.SelectedIndex = 0;
                textBox1.Clear();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                searchBook();
            }
        }
    }
}

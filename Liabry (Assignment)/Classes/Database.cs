using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liabry__Assignment_
{
    public class Database
    {
        MySqlConnection conn = new MySqlConnection();
        public List<BookDetails> books = new List<BookDetails>();
        private List<Rentals> rentals = new List<Rentals>();
        public List<userDetails> user = new List<userDetails>();

        private string server = "localhost";
        private string UID = "root";
        private string password = "";
        private string database = "library_db";

        public bool openConnection()
        {
            try
            {
                string connection = "SERVER= " + server + ";" + "DATABASE= " + database + ";" + "PASSWORD= " + password + ";" + "UID= " + UID + ";";
                conn = new MySql.Data.MySqlClient.MySqlConnection(connection);
                conn.Open();
                return true;

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public bool closeConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public AdminDetails adminLogin(String P_userName, String P_Password)
        {
            string query = "SELECT * FROM adminDetails WHERE username = '" + P_userName + "' and password='" + P_Password + "'";

            if (openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    try
                    {
                        dataReader.Read();

                        AdminDetails admin = new AdminDetails();

                        admin.userName = dataReader["username"].ToString();
                        admin.password = dataReader["password"].ToString();

                        dataReader.Close();

                        this.closeConnection();
                        return admin;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }

                }
                else
                {
                    this.closeConnection();
                    return null;
                }

            }
            else
            {
                MessageBox.Show("Error");
            }
            this.closeConnection();
            return null;
        }

        public userDetails searchUserRecord(string userid)
        {
            string query = "SELECT * FROM userDetails WHERE user_id=" + userid;

            if (openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    try
                    {
                        dataReader.Read();

                        userDetails newUser = new userDetails();

                        string title = dataReader["title"].ToString();
                        newUser.title = convert(title);
                        newUser.firstName = dataReader["first_name"].ToString();
                        newUser.lastName = dataReader["last_name"].ToString();
                        newUser.userName = dataReader["username"].ToString();
                        newUser.password = dataReader["password"].ToString();
                        newUser.firstLineAddress = dataReader["first_line_address"].ToString();
                        newUser.secondLineAddress = dataReader["second_line_address"].ToString();
                        newUser.postCode = dataReader["post_code"].ToString();
                        newUser.DOB = DateTime.Parse(dataReader["dob"].ToString());
                        newUser.email = dataReader["email"].ToString();

                        dataReader.Close();

                        this.closeConnection();
                        return newUser;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }
                }
                else
                {
                    this.closeConnection();
                    return null;
                }

            }
            else
            {
                MessageBox.Show("Error");
            }
            this.closeConnection();
            return null;
        }

        public void deleteUser(string userName)
        {
            string query = "DELETE FROM userDetails WHERE username=" + userName;

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MessageBox.Show("User details have been deleted!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oops somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error...");
                this.closeConnection();
            }
        }

        public void addBook(BookDetails newBook)
        {
            byte[] fileBytes = File.ReadAllBytes(newBook.bookDirectory);

            long value = 0;
            for (int i = 0; i < fileBytes.Count(); i++)
            {
                value += ((long)fileBytes[i] & 0xffL) << (8 * i);
            }

            string query = "INSERT INTO books VALUES(" + newBook.bookReference + "," + newBook.ISBN + ", '" + newBook.bookTitle + "' , '" + newBook.author + "', '" + newBook.genre + "', '" + newBook.publisher + "', " + newBook.rentalDurationInWeeks + ", " + newBook.isleNumber + "," + newBook.rowNumber + "," + newBook.stockNumber + "," + value + ")";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    MessageBox.Show("Book has been Added!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oops somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        public void editBook(BookDetails book)
        {
            string query = "UPDATE books WHERE bookReference= " + book.bookReference + "and title= '" + book.bookTitle + "' and autor= '" + book.author + "' and publisher= '" + book.publisher + "' and bookCover= ', LOAD_FILE(\"" + book.bookCover + "\"))";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    MessageBox.Show("Book has been updated!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        public List<Rentals> getRentalInfo()
        {
            string query = "SELECT * FROM rental";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    dataReader.Read();
                    Rentals rental = new Rentals();

                    rental.rental_ID = (int)dataReader["rental_ID"];
                    rental.bookReference_FK = int.Parse(dataReader["bookReference_FK"].ToString());
                    rental.customer_ID_FK = dataReader["user_ID_FK"].ToString();
                    rental.rental_date = DateTime.Parse(dataReader["rental_date"].ToString());
                    rental.return_date = DateTime.Parse(dataReader["return_date"].ToString());
                    rental.return_date = DateTime.Parse(dataReader["return_date"].ToString());

                    dataReader.Close();
                    cmd.ExecuteNonQuery();

                    rentals.Add(rental);

                    this.closeConnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error");
            }

            return rentals;
        }

        public userDetails userLogin(String P_userName, String P_Password)
        {
            string query = "SELECT * FROM userDetails WHERE username = '" + P_userName + "' and password='" + P_Password + "'";

            if (openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    try
                    {
                        dataReader.Read();

                        userDetails newUser = new userDetails();

                        string title = dataReader["title"].ToString();
                        newUser.title = convert(title);

                        newUser.userID = (int)dataReader["user_id"];
                        newUser.firstName = dataReader["first_name"].ToString();
                        newUser.lastName = dataReader["last_name"].ToString();
                        newUser.userName = dataReader["username"].ToString();
                        newUser.password = dataReader["password"].ToString();
                        newUser.firstLineAddress = dataReader["first_line_address"].ToString();
                        newUser.secondLineAddress = dataReader["second_line_address"].ToString();
                        newUser.postCode = dataReader["post_code"].ToString();
                        newUser.DOB = DateTime.Parse(dataReader["dob"].ToString());
                        newUser.email = dataReader["email"].ToString();
                        newUser.phoneNumber = dataReader["phonenumber"].ToString();

                        dataReader.Close();

                        this.closeConnection();
                        return newUser;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }
                }
                else
                {
                    this.closeConnection();
                    return null;
                }

            }
            else
            {
                MessageBox.Show("Error");
            }
            this.closeConnection();
            return null;
        }

        public title convert(string P_title)
        {
            switch (P_title)
            {
                case "Mr": return title.Mr;
                case "Mrs": return title.Mrs;
                case "Miss": return title.Miss;
                case "Master": return title.Master;
                case "Ms": return title.Ms;
                case "Mx": return title.Mx;
                case "Sir": return title.Sir;
                case "Professor": return title.Professor;
                case "Doctor": return title.Doctor;
                case "Dame": return title.Dame;
                case "Lady": return title.Lady;
                case "Lord": return title.Lord;
                case "Madam": return title.Madam;

                default: return title.None;
            }
        }

        public void createAccount(object user)
        {
            userDetails newUser = new userDetails();
            newUser = (userDetails)user;
            string insertQuery = "INSERT INTO userDetails (username, title, first_name, last_name, first_line_address, second_line_address, post_code, dob, email, password, phonenumber) VALUES( '" + newUser.userName + "' , '" + newUser.title + "' , '" + newUser.firstName + "' , '" + newUser.lastName + "' , '" + newUser.firstLineAddress + "' , '" + newUser.secondLineAddress + "' , '" + newUser.postCode + "' , '" + newUser.DOB + "' , '" + newUser.email + "' , '" + newUser.password + "' , '" + newUser.phoneNumber + "' )";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    MessageBox.Show("Account has been Created!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        public void updateUserRecord(userDetails user)
        {
            string updateUserQuery = "UPDATE userDetails SET username= '" + user.userName + "', title= '" + user.title + "', first_name= '" + user.firstName + "', last_name= '" + user.lastName + "', first_line_address= '" + user.firstLineAddress + "', second_line_address= '" + user.secondLineAddress + "', post_code=  '" + user.postCode + "', dob= '" + user.DOB.ToString("yyyy-MM-dd") + "', email= '" + user.email + "', password= '" + user.password + "', phonenumber= '" + user.phoneNumber + "' WHERE username= '" + user.userName + "' ";
            //
            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(updateUserQuery, conn);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    MessageBox.Show("Account has been Created!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        public List<BookDetails> getBooks(int userID)
        {
            string selectBookRentals = "SELECT * FROM books WHERE bookReference IN (SELECT rental.bookReference_FK FROM rental WHERE user_ID_FK = " + userID + " AND date_returned IS NULL)";

            books.Clear();

            if (openConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(selectBookRentals, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        try
                        {

                            BookDetails newBook = new BookDetails();

                            newBook.bookReference = int.Parse(dataReader["bookReference"].ToString());
                            newBook.bookTitle = dataReader["title"].ToString();
                            newBook.author = dataReader["author"].ToString();
                            newBook.publisher = dataReader["publisher"].ToString();
                            byte[] photo = (byte[])dataReader["bookCover"];
                            newBook.bookCover = byteArrayToImage(photo);
                            newBook.stockNumber = int.Parse(dataReader["numberOfStock"].ToString());
                            newBook.rowNumber = int.Parse(dataReader["rowNumber"].ToString());
                            newBook.isleNumber = int.Parse(dataReader["isleNumber"].ToString());
                            newBook.genre = dataReader["genre"].ToString();

                            books.Add(newBook);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                        }
                    }
                }

                dataReader.Close();

            }

            return books;
        }

        public List<Rentals> getRentals(int userID)
        {
            string getDates = "SELECT * FROM rental WHERE user_ID_FK= '" + userID + "' AND date_returned IS NULL";

            rentals.Clear();

            if (openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(getDates, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        try
                        {
                            Rentals Allrentals = new Rentals();

                            Allrentals.rental_ID = int.Parse(dataReader["rental_ID"].ToString());
                            Allrentals.bookReference_FK = int.Parse(dataReader["bookReference_FK"].ToString());
                            Allrentals.customer_ID_FK = dataReader["user_ID_FK"].ToString();
                            Allrentals.rental_date = DateTime.Parse(dataReader["rental_date"].ToString());
                            Allrentals.return_date = DateTime.Parse(dataReader["return_date"].ToString());

                            rentals.Add(Allrentals);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                        }
                    }
                }


                dataReader.Close();

            }
            else
            {
                MessageBox.Show("Error");
            }

            this.closeConnection();
            return rentals;
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public List<BookDetails> searchBookTitle(string title)
        {
            this.openConnection();
            string searchString = "SELECT * FROM books WHERE title LIKE '%" + title + "%'";

            books.Clear();

            try
            {
                MySqlCommand cmd = new MySqlCommand(searchString, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        try
                        {
                            BookDetails allBooks = new BookDetails();

                            allBooks.rentalDurationInWeeks = int.Parse(dataReader["rentalDurationInWeeks"].ToString());
                            allBooks.bookReference = int.Parse(dataReader["bookReference"].ToString());
                            allBooks.bookTitle = dataReader["title"].ToString();
                            allBooks.author = dataReader["author"].ToString();
                            allBooks.publisher = dataReader["publisher"].ToString();
                            allBooks.bookDirectory = dataReader["bookCover"].ToString();
                           // byte[] photo = (byte[])dataReader["bookCover"];
                            //MessageBox.Show(photo.ToString());
                            //allBooks.bookCover = byteArrayToImage(photo);
                            allBooks.stockNumber = int.Parse(dataReader["numberOfStock"].ToString());
                            allBooks.isleNumber = int.Parse(dataReader["isleNumber"].ToString());
                            allBooks.rowNumber = int.Parse(dataReader["rowNumber"].ToString());
                            allBooks.genre = dataReader["genre"].ToString();

                            books.Add(allBooks);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }

                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Book not recognised Please try again!!!" + ex.Message);
            }

            this.closeConnection();
            return books;
        }

        public List<BookDetails> searchBookID(int bookReference)
        {
            this.openConnection();
            string searchString = "SELECT * FROM books WHERE bookReference LIKE '%" + bookReference + "%'";

            books.Clear();

            try
            {
                MySqlCommand cmd = new MySqlCommand(searchString, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        try
                        {
                            BookDetails allBooks = new BookDetails();

                            allBooks.rentalDurationInWeeks = int.Parse(dataReader["rentalDurationInWeeks"].ToString());
                            allBooks.bookReference = int.Parse(dataReader["bookReference"].ToString());
                            allBooks.bookTitle = dataReader["title"].ToString();
                            allBooks.author = dataReader["author"].ToString();
                            allBooks.publisher = dataReader["publisher"].ToString();
                            byte[] photo = (byte[])dataReader["bookCover"];
                            allBooks.bookCover = byteArrayToImage(photo);
                            allBooks.stockNumber = int.Parse(dataReader["numberOfStock"].ToString());
                            allBooks.isleNumber = int.Parse(dataReader["isleNumber"].ToString());
                            allBooks.rowNumber = int.Parse(dataReader["rowNumber"].ToString());
                            allBooks.genre = dataReader["genre"].ToString();

                            books.Add(allBooks);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }

                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Book not recognised Please try again!!!" + ex.Message);
            }

            this.closeConnection();
            return books;
        }

        public List<BookDetails> searchBookAuthor(string author)
        {
            books.Clear();

            this.openConnection();
            string searchString = "SELECT * FROM books WHERE author LIKE '%" + author + "%'";

            MySqlCommand cmd = new MySqlCommand(searchString, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    try
                    {
                        BookDetails searchedBook = new BookDetails();

                        searchedBook.rentalDurationInWeeks = int.Parse(dataReader["rentalDurationInWeeks"].ToString());
                        searchedBook.bookReference = int.Parse(dataReader["bookReference"].ToString());
                        searchedBook.bookTitle = dataReader["title"].ToString();
                        searchedBook.author = dataReader["author"].ToString();
                        searchedBook.publisher = dataReader["publisher"].ToString();
                        byte[] photo = (byte[])dataReader["bookCover"];
                        searchedBook.bookCover = byteArrayToImage(photo);
                        searchedBook.stockNumber = int.Parse(dataReader["numberOfStock"].ToString());
                        searchedBook.isleNumber = int.Parse(dataReader["isleNumber"].ToString());
                        searchedBook.rowNumber = int.Parse(dataReader["rowNumber"].ToString());
                        searchedBook.genre = dataReader["genre"].ToString();

                        books.Add(searchedBook);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }

            dataReader.Close();
            this.closeConnection();
            return books;
        }

        public List<BookDetails> searchBookPublisher(string publisher)
        {
            this.openConnection();
            string searchString = "SELECT * FROM books WHERE publisher LIKE '%" + publisher + "%'";

            books.Clear();

            MySqlCommand cmd = new MySqlCommand(searchString, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    try
                    {
                        BookDetails searchedBook = new BookDetails();

                        searchedBook.rentalDurationInWeeks = int.Parse(dataReader["rentalDurationInWeeks"].ToString());
                        searchedBook.bookReference = int.Parse(dataReader["bookReference"].ToString());
                        searchedBook.bookTitle = dataReader["title"].ToString();
                        searchedBook.author = dataReader["author"].ToString();
                        searchedBook.publisher = dataReader["publisher"].ToString();
                        byte[] photo = (byte[])dataReader["bookCover"];
                        searchedBook.bookCover = byteArrayToImage(photo);
                        searchedBook.stockNumber = int.Parse(dataReader["numberOfStock"].ToString());
                        searchedBook.isleNumber = int.Parse(dataReader["isleNumber"].ToString());
                        searchedBook.rowNumber = int.Parse(dataReader["rowNumber"].ToString());
                        searchedBook.genre = dataReader["genre"].ToString();

                        books.Add(searchedBook);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }

            dataReader.Close();

            this.closeConnection();

            return books;

        }

        public BookDetails searchBookReference(int bookReference)
        {
            this.openConnection();
            string searchString = "SELECT * FROM books WHERE bookReference=" + bookReference;

            books.Clear();

            MySqlCommand cmd = new MySqlCommand(searchString, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            BookDetails searchedBook = new BookDetails();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    try
                    {
                        searchedBook.rentalDurationInWeeks = int.Parse(dataReader["rentalDurationInWeeks"].ToString());
                        searchedBook.bookReference = int.Parse(dataReader["bookReference"].ToString());
                        searchedBook.bookTitle = dataReader["title"].ToString();
                        searchedBook.author = dataReader["author"].ToString();
                        searchedBook.publisher = dataReader["publisher"].ToString();
                        byte[] photo = (byte[])dataReader["bookCover"];
                        searchedBook.bookCover = byteArrayToImage(photo);
                        searchedBook.stockNumber = int.Parse(dataReader["numberOfStock"].ToString());
                        searchedBook.isleNumber = int.Parse(dataReader["isleNumber"].ToString());
                        searchedBook.rowNumber = int.Parse(dataReader["rowNumber"].ToString());
                        searchedBook.genre = dataReader["genre"].ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }

            dataReader.Close();
            this.closeConnection();
            return searchedBook;
        }

        public void updateBook(BookDetails book, int bookReference)
        {
            string updateUserQuery = "UPDATE books SET title= '" + book.bookTitle + "', author= '" + book.author + "', publisher= '" + book.publisher + "' WHERE bookReference= '" + bookReference + "' ";
            //
            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(updateUserQuery, conn);
                    cmd.ExecuteNonQuery();
                    this.closeConnection();
                    MessageBox.Show("Account has been Created!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        public void rentBook(string bookReference, int user_id, DateTime returnDate, int newStock)
        {
            this.openConnection();

            string query = "INSERT INTO rental (rental_ID,bookReference_FK, user_ID_FK, rental_date ,return_date, date_returned) VALUES(NULL, '" + bookReference + "','" + user_id + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + returnDate.ToString("yyyy-MM-dd") + "',NULL)";

            string query2 = "UPDATE books SET numberOfStock=" + newStock;

            if (this.openConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    this.closeConnection();
                    MessageBox.Show("Book rented");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Error");
            }

            this.closeConnection();
        }

        public void returnBook(int rentalID, int newStock)
        {
            string query = "UPDATE rental SET date_returned= '" + DateTime.Now + "' WHERE rental_ID = " + rentalID;
            string query2 = "UPDATE books SET numberOfStock = " + newStock;

            if (openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                this.closeConnection();
                MessageBox.Show("Book has been returned!");
            }
            else
            {
                MessageBox.Show("Error");
            }

            this.closeConnection();
        }

        public List<BookDetails> getLateBooks()
        {
            string query = "SELECT books.bookCover, books.bookReference, books.title, DATEDIFF(NOW(),return_date) AS 'Days_Late' FROM rental INNER JOIN userDetails ON userDetails.user_ID = rental.user_ID_FK INNER JOIN books ON books.bookReference = rental.bookReference_FK WHERE rental.date_returned is NULL AND rental.return_date < NOW()";

            if (this.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();


                if (dataReader.HasRows)
                {
                    try
                    {
                        while (dataReader.Read())
                        {
                            BookDetails book = new BookDetails();
                            book.bookTitle = dataReader["title"].ToString();
                            book.bookReference = int.Parse(dataReader["bookReference"].ToString());
                            byte[] photo = (byte[])dataReader["bookCover"];
                            book.bookCover = byteArrayToImage(photo);

                            books.Add(book);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }

                dataReader.Close();
                this.closeConnection();
            }

            return books;
        }

        public List<BookDetails> allRentals()
        {
            string query = "SELECT books.bookCover, books.bookReference, books.title, DATEDIFF(NOW(),return_date) AS 'Days_Late' FROM rental INNER JOIN userDetails ON userDetails.user_ID = rental.user_ID_FK INNER JOIN books ON books.bookReference = rental.bookReference_FK WHERE rental.date_returned is NULL";

            if (this.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();


                if (dataReader.HasRows)
                {
                    try
                    {
                        while (dataReader.Read())
                        {
                            BookDetails book = new BookDetails();
                            book.bookTitle = dataReader["title"].ToString();
                            book.bookReference = int.Parse(dataReader["bookReference"].ToString());
                            byte[] photo = (byte[])dataReader["bookCover"];
                            book.bookCover = byteArrayToImage(photo);

                            books.Add(book);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }

                dataReader.Close();
                this.closeConnection();
            }

            return books;
        }

        public List<userDetails> getLateRentalsUserDetails(int bookReference)
        {
            string query = "SELECT userDetails.user_id, userDetails.first_name, userDetails.last_name, userDetails.userPhoto FROM rental INNER JOIN userDetails ON userDetails.user_ID = rental.user_ID_FK INNER JOIN books ON books.bookReference = rental.bookReference_FK WHERE rental.bookReference_FK=" + bookReference;

            if (this.openConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();


                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        try
                        {
                            userDetails user = new userDetails();
                            user.userID = int.Parse(dataReader["user_id"].ToString());
                            user.firstName = dataReader["first_name"].ToString();
                            user.lastName = dataReader["last_name"].ToString();
                            byte[] photo2 = (byte[])dataReader["userPhoto"];
                            user.userPhoto = byteArrayToImage(photo2);

                            this.user.Add(user);
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("OOps somthing went wrong\n" + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }

                dataReader.Close();
                this.closeConnection();
            }

            return this.user;
        }
    }
}
DROP DATABASE IF EXISTS library_db;
CREATE DATABASE library_db;
SHOW DATABASES;
USE library_db;


CREATE TABLE adminDetails(

	admin_id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
	username VARCHAR(40),
	password VARCHAR(30)
	
)engine=innodb;

CREATE TABLE userDetails(

	user_id INT PRIMARY KEY NOT NULL,
	username VARCHAR(40),
	title VARCHAR (25),
	first_name VARCHAR(50),
	last_name VARCHAR(50),
	first_line_address VARCHAR (150),
	second_line_address VARCHAR(150),
	post_code VARCHAR(11),
	dob DATETIME,
	email VARCHAR(100),
	password VARCHAR(30), 
	phonenumber VARCHAR(11),
	userPhoto BLOB

)engine=innodb;

CREATE TABLE books(

	bookReference INT PRIMARY KEY NOT NULL,
	ISBN INT(10),
	title VARCHAR(100),
	author VARCHAR(50),
	genre VARCHAR(70),
	publisher VARCHAR(50),
	rentalDurationInWeeks INT(2),
	isleNumber INT(5),
	rowNumber INT(5),
	numberOfStock INT(3),
	bookCover BLOB

)engine=innodb;

CREATE TABLE rental(

	rental_ID INT AUTO_INCREMENT PRIMARY KEY,
	bookReference_FK INT,
	user_ID_FK INT,
	rental_date DATETIME,
	return_date DATETIME,
	date_returned DATETIME

)engine=innodb;

INSERT INTO rental VALUES(NULL, 1111, 10072192, '2016-03-03', '2016-03-03', NULL);
INSERT INTO rental VALUES(NULL, 1111, 10072193, '2016-03-03', '2016-02-05', NULL);
INSERT INTO rental VALUES(NULL, 2222, 10072192, '2016-03-03', '2016-01-08', NULL);
INSERT INTO rental VALUES(NULL, 1113, 10072193, '2016-03-03', '2016-01-22', NULL);
INSERT INTO rental VALUES(NULL, 1123, 10072192, '2016-03-03', '2016-01-22', NULL);
INSERT INTO rental VALUES(NULL, 1114, 10072193, '2016-03-03', '2016-01-22', NULL);
INSERT INTO rental VALUES(NULL, 1114, 10134553, '2016-03-03', '2016-01-22', NULL);

INSERT INTO adminDetails VALUES(NULL, 'admin', 'admin');

INSERT INTO userDetails VALUES(10072192, 'dan', 'Mr','Daniel','Simkiss','Walsall', 'Walsall', 'WS3', '1992-10-29', 'thisisafakeemial@fake.com', '123','07599801670', LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\userPictures\\userPic.jpg"));
INSERT INTO userDetails VALUES(10072193, 'dan', 'Mr','Bill','Simkiss','Walsall', 'Walsall', 'WS3', '1992-10-29', 'thisisafakeemial@fake.com', '123','07599801670', LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\userPictures\\userPic2.jpg"));
INSERT INTO userDetails VALUES(10134553, 'dan', 'Mr','Leonardo','Decpario','Walsall', 'Walsall', 'WS3', '1992-10-29', 'thisisafakeemial@fake.com', '123','07599801670', LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\userPictures\\userPic3.jpg"));

INSERT INTO books VALUES
(1111, 9780553386790, 'A song of ice and fire. A game of thrones', 'George R. R. Martin', 'Fantasy', 'Bantam Spectra/US Voyager Books/UK', 3, 1, 1, 0, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1111cover.jpg")),
(1112, 9789024567157, 'A song of ice and fire. A feast for crows', 'George R. R. Martin', 'Fantasy', 'Bantam Spectra/US Voyager Books/UK', 3, 1, 1, 6, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1112cover.jpg")),
(1113, 9780007447831, 'A song of ice and fire. A clash of kings', 'George R. R. Martin', 'Fantasy', 'Bantam Spectra/US Voyager Books/UK', 3, 1, 1, 5, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1113cover.jpg")),
(1114, 9780007466078, 'A song of ice and fire. A dance with dragons', 'George R. R. Martin', 'Fantasy', 'Bantam Spectra/US Voyager Books/UK', 2, 1, 1, 10, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1114cover.jpg")),
(1115, 9785845105127, 'Harry Potter and the philosophers stone', 'J. K. Rowling', 'Fantasy - Mystery, Drama, Thriller', 'Bloomsbury', 3, 2, 3, 9, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1115cover.jpg")),
(1116, 9781781100011, 'Harry Potter and the chamber of secrets', 'J. K. Rowling', 'Fantasy - Mystery, Drama, Thriller', 'Bloomsbury', 4, 2, 3, 9, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1116cover.jpg")),
(1117, 9788478885688, 'Harry Potter and the prisoner of Azkaban', 'J. K. Rowling', 'Fantasy - Mystery, Drama, Thriller', 'Bloomsbury', 2, 2, 3, 5, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1117cover.jpg")),
(1118, 9782070543519, 'Harry Potter and the Goblet of Fire', 'J. K. Rowling', 'Fantasy - Mystery, Drama, Thriller', 'Bloomsbury', 3, 2, 3, 5, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1118cover.jpg")),
(1119, 9780320048395, 'Harry Potter and the order of the Phoenix', 'J. K. Rowling', 'Fantasy - Mystery, Drama, Thriller', 'Bloomsbury', 4, 2, 3, 5, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1119cover.jpg")),
(1121, 9780747581086, 'Harry Potter and the half blood prince', 'J. K. Rowling', 'Fantasy - Mystery, Drama, Thriller', 'Bloomsbury', 4, 2, 3, 7, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1121cover.jpg")),
(1122, 9780545010221, 'Harry Potter and the Deathly Hallows', 'J. K. Rowling', 'Fantasy - Mystery, Drama, Thriller', 'Bloomsbury', 4, 2, 3, 8, LOAD_FILE("D:\\HNC\\Joe\\Library (Assignment)\\Images\\Book Covers\\1122cover.jpg"));

SELECT userDetails.first_name, userDetails.last_name, books.bookReference, books.title, DATEDIFF(NOW(),return_date) AS 'Days_Late' FROM rental
INNER JOIN userDetails ON userDetails.user_id = rental.user_ID_FK
INNER JOIN books ON books.bookReference = rental.bookReference_FK
WHERE rental.date_returned is NULL AND rental.return_date < NOW();


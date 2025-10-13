-- CityLibrarySYS_DesignPatterns - Database Script

-- Drop tables if they exist (in dependency order)
IF OBJECT_ID('LoanItems', 'U') IS NOT NULL DROP TABLE LoanItems;
IF OBJECT_ID('Loans', 'U') IS NOT NULL DROP TABLE Loans;
IF OBJECT_ID('Books', 'U') IS NOT NULL DROP TABLE Books;
IF OBJECT_ID('Genres', 'U') IS NOT NULL DROP TABLE Genres;
IF OBJECT_ID('Members', 'U') IS NOT NULL DROP TABLE Members;


-- Create Members Table
CREATE TABLE Members (
    MemberID INT PRIMARY KEY,
    Forename VARCHAR(20) NOT NULL,
    Surname VARCHAR(20) NOT NULL,
    DoB DATE NOT NULL,
    Street VARCHAR(25) NOT NULL,
    Town VARCHAR(15) NOT NULL,
    County VARCHAR(15) NOT NULL,
    Eircode CHAR(7) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email VARCHAR(40) NOT NULL
);

-- Insert Members
INSERT INTO Members (MemberID, Forename, Surname, DoB, Street, Town, County, Eircode, Phone, Email)
VALUES
(0, 'Sophia', 'Loren', '1956-05-15', 'Galway Street', 'Galway City', 'Galway', 'A65F4E2', '+353612345678', 'not.sophia.loren@example.com'),
(1, 'Marco', 'Rossi', '1966-06-23', 'Tenth Street', 'Limerick City', 'Limerick', 'A65F4E1', '+353687654321', 'marcorossin1@example.com'),
(2, 'Luigi', 'Bros', '1986-09-06', 'Bros Street', 'Shannon', 'Clare', 'A65F4E3', '+353611223344', 'luigibros@example.com'),
(3, 'Mario', 'Bros', '1956-02-25', 'Clare Street', 'Kilrush', 'Clare', 'A65F4E7', '+353611113333', 'mariobros1@example.com'),
(4, 'Steven', 'Universe', '2009-04-22', 'Cork Street', 'Shannon', 'Clare', 'A65F4E4', '+353212345678', 'steven.universe@example.com'),
(5, 'Emma', 'Smith', '1978-03-10', 'Main Street', 'Galway City', 'Galway', 'A65F4E8', '+353619876543', 'emma.smith@example.com'),
(6, 'John', 'Doe', '1985-08-05', 'High Street', 'Limerick City', 'Limerick', 'A65F4E9', '+353614321098', 'john.doe@example.com'),
(7, 'Sarah', 'Johnson', '1990-11-20', 'Oak Avenue', 'Galway City', 'Galway', 'A65F4E1', '+353615678901', 'sarah.johnson@example.com'),
(8, 'Michael', 'Brown', '1980-09-15', 'Pine Road', 'Limerick City', 'Limerick', 'A65F4E7', '+353611234567', 'michael.brown@example.com'),
(9, 'Emily', 'Taylor', '1995-04-02', 'Cedar Lane', 'Kilrush', 'Clare', 'A65F4E4', '+353617890123', 'emily.taylor@example.com');


-- Create Genres Table
CREATE TABLE Genres (
    GenreCode CHAR(3) PRIMARY KEY,
    Description VARCHAR(20) NOT NULL
);

-- Insert into Genres table
INSERT INTO Genres VALUES
('ADV', 'Adventure'),
('ADF', 'Adventure Fiction'),
('AUT', 'Autobiography'),
('BIO', 'Biography'),
('BIF', 'Biographical Fiction'),
('CLA', 'Classic'),
('CRI', 'Crime'),
('FAN', 'Fantasy'),
('FIC', 'Fiction'),
('GOF', 'Gothic Fiction'),
('HIF', 'Historical Fiction'),
('HOR', 'Horror'),
('MYS', 'Mystery'),
('MUR', 'Murder Mystery'),
('NFI', 'Non-Fiction'),
('NOV', 'Novel'),
('PHI', 'Philosophy'),
('ROM', 'Romance'),
('SCI', 'Science'),
('THI', 'Thriller');


-- Create Books Table
CREATE TABLE Books (
    BookID INT PRIMARY KEY,
    ISBN VARCHAR(13) NOT NULL,
    Title VARCHAR(80) NOT NULL,
    Author VARCHAR(35) NOT NULL,
    Genre CHAR(3) NOT NULL,
    Publication DATE NOT NULL,
    Description VARCHAR(300) NOT NULL,
    Status CHAR(1) DEFAULT 'A' CHECK (Status IN ('A', 'U')) NOT NULL,
    CONSTRAINT fk_GenreCode FOREIGN KEY (Genre) REFERENCES Genres(GenreCode)
);

-- Insert Books
INSERT INTO Books (BookID, ISBN, Title, Author, Genre, Publication, Description, Status)
VALUES
(0, '812911612X', 'Animal Farm', 'George Orwell', 'NOV', '1945-08-17', 'A group of animals rebel against humans and face new tyranny.', 'A'),
(1, '1368051472', 'Percy Jackson And The Olympians: The Lightning Thief', 'Rick Riordan', 'FIC', '2005-06-28', 'A boy discovers he is Poseidon’s son and goes on a heroic quest.', 'A'),
(2, '0141182636', 'The Great Gatsby', 'F. Scott Fitzgerald', 'NOV', '1925-04-10', 'A story of love, wealth, and the American Dream in the Jazz Age.', 'A'),
(3, '9385887300', 'Pride And Prejudice', 'Jane Austen', 'CLA', '1813-06-28', 'Elizabeth Bennet navigates love, class, and pride.', 'A'),
(4, '009955545X', 'In The Sea There Are Crocodiles', 'Fabio Geda', 'BIF', '2010-04-22', 'A boy’s survival journey from Afghanistan to Italy.', 'A'),
(5, '0062073486', 'And Then There Were None', 'Agatha Christie', 'MUR', '1939-11-06', 'Ten strangers on an island die one by one.', 'A'),
(6, '1503280780', 'Moby-Dick', 'Herman Melville', 'ADF', '1851-10-18', 'Captain Ahab’s obsession with the white whale.', 'A'),
(7, '0141439572', 'The Picture Of Dorian Gray', 'Oscar Wilde', 'GOF', '1890-07-01', 'A man stays young while his portrait ages.', 'A'),
(8, '8809792688', 'Mystery Tales', 'Edgar Allan Poe', 'MYS', '1908-01-01', 'A collection of Poe’s suspenseful tales.', 'A'),
(9, '8862562586', 'The Wide Window', 'Daniel Handler', 'GOF', '2000-02-25', 'The Baudelaire orphans face a fearful guardian.', 'A');


-- Create Loans Table
CREATE TABLE Loans (
    LoanId INT PRIMARY KEY,
    MemberId INT NOT NULL,
    FOREIGN KEY (MemberId) REFERENCES Members(MemberID)
);

-- Insert Loans
INSERT INTO Loans (LoanId, MemberId) VALUES
(0, 0),
(1, 1),
(2, 2),
(3, 3),
(4, 4);


-- Create LoanItems Table
CREATE TABLE LoanItems (
    BookID INT NOT NULL,
    LoanID INT NOT NULL,
    LoanStart DATE NOT NULL,
    LoanEnd DATE NOT NULL,
    ReturnDate DATE,
    Status CHAR(1) CHECK (Status IN ('O', 'L')) NOT NULL,
    PRIMARY KEY (LoanID, BookID),
    FOREIGN KEY (LoanID) REFERENCES Loans(LoanId),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

-- Insert LoanItems
INSERT INTO LoanItems (LoanID, BookID, LoanStart, LoanEnd, ReturnDate, Status)
VALUES
(0, 0, '2023-01-01', '2023-01-31', '2023-01-29', 'O'),
(1, 1, '2023-01-05', '2023-02-04', '2023-02-20', 'L'),
(2, 2, '2023-02-19', '2023-03-21', '2023-03-19', 'O'),
(3, 3, '2023-03-01', '2023-03-31', '2023-04-05', 'L'),
(4, 4, '2023-03-12', '2023-04-11', '2023-04-07', 'O');

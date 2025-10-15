-- CityLibrarySYS_DesignPatterns - Database Script

-- Drop All Tables
DROP TABLE LoanItems;
DROP TABLE Loans;
DROP TABLE Books;
DROP TABLE Genres;
DROP TABLE Members;
DROP TABLE Counties;

-- Create Counties Table
CREATE TABLE Counties
(
    CountyCode VARCHAR(15),
    Description VARCHAR(15) NOT NULL,
    CONSTRAINT pk_CountyCode PRIMARY KEY(CountyCode)
);

-- Insert into Counties table
INSERT INTO Counties VALUES ('Antrim', 'Co. Antrim');
INSERT INTO Counties VALUES ('Armagh', 'Co. Armagh');
INSERT INTO Counties VALUES ('Cavan', 'Co. Cavan');
INSERT INTO Counties VALUES ('Derry', 'Co. Derry');
INSERT INTO Counties VALUES ('Donegal', 'Co. Donegal');
INSERT INTO Counties VALUES ('Down', 'Co. Down');
INSERT INTO Counties VALUES ('Fermanagh', 'Co. Fermanagh');
INSERT INTO Counties VALUES ('Monaghan', 'Co. Monaghan');
INSERT INTO Counties VALUES ('Tyrone', 'Co. Tyrone');
INSERT INTO Counties VALUES ('Cork', 'Co. Cork');
INSERT INTO Counties VALUES ('Clare', 'Co. Clare');
INSERT INTO Counties VALUES ('Limerick', 'Co. Limerick');
INSERT INTO Counties VALUES ('Tipperary', 'Co. Tipperary');
INSERT INTO Counties VALUES ('Waterford', 'Co. Waterford');
INSERT INTO Counties VALUES ('Carlow', 'Co. Carlow');
INSERT INTO Counties VALUES ('Dublin', 'Co. Dublin');
INSERT INTO Counties VALUES ('Kildare', 'Co. Kildare');
INSERT INTO Counties VALUES ('Kilkenny', 'Co. Kilkenny');
INSERT INTO Counties VALUES ('Laois', 'Co. Laois');
INSERT INTO Counties VALUES ('Longford', 'Co. Longford');
INSERT INTO Counties VALUES ('Louth', 'Co. Louth');
INSERT INTO Counties VALUES ('Meath', 'Co. Meath');
INSERT INTO Counties VALUES ('Offaly', 'Co. Offaly');
INSERT INTO Counties VALUES ('Westmeath', 'Co. Westmeath');
INSERT INTO Counties VALUES ('Wexford', 'Co. Wexford');
INSERT INTO Counties VALUES ('Sligo', 'Co. Sligo');
INSERT INTO Counties VALUES ('Mayo', 'Co. Mayo');
INSERT INTO Counties VALUES ('Galway', 'Co. Galway');
INSERT INTO Counties VALUES ('Leitrim', 'Co. Leitrim');
INSERT INTO Counties VALUES ('Roscommon', 'Co. Roscommon');

-- Create Members Table (updated to use CountyCode)
CREATE TABLE Members
(
    MemberID INT PRIMARY KEY,
    Forename VARCHAR(20) NOT NULL,
    Surname VARCHAR(20) NOT NULL,
    DoB DATE NOT NULL,
    Street VARCHAR(25) NOT NULL,
    Town VARCHAR(15) NOT NULL,
    CountyCode VARCHAR(15) NOT NULL,
    Eircode CHAR(7) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email VARCHAR(40) NOT NULL,
    CONSTRAINT fk_Member_County FOREIGN KEY (CountyCode)
        REFERENCES Counties(CountyCode)
);

-- Insert Members
INSERT INTO Members VALUES(0, 'Sophia', 'Loren', '1956-05-15', 'Galway Street', 'Galway City', 'Galway', 'A65F4E2', '+353612345678', 'not.sophia.loren@example.com');
INSERT INTO Members VALUES(1, 'Marco', 'Rossi', '1966-06-23', 'Tenth Street', 'Limerick City', 'Limerick', 'A65F4E1', '+353687654321', 'marcorossin1@example.com');
INSERT INTO Members VALUES(2, 'Luigi', 'Bros', '1986-09-06', 'Bros Street', 'Shannon', 'Clare', 'A65F4E3', '+353611223344', 'luigibros@example.com');
INSERT INTO Members VALUES(3, 'Mario', 'Bros', '1956-02-25', 'Clare Street', 'Kilrush', 'Clare', 'A65F4E7', '+353611113333', 'mariobros1@example.com');
INSERT INTO Members VALUES(4, 'Steven', 'Universe', '2009-04-22', 'Cork Street', 'Shannon', 'Clare', 'A65F4E4', '+353212345678', 'steven.universe@example.com');
INSERT INTO Members VALUES(5, 'Emma', 'Smith', '1978-03-10', 'Main Street', 'Galway City', 'Galway', 'A65F4E8', '+353619876543', 'emma.smith@example.com');
INSERT INTO Members VALUES(6, 'John', 'Doe', '1985-08-05', 'High Street', 'Limerick City', 'Limerick', 'A65F4E9', '+353614321098', 'john.doe@example.com');
INSERT INTO Members VALUES(7, 'Sarah', 'Johnson', '1990-11-20', 'Oak Avenue', 'Galway City', 'Galway', 'A65F4E1', '+353615678901', 'sarah.johnson@example.com');
INSERT INTO Members VALUES(8, 'Michael', 'Brown', '1980-09-15', 'Pine Road', 'Limerick City', 'Limerick', 'A65F4E7', '+353611234567', 'michael.brown@example.com');
INSERT INTO Members VALUES(9, 'Emily', 'Taylor', '1995-04-02', 'Cedar Lane', 'Kilrush', 'Clare', 'A65F4E4', '+353617890123', 'emily.taylor@example.com');

---------------------------------------------------
-- Create Genres Table
---------------------------------------------------
CREATE TABLE Genres (
    GenreCode CHAR(3),
    Description VARCHAR(20) NOT NULL,
    CONSTRAINT pk_GenreCode PRIMARY KEY(GenreCode)
);

-- Insert into Genres table
INSERT INTO Genres VALUES ('ADV', 'Adventure');
INSERT INTO Genres VALUES ('ADF', 'Adventure Fiction');
INSERT INTO Genres VALUES ('AUT', 'Autobiography');
INSERT INTO Genres VALUES ('BIO', 'Biography');
INSERT INTO Genres VALUES ('BIF', 'Biographical Fiction');
INSERT INTO Genres VALUES ('CLA', 'Classic');
INSERT INTO Genres VALUES ('CRI', 'Crime');
INSERT INTO Genres VALUES ('FAN', 'Fantasy');
INSERT INTO Genres VALUES ('FIC', 'Fiction');
INSERT INTO Genres VALUES ('GOF', 'Gothic Fiction');
INSERT INTO Genres VALUES ('HIF', 'Historical Fiction');
INSERT INTO Genres VALUES ('HOR', 'Horror');
INSERT INTO Genres VALUES ('MYS', 'Mystery');
INSERT INTO Genres VALUES ('MUR', 'Murder Mystery');
INSERT INTO Genres VALUES ('NFI', 'Non-Fiction');
INSERT INTO Genres VALUES ('NOV', 'Novel');
INSERT INTO Genres VALUES ('PHI', 'Philosophy');
INSERT INTO Genres VALUES ('ROM', 'Romance');
INSERT INTO Genres VALUES ('SCI', 'Science');
INSERT INTO Genres VALUES ('THI', 'Thriller');

---------------------------------------------------
-- Create Books Table
---------------------------------------------------
CREATE TABLE Books (
    BookID INT PRIMARY KEY,
    ISBN VARCHAR(13) NOT NULL,
    Title VARCHAR(80) NOT NULL,
    Author VARCHAR(35) NOT NULL,
    Genre CHAR(3) NOT NULL,
    Pubblication DATE NOT NULL,
    Description VARCHAR(300) NOT NULL,
    Status CHAR(1) DEFAULT 'A' CHECK (Status IN ('A', 'U')) NOT NULL,
    CONSTRAINT fk_GenreCode FOREIGN KEY (Genre) REFERENCES Genres(GenreCode)
);

-- Insert Books
INSERT INTO Books (BookID, ISBN, Title, Author, Genre, Pubblication, Description, Status)
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

---------------------------------------------------
-- Create Loans Table
---------------------------------------------------
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

---------------------------------------------------
-- Create LoanItems Table
---------------------------------------------------
CREATE TABLE LoanItems (
    BookID INT NOT NULL,
    LoanID INT NOT NULL,
    LoanStart DATE NOT NULL,
    LoanEnd DATE NOT NULL,
    RuternDate DATE,
    Status CHAR(1) CHECK (Status IN ('O', 'L')) NOT NULL,
    PRIMARY KEY (LoanID, BookID),
    FOREIGN KEY (LoanID) REFERENCES Loans(LoanId),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

-- Insert LoanItems
INSERT INTO LoanItems (LoanID, BookID, LoanStart, LoanEnd, RuternDate, Status)
VALUES
(0, 0, '2023-01-01', '2023-01-31', '2023-01-29', 'O'),
(1, 1, '2023-01-05', '2023-02-04', '2023-02-20', 'L'),
(2, 2, '2023-02-19', '2023-03-21', '2023-03-19', 'O'),
(3, 3, '2023-03-01', '2023-03-31', '2023-04-05', 'L'),
(4, 4, '2023-03-12', '2023-04-11', '2023-04-07', 'O');

COMMIT;

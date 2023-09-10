USE [master]
GO 

CREATE DATABASE OnlineExamDB
Go

USE OnlineExamDB
Go

--Create UserRole table
Create Table UserRoles(
  Id INT PRIMARY KEY,
  RoleName varchar(50) NOT NULL
)

-- Create Users table
CREATE TABLE Users (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserName VARCHAR(255) NOT NULL,
  Email VARCHAR(255) NOT NULL,
  Password VARCHAR(255) NOT NULL,
  FirstName VARCHAR(255) NOT NULL,
  LastName VARCHAR(255) NOT NULL,
  RoleId INT NOT NULL,

  FOREIGN KEY (RoleId) REFERENCES UserRoles(Id)
);

-- Create Examinations table
CREATE TABLE Examinations (
  Id UNIQUEIDENTIFIER  PRIMARY KEY,
  ExamTitle VARCHAR(100) NOT NULL,
  ExamDescription VARCHAR(255) NOT NULL,
  StartDate DATETIME NOT NULL,
  EndDate DATETIME NOT NULL,
  DurationInHours decimal NOT NULL,
);

-- Create Questions table
CREATE TABLE Questions (
  Id UNIQUEIDENTIFIER  PRIMARY KEY,
  ExamId UNIQUEIDENTIFIER NOT NULL,
  QuestionText TEXT NOT NULL,
  QuestionTypeId INT NOT NULL,

  FOREIGN KEY (ExamId) REFERENCES Examinations(Id)
);

--Create QuestionTypes table
CREATE TABLE QuestionTypes (
  Id INT PRIMARY KEY,
  TypeName VARCHAR(50) NOT NULL,
);


-- Create Choices table (for multiple-choice questions)
CREATE TABLE Choices (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  QuestionId UNIQUEIDENTIFIER NOT NULL,
  ChoiceText TEXT NOT NULL,
  IsCorrect BIT NOT NULL,
  
  FOREIGN KEY (QuestionId) REFERENCES Questions(Id)
);

-- Create Answers table
CREATE TABLE Answers (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserId UNIQUEIDENTIFIER NOT NULL,
  ExamId UNIQUEIDENTIFIER NOT NULL,
  QuestionId UNIQUEIDENTIFIER NOT NULL,
  AnswerText TEXT,
  SelectedChoiceId UNIQUEIDENTIFIER,

  FOREIGN KEY (UserId) REFERENCES Users(Id),
  FOREIGN KEY (ExamId) REFERENCES Examinations(Id),
  FOREIGN KEY (QuestionId) REFERENCES Questions(Id),
  FOREIGN KEY (SelectedChoiceId) REFERENCES Choices(Id)
);

--Create StudentProgresse
CREATE TABLE StudentProgress (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserId UNIQUEIDENTIFIER NOT NULL,
  ExamId UNIQUEIDENTIFIER NOT NULL,
  IsCompleted Bit NOT NULL,
  Score DECIMAL(5, 2),
  Timestamp DATETIME,
  
  FOREIGN KEY (UserId) REFERENCES Users(Id),
  FOREIGN KEY (ExamId) REFERENCES Examinations(Id)
);

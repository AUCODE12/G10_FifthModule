CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE
);

CREATE TABLE Professors (
    ProfessorID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Department NVARCHAR(100)
);

CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY,
    CourseName NVARCHAR(100),
    ProfessorID INT FOREIGN KEY REFERENCES Professors(ProfessorID) ON DELETE CASCADE
);

CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY IDENTITY,
    StudentID INT FOREIGN KEY REFERENCES Students(StudentID) ON DELETE CASCADE,
    CourseID INT FOREIGN KEY REFERENCES Courses(CourseID) ON DELETE CASCADE,
    EnrollmentDate DATE DEFAULT GETDATE()
);


CREATE TABLE AuditLog (
    AuditID INT PRIMARY KEY IDENTITY,
    TableName NVARCHAR(100),
    Operation NVARCHAR(50),
    Description NVARCHAR(500),
    Timestamp DATETIME DEFAULT GETDATE()
);



--* Har bir tableda qanday action sodir bolsa ham AuditLog tablega triggerlar yozib qo'ysin

--Students
CREATE TRIGGER trg_AfterInsertStudents
ON Students
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Students', 'inserted', 'Student Inserted')
END;

CREATE TRIGGER trg_AfterUpdateStudents
ON Students
AFTER UPDATE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Students', 'update', 'Student updated')
END;

CREATE TRIGGER trg_AfterDeleteStudents
ON Students
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Students', 'deleted', 'Student deleted')
END;

--Professors
CREATE TRIGGER trg_AfterInsertSProfessors
ON Professors
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Professors', 'inserted', 'Professors Inserted')
END;

CREATE TRIGGER trg_AfterUpdateProfessors
ON Professors
AFTER UPDATE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Professors', 'update', 'Professors updated')
END;

CREATE TRIGGER trg_AfterDeleteProfessors
ON Professors
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Professors', 'deleted', 'Professors deleted')
END;

--Enrollments
CREATE TRIGGER trg_AfterInsertEnrollments
ON Enrollments
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Enrollments', 'inserted', 'Enrollments Inserted')
END;

CREATE TRIGGER trg_AfterUpdateEnrollments
ON Enrollments
AFTER UPDATE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Enrollments', 'update', 'Enrollments updated')
END;

CREATE TRIGGER trg_AfterDeleteEnrollments
ON Enrollments
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Enrollments', 'deleted', 'Enrollments deleted')
END;

-- Courses
CREATE TRIGGER trg_AfterInsertSCourses
ON Courses
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Courses', 'inserted', 'Courses Inserted')
END;

CREATE TRIGGER trg_AfterUpdateCourses
ON Courses
AFTER UPDATE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Courses', 'update', 'Courses updated')
END;

CREATE TRIGGER trg_AfterDeleteCourses
ON Courses
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, Description)
	values ('Courses', 'deleted', 'Courses deleted')
END;


Insert into Students (Name, Email)
values ('Ahmadjon', 'qahmadjon11@gmail.com')

update Students 
set Name = 'Ahmadjon Qudratov'
where StudentID = 1;

delete 
from Students
where StudentID = 1;

select * from Students;
select * from AuditLog;


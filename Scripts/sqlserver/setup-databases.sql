-- SQL Server Setup Script

CREATE DATABASE patients;
GO
USE patients;
GO

CREATE TABLE Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Age INT,
    Phone NVARCHAR(50),
    Address NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

INSERT INTO Patients (Name, Age, Phone, Address) VALUES
(N'สมชาย ใจดี', 30, '0812345678', N'กรุงเทพฯ'),
(N'สมหญิง รักเรียน', 25, '0823456789', N'นนทบุรี'),
(N'มานี มานะ', 40, '0834567890', N'ปทุมธานี'),
(N'ประเสริฐ สุขใจ', 55, '0845678901', N'สมุทรปราการ'),
(N'วิไลวรรณ งามตา', 35, '0856789012', N'กรุงเทพฯ');
GO

CREATE DATABASE doctors;
GO
USE doctors;
GO

CREATE TABLE Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Specialty NVARCHAR(255),
    Phone NVARCHAR(50),
    Department NVARCHAR(255)
);
GO

INSERT INTO Doctors (Name, Specialty, Phone, Department) VALUES
(N'นพ.สมศักดิ์ เก่งกาจ', N'อายุรกรรม', '021111111', N'อายุรกรรม'),
(N'พญ.ประภาส รักษาดี', N'กุมารเวชกรรม', '022222222', N'กุมารเวชกรรม'),
(N'นพ.วิชัย ใจเย็น', N'ศัลยกรรม', '023333333', N'ศัลยกรรม'),
(N'พญ.รัตนา สว่างไสว', N'สูติ-นรีเวช', '024444444', N'สูติ-นรีเวช'),
(N'นพ.อนันต์ มีสุข', N'จักษุวิทยา', '025555555', N'จักษุวิทยา');
GO

CREATE DATABASE appointments;
GO
USE appointments;
GO

CREATE TABLE Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Status NVARCHAR(50) DEFAULT N'รอดำเนินการ',
    Notes NVARCHAR(500)
);
GO

INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status, Notes) VALUES
(1, 1, DATEADD(day, 1, GETDATE()), N'รอดำเนินการ', N'ตรวจสุขภาพประจำปี'),
(2, 2, DATEADD(day, 2, GETDATE()), N'ยืนยันแล้ว', N'ตรวจพัฒนาการเด็ก'),
(3, 1, DATEADD(day, 3, GETDATE()), N'รอดำเนินการ', N'ปวดท้องเรื้อรัง');
GO

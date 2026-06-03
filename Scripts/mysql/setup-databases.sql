-- MySQL Setup Script

CREATE DATABASE IF NOT EXISTS patients;
USE patients;

CREATE TABLE IF NOT EXISTS Patients (
    PatientID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Age INT,
    Phone VARCHAR(50),
    Address VARCHAR(500),
    CreatedAt DATETIME DEFAULT NOW()
);

INSERT INTO Patients (Name, Age, Phone, Address) VALUES
('สมชาย ใจดี', 30, '0812345678', 'กรุงเทพฯ'),
('สมหญิง รักเรียน', 25, '0823456789', 'นนทบุรี'),
('มานี มานะ', 40, '0834567890', 'ปทุมธานี'),
('ประเสริฐ สุขใจ', 55, '0845678901', 'สมุทรปราการ'),
('วิไลวรรณ งามตา', 35, '0856789012', 'กรุงเทพฯ');

CREATE DATABASE IF NOT EXISTS doctors;
USE doctors;

CREATE TABLE IF NOT EXISTS Doctors (
    DoctorID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Specialty VARCHAR(255),
    Phone VARCHAR(50),
    Department VARCHAR(255)
);

INSERT INTO Doctors (Name, Specialty, Phone, Department) VALUES
('นพ.สมศักดิ์ เก่งกาจ', 'อายุรกรรม', '021111111', 'อายุรกรรม'),
('พญ.ประภาส รักษาดี', 'กุมารเวชกรรม', '022222222', 'กุมารเวชกรรม'),
('นพ.วิชัย ใจเย็น', 'ศัลยกรรม', '023333333', 'ศัลยกรรม'),
('พญ.รัตนา สว่างไสว', 'สูติ-นรีเวช', '024444444', 'สูติ-นรีเวช'),
('นพ.อนันต์ มีสุข', 'จักษุวิทยา', '025555555', 'จักษุวิทยา');

CREATE DATABASE IF NOT EXISTS appointments;
USE appointments;

CREATE TABLE IF NOT EXISTS Appointments (
    AppointmentID INT AUTO_INCREMENT PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Status VARCHAR(50) DEFAULT 'รอดำเนินการ',
    Notes VARCHAR(500)
);

INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status, Notes) VALUES
(1, 1, DATE_ADD(NOW(), INTERVAL 1 DAY), 'รอดำเนินการ', 'ตรวจสุขภาพประจำปี'),
(2, 2, DATE_ADD(NOW(), INTERVAL 2 DAY), 'ยืนยันแล้ว', 'ตรวจพัฒนาการเด็ก'),
(3, 1, DATE_ADD(NOW(), INTERVAL 3 DAY), 'รอดำเนินการ', 'ปวดท้องเรื้อรัง');

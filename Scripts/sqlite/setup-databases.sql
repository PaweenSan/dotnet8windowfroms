-- SQLite Setup Script
-- Run this to create tables and seed data for 3 databases

-- patients.db
CREATE TABLE IF NOT EXISTS Patients (
    PatientID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Age INTEGER,
    Phone TEXT,
    Address TEXT,
    CreatedAt TEXT DEFAULT (datetime('now'))
);

INSERT INTO Patients (Name, Age, Phone, Address) VALUES
('สมชาย ใจดี', 30, '0812345678', 'กรุงเทพฯ'),
('สมหญิง รักเรียน', 25, '0823456789', 'นนทบุรี'),
('มานี มานะ', 40, '0834567890', 'ปทุมธานี'),
('ประเสริฐ สุขใจ', 55, '0845678901', 'สมุทรปราการ'),
('วิไลวรรณ งามตา', 35, '0856789012', 'กรุงเทพฯ');

-- doctors.db
CREATE TABLE IF NOT EXISTS Doctors (
    DoctorID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Specialty TEXT,
    Phone TEXT,
    Department TEXT
);

INSERT INTO Doctors (Name, Specialty, Phone, Department) VALUES
('นพ.สมศักดิ์ เก่งกาจ', 'อายุรกรรม', '021111111', 'อายุรกรรม'),
('พญ.ประภาส รักษาดี', 'กุมารเวชกรรม', '022222222', 'กุมารเวชกรรม'),
('นพ.วิชัย ใจเย็น', 'ศัลยกรรม', '023333333', 'ศัลยกรรม'),
('พญ.รัตนา สว่างไสว', 'สูติ-นรีเวช', '024444444', 'สูติ-นรีเวช'),
('นพ.อนันต์ มีสุข', 'จักษุวิทยา', '025555555', 'จักษุวิทยา');

-- appointments.db
CREATE TABLE IF NOT EXISTS Appointments (
    AppointmentID INTEGER PRIMARY KEY AUTOINCREMENT,
    PatientID INTEGER NOT NULL,
    DoctorID INTEGER NOT NULL,
    AppointmentDate TEXT NOT NULL,
    Status TEXT DEFAULT 'รอดำเนินการ',
    Notes TEXT
);

INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status, Notes) VALUES
(1, 1, datetime('now', '+1 day'), 'รอดำเนินการ', 'ตรวจสุขภาพประจำปี'),
(2, 2, datetime('now', '+2 day'), 'ยืนยันแล้ว', 'ตรวจพัฒนาการเด็ก'),
(3, 1, datetime('now', '+3 day'), 'รอดำเนินการ', 'ปวดท้องเรื้อรัง');

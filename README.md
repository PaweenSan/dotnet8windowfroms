# ระบบจัดการคลินิก (Clinic Management System)

โปรแกรมจัดการคลินิกพัฒนาด้วย C# WinForms เชื่อมต่อฐานข้อมูล 3 ฐานข้อมูล (SQLite)
---

## ความสามารถหลัก (Features)

- **จัดการผู้ป่วย:** เพิ่ม, แสดง, ค้นหา, แก้ไข, ลบ ข้อมูลผู้ป่วย
- **จัดการแพทย์:** เพิ่ม, แสดง, กรอง, แก้ไข, ลบ ข้อมูลแพทย์
- **จัดการนัดหมาย:** นัดหมายผู้ป่วยกับแพทย์ อัปเดตสถานะ กรองตามวันที่/สถานะ
- **เชื่อมต่อ 3 ฐานข้อมูล:** patients.db, doctors.db, appointments.db
- **MessageBox ยืนยัน:** ทุกการ Add/Update/Delete มี Yes/No
- **รองรับหลายฐานข้อมูล:** SQLite (default), SQL Server, MySQL

---

## โครงสร้างโปรแกรม

```
ClinicManagement/
├── ClinicWinForms/           -- WinForms App (หลัก)
│   ├── frmMain.cs             -- เมนูหลัก + สถานะ DB
│   ├── frmPatients.cs         -- CRUD ผู้ป่วย
│   ├── frmDoctors.cs          -- CRUD แพทย์
│   ├── frmAppointments.cs    -- CRUD นัดหมาย
│   └── Data/
│       ├── DatabaseConfig.cs       -- ตั้งค่า DB type
│       ├── DbConnectionFactory.cs  -- สร้าง connection
│       ├── DatabaseInitializer.cs  -- สร้างตาราง + seed data
│       ├── PatientRepository.cs    -- SQL ผู้ป่วย
│       ├── DoctorRepository.cs     -- SQL แพทย์
│       └── AppointmentRepository.cs -- SQL นัดหมาย + ATTACH DB
├── ClinicMcpServer/          -- MCP Server (Bonus)
│   ├── Program.cs
│   └── Services/
│       ├── PatientService.cs
│       ├── DoctorService.cs
│       └── AppointmentService.cs
└── Scripts/
    ├── sqlite/
    ├── sqlserver/
    └── mysql/
```

---

## เทคนิคการเชื่อมต่อฐานข้อมูล

### สลับฐานข้อมูลได้ด้วยการเปลี่ยน 1 ตัวแปร
ใน `DatabaseConfig.cs`:
```csharp
public const string DB_TYPE = "sqlite";
// เปลี่ยนเป็น "sqlserver" หรือ "mysql" ได้ทันที
```

### ใช้ Interface เพื่อสลับ Database ไม่ต้องแก้โค้ดทุกจุด
```csharp
public static IDbConnection CreateConnection(string connectionString)
{
    return DatabaseConfig.DB_TYPE switch
    {
        "sqlite" => new SqliteConnection(connectionString),
        "sqlserver" => new SqlConnection(connectionString),
        "mysql" => new MySqlConnection(connectionString),
    };
}
```
คืนค่าเป็น `IDbConnection` (interface) ทำให้ Repository ทุกตัวใช้ได้กับทุก DB

### เชื่อม 3 ฐานข้อมูลแยกไฟล์ (SQLite)
ใช้ `ATTACH DATABASE` เพื่อ JOIN ข้ามไฟล์:
```sql
ATTACH DATABASE 'patients.db' AS patients;
ATTACH DATABASE 'doctors.db' AS doctors;

SELECT a.*, p.Name AS PatientName, d.Name AS DoctorName
FROM Appointments a
LEFT JOIN patients.Patients p ON a.PatientID = p.PatientID
LEFT JOIN doctors.Doctors d ON a.DoctorID = d.DoctorID
```

---

## วิธีรันโปรแกรม

### วิธีที่ 1: รันผ่าน .NET SDK (ต้องติดตั้ง SDK ก่อน)

**WinForms:**
```bash
dotnet run --project ClinicWinForms/ClinicWinForms.csproj
```

**MCP Server (Bonus):**
```bash
dotnet run --project ClinicMcpServer/ClinicMcpServer.csproj
```

หรือเปิด `ClinicManagement.sln` ใน Visual Studio แล้วกด F5

### วิธีที่ 2: รันไฟล์ .exe ตรงๆ (ไม่ต้องมี SDK)

หากเครื่องมีแค่ **.NET Runtime** แต่ไม่มี **SDK** (`dotnet run` ใช้ไม่ได้) ให้รันไฟล์ที่ Build ไว้แล้วได้เลย:

**WinForms:**
```bash
ClinicWinForms/bin/Debug/net8.0-windows/ClinicWinForms.exe
```

**MCP Server (Bonus):**
```bash
ClinicMcpServer/bin/Debug/net8.0/ClinicMcpServer.exe
```

> **หมายเหตุ:** วิธีที่ 2 ใช้ได้เฉพาะรันโปรแกรมที่ Build ไว้แล้ว หากต้องการแก้ไขโค้ดและ Build ใหม่ จำเป็นต้องติดตั้ง **.NET 8.0 SDK**

---

## ข้อกำหนดการต่อขยาย

- แก้ `DatabaseConfig.DB_TYPE` เป็น `"sqlserver"` หรือ `"mysql"`
- รัน SQL script ใน `Scripts/[dbtype]/setup-databases.sql`
- เพิ่ม NuGet package `System.Data.SqlClient` หรือ `MySql.Data` ใน `.csproj`
- ไม่ต้องแก้ไข Repository ใดๆ เพราะใช้ `IDb*` interfaces ทั้งหมด

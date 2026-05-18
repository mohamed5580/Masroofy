<!-- ========================= -->
<!--        HERO SECTION       -->
<!-- ========================= -->

<div align="center">

<img src="https://raw.githubusercontent.com/github/explore/main/topics/csharp/csharp.png"
     width="120"
     alt="Masroofy Logo"/>

# 💰 Masroofy — Personal Budget Tracker

<p>
<b>Masroofy</b> (<i>"My Expenses"</i> in Arabic) is a Windows desktop application for personal budget management built with <b>C# / .NET 9</b> and <b>Windows Forms</b>.
</p>

<p>
It allows users to manage budget cycles, track expenses, visualize spending statistics, and secure the application with a PIN system.
</p>

<p>

<img src="https://img.shields.io/badge/.NET-9.0-purple?style=for-the-badge&logo=dotnet" />
<img src="https://img.shields.io/badge/C%23-WinForms-blue?style=for-the-badge&logo=csharp" />
<img src="https://img.shields.io/badge/Architecture-3--Layer-success?style=for-the-badge" />
<img src="https://img.shields.io/badge/Database-SQLite%20%7C%20MySQL%20%7C%20SQL%20Server-orange?style=for-the-badge" />

</p>

</div>

---

## 📸 Preview

<p align="center">
  <img src="https://via.placeholder.com/1000x500.png?text=Masroofy+Dashboard+Preview"
       alt="Masroofy Preview"
       width="90%">
</p>

> Replace the preview image above with screenshots from your application.

---

# ✨ Features

| Feature | Description |
|---|---|
| 💵 **Budget Cycles** | Create spending periods with allowance, start date, and end date |
| 🧾 **Expense Logging** | Add expenses with categories and timestamps |
| 📊 **Statistics Dashboard** | Pie chart + daily spending gauge |
| 🔒 **PIN Security** | SHA-256 hashed 4-digit PIN with lockout |
| 🗂️ **Transaction History** | View complete expense history |
| 🔄 **Multi-Database Support** | SQLite, SQL Server, and MySQL |

---

# 🏗️ Architecture

```text
Masroofy.UI        → Windows Forms Presentation Layer
Masroofy.Business  → Business Logic & Services
Masroofy.Data      → Database & Raw SQL Access
```
<p align="center"> <img src="https://raw.githubusercontent.com/github/explore/main/topics/dotnet/dotnet.png" width="100" alt=".NET"/> </p>

📁 Project Structure
Masroofy/
│
├── Masroofy.slnx
│
├── Masroofy.Data/
│   ├── Models/
│   ├── SQLiteDatabase/
│   ├── Database/
│   ├── Resources/
│   └── Properties/
│
├── Masroofy.Business/
│   ├── Repositories/
│   └── Services/
│
├── Masroofy.UI/
│   ├── Helper/
│   ├── Resources/
│   └── Properties/
│
└── Doc/


🚀 Technologies Used

<div align="center"> <table> <tr> <td align="center"> <img src="https://raw.githubusercontent.com/github/explore/main/topics/csharp/csharp.png" width="70"/><br/> <b>C#</b> </td> <td align="center"> <img src="https://raw.githubusercontent.com/github/explore/main/topics/dotnet/dotnet.png" width="70"/><br/> <b>.NET 9</b> </td> <td align="center"> <img src="https://raw.githubusercontent.com/github/explore/main/topics/sqlite/sqlite.png" width="70"/><br/> <b>SQLite</b> </td> <td align="center"> <img src="https://raw.githubusercontent.com/github/explore/main/topics/mysql/mysql.png" width="70"/><br/> <b>MySQL</b> </td> <td align="center"> <img src="https://raw.githubusercontent.com/github/explore/main/topics/microsoft-sql-server/microsoft-sql-server.png" width="70"/><br/> <b>SQL Server</b> </td> </tr> </table> </div>

⚙️ How to Run
1️⃣ Clone Repository
git clone https://github.com/your-username/Masroofy.git
cd Masroofy
2️⃣ Open Solution
start Masroofy.slnx

Or open manually with Visual Studio 2022.

3️⃣ Restore Packages
dotnet restore
4️⃣ Run Application

🔐 Security
SHA-256 PIN hashing
3 failed attempts protection
30-second lockout system
📊 Dashboard

The dashboard provides:

Remaining budget tracking
Daily safe spending limit
Pie chart analytics
Final-day warnings

<p align="center"> <img src="https://via.placeholder.com/900x400.png?text=Statistics+Dashboard" width="85%" alt="Dashboard"/> </p>

📦 Dependencies
Package	Purpose
Microsoft.Data.Sqlite	SQLite Driver
Microsoft.Data.SqlClient	SQL Server Driver
MySql.Data	MySQL Driver
Microsoft.Extensions.DependencyInjection	Dependency Injection
MetroFramework	UI Styling

🧠 Design Patterns & Principles
Repository Pattern
Dependency Injection
Layered Architecture
Separation of Concerns
Stateless Services

👨‍💻 Author
<div align="center">
Mohamed Ahmed
<p> Personal Budget Management System built with ❤️ using C# and .NET. </p> </div>


⭐ Support

If you like this project:

Give it a ⭐ on GitHub
# 💰 Masroofy — Personal Budget Tracker

<<<<<<< HEAD
**Masroofy** (مصروفي — *"My Expenses"* in Arabic) is a Windows desktop application for personal budget management. It lets users define time-bounded budget cycles, log daily expenses by category, visualise spending with a live pie chart and daily-limit indicator, and protect the app with a numeric PIN.
=======
**Masroofy** (Masroofy — *"My Expenses"* in Arabic) is a Windows desktop application for personal budget management. It lets users define time-bounded budget cycles, log daily expenses by category, visualise spending with a live pie chart and daily-limit indicator, and protect the app with a numeric PIN.
>>>>>>> ab0c5db03f605a7b2a0833a0f293459950d61d52

The project is built in **C# / .NET 9** using **Windows Forms** and follows a clean **three-layer architecture** (Data → Business → UI) with full **dependency injection** via `Microsoft.Extensions.DependencyInjection`.

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Folder Structure](#2-folder-structure)
3. [File-by-File Explanation](#3-file-by-file-explanation)
   - [Masroofy.Data](#masroofydata)
   - [Masroofy.Business](#masroofybusiness)
   - [Masroofy.UI](#masroofyui)
   - [Doc](#doc)
4. [How the Layers Work Together](#4-how-the-layers-work-together)
5. [How to Run the Project](#5-how-to-run-the-project)
6. [Dependencies](#6-dependencies)
7. [Important Classes & Functions — Quick Reference](#7-important-classes--functions--quick-reference)

---

## 1. Project Overview

| Feature | Description |
|---|---|
| **Budget Cycles** | Define a spending period with a total allowance, start date, and end date. Only one cycle can be active at a time. |
| **Expense Logging** | Record individual expenses, each assigned to one of five categories: Food, Transport, Entertainment, Utilities, Other. |
| **Daily Limit** | Automatically recalculates the *safe daily spending limit* based on the remaining balance divided by remaining days in the active cycle. |
| **Statistics Dashboard** | Live pie chart of spending by category and a double-ring "Today's Limit" gauge. Turns orange when the user is overspent. |
| **Transaction History** | View all past expenses with timestamps and category names. |
| **PIN Security** | Optional 4-digit PIN stored and verified as a SHA-256 hash. Three wrong attempts trigger a 30-second lockout. |
| **Multi-Database Support** | Works with **SQLite** (default), **SQL Server**, or **MySQL** — swappable at runtime via the Settings screen. |

---

## 2. Folder Structure

```
Masroofy/
│
├── Masroofy.slnx              # Solution file
│
├── Masroofy.Data/             # Layer 1 — Data access, models, database file
│   ├── Models/                # Plain C# entity classes
│   ├── SQLiteDatabase/        # DataAccessLayer (DAL) — raw SQL execution
│   ├── Database/              # SQLite .db file and DB Browser project
│   ├── Resources/             # Embedded resources for the data project
│   └── Properties/            # App settings (connection string, DB provider)
│
├── Masroofy.Business/         # Layer 2 — Business logic and repository contracts
│   ├── Repositories/          # Concrete repository implementations + interfaces
│   └── Services/              # Domain services (budget math, security, validation)
│
├── Masroofy.UI/               # Layer 3 — Windows Forms screens
│   ├── Helper/                # Lightweight UI controllers (no business logic)
│   ├── Resources/             # Icons, images used by forms
│   └── Properties/            # UI-level app settings
│
└── Doc/                       # Doxygen-generated HTML/LaTeX API documentation
```

---

## 3. File-by-File Explanation

### Masroofy.Data

This project owns everything related to **storage** — models, raw SQL, and the physical database file. It has no knowledge of business rules or the UI.

---

#### `Models/BudgetCycle.cs`
Represents one spending period.

| Property | Type | Description |
|---|---|---|
| `Id` | `int` | Primary key (auto-assigned by SQLite) |
| `TotalAllowance` | `decimal` | The total budget for this cycle |
| `StartDate` | `DateTime` | First day of the cycle |
| `EndDate` | `DateTime` | Last day of the cycle |
| `IsActive` | `bool` | Only one cycle may be active at a time |

---

#### `Models/Transaction.cs`
Represents a single expense entry.

| Property | Type | Description |
|---|---|---|
| `Id` | `int` | Primary key |
| `Amount` | `decimal` | Expense amount |
| `Timestamp` | `DateTime` | When the expense was recorded |
| `CategoryId` | `int` | Foreign key → Categories table |
| `BudgetCycleId` | `int` | Foreign key → BudgetCycles table |
| `CategoryName` | `string` | Populated on read (JOIN result) |

---

#### `Models/Category.cs`
Lookup entity for expense categories (Food, Transport, etc.).

---

#### `SQLiteDatabase/DataAccessLayer.cs`
The **central database gateway**. All SQL execution in the application flows through this static class.

| Member | Purpose |
|---|---|
| `DatabaseProvider` enum | `SQLite`, `SqlServer`, `MySQL` — selects which driver to use |
| `Configure(provider, connectionString)` | Called once at startup from `Program.cs` to set the active provider |
| `CreateConnection()` | Factory — returns the correct `DbConnection` subclass |
| `ExecuteNonQueryAsync(...)` | Runs INSERT / UPDATE / DELETE, returns rows affected |
| `ExecuteScalarAsync(...)` | Runs a query returning one value (e.g. `last_insert_rowid()`) |
| `ExecuteReaderAsync(...)` | Runs a SELECT, returns a `DbDataReader` (connection closed on dispose) |
| `CreateParameter(name, type, value)` | Creates a provider-correct `DbParameter` |
| `SeedCategoriesAsync()` | Seeds the five default categories into the Categories table on first run |

> **Design note:** Using `DbConnection` / `DbCommand` / `DbParameter` abstractions (instead of SQLite-specific types) is what makes the multi-database swap possible without touching any other file.

---

#### `Database/Mastoofy.db`
The physical **SQLite database file** bundled with the project. Contains three tables:
- `BudgetCycles`
- `Transactions`
- `Categories` (seeded on startup)
- `Authentication` (stores the hashed PIN)

#### `Database/Mastoofy.sqbpro`
DB Browser for SQLite project file — used for inspecting/editing the database during development. Not required at runtime.

---

### Masroofy.Business

This project contains all **business rules** and **repository contracts**. It depends on `Masroofy.Data` for models and the DAL, but is completely unaware of Windows Forms.

---

#### `Repositories/IBudgetCycleRepository.cs`
Interface defining all budget-cycle database operations:
- `CreateAsync`, `GetByIdAsync`, `GetActiveCycleAsync`, `GetAllCyclesAsync`
- `UpdateAsync`, `DeleteAsync`, `DeactivateCurrentCycleAsync`
- `GetTotalCycleAsync`, `GetByCategoryAsync`

#### `Repositories/BudgetCycleRepository.cs`
Concrete implementation of `IBudgetCycleRepository`. Uses `DataAccessLayer` to run parameterised SQL queries. Notable methods:

| Method | What it does |
|---|---|
| `CreateAsync` | Inserts a new cycle and returns the new row ID |
| `GetActiveCycleAsync` | Fetches the single cycle where `IsActive = 1` |
| `DeactivateCurrentCycleAsync` | Sets `IsActive = 0` on all cycles before a new one is created |
| `GetByCategoryAsync` | Searches the Categories table by name (used for autocomplete) |

---

#### `Repositories/ITransactionRepository.cs`
Interface for transaction CRUD plus history retrieval.

#### `Repositories/TransactionRepository.cs`
Concrete implementation. Notable methods:

| Method | What it does |
|---|---|
| `AddAsync` | Inserts a new transaction and returns its new ID |
| `GetByCycleIdAsync` | Returns all transactions for a given budget cycle (used for balance calculations) |
| `GetHistoryAsync` | Returns all transactions joined with category names (used by the Transactions screen) |

---

#### `Services/BudgetService.cs`
The **core domain service** — the only place where budget math lives.

| Method | What it does |
|---|---|
| `FindAllAndCalculateRemainingAsync(cycleId)` | Fetches the cycle + its transactions, delegates math to `RolloverEngine`, returns `(remainingBalance, remainingDays)` |
| `RecalculateAfterExpenseAsync(cycleId)` | Returns `(newLimit, totalSpent, percentage)` — used to refresh the main dashboard after an expense is saved |
| `GetSafeDailyLimitAsync(cycleId)` | `remainingBalance / remainingDays`, rounded to 2 decimal places |
| `GetTotalSpentAsync(cycleId)` | Sum of all transaction amounts in the cycle |
| `GetCategoryBreakdownAsync(cycleId)` | Groups transactions by category and sums amounts — feeds the pie chart |
| `CreateCycleAsync(cycle)` | Deactivates the current cycle, then creates the new one |

---

#### `Services/RolloverEngine.cs`
A pure, stateless calculation helper.

```
CalculateRemaining(cycle, transactions, currentDate)
  → remainingBalance = cycle.TotalAllowance - sum(transactions.Amount)
  → remainingDays    = ceil((cycle.EndDate - currentDate).TotalDays)  [min 1]
```

Keeping this logic in its own class makes it easy to unit-test independently.

---

#### `Services/SecurityService.cs`
Handles PIN-based access control **in memory**.

| Method | What it does |
|---|---|
| `SetPin(plainPin)` | Hashes the PIN with SHA-256 and stores the hash |
| `VerifyPin(enteredPin)` | Hashes the input and compares; tracks failed attempts |
| `IsLockedOut()` | Returns `true` if the user has failed 3 attempts (30-second lockout) |
| `HasPin()` | `true` if a PIN hash is stored |

> **Note:** The PIN is also persisted in the `Authentication` database table. `Setting.cs` handles the DB side; `SecurityService` handles the in-memory/runtime side.

---

#### `Services/ValidationService.cs`
Simple, stateless validators:
- `IsValidAmount(decimal)` — must be `> 0`
- `IsValidDateRange(start, end)` — `start < end`
- `IsValidPin(string)` — exactly 4 numeric digits

---

### Masroofy.UI

The Windows Forms project. It is the **entry point** for the application and depends on both `Masroofy.Data` and `Masroofy.Business`.

---

#### `Program.cs`
Application entry point. Responsibilities:
1. Reads the chosen database provider from `Settings` (`SQLite` / `MySQL` / `SqlServer`) and calls `DataAccessLayer.Configure(...)`.
2. Builds the **DI container** (`ServiceCollection`) registering all repositories, services, and form instances.
3. Key lifetime decisions:
   - `StatisticsDashbourd` and `Dashbourd` are **singletons** — only one instance ever exists so live data references stay valid.
   - All repositories and services are **transient** — fresh instance per resolve.
4. Launches the app with `Application.Run(Dashbourd)`.

---

#### `Dashbourd.cs` / `Dashbourd.Designer.cs`
The **main window** — the shell that hosts the navigation buttons, a clock/date display, and a system-tray notification icon.

| Responsibility | Detail |
|---|---|
| Navigation | Opens `ExpenseEntryScreen`, `BudgetCycleForm`, `StatisticsDashbourd`, `Transactions`, and `Setting` forms via DI |
| `RefreshData()` | Calls `BudgetService.RecalculateAfterExpenseAsync` after an expense is saved |
| `ShowNotification(title, message)` | Displays a Windows balloon tooltip via `NotifyIcon` |
| Exit confirmation | Shows an Arabic-language confirmation dialog on close |

---

#### `StatisticsDashbourd.cs` / `StatisticsDashbourd.Designer.cs`
The **statistics view** with two custom-painted panels.

| Panel | What it renders |
|---|---|
| `pnlLimitCircle` | A double concentric ring gauge showing "Today's Limit" in EGP. Turns orange/red when the user is in a deficit (tight budget). |
| `pnlPieChart` | A colour-coded pie chart of spending by category, plus a legend with amounts and percentages. |

Key methods:

| Method | Purpose |
|---|---|
| `RefreshDashboardData()` | Async method — fetches the active cycle, creates a `DashboardUIController`, and calls `OnOpen()` |
| `Refresh()` | Resets all chart state and repaints the panels |
| `Display(safeDailyLimit, remainingBalance, categoryTotals)` | Populates chart data and triggers a repaint |
| `ShowFinalDayBadge()` | Makes the red "⚠ FINAL DAY" label visible on the last day of a cycle |

> This form is registered as a **singleton** in DI and hides (instead of closes) when the user clicks X, so the same instance — and its data — persists for the lifetime of the application.

---

#### `ExpenseEntryScreen.cs` / `ExpenseEntryScreen.Designer.cs`
The **"Log Expense"** dialog. Features:
- Five category buttons (Food, Transport, Entertainment, Utilities, Other) — selected button turns lime green.
- Amount text field that only accepts digits and one decimal point.
- Budget warning logic: if the expense would exhaust > 80 % of the remaining budget, or push it negative, the user is shown a confirmation dialog before the save proceeds.
- After a successful save, `LoggingUIController.OnSaveTapped` is called, which persists the transaction and triggers a full dashboard refresh.

---

#### `BudgetCycleForm.cs` / `BudgetCycleForm.Designer.cs`
CRUD form for budget cycles. Supports Create, Update, and Delete.

- Raises the static `CycleChanged` event after every mutation so that `StatisticsDashbourd` (and any other subscriber) can refresh automatically.
- Validates that the amount is positive and that the end date is after the start date.
- Has an edit mode (loaded via `LoadCycleForEdit`) and a create mode, toggled by enabling/disabling the Save / Update / Delete buttons.

---

#### `BudgetCycleScreen.cs` / `BudgetCycleScreen.Designer.cs`
A read-only list view of all past budget cycles. Lets the user select a cycle and open it in `BudgetCycleForm` for editing.

---

#### `Transactions.cs` / `Transactions.Designer.cs`
Displays the full transaction history using `TransactionRepository.GetHistoryAsync`, which joins transactions with their category names.

---

#### `Setting.cs` / `Setting.Designer.cs`
The settings / configuration screen. Allows the user to:
- Switch between **SQLite**, **SQL Server**, and **MySQL** as the database provider.
- Enter a connection string (server, database name, username, password).
- **Set or change the PIN** (stored in the `Authentication` table via `SetPinAsync` / `ChangePinAsync`).
- **Reset all data** (delete all transactions and budget cycles).

Settings are persisted to `Masroofy.Data.Properties.Settings` (an XML-backed application settings file).

---

#### `PIN.cs` / `PIN.Designer.cs`
A simple PIN entry dialog used at application start (if a PIN has been set). Integrates with `SecurityService` for hash verification and lockout logic.

---

#### `Helper/DashbourdUIController.cs`
A lightweight **presentation controller** that follows the US#3 sequence diagram:

```
OnOpen(activeCycleId)
  1. CalculateRemainingBalance()   → BudgetService → DB
  2. CalculateSafeDailyLimit()     (pure math)
  3. _dashboardScreen.Refresh()
  4. _dashboardScreen.Display(...)
  5. [if last day] _dashboardScreen.ShowFinalDayBadge()
```

This keeps `StatisticsDashbourd` as a pure "view" — it never calls business services directly.

---

#### `Helper/LoggingUIController.cs`
Presentation controller for the expense-save flow:

```
OnSaveTapped(amountText, categoryId, budgetCycleId)
  1. Validate amount (ValidationService)
  2. Persist transaction (TransactionRepository.AddAsync)
  3. Trigger dashboard refresh (_dashboard.RefreshDashboardData())
```

Returns `true` on success, `false` on validation failure.

---

### Doc

Generated **Doxygen** documentation for the entire solution.

| Path | Content |
|---|---|
| `Doc/Doxyfile` | Doxygen configuration file |
| `Doc/html/` | Browsable HTML API reference |
| `Doc/latex/` | LaTeX source for a PDF version |

To regenerate: install [Doxygen](https://www.doxygen.nl/) and run `doxygen Doxyfile` from the `Doc/` directory.

---

## 4. How the Layers Work Together

```
┌────────────────────────────────────────────────────────┐
│                    Masroofy.UI                          │
│  Dashbourd ─── StatisticsDashbourd ─── ExpenseEntry    │
│       │                 │                    │          │
│  DashboardUIController  │         LoggingUIController   │
└────────────────┬────────┴────────────────┬─────────────┘
                 │ calls services          │
┌────────────────▼────────────────────────▼─────────────┐
│                  Masroofy.Business                      │
│  BudgetService  RolloverEngine  SecurityService        │
│       │                              ValidationService  │
│  IBudgetCycleRepository  ITransactionRepository        │
└────────────────┬────────────────────────┬─────────────┘
                 │ implements             │
┌────────────────▼────────────────────────▼─────────────┐
│                   Masroofy.Data                         │
│  BudgetCycleRepository   TransactionRepository         │
│              │                    │                    │
│              └────────────────────┘                    │
│                    DataAccessLayer                      │
│          (SQLite / SQL Server / MySQL)                  │
│                   Mastoofy.db                           │
└────────────────────────────────────────────────────────┘
```

**Data flow for logging an expense:**
1. User fills in amount + category in `ExpenseEntryScreen` and clicks Confirm.
2. `ExpenseEntryScreen` calls `LoggingUIController.OnSaveTapped(...)`.
3. `LoggingUIController` calls `ValidationService.IsValidAmount(...)`.
4. If valid, calls `TransactionRepository.AddAsync(transaction)` → `DataAccessLayer.ExecuteScalarAsync(...)` → SQLite.
5. Then calls `StatisticsDashbourd.RefreshDashboardData()`.
6. `StatisticsDashbourd` calls `DashboardUIController.OnOpen(cycleId)`.
7. `DashboardUIController` calls `BudgetService.FindAllAndCalculateRemainingAsync(cycleId)` → `RolloverEngine.CalculateRemaining(...)`.
8. Dashboard panels repaint with the new figures.

---

## 5. How to Run the Project

### Prerequisites

| Requirement | Version |
|---|---|
| .NET SDK | 9.0 or later |
| Visual Studio | 2022 (Community or higher) with the **.NET Desktop Development** workload |
| Windows | 10 / 11 (Windows Forms is Windows-only) |
| SQLite | No installation needed — the `.db` file is bundled |

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/Masroofy.git
   cd Masroofy
   ```

2. **Open the solution**
   Double-click `Masroofy.slnx` in Visual Studio 2022, or:
   ```bash
   start Masroofy.slnx
   ```

3. **Restore NuGet packages**
   Visual Studio does this automatically on build. If needed:
   ```bash
   dotnet restore
   ```

4. **Set the startup project**
   Right-click `Masroofy.UI` → **Set as Startup Project**.

5. **Run**
   Press **F5** (Debug) or **Ctrl + F5** (without debugger).
   The application starts with `Dashbourd` as the main window.

6. **First-time setup**
   - Open the **Settings** screen (⚙ icon or menu) and confirm the database path points to `Masroofy.Data/Database/Mastoofy.db`.
   - Create your first budget cycle via the **Budget Cycle** button.

> **Switching database:** Open Settings, select SQL Server or MySQL, fill in connection details, and restart the app. All tables must already exist in the target database (schema is identical to the SQLite structure).

---

## 6. Dependencies

### NuGet Packages

| Package | Version | Used by | Purpose |
|---|---|---|---|
| `Microsoft.Data.Sqlite.Core` | 11.0.0-preview | Masroofy.Data | SQLite ADO.NET driver |
| `Microsoft.Data.SqlClient` | 7.0.1 | Masroofy.Data | SQL Server driver |
| `MySql.Data` | 9.7.0 | Masroofy.Data | MySQL driver |
| `Microsoft.Extensions.DependencyInjection` | (via .NET SDK) | Masroofy.UI | DI container |
| `MetroFramework` | (referenced in Setting.cs) | Masroofy.UI | Modern metro-style form theming |

### System / SDK

| Requirement | Notes |
|---|---|
| `System.Security.Cryptography` | Built-in — used for SHA-256 PIN hashing |
| `System.Windows.Forms` | Built-in .NET 9 WinForms — the entire UI framework |
| `System.Drawing` | Built-in — used for custom pie chart and gauge painting |

---

## 7. Important Classes & Functions — Quick Reference

| Class | File | Key Responsibility |
|---|---|---|
| `DataAccessLayer` | `Masroofy.Data/SQLiteDatabase/DataAccessLayer.cs` | All database I/O; provider-agnostic SQL execution |
| `BudgetService` | `Masroofy.Business/Services/BudgetService.cs` | All budget math; the primary entry point for business logic |
| `RolloverEngine` | `Masroofy.Business/Services/RolloverEngine.cs` | Pure `(remainingBalance, remainingDays)` calculation |
| `SecurityService` | `Masroofy.Business/Services/SecurityService.cs` | In-memory PIN verification with SHA-256 and lockout |
| `ValidationService` | `Masroofy.Business/Services/ValidationService.cs` | Stateless input validators |
| `BudgetCycleRepository` | `Masroofy.Business/Repositories/BudgetCycleRepository.cs` | CRUD + queries for the BudgetCycles table |
| `TransactionRepository` | `Masroofy.Business/Repositories/TransactionRepository.cs` | CRUD + history query for the Transactions table |
| `DashboardUIController` | `Masroofy.UI/Helper/DashbourdUIController.cs` | Orchestrates the stats-dashboard refresh sequence |
| `LoggingUIController` | `Masroofy.UI/Helper/LoggingUIController.cs` | Validates, persists, and refreshes after expense save |
| `StatisticsDashbourd` | `Masroofy.UI/StatisticsDashbourd.cs` | Pie chart + daily-limit gauge; singleton view |
| `Program` | `Masroofy.UI/Program.cs` | App entry point; DI container composition root |

---

> **Project name etymology:** *Masroofy* (مصروفي) is Arabic for *"my expenses"* — reflecting the application's core purpose of helping users track and control their daily spending.

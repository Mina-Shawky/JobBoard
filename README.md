 <div align="center">

# 💼 JobBoard API

### A Job Portal RESTful API built with ASP.NET Core

<p>
  A backend solution that connects job seekers with recruiters, allowing users to explore job opportunities, apply for jobs, and track their applications.
</p>

<p>
  <strong>ASP.NET Core Web API</strong> · <strong>Entity Framework Core</strong> · <strong>SQL Server</strong> · <strong>ASP.NET Core Identity</strong>
</p>

</div>

---

## 📌 About The Project

**JobBoard API** is a job portal backend application designed to manage job opportunities and job applications.

The system provides role-based access for different types of users, ensuring that each user can access only the features permitted by their role.

The main goal is to provide a structured and secure API for managing recruiters, job seekers, job postings, and applications.

## ✨ Features

### 👤 User Management & Authentication
- User registration and login.
- Secure password management using ASP.NET Core Identity.
- Role-based authorization.
- Support for three roles: Admin, Recruiter, and User.

### 💼 Job Management
- Retrieve all available jobs.
- Search jobs by keyword.
- Retrieve job details by ID.
- Recruiters can create new job postings.
- Recruiters can update and delete their own jobs.
- Job ownership validation to prevent unauthorized modifications.

### 📄 Job Applications
- Users can apply for job opportunities.
- Prevent users from applying multiple times to the same job.
- Users can view their submitted applications.
- Recruiters can view applications submitted to their jobs.
- Recruiters can update application statuses.

### 📊 Application Status Tracking

Each application has one of the following statuses:

| Status | Description |
|---|---|
| Submitted | Application successfully submitted |
| Reviewed | Application is under review |
| Accepted | Application has been accepted |
| Rejected | Application has been rejected |

## 🔐 User Roles & Permissions

| Role | Permissions |
|---|---|
| Admin | Administrative role for managing the platform |
| Recruiter | Create and manage own jobs, view applicants, update application statuses |
| User | Browse jobs, apply for jobs, track applications |

## 🛠️ Technologies Used

- **C#** – Main programming language.
- **ASP.NET Core Web API** – Building RESTful APIs.
- **Entity Framework Core** – Object-relational mapping (ORM).
- **SQL Server** – Database management.
- **ASP.NET Core Identity** – User and role management.
- **LINQ** – Querying and filtering data.
- **DTOs** – Data transfer and API request/response organization.
- **Git & GitHub** – Version control and source code management.

## 🏗️ Project Architecture

The project follows a structured approach to organize its main components:

- **Controllers:** Handle HTTP requests and API endpoints.
- **Models:** Define application entities and relationships.
- **DTOs:** Define the data exchanged between clients and the API.
- **Data:** Contains the database context and Entity Framework configurations.
- **Migrations:** Track database schema changes.
- **Identity:** Handles authentication, users, and roles.

## ⚙️ Getting Started

Follow these steps to run the project locally.

### 1. Clone the Repository

```bash
git clone https://github.com/Mina-Shawky/JobBoard.git
```

### 2. Open the Project

Open the solution file (`.sln` or `.slnx`) using Visual Studio.

### 3. Configure the Database

Update the connection string in your local configuration.

Make sure SQL Server is installed and running.

> Important: Never commit real database passwords, JWT secrets, or other sensitive configuration values to GitHub.

### 4. Apply Database Migrations

Open the Package Manager Console in Visual Studio and run:

```powershell
Update-Database
```

Alternatively, use the .NET CLI:

```bash
dotnet ef database update
```

### 5. Run the Application

Run the project from Visual Studio or use:

```bash
dotnet run
```

The API will start on the configured local development URL.

## 🧪 API Testing

You can test the API endpoints using:

- Postman.

Test the main workflows:

1. Register a new User or Recruiter.
2. Log in and obtain authentication credentials.
3. Browse available jobs.
4. Apply for a job as a User.
5. View submitted applications.
6. View applicants as the Recruiter who owns the job.
7. Update an application's status.

## 🚀 Future Improvements

- Resume / CV upload.
- Job categories and advanced filtering.
- Pagination and sorting.
- Saved jobs.
- Job expiration and closing dates.
- Notifications for application updates.
- Refresh token support.
- Admin dashboard and advanced management features.
- Unit and integration testing.

## 👨‍💻 Author

**Mina Shawky**

GitHub: [@Mina-Shawky](https://github.com/Mina-Shawky)

Project Repository: [JobBoard API](https://github.com/Mina-Shawky/JobBoard)

---

<div align="center">
  <p><strong>Built with ❤️ using ASP.NET Core</strong></p>
  <p>© 2026 JobBoard API</p>
</div>

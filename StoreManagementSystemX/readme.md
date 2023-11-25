# Store Management System

## Introduction

Welcome to the Store Management System repository! This application is designed to help manage the various aspects of a store, including inventory, sales, and customer information. Built with C#, WPF (Windows Presentation Foundation), and Entity Framework Core, this system provides a user-friendly interface for efficient store management.

## Tech Stack

-   C#: The primary programming language used for the application logic.
-   WPF (Windows Presentation Foundation): Used for creating the graphical user interface.
-   Entity Framework Core: Employed as the Object-Relational Mapping (ORM) tool for database interactions.

## Getting Started

To run the Store Management System on your local machine, follow these steps:

### Prerequisites

Visual Studio (2019 or later) with .NET Desktop Development workload.
.NET Core SDK.
The app uses SQLite for storage so it doesn't require database configuration.

#### Installation

1. Clone the repository to your local machine:

```bash
git clone https://github.com/your-username/store-management-system.git
```

2. Open the nuget package manager console and run these commands for the StoreManagementX.Database project

```powershell
    Add-Migration InitialMigrate
```

```powershell
    Add-Migration InitialMigrate
```

Open the solution file (StoreManagementSystem.sln) in Visual Studio.

Build and run the application from Visual Studio.

### Features

-   **Inventory Management**: Keep track of products, and stocks.
-   **Sales Management**: Record and manage transactions.
-   **User-Friendly Interface**: WPF provides an intuitive and visually appealing user interface.
-   **Data Persistence**: Utilizes Entity Framework Core for efficient and reliable data storage.

### Roadmap

The project roadmap outlines future enhancements and features planned for the Store Management System. The current focus includes:
[ ] - Cash register for mirroring the actual cash in the actual register

Integration of additional reporting features.
Enhanced user authentication and authorization mechanisms.
Performance optimizations and code refactoring.
Contribution
Contributions are welcome! If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

License
This project is licensed under the MIT License.

> _The purpose of this repository is to guide you in a step by step plan to create your first desktop application using the MVVM Design Pattern with C#, .NET and WPF._

# Problem
In a small library, managing the catalog of books can become cumbersome. Librarians need a simple yet efficient system to:

Keep track of all books available in the library.
- Quickly add new books to the catalog.
- Easily update the details of existing books.
- Remove outdated or unwanted books from the catalog.

Manual record-keeping or using basic spreadsheets can lead to errors, data redundancy, and inefficiencies in searching for books or updating records.

# Solution
To solve thsi little issue, we aroing going to develop a simple **_Library Management System_** using the MVVM pattern in a WPF application. The system will provide a streamlined way for librarians to manage the book catalog efficiently.

**Requirements**
1. View a List of Books
2. Add a New Book
3. Edit Book Details
4. Delete a Book

By implementing this Library Book Management System, librarians can efficiently manage their book catalog, ensuring accurate and up-to-date records. This solution addresses the common problems of manual record-keeping, reducing errors, and enhancing the overall efficiency of the library's operations.

## Implementation Plan
To design and implement the Library Management System using the MVVM pattern in a WPF application, we will follow a structured approach. This will involve setting up the project, designing the model, view, and viewmodel components, and then integrating them to achieve the desired functionality.

<Details>
  <Summary><b>1. What is MVVM?</b></Summary>
  

The **Model-View-ViewModel** architecture _**separates**_ the development of the graphical user interface (**_View_**) from the business logic or back-end logic (**_Model_**). 
The **_ViewModel_** acts as an intermediary, handling the logic and data binding between the View and the Model.

In MVVM, the ViewModel updates the View through data binding, while user interactions in the View are sent back to the ViewModel, ensuring a clear and efficient flow of information and control.

This separation improves code maintainability, testability, and scalability by organizing the code into distinct components. 

![image](https://github.com/user-attachments/assets/24e54332-eb93-4652-8e92-a9f3ef03859b)
</Details>

<Details>
  <Summary><b>2. Why .NET, C#, and MSSQL?</b></Summary>

- **.NET Core:** Provides a cross-platform, high-performance framework that supports modern application development. Its integration with MVVM enhances separation of concerns and streamlines development for various platforms.

- **C#:** A modern programming language that works seamlessly with .NET, offering powerful features for implementing MVVM and ensuring efficient, maintainable code.

- **MSSQL:** A reliable database system that integrates well with .NET applications, providing robust data management and security features to support the application's data needs effectively.

</Details>

<Details>
    <Summary><b>3. Required Tools</b></Summary>
  

To get started with this project, you'll need to install the following tools:
- [**Visual Studio:**](https://visualstudio.microsoft.com/) The integrated development environment (IDE) for building .NET applications.

- [**Microsoft SQL Server:**](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) The relational database management system used to manage the application's data.

- [**SQL Server Management Studio (SSMS):**](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) A tool for managing your SQL Server databases, running queries, and performing administrative tasks.

- **Install** [**.NET 8:**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) The latest version of the .NET framework required for developing and running your application. Ensure you download the SDK to develop and the Runtime to run your applications.

</Details>

<Details>
  <Summary><b>4. Setting Up Your Solution</b></Summary>

Start by opening Visual Studio and get ready to add your projects!

## 1. Initialize the Solution
Create a New Solution:
  Select File > New Project > Blank Solution and name it: Library
For better organization, create a "src" folder in which all the projects will be located.

## 2. Project Structure Setup
To facilitate development, maintenance, and testing, we will divide this solution into five projects: View, Client, Server, Shared, and Test.

- **View**
Contains XAML files for defining the user interface and UI resources like styles and templates.
Add a WPF Application project named: Library.View.

- **Client**
Holds ViewModelsfor data binding and client-side logic, including commands and services.
Add a Class Library project named: Library.Client.

- **Server**
Includes business logic, data models, and data access code for handling core application operations.
Add a Class Library project named: Library.Server.

- **Shared**
Features code and resources shared between client and server, such as DTOs (data transfer objects), utilities, and common interfaces.
Add a Class Library project named: Library.Shared.

- **Test**
Contains unit tests to ensure the application functions correctly based on the defined requirements.
Add an xUnit Test Project named: Library.Test.

## 3. Set Up Project References
To ensure proper communication and dependency between the projects, set up project references as follows:
```Right Click on  the Project > Select Add > Project Reference.```

- **Library.View** has the following reference: Library.Client
- **Library.Client** has the following reference: Library.Shared
- **Library.Server** has the following reference: Library.Shared
- **Library.Test** has the following reference: Library.Server

You will end up with this!

![image](https://github.com/user-attachments/assets/4632d4e9-ea53-45d6-8905-6ab6baeac098)
</Details> 

<Details>
  <Summary><b>5. Set Up EF Core</b></Summary>

 ## 1. Add EF Core Packages
  To work with Entity Framework Core (EF Core) for data access, add the following NuGet packages to the Library.Server project:

  - **Microsoft.EntityFrameworkCore:** The core EF package for data access.

     ```bash
	 PM> dotnet add package Microsoft.EntityFrameworkCore
	 ```

  - **Microsoft.EntityFrameworkCore.SqlServer:** The SQL Server provider for EF Core.

      ```bash
	  PM> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
	  ```

  - **Microsoft.EntityFrameworkCore.Tools:** Tools for EF Core migrations and commands.

     ```bash
	 PM> dotnet add package Microsoft.EntityFrameworkCore.Tools
	 ```
	 
## 2. Create the Data Model

  Define the data model for the Book and Author entity in the Library.Server project. This model will represent the book catalog and its properties.
  ```csharp
  //Project: Server
  //File: Book.cs
internal class Book
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Title { get; set; }
    [Display(Name = "Author")]
    public virtual int AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public virtual required Author Author { get; set; }
    public int Year { get; set; }
    public BookGenre Genre { get; set; }
}

  ```
  ```csharp
  //Project: Server
  //File: Author.cs
  internal class Author
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [MaxLength(50)]
    public required string LastName { get; set; }
}
  ```
  ```csharp
  //Project: Shared
  //File: DbEnum.cs
  public enum BookGenre
  {
	  Fiction,
	  NonFiction,
	  Mystery,
	  Romance,
	  ScienceFiction,
	  Biography,
	  History,
	  SelfHelp
  }
  ```

 ## 3. Set Up the Database Context

  Create a database context class that inherits from DbContext in the Library.Server project. This class will define the database connection and include a DbSet for the Book entity.
  ```csharp
  //Project: Server
  //File: LibraryContext.cs

internal class LibraryContext : DbContext, ILibraryEntities
{
    public DbSet<Book> Book { get; set; }
    public DbSet<Author> Author { get; set; }

    public IQueryable<Book> BookEntities => Set<Book>();
    public Book? FindBook(int id) => Find<Book>(id);
    public IQueryable<Author> AuthorEntities => Set<Author>();
    public Author? FindAuthor(int id) => Find<Author>(id);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(local);Integrated Security=true;Initial Catalog=Library;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False");
    }
}
interface ILibraryEntities
{
    IQueryable<Book> BookEntities { get; }
    Book? FindBook(int id);
    IQueryable<Author> AuthorEntities { get; }
    Author? FindAuthor(int id);
}
  ```

 ## 4. Add Migration and Update Database

  Use EF Core migrations to create the database schema based on the data model. Run the following commands in the Library.Server project directory:
  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```
  These commands will generate the migration files and update the database with the Book table.
</Details>



 
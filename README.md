# Coding Hours Tracker
Console based CRUD application to track time spent coding. Developed using C# and SQLite.

# Given Requirements:
- [x] The application should store and retrieve data from a real database

- [x] When the application starts, it should create a sqlite database, if one isn’t present.

- [x] It should also create a table in the database, where the coding session will be logged.

- [x] The users should be able to insert, delete, update and view their coding session.

- [x] You should handle all possible errors so that the application never crashes.

- [x] To show the data on the console, you should use the "Spectre.Console" library.

- [x] You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)

- [x] You should tell the user the specific format you want the date and time to be logged and not allow any other format.

- [x] You'll need to create a configuration file that you'll contain your database path and connection strings.

- [x] You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration

- [x] The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.

- [x] The user should be able to input the start and end times manually.

- [x] You need to use Dapper ORM for the data access.

- [x] When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.

- [x] Follow the DRY Principle, and avoid code repetition.

# Features
- SQLite database connection
    - The program uses a SQLite db connection to store and read information.
    - If no database exists, or the correct table does not exist they will be created on program start.

- A console based UI where users can navigate via key presses.

- CRUD Database Functions
    - From the main menu users can Create, Read, Update or Delete entries for whichever date they want, entered in yyyy-MM-dd HH:mm format.
    - Time and Dates inputted are checked to make sure they are in the correct and realistic format.

- Data output utilizes the Spectre.Console library to enhance the visual appeal of the console, allowing for structured tables, colored text and more.

# Lessons Learned
- This is my first application using the OOP paradigm, so I had to become familiar with using multiple classes, getting and setting properties, writing methods with overloads, and utilizing the appropriate access modifiers on classes and members.

- I had to become familiar reading official documentation (particularly for Dapper and Spectre.Console) to understand how to translate newly-learned concepts to solutions in my code.

- This application made heavy use of DateTime and TimeSpan types, so I had to learn how to validate the input for these types and use their built in methods and properties.

- I learned that in Visual Studio, I can highlight a selection and press F12 (or Alt+F12 to stay in the same window) to view it's "definition" a.k.a source code. This was helpful as a quick reference to see the implementation, overloads, etc. of a method.

- I became more comfortable using the debugger to identify the source of errors and application logic issues.

# Areas to Improve
- Although I gave my application some structure by separating functionality into different classes, I would like to explore other features of OOP that can lend even more structure to my application.

- I could have offloaded some functionality in many of my methods into smaller, more reusable methods to better adhere to DRY and the "single responsibility" rule.

- I should probably use more descriptive comments throughout my code to give new users an overview of the application at first glance and help explain complicated functionality in plain English.

# Resources Used
- [Dapper Tutorial](learndapper.com)

- [Official Spectre.Console documentation](spectreconsole.net)

- [OOP guide from C# Academy](https://www.thecsharpacademy.com/course/1/article/0/500025/false)

- LLMs such as ChatGPT for help in explaining new concepts (particularly OOP) and troubleshooting compiler errors.

# NOTES
To run the application for the first time, you will need to include a config.json with the following contents in the bin\Debug\net8.0 folder of your project.
`
{
  "Database": {
    "Path": "coding_tracker.db",
    "ConnectionString": "Data Source=coding_tracker.db;Version=3;"
  }
}
`

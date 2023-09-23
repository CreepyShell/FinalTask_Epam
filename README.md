# FinalTask_Epam
Internet Forum Project

The Internet Forum project is a robust web application designed for creating and managing posts, comments, and user interactions within an online community. 
It offers a feature-rich forum experience with the following functionalities:
-  User-Friendly Interface: The forum provides an intuitive user interface for creating, viewing, and interacting with posts and comments.
-  Like and Dislike: Users can express their opinions by liking and disliking posts and comments.
-  Sorting Options: Posts can be sorted by date, popularity, and most liked, enhancing the user experience.
-  Authentication and Authorization: The project implements a secure authentication and authorization system using JWT tokens. Users can register and log in to access their profiles and participate in discussions.
-  User Roles: Two distinct user roles exist—admins and users. Admins have the authority to delete posts and comments, while users can delete their own contributions.
-  Backend: The backend is developed using C# with ASP.NET Core Web API, utilizing Entity Framework Core for efficient database management. Two separate databases are employed—one for application data and the other for security using the Identity library for roles and user data.
-  Unit of Work Pattern: The project employs the unit of work pattern to manage interactions between the two databases effectively.
-  Libraries: Several external libraries are integrated, including AutoMapper for data mapping, Fluent Validation for input validation, and Serilog for logging purposes.
-  Architecture: The backend follows a three-layer architecture, ensuring modularity and maintainability. Extensive unit testing of business logic is performed using Xunit.
-  Frontend: The frontend is developed using Angular and leverages RxJS for reactive programming. The user interface is designed with a focus on responsiveness and usability.

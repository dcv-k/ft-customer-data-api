# Customer API

This repository contains the code for a client application and a server API. The client application is built using npm, and the server API is built using .NET.  

## Motivation  
Welcome to our Customer API .NET application. I understand the importance of secure and seamless user authentication, which is why this application features a robust authentication system that prioritizes user data protection. Beyond that, we've streamlined the management of customer information with easy-to-use CRUD operations, allowing you to effortlessly create, read, update, and delete customer records. I believe that technology should work for you, and our application embodies that belief with user authentication and customer CRUD capabilities that exceed expectations. 

## Features  
### User Authentication System
- **Secure User Registration:** Register new users with encrypted password storage for enhanced security.
- **User Login and Logout:** Allow users to log in securely and log out when their session is complete.
- **Role-Based Access Control:** Implement role-based access control to manage user permissions and access levels.  

### Customer Management (CRUD)
- **Create Customers:** Easily add new customers to the system with details such as name, contact information, and more.
- **Read Customer Data:** Access and view customer information, including a searchable and sortable list of customers.
- **Update Customer Information:** Edit and update customer records, ensuring the latest data is maintained.
- **Delete Customers:** Remove customer records securely from the database.  
- **Validation and Error Handling:** Include validation and error handling to ensure data accuracy.

### API Versioning
- **Versioned Endpoints:** Implement API versioning to maintain backward compatibility while introducing new features and improvements.
- **API Documentation:** Provide clear and updated API documentation for each version to guide developers and consumers.  

### User-Friendly Interface
- **Intuitive User Experience:** Offer a user-friendly interface for both user authentication and customer management.
- **Responsive Design:** Ensure the application is responsive and accessible across various devices and screen sizes.
- **Customization Options:** Allow users to personalize their profiles and settings.

## Installation & Usage  
### Starting the Client Application

To run the client application on your local machine, follow these steps:

- Clone this repository to your local machine.
- Open a terminal or command prompt.
- Navigate to the client directory using the following command:  
   `cd client`
- Install the required dependencies using npm:  
   `npm install`
- Start the client application using the following command:  
   `npm run start`

### Starting the Server API

To run the server API on your local machine, follow these steps:

- Download and install Visual Studio from the official website.  
- Launch Visual Studio from your applications or Start menu.  
- Go to `File` > `Open` > `Project/Solution`.  
- Navigate to the directory where your .NET project is located and select the project file (usually with a `.csproj` extension).  
- Right-click on the project you want to run in the Solution Explorer and select "Set as Startup Project."  
- Press `Ctrl + Shift + B` or go to `Build` > `Build Solution` to compile the project.  
- Press `F5` or go to `Debug` > `Start Debugging` to run the project. If it's a web application, a browser window should open, and your application will start.  

## Testing the Application

Use the following credentials to test the application:

- Username: admin
- Password: test123

Please make sure you have the necessary software installed before running the application:

- Node.js and npm for the client application
- .NET SDK for the server API

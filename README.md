# ft-customer-data-api

This repository contains the code for a client application and a server API. The client application is built using npm, and the server API is built using .NET.

## Running the Client Application

To run the client application on your local machine, follow these steps:

1. Clone this repository to your local machine.
2. Open a terminal or command prompt.
3. Navigate to the client directory using the following command:  
   `cd client`
4. Install the required dependencies using npm:  
   `npm install`
5. Start the client application using the following command:  
   `npm run start`

## Running the Server API

To run the server API on your local machine, follow these steps:

1. Clone this repository to your local machine (if you haven't already).
2. Open a terminal or command prompt.
3. Navigate to the API directory using the following command:  
   `cd api`
4. Create SQL Lite database with following command:  
   `dotnet ef database update`
5. To Seed the database execute following command:  
   `dotnet run seeddata`
6. Run the server API using the following command:
   `dotnet run`

## Testing the Application

Use the following credentials to test the application:

- Username: admin
- Password: test123

Please make sure you have the necessary software installed before running the application:

- Node.js and npm for the client application
- .NET SDK for the server API

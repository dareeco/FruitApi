# FruitApi

## MVC Application Documentation: Fruit Management System

This MVC application provides full CRUD functionalities by integrating with the Fruityvice API: https://www.fruityvice.com/api/fruit/. It uses PostgreSQL as the database and includes a Swagger API for a developer-friendly interface.

### Key Features:

Primary Method: The core functionality is the POST method: https://www.fruityvice.com/api/fruit/{fruitName}. This method allows users to search for a specific fruit by name. If the fruit is not present in the database, the application fetches the information from the Fruityvice API and stores it in the database. If the fruit is already in the database, it retrieves and displays the information directly from the database.

### Additional Methods:

  - GET: Retrieve a specific fruit by its name.
  - DELETE: Remove a fruit from the database.
  - PUT: Update the details of a fruit.
  - GET: List all fruits saved in the database.

### Setup Instructions:
To run and use the application:

  - Connect to a PostgreSQL database.
  - Provide your database credentials in the appsettings.json file.
    
For testing HTTP methods and functionalities, access the Swagger interface at: http://localhost:5223/swagger/index.html.


# TodoList

TodoList Api using EntityFramework, Identity, Swagger

To run or deploy you need to change the default connection string in the api, but first change it in the library and run the command 
```cmd
update-database
```
 using the packer manager console pointing to the Todo.Domain project.

After that you can create an Identity account using the Account/Register endpoint.
With the user and password created, use the Account/Login endpoint to get a token, and with that token you can use the TodoController.

The project has a custom and simple security handler, that is basically a token with expiration saved in the db.
The project has Swagger with the Autentication integrated when is required.

IntegrationTest added, to run integration tests you need to overwrite email and password in 'IntegrationTest' section in appsettings.json file in Todo.Api project.

```json
  "IntegrationTest": {
    "email": "test@123.com",
    "password": "Password!1"
  }
```

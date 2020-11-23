# fSight Incident Management

fSight is a simple incident/ticket management platform with functionality, similar to ServiceNow or Jira. Created purely for learning purposes, however if you find this useful - even better!

Currently, I am working on the backend functionality, however I am planning to create a UI using Angular in the near future.

At this moment the list of features are (all endpoints require users to be authorized, except **account**):

### /api/v1/tickets:
* **GET** a single or a list of tickets.
* **POST** a new ticket.
* **PATCH** a ticket, e.g. Assign a developer or add a comment.
* **OPTIONS** and **HEAD** are also supported as per RESTful guidelines.

### /api/v1/account
* **POST** to **/register** requires a following JSON format (if validation fails, **Status 422** will be returned):
```
{
    "firstName": string,
    "lastName": string,
    "email": string,
    "password": string
}
```

* **POST** to **/login** requires an ```email``` and ```password``` (5 consecutive failed logins will result in 5 minute lockout):
```
{
    "email": string,
    "password": string
}
```

## What's next?

- :white_check_mark: Authentication and Authorization with JWT
- :white_check_mark: Tickets Controller
- Projects Controller
- Angular Client

## Installation

Download [.NET 5 SDK](https://dotnet.microsoft.com/download) and install it, then clone the repository and in root folder run:

```cs
dotnet restore
```

## Usage

fSight requires some configuration before using it. I am utilizing ```dotnet user-secrets``` to store my SQL Connection String as well as JWT-related info.  

For Linux/macOS users, secrets will be located under
```~/.microsoft/usersecrets/<user_secrets_id>/secrets.json```

For Windows users: ```%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json```

For ```<user_secrets_id>``` you can always check **FSight.API.csproj** since it is here that initial API config takes place: 
```
<UserSecretsId>adac732e-5f75-4b52-82d9-19d5a5d8b4ae</UserSecretsId>
```

For example, if you are on a mac, You can ```cd``` into ```~/.microsoft/usersecrets/```, create a new folder ```adac732e-5f75-4b52-82d9-19d5a5d8b4ae```, ```ls``` inside and ```touch secrets.json```. ```secrets.json``` should contain the following:
```
{
    "ConnectionStrings": {
        "SqlDb": "your_sql_server_connection_string"
    },
    "Token":
    {
        "Key": "your_super_secret_key_to_encode_jwt",
        // in development environment, it should be your dev host, e.g. "https://localhost:5001"
        "Issuer": "https://localhost:5001"
    }
}
```
Once the above is done, you can now run ```dotnet build``` to build the project and ```dotnet run``` to run it.

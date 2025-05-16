# Contacts app

Contacts app is a simple api example using vertical slice architecture.

## Installation

```bash
git clone https://github.com/cassiano-swe/contacts-app.git
```

## Usage

### Running locally

```bash
cd <root folder>
dotnet restore
dotnet run
google-chrome http://localhost:5147/swagger/index.html 
```

### Running from docker image

```bash
docker build -t contactsapp .
docker run -p 8000:8080 --name my_api contactsapp
google-chrome http://localhost:8000/swagger/index.html

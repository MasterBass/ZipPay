# User API Dev Guide
* git clone https://git-rba.hackerrank.com/git/d6eb40ea-2540-4bf9-993a-2a5884aed729
* cd d6eb40ea-2540-4bf9-993a-2a5884aed729

## Building
* dotnet build

## Testing
* dotnet test

## Deploying
* docker-compose build
* docker-compose up

## Additional Information
* GET http://localhost:5000/api/user
* GET http://localhost:5000/api/user/1
* POST http://localhost:5000/api/user

```json
{
   "email": "peter@test.com",
   "monthlySalary": 3000,
   "monthlyExpenses": 800 
}
```

* GET  http://localhost:5000/api/account
* POST  http://localhost:5000/api/account
```json
{
    "id": "1",
    "iban": "902309832092430923434534"
}
```
> create account for user with id = 1
# OtpGenerator Assesment
  This application is used to generate the Otp and validate the Otp.

## Setup and Run
1. Clone repository from given source control url(GitHub).
2. After cloning build your application.
3. Update the migrations with database, open package manager console and type `update-database` to create database and tables.
4. Run the application to open in swagger.
5. 2 endpoints crated.
    ### Generate
    1. Use proper email adderss with domain of 'dso.org.sg' to generated Otp.
        Example: {
                    "email":"shashiguru.keluth@dso.org.sg"
                 }
    ### Validate
    1. Use same email address with otp to validate.
        Example: {
                    "email": "shashiguru.keluth@dso.org.sg",
                    "otp": 123456
                 }

## Create an instance of MSSQL
1. Open command prompt and type `sqllocaldb create localserver` to create a server.
2. Connect MSSQL with window authentication and use server name as `(localdb)\localserver`.

## Run unit test:
1. Build your application and run all the test cases


# OTP Generator And Validator Assesment
  This application is used to generate the Otp and validate the Otp.

  ## Setup and Run
    1. Clone repository from given source control url(GitHub).
    2. After cloning, build your application.

  ## Creation of Migrations
    a. Open Visual Studio anf go to tools.
    b. Go to Nuget Package Manager and click on Package Manager Console.
    c. Create migrations by typing command of `add-migration -context OtpGenerateDbContext`.
    d. Give name to create migration file.
    e. Update the migrations with database by typing command of `update-database` to create database and tables.

  ## Run your application
    a. Upon running the application, it will open in swagger.
    b. two endpoints created.

  ### Generate
      1. Endpoint to validate the given email and create OTP with Email service.
      2. From emails should be ends with domain of `dso.org.sg`.
      
  #### Example: 
               {
                  "email":"shashiguru.keluth@dso.org.sg"
               }
  #### Request Url:
            `https://localhost:7234/api/EmailOTP/generate`
  #### Request Body:
            {
              "email": "shashiguru.keluth@dso.org.sg"
            }
  #### Response Body:
            {
              "email": "shashiguru.keluth@dso.org.sg",
              "otp": 602485
            }

  ### Validate
      1. Use same email address and generated OTP to validate.
  #### Example: 
               {
                  "email": "shashiguru.keluth@dso.org.sg",
                  "otp": 123456
               }
  #### Request Url:
            `https://localhost:7234/api/EmailOTP/validate`
  #### Request Body:
            {
              "email": "shashiguru.keluth@dso.org.sg",
              "otp": 602485
            }
  #### Response Body:
            `Email validated successfully.`

## Create an instance of MSSQL
1. Open command prompt and type `sqllocaldb create localserver` to create a server.
2. Connect MSSQL with window authentication and use server name as `(localdb)\localserver`.

## Run unit test:
1. Build your application and run all the test cases


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

  ## Run and Test the application
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

## Assumptions:
  ### Generate:
    1. Entered email id should be valid email address.
    2. Email address should be ends with domain name of `dso.org.sg`.
    3. Email Id will be validated against the database, if email present in db then checks the generated date and time 
      a. if generated datetime is more than 1 minute then it will ask user to try after 1 minute.
      b. If generated datetime is less than 1 minute then it will generate the new OTP.
    4. If Email Id not present in db then it will create a record under db with OTP and return OTP to user.

  ### Validate:
    1. Entered email address should be present in database.
    2. If present in database then it will checks
      a. If generated datetime is less than 1 minute then it will check the entered Otp, 
          if otp is correct then it will return as 
            `Successfully validated`
          If otp is not correct then it will increase tryCount to 1 and return as
            `Otp invalid`.
      b. If generated datetime is more than 1 minute then it will return as
            `Timeout for entered Otp`
    3. If not present in database then it will return error as
            `Email not found`
  
## Test Results:
  ### Generate:
  1. When user enters the invalid email address.
     ![image](https://user-images.githubusercontent.com/30490543/232784786-7c076cd4-7fe3-47c0-9d6a-6ee26cc5f624.png)
  
  2. When user enters the valid email address.
     ![image](https://user-images.githubusercontent.com/30490543/232784558-c2877a86-7746-4260-88c5-3d9e62a024d8.png)
     
  3. When user tries to generate the OTP with in minute
     ![image](https://user-images.githubusercontent.com/30490543/232785121-7e7df67f-e040-47a0-b1d9-18034963c56b.png)


## Run unit test:
1. Build your application and run all the test cases


ADP Labs Pre-Interview Assignment

1. Introduction

This test is part of ADP's recruitment process.

The main goal of this test is to evaluate the candidate skills thru the below criterias

• A reviewer should be able to clone this repository (e.g. from Github, Bitbucket).
• A reviewer should be able to open, build and debug solution with no issues.
• A reviewer should be able to see that calls are successful or not.
• A reviewer should be able to test the application with a tool (Postman, Swagger, SoapUI)
• Use of industry best practices
• Functionality, maintainability and scalability

The test consists in to get a task from a API service that refers to a math operation, calculate the result for the given operation and submit back the result.

So the User Story for this test can be written as follow.

As a reviewer for the ADP Labs Pre-Interview Assignment, I would be able to perform these math operations: 
addition, subtraction, multiplication, division and remainder, using a task definition provided by an API Service. 

FEATURE: Math Calculation

GIVEN: 
        The following Endpoint https://interview.adpeai.com/api/v1/get-task
WHEN:
        I send a GET request to the endpoint to get the task
THEN: 
        A JSON object with the following information for a task must be received 
        Id -> a GUID string that identifies the task,
        Operation -> a string that determines the math operation to be executed
        Left -> a integer number that represents the left member of the math operation 
        Right -> a integer number that represents the right member of the math operation


GIVEN:
       The math operation to be executed 
THEN: 
       Collect the result
AND:
      Send a POST request to the endpoint https://interview.adpeai.com/api/v1/get-task  
      with the following info in JSON Format as the body for the request
      Id -> a GUID string that identifies the task,
      Result -> the given result off the math operation.
AND:
      I should see the response message from the ADP API Service.
      
RULE: 
        Division by Zero
GIVEN:
        The operation is equal to division 
AND:    
        The left number is equal to 0 (zero)
THEN:
        Raise an exception with the message "Left value cannot 

2. Solution

The solution was built using Visual Studio 2019 and C# as the programming language, and enabled SWAGGER OPEN API to expose the endpoint.

For unit tests was used NUnit.

Dependecy Injection was used to let services available for the entire solution and reduce the code and services instances management.

SOLID principles was applied to have a clean a readable code.

3. Running the solution

Open the solution in a Visual Studio 2019 or above, press CTRL + F5.

After the application had been started, the SWAGGER Open API should be visible with the following endpoint listed. 

GET /ADPTes/executeTasksFromApi 

Click on it, after in the Try Out Button at right, ad finally in Execute Button.

After this, a message will be returned by the ADP API Service if the task was submited with correct answer or not.


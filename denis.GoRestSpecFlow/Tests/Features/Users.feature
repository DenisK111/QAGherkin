Feature: Users

@Authenticate
Scenario Outline: Create a new user
Given I have generated <uservalidity> user data
When I send a request to the users endpoint
Then the response status code should be <statusCode>
	And the response content should be valid

Examples: 
    | statusCode          |  uservalidity |
    | Created             |  valid        |
    | UnprocessableEntity |  invalid      |

Scenario: Get All Users
Given I want to get all users from the endpoint
When I send a Get request to the users endpoint
Then The response should be OK
    And the content should not be empty

@Authenticate
Scenario: Update a user
Given I have a created user
When I send a PUT request to the users endpoint
Then The response should be OK
    And the user should be updated successfully
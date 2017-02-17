Feature: User Registration

Scenario: Register a User
	Given I am on the Log In page
	And I have entered 'Mikalai' into First Name
	And I have entered 'Kardash' into Last Name
	And I have entered 'mikalai.kardash@gmail.com' into Email
	And I have entered 'testtest' into Password
	When I press Submit
	Then I must be redirected to Property Access Request page

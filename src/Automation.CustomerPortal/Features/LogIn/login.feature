Feature: Log In

Scenario: Registered Users Able to Log In
	Given I am on the Log In page
	And I have entered correct credentials
	When I press Login button
	Then I should be logged in

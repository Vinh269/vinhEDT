Feature: Login
	Login feature for testing
	
	Background: 
	I logged into the EDT


@SmokeTest
Scenario: Login with correct Username and Password
	Given I navigate to the login page
	And I enter username in the Username
	And I enter password in the Password
	When I click on the Login button
	Then I can see Homepage is displayed
	Then I can see Homepage is displayed
Feature: View Forum Index
	In order to see what forums exist
	As a logged in user
	I want to see an overview over all forums

Scenario: View Forum Index
	Given I am logged in
	And I navigate to "/Forums"
	Then I see the Forum Index containing 3 forums

Scenario: View Forum Index without permission
	Given I am not logged in
	And I navigate to "/Forums"
	Then I am redirected to "/Account/Logon.*?"
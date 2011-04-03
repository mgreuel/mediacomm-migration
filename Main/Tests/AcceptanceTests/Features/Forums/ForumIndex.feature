Feature: Addition
	In order to see what forums exist
	As a logged in user
	I want to see an overview over all forums

@mytag
Scenario: View Forum Index
	Given I am logged in
	And I navigate to "/Forums"
	Then I see the Forum Index containing 3 forums

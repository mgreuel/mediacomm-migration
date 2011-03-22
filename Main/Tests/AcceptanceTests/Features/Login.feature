Feature: Login
	In order to browse protected sites
	As member of the site
	I want to be able to log into the page

Scenario: Login
	Given I navigate to "/Account/Logon" 
	And I have entered "testuser" as username and "secret" as password
	When I press "loginButton"
	Then I am redirected to "/"

Scenario: NoUserNameEntered
	Given I navigate to "/Account/Logon" 
	And I have entered "" as username and "secret" as password
	When I press "loginButton"
	Then I see an error message telling me "The Username field is required"

Scenario: NoPasswordEntered
	Given I navigate to "/Account/Logon" 
	And I have entered "testuser" as username and "" as password
	When I press "loginButton"
	Then I see an error message telling me "The Password field is required"

Scenario: ShortUserNameEntered
	Given I navigate to "/Account/Logon" 
	And I have entered "ab" as username and "secret" as password
	When I press "loginButton"
	Then I see an error message telling me "The field Username must be a string with a minimum length of 3 and a maximum length of 20"

Scenario: ShortPasswordEntered
	Given I navigate to "/Account/Logon" 
	And I have entered "myusername" as username and "1234" as password
	When I press "loginButton"
	Then I see an error message telling me "The field Password must be a string with a minimum length of 5 and a maximum length of 20"

Scenario: LongUserNameEntered
	Given I navigate to "/Account/Logon" 
	And I have entered "ThisIsAveryLongUser.." as username and "secret" as password
	When I press "loginButton"
	Then I see an error message telling me "The field Username must be a string with a minimum length of 3 and a maximum length of 20"

Scenario: LongPasswordEntered
	Given I navigate to "/Account/Logon" 
	And I have entered "myusername" as username and "ThisIsAreallyLongPass" as password
	When I press "loginButton"
	Then I see an error message telling me "The field Password must be a string with a minimum length of 5 and a maximum length of 20"

Scenario: UsernameAndPasswordDoNotMatch
	Given I navigate to "/Account/Logon" 
	And I have entered "testuser" as username and "wrongPwd" as password
	When I press "loginButton"
	Then I see an error message telling me "Wrong username or password"


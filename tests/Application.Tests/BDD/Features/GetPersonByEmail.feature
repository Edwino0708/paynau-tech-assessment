Feature: GetPersonByEmail

  Scenario: Fetch person by email successfully
    Given a repository with a person with email "john.doe@example.com"
    When I request a person by email "john.doe@example.com"
    Then I should receive the person information by email

  Scenario: Fail to fetch person by email
    Given a repository without a person with email "john.doe@example.com"
    When I request a person by email "john.doe@example.com"
    Then I should receive null person information by email

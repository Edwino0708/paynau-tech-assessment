Feature: GetPersonById

  Scenario: Fetch person by id successfully
    Given a repository with a person with id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    When I request a person by id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    Then I should receive the person information by id

  Scenario: Fail to fetch person by id
    Given a repository without a person with id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    When I request a person by id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    Then I should receive null person information by id

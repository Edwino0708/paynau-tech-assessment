Feature: Update existing person

  Scenario: Update person successfully
    Given a repository with a person with update id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    When I update the person with id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    Then I should receive a successful update result

  Scenario: Fail to update non-existent person
    Given a repository without a person with update id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    When I update the person with id "d290f1ee-6c54-4b01-90e6-d701748f0851"
    Then I should receive a person not found exception

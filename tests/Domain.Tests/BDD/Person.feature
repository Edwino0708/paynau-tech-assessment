Feature: Person Creation
    Scenario: Create a valid person
        Given I have a valid person data
        When I create a person
        Then the person should be created with the correct details

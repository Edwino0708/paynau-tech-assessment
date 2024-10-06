Feature: GetPersons

  Scenario: Fetch persons from cache
    Given a cache with the persons data for page 1 and page size 10
    When I request persons for page 1 and page size 10
    Then I should receive persons from the cache

  Scenario: Fetch persons from repository
    Given an empty cache
    And a repository with 10 persons for page 1 and page size 10
    When I request persons for page 1 and page size 10
    Then I should receive persons from the repository

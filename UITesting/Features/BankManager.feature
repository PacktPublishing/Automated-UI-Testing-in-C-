Feature: Bank Manager
  Scenario: Add new customer
    Given the banking application has been started
    And I am on the "Banking Home" page
    When I click on the "Banking Manager Login" button
    Then I should see the "Banking Manager Home" page
    When I go to the "Customers" page
    Then I should see the "Customers" page
    And the "Customer List" table is not empty
    When I note the "Customer List" table row count as "Initial Count"
    And go to the "Add Customer" page
    Then I should see the "Add Customer" page
    And the following fields are shown:
      | Field      |
      | First Name |
      | Last Name  |
      | Post Code  |
    And the "Submit" field is available
    When I populate current page with the following data:
      | Field      | Value |
      | First Name | Test  |
      | Last Name  | User  |
      | Post Code  | WWW99 |
    Then I should see the page contains the following data:
      | Field      | Value |
      | First Name | Test  |
      | Last Name  | User  |
      | Post Code  | WWW99 |
    When I click on the "Submit" button
    And accept the alert message
    Then I should see the "Add Customer" page
    When I go to the "Customers" page
    Then I should see the "Customers" page
    And the following labels are shown:
      | Label |
      | Test  |
      | User  |
      | WWW99 |
    And the "Customer List" table has "Initial Count + 1" rows
    And the "Customer List" table has "100 * (Initial Count + 1) / 100" rows
  	And the last row of the "Customer List" table contains the following data:
      | First Name | Last Name | Post Code |
      | Test       | User      | WWW99     |
    When I click on the last "Delete Customer" element of the "Customer List" table
    Then I should see the "Customer List" table has "Initial Count" rows

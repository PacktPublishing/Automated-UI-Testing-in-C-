Feature: Bank user
  Scenario: Deposit money to account
    Given the banking application has been started
    And I am logged as the "Harry Potter" customer
    And I am on the "Customer Home" page
    When I note the "Balance" field text as "Initial Balance"
    And go to the "Deposit" page
    And enter "100" text into the "Deposit Amount" field
    And click on the "Submit Deposit" button
    Then I should see the "Deposit Successful" text is shown
    And the "Balance" field value is calculated using the following formula:
		"""
		Initial Balance + 100
		"""

Feature: TestDevLab Automation
  As a user, I want to automate the booking process on the TestDevLab website.

Scenario: Book a meeting on TestDevLab
  Given I am on the TestDevLab test automation services page
  When I click the Book a meeting button
  And I switch to the new booking tab
  And I select the date "Tuesday, April 29 - Times" and time
  And I click Next on the booking form
  And I fill in the booking form with valid details
    | Name   | Email            | Company        | Description         |
    | Andrew | andrew@gmail.com | Test Assignment| Test tes meeting    |
  And I click Schedule Event
  Then I should see a confirmation of the scheduled meeting
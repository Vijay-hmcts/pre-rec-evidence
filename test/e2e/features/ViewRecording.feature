Feature: View Recording
 In order to view a recording
 As a judge I want to view recordings before trials

  @View
  Scenario: View Recording
  Given I search for a case reference
  Then the recordings for that case reference will show

  @View
  Scenario: View Recording without timestamp
  Given  I turn off the timestamp
  Then the video will no longer show a timestamp
  
  @View
  Scenario: View Recording with a timestamp
  Given  I turn on the timestamp
  Then the video will show a timestamp with hours, mins and seconds

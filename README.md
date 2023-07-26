# WpfExercise_TeamComposition
WPF application that helps user setup team fulfilling member preferences on team-mates

# Task description
Implement a WPF application that fulfills following requirements.
## Functional requirements
F01 - Application shall read a configuration file during startup and store its content in memory. 
Note: The location of file is fixed, it is attached in repository in root folder. It contains list of team-members together with list of attributes. Those attributes represent names of other team-members that are not compatible to be with in a team.  
Note 2: It is up to developer how to design structures/classes that will contain the data from configuration file.

F02 - Application provides 5 combo-boxes that list the available team members. 

F03 - Combo-boxes always show only team members that are compatible with already selected team members.

F04 - If user is able to have complete 5 team members selected, the aplication shall show "Success" message. If the team is not possible to complete due to incompatibility with already selected team members, the application shall show "Fail" message.

F05 - Application shall provide a reset button that resets combo-boxes to initial state.
Note: Initial state shall be empty value.

## Compatibility definition
Given team member A will not join a team that already contains some other team member that A is not compatible with. At the same moment compatibility is not a symmetric relationship. 
Example
Lets have team members A, B, C, D, E where:
  - A is not compatible with B
  - B is not compatible with C
Then the following compositions are valid (the most left team member was selected first, the most right was the last):
A, C, D, E -> FAIL
B, A, D, E -> FAIL
C, B, A, E, D -> SUCCESS

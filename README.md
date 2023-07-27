# WpfExercise_TeamComposition
Excercise: Implement WPF application that helps user setup team fulfilling member preferences on team-mates.

# Task description
Implement a WPF application that fulfills following requirements.
## Functional requirements
**F01** - Application shall read a configuration file during startup and store its content in memory. 

*Note*: The location of file is fixed, it is attached in repository in root folder. It contains list of team-members together with list of their attributes. Those attributes represent names of other team-members that are **hated** by the given team-member.  

*Note 2*: It is up to developer how to design structures/classes that will contain the data from configuration file.

**F02** - Application provides 5 combo-boxes that list the available team-members. 

**F03** - Combo-boxes always show only team-members that are possible to select with already selected team-members.

**F04** - If user is able to have 5 team-members selected (i.e. complete team), the application shall show "Success" message. If the team is not possible to complete due to incompatibility with already selected team members, the application shall show "Fail" message.

**F05** - Application shall provide a reset button that resets combo-boxes to initial state.

*Note*: Initial state shall be empty value.

## Hate definition
Given team member A will not join a team that already contains some other team member that A **hates**. At the same moment **hate** is not a symmetric relationship. 

#### Example

Lets have team members A, B, C, D, E where:
  - A hates B
  - B hates C

Then the following compositions are valid (the most left team member was selected first, the most right was the last):
- A, B, C, D, E -> SUCCESS
- B, C, D, E -> FAIL
- C, A, E, D -> FAIL


## Hint

Consider using MVVM architecture pattern.

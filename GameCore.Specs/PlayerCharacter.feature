Feature: PlayerCharacter
	In order to play the game
	As a human player
	I want to be told the sum of two numbers

Background: 
	Given I'm a new player

Scenario Outline: Health reduction
	When I take <damage> damage
	Then My health should now be <expectedHealth>

	Examples: 
	| damage | expectedHealth |
	| 0      | 100            |
	| 100    | 0              |
	| 40     | 60             |

Scenario: Elf race et additional 20 damage resistance
	And I have a damage resistance of 10
	And I'm an Elf
	When I take 40 damage
	Then My health should now be 90

Scenario: Elf race et additional 20 damage resistance using data table
	And I have following attributes
		| attribute  | value |
		| Race       | Elf   |
		| Resistance | 10    |
	When I take 40 damage
	Then My health should now be 90

Scenario: Healers restore all health
	Given My Character class is set to Healer
	When I take 40 damage
	And Cast a healing spell
	Then My health should now be 100
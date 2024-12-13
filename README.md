# Need improvement:
1. Optimize or write a new algorithm to spawn the sudoku field.
Currently, it's not stable and could take a bit of time or even throw an error

# Content:
- MainMenuScene:
  - New Game
  - Difficulty
  - Quit
 - GameScene
   - Field
   - Control panel
   - Information button
   - Finish

Main Menu:
- New game -> Start a new game with chosen difficulty (default - easy)
- Difficulty -> Choose the difficulty of the game (easy, middle hard). Difference - how many squares will be empty
- Quit -> Exit from the game

Game:
- Field -> contains the fields which generated for the sudoku game
- Control panel -> contains the numbers which the player could input into the field
- Information button -> toggle to change the input of numbers in the field. Change between Number and Small Number (note)
- Finish -> checks for the correctness of the field inputs. Mark red - if incorrect, green - if correct

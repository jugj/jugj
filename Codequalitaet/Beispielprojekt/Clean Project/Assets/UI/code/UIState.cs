namespace reibner.hangman {
    public class UIState {
        public string NumberOfGuesses;
        public string GuessedLetters;
        public string Word;

        public void Update(GameState gameState){
            this.NumberOfGuesses = gameState.NumberOfGuesses.ToString();
            this.GuessedLetters = gameState.GuessedLetters;
            this.Word = gameState.CurrentWordState.Replace("_", "_ ");
        } 
    }
}
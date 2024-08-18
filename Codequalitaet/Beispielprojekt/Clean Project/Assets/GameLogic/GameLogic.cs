using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reibner.hangman{
    public class GameLogic
    {
        private static readonly string[] WORDS = new string[]{"Tante", "begrenzt", "Herbst", "Pyschologie", "Ingenieur", "Vorstandsvorsizender", "Plastik", "fliegen", "meinen", "Umgebung"};
        private static readonly int MAX_GUESSES = 8;
        private static readonly string CASEINSENSITIVE_GUESS_MATCHER_FORMAT = "(?i){0}(?-i)";
        public GameState GameState;

        public GameLogic(){
            NewRound();
        }

        public GameState NextGuess(char guess){
            if(GameState.GuessedLetters.Contains(guess)){
                //Guessed letter was already guessed, no further evaluation necessary.
                return GameState;
            }

            GameState.GuessedLetters += guess;
            ApplyGuess(guess);

            return GameState;
        }

        private void ApplyGuess(char guess){
            if(GameState.CurrentWord.Contains(guess)){
                ApplyGuessToWord(guess);
            } else {
                GameState.NumberOfGuesses++;

                if(GameState.NumberOfGuesses == MAX_GUESSES){
                    NewRound();
                }
            }
        }

        private void NewRound(){
            this.GameState = new GameState(DrawRandomWord());
        }

        private void ApplyGuessToWord(char guess){
            var RawRegex = string.Format(CASEINSENSITIVE_GUESS_MATCHER_FORMAT, guess);
            var Match = Regex.Match(GameState.CurrentWord, RawRegex);
            var sb = new StringBuilder(GameState.CurrentWordState);
            var WordArray = GameState.CurrentWord.ToCharArray();
            while(Match.Success){
                foreach(Capture capture in Match.Captures){
                    sb[capture.Index] = WordArray[capture.Index];
                }
                Match = Match.NextMatch();
            }
            GameState.CurrentWordState = sb.ToString();
        }

        private string DrawRandomWord(){
            return WORDS[Random.Range(0, WORDS.Length-1)];
        }

/*
        private void OnEnable(){
            var word = words[UnityEngine.Random.Range(0, words.Length-1)];
            var underscores = "";
            foreach (var c in word.ToCharArray()){
                underscores += "_ ";   
            }
            rootVisualElement.Q<Label>("word").text = underscores;
            rootVisualElement.Q<Button>("submit").RegisterCallback((ClickEvent e) => {
                var userInput = rootVisualElement.Q<TextField>("userInput").text;
                guessedLetters += userInput;
                var match = Regex.Match(word, String.Format("(?i){0}(?-i)",userInput));
                if(guessedLetters.Contains(userInput.ToCharArray()[0]))
                if(match.Success){
                    StringBuilder sb = new StringBuilder(underscores);
                    while(match.Success){
                        foreach(Capture capture in match.Captures){
                            sb[capture.Index*2] = word.ToCharArray()[capture.Index];
                        }
                        match = match.NextMatch();
                    }
                    underscores = sb.ToString();
                    if(underscores.Replace(" ", "") == word){
                        guesses = 0;
                        guessedLetters = "";
                        word = words[UnityEngine.Random.Range(0, words.Length-1)];
                        underscores = "";
                        foreach (var c in word.ToCharArray())
                        {
                            underscores += "_ ";   
                        }
                    }
                    rootVisualElement.Q<Label>("word").text = underscores;
                } else {
                    guesses++;

                    if(guesses == 8){
                        guesses = 0;
                        guessedLetters = "";
                        word = words[UnityEngine.Random.Range(0, words.Length-1)];
                        var underscores = "";
                        foreach (var c in word.ToCharArray())
                        {
                            underscores += "_ ";   
                        }
                        rootVisualElement.Q<Label>("word").text = underscores;
                    }    
                }

                rootVisualElement.Q<Label>("word").text = underscores;
                rootVisualElement.Q<Label>("guesses").text = guesses.ToString();
                rootVisualElement.Q<Label>("guessedLetters").text = guessedLetters;
            });
        }
        */
    }

}
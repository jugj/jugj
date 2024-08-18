using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


namespace reibner.hangman{
    public record GameState {
        public int NumberOfGuesses = 0;
        public string GuessedLetters = "";
        public readonly string CurrentWord = "";
        public string CurrentWordState = "";

        public GameState(string word){
            this.CurrentWord = word;
            this.CurrentWordState = Regex.Replace(CurrentWord, ".", "_");
        }
    }
}

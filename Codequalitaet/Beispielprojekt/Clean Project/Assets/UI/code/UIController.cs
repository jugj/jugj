using UnityEngine;
using UnityEngine.UIElements;

namespace reibner.hangman.ui {
    public class UIController : MonoBehaviour{
        private static readonly string GUESSED_LETTERS_OUTPUT_ID = "guessedLetters";
        private static readonly string USER_INPUT_ID = "userInput";
        private static readonly string WORD_OUTPUT_ID = "word";
        private static readonly string SUBMIT_GUESS_BUTTON_ID = "submit";
        private static readonly string NUMBER_OF_GUESSES_OUTPUT_ID = "guesses";

        private VisualElement RootVisualElement;
        private UIModel Model;
        
        public void OnEnable()
        {
            Model = new(new GameLogic());
            RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            InitializeUI();
        }
        private void UpdateUI(UIState state){
            GetUIElement<Label>(GUESSED_LETTERS_OUTPUT_ID).text = state.GuessedLetters;
            GetUIElement<Label>(NUMBER_OF_GUESSES_OUTPUT_ID).text = state.NumberOfGuesses;
            GetUIElement<Label>(WORD_OUTPUT_ID).text = state.Word;
        }

        private void InitializeUI(){
            RootVisualElement.Q<Button>(SUBMIT_GUESS_BUTTON_ID).RegisterCallback((ClickEvent e) => OnSubmit());
            UpdateUI(Model.State);
        }

        private void OnSubmit(){
            Model.AddGuess(GetGuess());
            UpdateUI(Model.State);
        }

        private char GetGuess(){
            char guess = GetUIElement<TextField>(USER_INPUT_ID).text.ToCharArray()[0];
            return guess;
        }

        private T GetUIElement<T>  (string elementID) where T : VisualElement {
            return RootVisualElement.Q<T>(elementID);
        }
    }
}
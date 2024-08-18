using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class UiModel : MonoBehaviour
{
    private string guessedLetters = ""; 
    private int guesses = 0;

    private VisualElement rootVisualElement;

    private void OnEnable(){
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        var words = new String[]{"Tante", "begrenzt", "Herbst", "Pyschologie", "Ingenieur", "Vorstandsvorsizender", "Plastik", "fliegen", "meinen", "Umgebung"};
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
                    underscores = "";
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
}

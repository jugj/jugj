namespace reibner.hangman
{
    public class UIModel{
        public readonly UIState State = new UIState();
        private readonly GameLogic logic;

        public UIModel(GameLogic logic){
            this.logic = logic;
            State.Update(logic.GameState);
        }

        public void AddGuess(char guess){
            logic.NextGuess(guess);
            State.Update(logic.GameState);
        }
    }
}
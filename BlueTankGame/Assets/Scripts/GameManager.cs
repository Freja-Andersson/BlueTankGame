using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    public enum GameState
    {
        MainMenu,
        ChoseTank,
        Playing,
        Paused,
        GameOver
    }

    public GameState currentState = GameState.MainMenu;

    void Update()
    {
        CheckCurrentGamestate();
    }

    void CheckCurrentGamestate()
    {
        switch (currentState)
        {
            case GameState.MainMenu: // se till att båda spelarna trycka på knapparna
                Debug.Log("Main Menu");
                break;
            case GameState.ChoseTank: // när båda spelare har valt varsin tank så kan spelet börja
                Debug.Log("Chosing tank");
                break;
            case GameState.Playing: // inget kan röra på sig eller hända innan spelet har börjat
                Debug.Log("Playing");
                break;
            case GameState.Paused: // fica options menu
                Debug.Log("Game Paused");
                break;
            case GameState.GameOver: // fixa game over/win scene
                Debug.Log("Game over");
                break;
        }
    }

}

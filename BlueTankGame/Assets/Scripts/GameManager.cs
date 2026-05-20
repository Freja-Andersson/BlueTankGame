using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject startMenuPanel;
    [SerializeField] GameObject choseTankPanel;
    [SerializeField] GameObject gameOverOrWinPanel;

    [SerializeField] Button startButton;
    [SerializeField] Button bigTankButton;
    [SerializeField] Button smallTankButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button restartButton;

    [SerializeField] TextMeshProUGUI winText;


    AssignGamepad assignGamepad;

    public enum GameState
    {
        MainMenu,
        ChoseTank,
        Playing,
        GameOver,
        QuitGame
    }

    public GameState currentState = GameState.MainMenu;


    void Start()
    {
        startMenuPanel.SetActive(true);
        choseTankPanel.SetActive(false);
        gameOverOrWinPanel.SetActive(false);

        //Check if the buttons get pressed and then call the corresponing function
        startButton.onClick.AddListener(StartGame);
        bigTankButton.onClick.AddListener(HandleChosingTank);
        smallTankButton.onClick.AddListener(HandleChosingTank);
        restartButton.onClick.AddListener(ResetGame);
        quitButton.onClick.AddListener(QuitGame);
    }

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
                HandlePlaying();
                break;

            case GameState.GameOver: // fixa game over/win scene
                Debug.Log("Game over");
                HandleGameOver();
                break;

            case GameState.QuitGame: // fixa game over/win scene
                Debug.Log("Quitting game");
                break;
        }
    }
    void StartGame() // when startbutton is pressed, the game start and menu dissapears
    {
        currentState = GameState.ChoseTank;
        startMenuPanel.SetActive(false);
        choseTankPanel.SetActive(true);
    }

    void HandleChosingTank()
    {
        //Fixa så att controllen och att båda spelarna kan använda knapparna och välja spelare
        // kolla vilken kontroll som valde vilken tank och koppla det till tanken
        // ifall båda knapparna är nertryckta (spelarna har båda valt vilken tank), så börjar spelet
        currentState = GameState.Playing;
        choseTankPanel.SetActive(false);
    }

    void HandlePlaying()
    {
        // checks is a player has died, if so --> currentGameState = GameState.GameOver;
    }

    void HandleGameOver()
    {
        // Fix the text to show witch tank won
        gameOverOrWinPanel.SetActive(true);
    }

    void ResetGame() // when restartbutton is pressed, the game resets to the start menu again
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void QuitGame()
    {
        Application.Quit();
    }

}

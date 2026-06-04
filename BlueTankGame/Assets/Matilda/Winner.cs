using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    public static Winner Instance;
    [SerializeField] TextMeshProUGUI winnerText;
    public int whowon;
    

    private void Start()
    {
        if (Object.FindObjectsByType<Winner>(FindObjectsSortMode.None).Length > 0 && Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject textObject = GameObject.Find("winnerText");

        if (textObject == null)
        {
            Debug.LogError("winnerText GameObject not found in scene!");
            return;
        }

        TextMeshProUGUI winnerText = textObject.GetComponent<TextMeshProUGUI>();

        if (winnerText == null)
        {
            Debug.LogError("TextMeshProUGUI component missing!");
            return;
        }

        switch (whowon)
        {
            case 1:
                winnerText.text = "Red Tank Won!";
                break;

            case 2:
                winnerText.text = "Blue Tank Won!";
                break;

            default:
                winnerText.text = "";
                break;
        }
    }
}
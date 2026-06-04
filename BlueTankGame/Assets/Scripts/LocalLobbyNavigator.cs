using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LocalLobbyNavigator : MonoBehaviour
{
    [Header("Spelarens input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Denna spelares egna knappar")]
    [SerializeField] private Button bigTankButton;
    [SerializeField] private Button smallTankButton;

    [Header("Färger för val")]
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;

    private int currentSelection = 0;
    private bool canMove = true;

    private InputAction navigateAction;
    private InputAction submitAction;
    private bool isInitialized = false;

    // Vi använder OnEnable istället för Start för att aktivera kontrollerna 
    // exakt när panelen blir synlig på din Canvas.
    void OnEnable()
    {
        InitializeInput();
    }

    void InitializeInput()
    {
        if (playerInput == null) return;

        // Felsäkert sätt att hitta Unitys standard-actions oavsett Control Scheme
        navigateAction = playerInput.actions.FindAction("Navigate");
        submitAction = playerInput.actions.FindAction("Submit");

        // Om de inte hittades i UI-mappen, leta i hela asseten
        if (navigateAction == null) navigateAction = playerInput.actions.FindAction("UI/Navigate");
        if (submitAction == null) submitAction = playerInput.actions.FindAction("UI/Submit");

        if (navigateAction != null)
        {
            navigateAction.Enable();
            isInitialized = true;
        }
        if (submitAction != null)
        {
            submitAction.Enable();
        }

        UpdateVisuals();
    }

    void Update()
    {
        // Säkerhetsspärr: Gör inget om panelen är dold eller om input inte laddat än
        if (!gameObject.activeInHierarchy || !isInitialized || navigateAction == null) return;

        // Läs av styrspaken/piltangenterna
        Vector2 input = navigateAction.ReadValue<Vector2>();

        // Navigera nedåt
        if (input.y < -0.5f && canMove)
        {
            currentSelection = 1;
            canMove = false;
            UpdateVisuals();
        }
        // Navigera uppåt
        else if (input.y > 0.5f && canMove)
        {
            currentSelection = 0;
            canMove = false;
            UpdateVisuals();
        }
        // Återställ när spaken släpps
        else if (Mathf.Abs(input.y) < 0.2f)
        {
            canMove = true;
        }

        // Klicka på knappen
        if (submitAction != null && submitAction.WasPressedThisFrame())
        {
            ConfirmSelection();
        }
    }

    void UpdateVisuals()
    {
        if (bigTankButton != null) bigTankButton.image.color = (currentSelection == 0) ? selectedColor : normalColor;
        if (smallTankButton != null) smallTankButton.image.color = (currentSelection == 1) ? selectedColor : normalColor;
    }

    void ConfirmSelection()
    {
        if (currentSelection == 0 && bigTankButton != null)
        {
            bigTankButton.onClick.Invoke();
        }
        else if (currentSelection == 1 && smallTankButton != null)
        {
            smallTankButton.onClick.Invoke();
        }
    }
}
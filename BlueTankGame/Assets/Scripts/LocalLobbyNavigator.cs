using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LocalLobbyNavigator : MonoBehaviour
{
    [Header("Spelarens input")]
    [SerializeField] private PlayerInput playerInput; // Dra in BigTank här på Canvas1, och SmallTank på Canvas2

    [Header("Denna spelares egna knappar")]
    [SerializeField] private Button bigTankButton;
    [SerializeField] private Button smallTankButton;

    [Header("Färger för val")]
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;

    private int currentSelection = 0; // 0 = Big Tank, 1 = Small Tank
    private bool canMove = true;
    private InputAction navigateAction;
    private InputAction submitAction;

    void OnEnable()
    {
        if (playerInput == null) return;

        // Byt till UI-kartan så att menyknappar registreras istället för gameplay
        playerInput.SwitchCurrentControlScheme(playerInput.currentControlScheme, playerInput.devices.ToArray());

        // Hitta standard-actions från Unitys inbyggda paket
        navigateAction = playerInput.actions.FindAction("UI/Navigate");
        submitAction = playerInput.actions.FindAction("UI/Submit");
    }

    void Start()
    {
        UpdateVisuals();
    }

    void Update()
    {
        if (playerInput == null || navigateAction == null) return;

        // Läs av styrspaken/piltangenterna (från Unitys standard UI/Navigate)
        Vector2 input = navigateAction.ReadValue<Vector2>();

        // Navigera nedåt (Välj Small Tank)
        if (input.y < -0.5f && canMove)
        {
            currentSelection = 1;
            canMove = false;
            UpdateVisuals();
        }
        // Navigera uppåt (Välj Big Tank)
        else if (input.y > 0.5f && canMove)
        {
            currentSelection = 0;
            canMove = false;
            UpdateVisuals();
        }
        // Återställ så man kan bläddra igen när man släpper spaken/knappen
        else if (Mathf.Abs(input.y) < 0.2f)
        {
            canMove = true;
        }

        // Klicka på knappen (A-knappen på handkontroll, eller Enter/Space på tangentbord)
        if (submitAction != null && submitAction.WasPressedThisFrame())
        {
            ConfirmSelection();
        }
    }

    void UpdateVisuals()
    {
        // Ändra färg på knapparna så spelaren ser vad den har markerat
        if (bigTankButton != null) bigTankButton.image.color = (currentSelection == 0) ? selectedColor : normalColor;
        if (smallTankButton != null) smallTankButton.image.color = (currentSelection == 1) ? selectedColor : normalColor;
    }

    void ConfirmSelection()
    {
        // Kör det inbyggda onClick-eventet på den valda knappen
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

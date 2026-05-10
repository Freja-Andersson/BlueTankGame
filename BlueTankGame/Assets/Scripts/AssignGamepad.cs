using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class AssignGamepad : MonoBehaviour
{
    [SerializeField] PlayerInput player1;
    [SerializeField] PlayerInput player2;

    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        AssignDevices();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            AssignDevices();
        }
    }
    void AssignDevices()
    {
        if (player1 == null || player2 == null) { return; }
        if (player1 != null) player1.user.UnpairDevices();
        if (player2 != null) player2.user.UnpairDevices();

        var gamepads = Gamepad.all;
        var keyboard = Keyboard.current;


        if (gamepads.Count == 0)
        {
            InputUser.PerformPairingWithDevice(keyboard, player1.user);
            InputUser.PerformPairingWithDevice(keyboard, player2.user);
            RenamePlayers();
        }
        else if (gamepads.Count == 1)
        {
            InputUser.PerformPairingWithDevice(gamepads[0], player1.user);
            InputUser.PerformPairingWithDevice(keyboard, player2.user);
            RenamePlayers();
        }
        else if (gamepads.Count == 2)
        {
            InputUser.PerformPairingWithDevice(gamepads[0], player1.user);
            InputUser.PerformPairingWithDevice(gamepads[1], player2.user);
            RenamePlayers();
        }

        /*
        if (gamepads.Count > 0 && player1 != null)
        {
            var d0 = gamepads[0];
            InputUser.PerformPairingWithDevice(d0, player1.user);
            player1.user.AssociateActionsWithUser(player1.actions);
            player1.gameObject.name = "Player1";

            if (gamepads.Count == 1 && player2 != null) // connect player 2 to arrows
            {
                Debug.Log("Only one gamepad found");

            }

        }

        if (gamepads.Count > 1 && player2 != null)
        {
            var d1 = gamepads[1];
            InputUser.PerformPairingWithDevice(d1, player2.user);
            player2.user.AssociateActionsWithUser(player2.actions);
            player2.gameObject.name = "Player2";
        } */


        player1.user.AssociateActionsWithUser(player1.actions);
        player2.user.AssociateActionsWithUser(player2.actions);

    }

    void RenamePlayers()
    {
        player1.gameObject.name = "Player1";
        player2.gameObject.name = "Player2";
    }

}

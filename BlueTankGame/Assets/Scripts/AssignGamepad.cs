using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class AssignGamepad : MonoBehaviour
{
    [SerializeField] PlayerInput bigTank;
    [SerializeField] PlayerInput smallTank;

    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        AssignDevices();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            AssignDevices();
        }
    }
    void AssignDevices()
    {
        if (bigTank == null || smallTank == null) { return; }
        if (bigTank != null) bigTank.user.UnpairDevices();
        if (smallTank != null) smallTank.user.UnpairDevices();

        var gamepads = Gamepad.all;
        var keyboard = Keyboard.current;


        if (gamepads.Count == 0)
        {
            InputUser.PerformPairingWithDevice(keyboard, bigTank.user);
            InputUser.PerformPairingWithDevice(keyboard, smallTank.user);
            RenamePlayers();
        }
        else if (gamepads.Count == 1)
        {
            InputUser.PerformPairingWithDevice(gamepads[0], bigTank.user);
            InputUser.PerformPairingWithDevice(keyboard, smallTank.user);
            RenamePlayers();
        }
        else if (gamepads.Count == 2)
        {
            InputUser.PerformPairingWithDevice(gamepads[0], bigTank.user);
            InputUser.PerformPairingWithDevice(gamepads[1], smallTank.user);
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


        bigTank.user.AssociateActionsWithUser(bigTank.actions);
        smallTank.user.AssociateActionsWithUser(smallTank.actions);

    }

    void RenamePlayers()
    {
        bigTank.gameObject.name = "BigTank";
        smallTank.gameObject.name = "SmallTank";
    }

}

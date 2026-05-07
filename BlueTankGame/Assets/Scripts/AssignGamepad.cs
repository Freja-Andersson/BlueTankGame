using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class AssignGamepad : MonoBehaviour
{
    [SerializeField] PlayerInput player1;
    [SerializeField] PlayerInput player2;


    void Start()
    {
        if (player1 != null) player1.user.UnpairDevices();
        if (player2 != null) player2.user.UnpairDevices();
    }

    void Update()
    {
        var gamepads = Gamepad.all;

        if (gamepads.Count > 0 && player1 != null)
        {
            var d0 = gamepads[0];
            InputUser.PerformPairingWithDevice(d0, player1.user);
            player1.user.AssociateActionsWithUser(player1.actions);
            player1.gameObject.name = "Player1";

            if(gamepads.Count == 1 && player2 != null) // connect player 2 to arrows
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
        }

        if (gamepads.Count <= 0)  // connect player 1 to wasd and player 2 to arrows
        {
            Debug.Log("no gamepads found");
        }

    }

}

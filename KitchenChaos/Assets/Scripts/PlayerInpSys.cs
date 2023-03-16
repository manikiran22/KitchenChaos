using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInpSys : MonoBehaviour
{

    private PlayerInpAct act;

    public event EventHandler OnInteractAction;

    private void Awake()
    {
        act = new PlayerInpAct();
        act.Player.Enable();
        act.Player.Interact.performed += Interact_performed; //+= subscribing the event 
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null) //checking if there are subscribers
        {
            OnInteractAction(this, EventArgs.Empty);
        }

        //NullConditional operator OnInteractAction?(this,EventArgs.Empty)
    }

    public Vector2 InputVecNor()
    {
        

        Vector2 inputVector = act.Player.Move.ReadValue<Vector2>(); 


        inputVector = inputVector.normalized;

        
        return inputVector;
    }
}

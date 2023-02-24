using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInpSys : MonoBehaviour
{

    private PlayerInpAct act;

    private void Awake()
    {
        act = new PlayerInpAct();
        act.Player.Enable();
    }

    public Vector2 InputVecNor()
    {
        

        Vector2 inputVector = act.Player.Move.ReadValue<Vector2>(); 


        inputVector = inputVector.normalized;

        
        return inputVector;
    }
}

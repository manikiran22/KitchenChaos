using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIS_F : MonoBehaviour
{
    [SerializeField]
    private float movSpeed = 7f;
    [SerializeField]
    private PlayerInpSys playerInpSys;


    bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 inputVector = playerInpSys.InputVecNor();
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        float movDist = movSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHead = 2f;

        //physics is the class and Raycast is the function that checks if has object in the way
        //Capsulecast is for better reach of the body 
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHead,playerRadius, movDir, movDist);
        //Physics.Raycast()

        //so when the player is against the wall and cant move forward but is in the diagonal position 
        //then the player must be able to move the other direction aliong side of the object 

        if (!canMove)
        {
            //basically cannot move towards the moveDirection or the object

            //Attempt to move towards X (if W->D)
            Vector3 movDirX = new Vector3(movDir.x,0,0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHead, playerRadius, movDirX, movDist);

            if (canMove)
            {
                //can move only in movDirX
                movDir = movDirX;
            }
            else { 
                //can move only on X
                //Attempt to move z
                Vector3 movDirZ = new Vector3(0,0,movDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHead, playerRadius, movDirZ, movDist);

                if(canMove)
                {
                    movDir= movDirZ;
                }
            }
        }


        isWalking = movDir != Vector3.zero;

        if (canMove)
        {
            //All about positions
            transform.position += movDir * movDist;
        }
        //All about rotations
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * movSpeed);


        //Debug.Log(inputVector);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}

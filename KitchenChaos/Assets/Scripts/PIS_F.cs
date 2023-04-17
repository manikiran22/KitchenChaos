using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIS_F : MonoBehaviour
{
    [SerializeField]
    private float movSpeed = 7f;
    [SerializeField]
    private PlayerInpSys playerInpSys;
    [SerializeField]
    private LayerMask clearCountMask;

    private ClearCount selectedClrCnt;

    public event EventHandler OnSelectedCounterChanged;
    
    //eventargs are the extension of the event when we want to pass in more info to it


    bool isWalking = false;
    Vector3 lastDir;

    // Start is called before the first frame update
    private void Start()
    {
        playerInpSys.OnInteractAction += PlayerInpSys_OnInteractAction;
    }

    private void PlayerInpSys_OnInteractAction(object sender, System.EventArgs e)
    {

        if (selectedClrCnt != null)
        {
            selectedClrCnt.Interact();
            OnSelectedCounterChanged?.Invoke(this,EventArgs.Empty);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    //All the Walking Animation
    public bool IsWalking()
    {
        return isWalking;
    }

    //All the movement - rotation - collision 
    private void HandleMovement()
    {

        Vector2 inputVector = playerInpSys.InputVecNor();
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        float movDist = movSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHead = 2f;

        //physics is the class and Raycast is the function that checks if has object in the way
        //Capsulecast is for better reach of the body 
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHead, playerRadius, movDir, movDist);
        //Physics.Raycast()

        //so when the player is against the wall and cant move forward but is in the diagonal position 
        //then the player must be able to move the other direction aliong side of the object 

        if (!canMove)
        {
            //basically cannot move towards the moveDirection or the object

            //Attempt to move towards X (if W->D)
            Vector3 movDirX = new Vector3(movDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHead, playerRadius, movDirX, movDist);

            if (canMove)
            {
                //can move only in movDirX
                movDir = movDirX;
            }
            else
            {
                //can move only on X
                //Attempt to move z
                Vector3 movDirZ = new Vector3(0, 0, movDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHead, playerRadius, movDirZ, movDist);

                if (canMove)
                {
                    movDir = movDirZ;
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

    //All the Interaction
    private void HandleInteraction()
    {
        Vector2 inputVec = playerInpSys.InputVecNor();
        Vector3 movDir = new Vector3(inputVec.x, 0, inputVec.y);

        if (movDir != Vector3.zero)
        { 
            lastDir= movDir;
        }

        float interactDist = 2f;
        if (Physics.Raycast(transform.position, lastDir, out RaycastHit raycastHit, interactDist, clearCountMask))
        {
            //Debug.Log("Interacted");
            //Debug.Log(raycastHit.transform);
            //this is saying if the object that was hit by a raycast
            if (raycastHit.transform.TryGetComponent(out ClearCount clearCount))  
            {
                if (clearCount != selectedClrCnt)
                {
                    selectedClrCnt = clearCount;
                }
            }//if there is something but that is not a clearcounter then mark it to null
            else {
                selectedClrCnt = null;
            }
        }//if there is nothing infront of the player then 
        else {
            selectedClrCnt = null;
            Debug.Log(null);
        }

    }
}

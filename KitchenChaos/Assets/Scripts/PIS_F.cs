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

        isWalking = movDir != Vector3.zero;

        //All about positions
        transform.position += movDir * Time.deltaTime * movSpeed;

        //All about rotations
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * movSpeed);


        //Debug.Log(inputVector);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}

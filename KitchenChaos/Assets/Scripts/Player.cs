using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float movSpeed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y= 1;
        }   
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }

        inputVector = inputVector.normalized;

        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);
        
        //All about positions
        transform.position += movDir * Time.deltaTime * movSpeed;

        //All about rotations
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * movSpeed);
       

        //Debug.Log(inputVector);
    }

    
}

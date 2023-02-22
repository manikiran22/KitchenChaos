using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;

    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;
    
    void Awake()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}

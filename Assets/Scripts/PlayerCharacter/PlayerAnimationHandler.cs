using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimationHandler : MonoBehaviour
{
    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("velocity", PlayerBrain.instance.magnitude);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private Rigidbody rb;

    private float movementSpeed = 8;
    private float jumpingPower = 20;
    private float horizontal;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

    }

    //private bool IsGrounded()
    //{
    //    return Physics.OverlapSphere(groundCheck.position, 0.2f, groundLayer);
    //}

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        Debug.Log("Moving");
    }

    //public void Jump(InputAction.CallbackContext context)
    //{
    //    if(context.performed && IsGrounded())
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    //    }
    //}

}

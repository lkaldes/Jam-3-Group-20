using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float speed = 10f;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private Vector3 movement = new();

    void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z;
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        rigidbody.AddForce(movement * speed * 10f, ForceMode.Force);
        
        Vector3 flatVelocity = new(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        if (flatVelocity.magnitude > speed)
        {
            Vector3 velocity = flatVelocity.normalized * speed;
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }

    }
}

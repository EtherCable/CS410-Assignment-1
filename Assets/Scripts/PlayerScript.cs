using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    public float movementSpeed=0f;
    public int jumpLimit = 2;
    private float movementX, movementY;
    private int jumpCounter = 0;
    public Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(jumpCounter < jumpLimit && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            
            jumpCounter++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.GetComponent<Ga>() != null)
        //{
            jumpCounter = 0;
        //}
    }
    private void OnMove(InputValue input)
    {
        Vector2 vec = input.Get<Vector2> ();
        movementX = vec.x;
        movementY = vec.y;

    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(movementX,0f,movementY)*movementSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    public int jumpLimit = 2;
    private bool canJump = true;
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
}

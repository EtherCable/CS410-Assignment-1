using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    public float movementSpeed=0f;
    public int jumpLimit = 2;
    private float movementX, movementY;
    private int jumpCounter = 0;
    private int score;
    private Rigidbody rb;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI statusLabel;
    public GameObject pickups;
    

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(jumpCounter < jumpLimit && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            
            jumpCounter++;
            Debug.Log("JUMP!");
        }
    }

    private void OnMove(InputValue input)
    {
        Vector2 vec = input.Get<Vector2> ();
        movementX = vec.x;
        movementY = vec.y;

    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.GetComponent<Ga>() != null)
        //{
        if(collision.gameObject.CompareTag("Floor Object"))
        {
            jumpCounter = 0;

        }
        //}
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(movementX,0f,movementY)*movementSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup Object"))
        {
            other.gameObject.SetActive(false);
            score++;
            UpdateScoreLabel();

            if(score >= pickups.transform.childCount)
            {
                statusLabel.color = new Color(0f, 1f, 0f, 1f);
                statusLabel.text = "YOU WIN";
            }

        } else if (other.gameObject.CompareTag("Respawn Object"))
        {
            StartCoroutine(ProcessLoss());
        }
    }


    private IEnumerator ProcessLoss()
    {
        statusLabel.color = new Color(1f,0f,0f,1f);
        statusLabel.text = "YOU LOSE";
        for(int i = 0;i<4;i++)
        {
            yield return new WaitForSecondsRealtime(1);
            statusLabel.text = statusLabel.text + "\nL";

        }
        scoreLabel.color = Color.yellow;
        scoreLabel.text = "YOU SUCK!!!";
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void UpdateScoreLabel()
    {
        scoreLabel.text = $"Score: {score}";

    }
}

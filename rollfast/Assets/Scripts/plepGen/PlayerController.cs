using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool wasGroundedeOnce;
    public bool isGrounded;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            wasGroundedeOnce = false;
            rb.velocity = Vector3.zero;
            gameObject.transform.position = new Vector3(0, 50, 0);
        }
    }

    private void FixedUpdate()
    {
        float DisstanceToTheGround = GetComponent<SphereCollider>().bounds.extents.y;
        if (!wasGroundedeOnce)
        {
            wasGroundedeOnce = Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.2f);
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.3f);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        if (isGrounded)
        {
            rb.AddForce(movement * speed);   
        }
    }
}
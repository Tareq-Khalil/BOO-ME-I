using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;


    [Header("Trowing")]
    public GameObject jarPrefab;
    public int jarsAmount = 0; 
    public bool canThrow;
    public float throwForce = 10;

    public bool isInTraining = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canThrow)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Throw();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput > 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        if (horizontalInput < 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        movement.Normalize();

        MovePlayer(movement);


    }
 
    void Throw()
    {
        jarsAmount--;
        if(jarsAmount <= 0)
        {
            canThrow = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        // Instantiate the jar prefab in front of the player
        GameObject jar = Instantiate(jarPrefab, transform.position + transform.right, Quaternion.identity);

        // Apply a force to the jar in the direction the player is facing
        Rigidbody2D jarRb = jar.GetComponent<Rigidbody2D>();
        jarRb.AddForce(transform.right * throwForce, ForceMode2D.Impulse);

    }
  
    void MovePlayer(Vector2 movement)
    {
        Vector2 currentPos = rb.position;
        Vector2 newPos = currentPos + movement * moveSpeed * Time.deltaTime;

        rb.MovePosition(newPos);
    }

    public void ShowJar()
    {
        jarsAmount++;
        canThrow = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEnemy : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MimicPlayerMovement();
    }

    void MimicPlayerMovement()
    {
        // Get the player's movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput > 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        if (horizontalInput < 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        movement.Normalize();

        // Mimic the player's movement
        MoveEnemy(movement);
    }

    void MoveEnemy(Vector2 movement)
    {
        Vector2 currentPos = rb.position;
        Vector2 newPos = currentPos + movement * moveSpeed * Time.deltaTime;

        rb.MovePosition(newPos);
    }

}

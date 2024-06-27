using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoochieMovementScript : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1.0f;


    private Rigidbody rb;
    private Vector3 lastMovementVector;
    private Vector3 newMovementVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementController();

        //IN THE END APPLY THE COMPUTED MOVEMENT VECTOR;
        Move();
    }

    void Move()
    {
        rb.velocity = newMovementVector;

        lastMovementVector = newMovementVector;
        newMovementVector = Vector3.zero;
    }

    void MovementController()
    {
        float moveX = -Input.GetAxis("Horizontal") * movementSpeed;
        float moveZ = Input.GetAxis("Vertical") * movementSpeed;

        newMovementVector += new Vector3(moveX, rb.velocity.y, moveZ);
    }
}

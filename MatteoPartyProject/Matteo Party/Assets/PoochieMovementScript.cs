using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoochieMovementScript : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] bool invesedHorizontalControls = false;


    private Rigidbody rb;


    //FOR THE BOOSTING
    private Vector3 boostVector;
    private Vector3 boostDirection;
    [SerializeField] private float initialBoostPower;
    private float boostPower;
    private float boostAccumulated;
    [SerializeField] private float boostAccumulationSpeed;
    [SerializeField] private float boostDuration;
    private float boostCounter;

    private Vector3 lastMovementVector;
    private Vector3 newMovementVector;

    // Start is called before the first frame update
    void Start()
    {
        boostCounter = 0;
        boostAccumulated = initialBoostPower;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementController();


        AccumulateBoost();
        RealeaseBoost();

        //IN THE END APPLY THE COMPUTED MOVEMENT VECTOR;
        Move();
    }

    void Move()
    {
        rb.velocity = newMovementVector + boostVector;


        lastMovementVector = newMovementVector;
        newMovementVector = Vector3.zero;
    }

    void MovementController()
    {
        int inverser = 1;
        if (invesedHorizontalControls)
        {
            inverser = -1;
        }

        float moveX = -Input.GetAxis("Horizontal") * movementSpeed * inverser;
        float moveZ = Input.GetAxis("Vertical") * movementSpeed;

        newMovementVector += new Vector3(moveX, rb.velocity.y, moveZ);
    }

    void AccumulateBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            boostAccumulated += Time.deltaTime * boostAccumulationSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            boostPower = boostAccumulated;
            boostAccumulated = initialBoostPower;
        }
    }

    void RealeaseBoost()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Vector3 direction = new Vector3(newMovementVector.x, 0, newMovementVector.z);

            if(direction != Vector3.zero)
            {
                boostDirection = direction.normalized;
            }

            boostVector = boostDirection * boostPower;

            boostCounter = boostDuration;
        }

        if(boostCounter > 0)
        {
            boostVector = boostDirection * boostPower * boostCounter / boostDuration;
            boostCounter -= Time.deltaTime;
        }

        if(boostCounter < 0)
        {
            boostCounter = 0;
            boostVector = Vector3.zero;
        }

    }
}

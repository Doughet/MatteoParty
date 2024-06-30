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
    private Vector3 boostDirectionRegistered;
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
        //MovementController();

        AccumulateBoost();

        SelectDirection();

        RealeaseBoost();

        //IN THE END APPLY THE COMPUTED MOVEMENT VECTOR;
        Move();
    }

    void Move()
    {
        newMovementVector = new Vector3(0.0f, rb.velocity.y, 0.0f);

        rb.velocity = newMovementVector + boostVector;

        lastMovementVector = newMovementVector;
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

    void SelectDirection() {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        boostDirection = new Vector3(inputX, 0.0f, inputY);
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
            boostDirectionRegistered = boostDirection;

            boostVector = boostDirectionRegistered * boostPower;

            boostCounter = boostDuration;
        }

        if(boostCounter > 0)
        {
            boostVector = boostDirectionRegistered * boostPower * boostCounter / boostDuration;
            boostCounter -= Time.deltaTime;
        }

        if(boostCounter < 0)
        {
            boostCounter = 0;
            boostVector = Vector3.zero;
        }

    }
}

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
    private float boostAccumulatedReal;
    [SerializeField] private float boostAccumulationSpeed;
    [SerializeField] private float boostDuration;
    private float boostCounter;

    [SerializeField] private ParticleSystem particleFocus;
    [SerializeField] private Color color0Focus;
    [SerializeField] private Color color100Focus;
    [SerializeField] private Color color300Focus;
    [SerializeField] private Color color500Focus;

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

        SelectPower();

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

    void SelectPower()
    {
        if (boostAccumulated > 600)
        {
            particleFocus.startColor = color500Focus;
            boostAccumulatedReal = 600;
        }
        else if (boostAccumulated > 350)
        {
            particleFocus.startColor = color300Focus;
            boostAccumulatedReal = 350;
        }
        else if (boostAccumulated > 100)
        {
            particleFocus.startColor = color100Focus;
            boostAccumulatedReal = 100;
        }
        else if (boostAccumulated > 0)
        {
            particleFocus.startColor = color0Focus;
        }
    }

    void AccumulateBoost()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            particleFocus.Play();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            boostAccumulated += Time.deltaTime * boostAccumulationSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            boostPower = boostAccumulatedReal;
            boostAccumulatedReal = 0;
            boostAccumulated = initialBoostPower;
            particleFocus.Stop();
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

    private void OnTriggerEnter(Collider other)
    {
        var targetDash = other.GetComponent<TargetDashScript>();

        if (targetDash)
        {
            targetDash.LaunchEvent(boostPower);
        }
    }
}

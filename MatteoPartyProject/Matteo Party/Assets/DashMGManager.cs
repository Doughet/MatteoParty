using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashMGManager : MonoBehaviour
{
    [SerializeField] private Vector3 centerPosition;
    [SerializeField] private float radius;
    [SerializeField] private float minDistanceFromPlayer;

    [SerializeField] private TargetDashScript targetPrefab;

    [SerializeField] private PoochieMovementScript playerScript;

    private float maxCounter;
    private float currentCounter;

    private float refillCounter;
    private float spawnCounter;

    private List<TargetDashScript> targets;

    private float score;

    //UI Part
    [SerializeField] private Slider timeSlider;
    [SerializeField] private float maxValueTimeSlider;
    [SerializeField] private Color startColorTime;
    [SerializeField] private Color endColorTime;
    private float currentValueTimeSlider;

    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        score = 0.0f;
        currentCounter = maxCounter;

        refillCounter = 5.0f;
        spawnCounter = refillCounter;

        targets = new List<TargetDashScript>();

        playerScript = FindAnyObjectByType<PoochieMovementScript>();

        currentValueTimeSlider = maxValueTimeSlider;
        timeSlider.maxValue = maxValueTimeSlider;
        timeSlider.value = currentValueTimeSlider;

        scoreText.text = "Score : 0";
    }

    private void Update()
    {
        TimeManagement();

        currentCounter -= Time.deltaTime;
        spawnCounter -= Time.deltaTime;

        ManageRefill();

        if(spawnCounter <= 0)
        {
            SpawnTarget();
        }

        if(currentCounter <= 0)
        {
            FinishMG();
        }
    }

    private void ManageRefill()
    {

    }

    private void TimeManagement()
    {
        currentValueTimeSlider -= Time.deltaTime;
        timeSlider.value = currentValueTimeSlider;

        var color = Color.Lerp(endColorTime, startColorTime, currentValueTimeSlider / maxValueTimeSlider);
        timeSlider.fillRect.GetComponent<Image>().color = color;
    }

    private void SpawnTarget()
    {
        Vector3 position = new Vector3(0, 3.5f, 0);

        do
        {
            float radiusFraction = Random.Range(0.0f, 1.0f);
            float angleFraction = Random.Range(0.0f, 1.0f);

            float radiusSelected = radius * radiusFraction;
            float angleSelected = angleFraction * 2 * Mathf.PI;

            float xValue = centerPosition.x + Mathf.Cos(angleSelected) * radiusSelected;
            float zValue = centerPosition.z + Mathf.Sin(angleSelected) * radiusSelected;

            position = new Vector3(xValue, 3.5f, zValue);

        }
        while (Vector3.Distance(position, playerScript.gameObject.transform.position) < minDistanceFromPlayer);

        TargetDashScript target = Instantiate(targetPrefab);
        target.transform.position = position;

        targets.Add(target);
        target.OnHit.AddListener(HandleTargetCollision);

        spawnCounter = refillCounter;
    }

    private void HandleTargetCollision(TargetDashScript.DashTargetBundle bundle)
    {
        score += bundle.score;
        targets.Remove(bundle.target);
        Destroy(bundle.target.gameObject);

        scoreText.text = "Score: " + ((int)score).ToString();
    }

    private void FinishMG()
    {

    }
}

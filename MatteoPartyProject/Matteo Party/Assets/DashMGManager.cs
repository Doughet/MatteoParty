using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMGManager : MonoBehaviour
{
    [SerializeField] private Vector3 centerPosition;
    [SerializeField] private float radius;

    [SerializeField] private TargetDashScript targetPrefab;

    private float maxCounter;
    private float currentCounter;

    private float refillCounter;
    private float spawnCounter;

    private List<TargetDashScript> targets;

    private float score;

    private void Start()
    {
        score = 0.0f;
        currentCounter = maxCounter;

        refillCounter = 2.0f;
        spawnCounter = refillCounter;

        targets = new List<TargetDashScript>();
    }

    private void Update()
    {
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

    private void SpawnTarget()
    {
        float radiusFraction = Random.Range(0.0f, 1.0f);
        float angleFraction = Random.Range(0.0f, 1.0f);

        float radiusSelected = radius * radiusFraction;
        float angleSelected = angleFraction * 2 * Mathf.PI;

        float xValue = centerPosition.x + Mathf.Cos(angleSelected) * radiusSelected;
        float zValue = centerPosition.z +  Mathf.Sin(angleSelected) * radiusSelected;

        Vector3 position = new Vector3(xValue, 3.5f, zValue);

        TargetDashScript target = Instantiate(targetPrefab);
        target.transform.position = position;

        targets.Add(target);

        spawnCounter = refillCounter;
    }

    private void FinishMG()
    {

    }
}

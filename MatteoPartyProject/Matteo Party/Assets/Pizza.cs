using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public List<PizzaSlice> slices = new List<PizzaSlice>();
    public float rotationSpeed = 10f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Initialize()
    {
        // Create 8 slices and position them around the center
        for (int i = 0; i < 8; i++)
        {
            GameObject sliceObj = new GameObject("Slice" + i);
            PizzaSlice slice = sliceObj.AddComponent<PizzaSlice>();
            slice.transform.SetParent(transform);

            float angle = i * (360f / 8f);
            slice.transform.localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * 5f;
            slice.transform.localRotation = Quaternion.Euler(0, 0, -angle);

            slice.GameManager = gameManager; 
            slices.Add(slice);
        }
    }

    void Update()
    {
        // Rotate the pizza
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
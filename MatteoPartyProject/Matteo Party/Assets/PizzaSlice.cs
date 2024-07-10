using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSlice : MonoBehaviour
{
    public bool hasMushroom = false;
    public bool hasCheese = false;
    public bool hasEggplant = false;
    public bool hasBroccoli = false;
    public GameManager GameManager;

    void Start()
    {
        // Set up the slice appearance
        gameObject.AddComponent<SpriteRenderer>(); // Add a SpriteRenderer for visual representation
        GetComponent<SpriteRenderer>().color = Color.black; // Initial color of the slice
    }

    void OnMouseDown()
    { // This has to be changed so it updates each ingredient, currently is only for mushroom I guess
        if (!hasMushroom)
        {
            // Handle placing an ingredient on the slice
            hasMushroom = true;
            GetComponent<SpriteRenderer>().color = Color.yellow; // Change the slice appearance

            // Notify the GameManager
            GameManager.OnIngredientPlaced();
        }
    }
}
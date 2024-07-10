using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public virtual void Start()
    {
        gameObject.AddComponent<Rigidbody2D>(); // Add a Rigidbody2D for physics
        gameObject.AddComponent<BoxCollider2D>(); // Add a BoxCollider2D for collision detection
        gameObject.tag = "Ingredient"; // Set the tag for collision detection
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerID
    {
        Player1,
        Player2,
        Player3,
        Player4
    }

    public PlayerID playerID;
    public GameObject ingredientPrefab;  // Prefab for the ingredient specific to this player
    public float throwForce = 10f;

    public PlayerController(PlayerID id, GameObject ingredientPrefab)
    {
        this.playerID = id;
        this.ingredientPrefab = ingredientPrefab;
    }

    void Update()
    {
        // Check for player-specific input
        if (Input.GetMouseButtonDown(0))
        {
            ThrowIngredient();
        }
    }

    void ThrowIngredient()
    {
        // Instantiate the selected ingredient at the player's position
        GameObject ingredient = Instantiate(ingredientPrefab, transform.position, Quaternion.identity);

        // Get the direction to throw the ingredient
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Apply a force to the ingredient
        ingredient.GetComponent<Rigidbody2D>().AddForce(direction * throwForce, ForceMode2D.Impulse);
    }
}
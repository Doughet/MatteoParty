using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pizza pizza;
    public List<int> totalIngredientsPlaced;
    public int maxIngredients = 8;
    
    void Start()
    {
        // Initialize the pizza
        pizza.Initialize();
        for (int i = 0; i < 4; i++)
            totalIngredientsPlaced[0] = 0;
    }

    public void OnIngredientPlaced()
    {
        int numIngredientsPlayerWinning = checkScoreBestPlayer();
        
        // I have to change this, I know its only taking into account the player that is winning
        pizza.rotationSpeed -= 1f * numIngredientsPlayerWinning; // Increase rotation speed each time an ingredient is placed

        /*
         
         
         */
        
        if (totalIngredientsPlaced[0] >= maxIngredients)
        {
            // Handle winning condition
            Debug.Log("All ingredients placed! Player 1 won bitches!");
        }else if (totalIngredientsPlaced[1] >= maxIngredients)
        {
            Debug.Log("All ingredients placed! Player 2 won bitches!");
        }
    }

    int checkScoreBestPlayer()
    {
        // I don't know if this works as I think it will
        return totalIngredientsPlaced.Max();
    }
}
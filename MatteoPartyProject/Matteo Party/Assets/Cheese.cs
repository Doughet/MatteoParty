public class Cheese : Ingredient
{
    public override void Start()
    {
        base.Start();
        gameObject.tag = "Cheese";
    }
}
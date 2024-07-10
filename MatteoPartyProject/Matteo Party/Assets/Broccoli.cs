public class Broccoli : Ingredient
{
    public override void Start()
    {
        base.Start();
        gameObject.tag = "Broccoli";
    }
}
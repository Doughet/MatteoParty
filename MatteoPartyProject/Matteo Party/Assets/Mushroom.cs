public class Mushroom : Ingredient
{
    public override void Start()
    {
        base.Start();
        gameObject.tag = "Mushroom";
    }
}
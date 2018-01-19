using UnityEngine;

public class Shrink : Pickup
{
    protected override void Setup ()
    {
        this.tag = "Food";
    }

    protected override void Collect (Collider2D other)
    {
        //TODO: Shrink player size.

        base.Collect (other);
    }
}

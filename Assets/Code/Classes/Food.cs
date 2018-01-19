using UnityEngine;

public class Food : Pickup
{
    protected override void Setup ()
    {
        this.tag = "Food";
    }

    protected override void Collect (Collider2D other)
    {
        //TODO: Increase player score.

        base.Collect (other);
    }
}

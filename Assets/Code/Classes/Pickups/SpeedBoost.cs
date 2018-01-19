using UnityEngine;

public class SpeedBoost : Pickup
{
    protected override void Setup ()
    {
        this.tag = "Food";
    }

    protected override void Collect (Collider2D other)
    {
        //TODO: Increase player speed.

        base.Collect (other);
    }
}

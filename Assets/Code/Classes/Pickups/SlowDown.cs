using UnityEngine;

public class SlowDown : Pickup
{
    protected override void Setup ()
    {
        this.tag = "Food";
    }

    protected override void Collect (Collider2D other)
    {
        //TODO: Decrease player speed.

        base.Collect (other);
    }
}

using UnityEngine;

public class Immunity : Pickup
{
    protected override void Collect (Collider2D other)
    {
        //TODO: Grant timed immunity.

        base.Collect (other);
    }
}

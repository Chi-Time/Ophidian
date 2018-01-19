using UnityEngine;

public class Food : Pickup
{
    protected override void Collect (Collider2D other)
    {
        base.Collect (other);
    }
}

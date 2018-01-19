using UnityEngine;

public class SlowDown : Pickup
{
    [SerializeField] private float _SlowedAmount = 0.0f;

    protected override void Collect (Collider2D other)
    {
        //TODO: Decrease player speed.

        base.Collect (other);
    }
}

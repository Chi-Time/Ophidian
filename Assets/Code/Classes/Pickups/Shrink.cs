using UnityEngine;

public class Shrink : Pickup
{
    [SerializeField] private int _ShrinkAmount = 0;

    protected override void Collect (Collider2D other)
    {
        //TODO: Shrink player size.

        base.Collect (other);
    }
}

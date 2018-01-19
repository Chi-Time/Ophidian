using UnityEngine;

public class SpeedBoost : Pickup
{
    [SerializeField] private float _BoostAmount = 0.0f;

    protected override void Collect (Collider2D other)
    {
        //TODO: Increase player speed.

        base.Collect (other);
    }
}

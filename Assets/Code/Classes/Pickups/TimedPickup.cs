using UnityEngine;

public abstract class TimedPickup : Pickup
{
    [Tooltip ("How long will this pickup's effect last for?")]
    private float _Duration = 0.0f;

    protected override void Collect (Collider2D other)
    {
        Invoke ("EndEffect", _Duration);
        base.Collect (other);
    }

    protected abstract void EndEffect ();

    private void OnDestroy ()
    {
        CancelInvoke ("EndEffect");
    }
}

using UnityEngine;

public class Immunity : TimedPickup
{
    protected override void Collect (Collider2D other)
    {
        EventManager.Instance.Raise (new ImmunityGranted (true));
        base.Collect (other);
    }

    protected override void EndEffect ()
    {
        EventManager.Instance.Raise (new ImmunityGranted (false));
    }
}

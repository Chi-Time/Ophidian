using UnityEngine;

public class Speed : TimedPickup
{
    [SerializeField] private float _Speed = 0.0f;

    protected override void Collect (Collider2D other)
    {
        EventManager.Instance.Raise (new SpeedChanged (_Speed));

        base.Collect (other);
    }

    protected override void EndEffect ()
    {
        EventManager.Instance.Raise (new SpeedChanged (-_Speed));
    }
}

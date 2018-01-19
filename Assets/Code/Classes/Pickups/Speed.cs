using UnityEngine;

public class Speed : TimedPickup
{
    [Tooltip ("The speed modifier to apply to the player. Negative numbers slow and positive numbers speed up.")]
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

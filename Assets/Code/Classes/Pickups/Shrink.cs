using UnityEngine;

public class Shrink : Pickup
{
    [Range (1, 3)]
    [SerializeField] private int _MinShrinkAmount = 0;
    [Range (1, 4)]
    [SerializeField] private int _MaxShrinkAmount = 0;

    private int _ShrinkAmount = 0;

    private void Awake ()
    {
        _ShrinkAmount = Random.Range (_MinShrinkAmount, _MaxShrinkAmount);
    }

    protected override void Collect (Collider2D other)
    {
        EventManager.Instance.Raise (new TailShrunk (_ShrinkAmount));

        base.Collect (other);
    }
}

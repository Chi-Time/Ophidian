using UnityEngine;

public class Food : Pickup
{
    protected override void Collect (Collider2D other)
    {
        GameObject.FindGameObjectWithTag ("Player").GetComponent<SnakeController> ().GrowTail ();

        base.Collect (other);
    }
}

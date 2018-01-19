using UnityEngine;

[RequireComponent (typeof (Rigidbody2D), typeof (Collider2D))]
public abstract class Pickup : MonoBehaviour
{
    private void Awake ()
    {
        AssignReferences ();
        Setup ();
    }

    private void AssignReferences ()
    {
        var rb2D = GetComponent<Rigidbody2D> ();
        rb2D.isKinematic = true;

        GetComponent<Collider2D> ().isTrigger = true;
    }

    protected abstract void Setup ();

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag ("Player"))
            Collect (other);
    }

    protected virtual void Collect (Collider2D other)
    {
        Destroy (this.gameObject);
    }
}

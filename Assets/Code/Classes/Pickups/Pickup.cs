using UnityEngine;

[RequireComponent (typeof (Rigidbody2D), typeof (Collider2D))]
public abstract class Pickup : MonoBehaviour
{
    [Tooltip ("The score awarded to the player upon collecting the pickup.")]
    [SerializeField] private int _Value = 0;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        var rb2D = GetComponent<Rigidbody2D> ();
        rb2D.isKinematic = true;

        GetComponent<Collider2D> ().isTrigger = true;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag ("Player"))
            Collect (other);
    }

    protected virtual void Collect (Collider2D other)
    {
        EventManager.Instance.Raise (new ScoreIncreased (_Value));
        this.gameObject.SetActive (false);
    }
}

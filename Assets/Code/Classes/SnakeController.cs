using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody2D), typeof (Collider2D))]
public class SnakeController : MonoBehaviour
{
    [Tooltip ("The speed of the snake's movement on screen.")]
    [SerializeField] private float _Speed = 0.5f;
    [Tooltip ("The prefab to use as the snake's tail.")]
    [SerializeField] private GameObject _TailPiecePrefab = null;
    [Tooltip ("The snake's current tail size and objects.")]
    [SerializeField] private List<Transform> _Tail = new List<Transform> ();

    private bool _IsImmune = false;
    private Vector2 _Direction = Vector2.zero;
    private Transform _Transform = null;
    private Rigidbody2D _Rigidbody2D = null;

    private void Awake ()
    {
        AssignReferences ();
        SetupRigidbody ();
    }

    private void AssignReferences ()
    {
        _Transform = GetComponent<Transform> ();
        _Rigidbody2D = GetComponent<Rigidbody2D> ();
        GetComponent<Collider2D> ().isTrigger = true;
    }

    private void SetupRigidbody ()
    {
        _Rigidbody2D.gravityScale = 0.0f;
        _Rigidbody2D.isKinematic = true;
        _Rigidbody2D.freezeRotation = true;
    }

    private void Start ()
    {
        SubscribeToEvents ();

        StartCoroutine (Move ());
    }

    private void SubscribeToEvents ()
    {
        EventManager.Instance.AddListener<SpeedChanged> (OnSpeedChanged);
        EventManager.Instance.AddListener<TailShrunk> (OnTailShrunk);
        EventManager.Instance.AddListener<ImmunityGranted> (OnImmunityGranted);
    }

    private IEnumerator Move ()
    {
        yield return new WaitForSeconds (_Speed);

        MoveTail ();
        MoveHead ();

        StopCoroutine (Move ());
        StartCoroutine (Move ());
    }

    private void MoveTail ()
    {
        if (_Tail.Count == 0)
            return;

        var currentHeadPosition = _Transform.position;
        var lastTailPiece = _Tail[_Tail.Count - 1];
        _Tail.RemoveAt (_Tail.Count - 1);

        lastTailPiece.position = currentHeadPosition;
        _Tail.Insert (0, lastTailPiece);
    }

    private void MoveHead ()
    {
        _Rigidbody2D.MovePosition (_Rigidbody2D.position + _Direction);
    }

    private void Update ()
    {
        GetInput ();
    }

    private void GetInput ()
    {
        // Get the snakes next movement direction ensuring that it can never turn back on itself thus causing instant death.

        if (Input.GetAxisRaw ("Horizontal") > 0.0f && _Direction != Vector2.left)
            _Direction = Vector2.right;
        else if (Input.GetAxisRaw ("Horizontal") < 0.0f && _Direction != Vector2.right)
            _Direction = Vector2.left;
        else if (Input.GetAxisRaw ("Vertical") > 0.0f && _Direction != Vector2.down)
            _Direction = Vector2.up;
        else if (Input.GetAxisRaw ("Vertical") < 0.0f && _Direction != Vector2.up)
            _Direction = Vector2.down;
    }

    public void GrowTail ()
    {
        var piece = Instantiate (_TailPiecePrefab, Vector3.zero, Quaternion.identity).transform;
        piece.position = _Tail[_Tail.Count - 1].position;

        _Tail.Add (piece);
    }

    private void OnSpeedChanged (SpeedChanged e)
    {
        _Speed += e.Speed;
    }

    private void OnTailShrunk (TailShrunk e)
    {
        int shrinkAmount = 0;

        if (_Tail.Count > e.ShrinkAmount)
            shrinkAmount = _Tail.Count - 1;
        else
            shrinkAmount = e.ShrinkAmount;

        for (int i = 1; i < shrinkAmount; i++)
        {
            var tailPiece = _Tail[_Tail.Count - i];
            _Tail.Remove (tailPiece);

            Destroy (tailPiece);
        }
    }

    private void OnImmunityGranted (ImmunityGranted e)
    {
        _IsImmune = e.IsImmune;
    }

    private void OnDestroy ()
    {
        UnsubscribeFromEvents ();
    }

    private void UnsubscribeFromEvents ()
    {
        EventManager.Instance.RemoveListener<SpeedChanged> (OnSpeedChanged);
        EventManager.Instance.RemoveListener<TailShrunk> (OnTailShrunk);
        EventManager.Instance.RemoveListener<ImmunityGranted> (OnImmunityGranted);
    }
}

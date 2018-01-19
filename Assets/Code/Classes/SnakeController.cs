using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody2D), typeof (Collider2D))]
public class SnakeController : MonoBehaviour
{
    [Tooltip ("The speed of the snake's movement on screen.")]
    [SerializeField] private float _Speed = 0.5f;
    [SerializeField] private GameObject _TailPiecePrefab = null;
    [SerializeField] private List<Transform> _Tail = new List<Transform> ();

    private Vector3 _Direction = Vector2.zero;
    private Transform _Transform = null;
    private Rigidbody2D _Rigidbody2D = null;

    private void Awake ()
    {
        _Transform = GetComponent<Transform> ();
        _Rigidbody2D = GetComponent<Rigidbody2D> ();
        GetComponent<Collider2D> ().isTrigger = true;

        _Rigidbody2D.gravityScale = 0.0f;
        _Rigidbody2D.isKinematic = true;
        _Rigidbody2D.freezeRotation = true;

        for (int i = 0; i < 10; i++)
        {
            var piece = Instantiate (_TailPiecePrefab, Vector3.zero, Quaternion.identity);

            _Tail.Add (piece.transform);
        }
    }

    private void Start ()
    {
        StartCoroutine (Move ());
    }

    private void Update ()
    {
        GetInput ();
    }

    private void GetInput ()
    {
        // Get the snakes next movement direction ensuring that it can never turn back on itself thus causing instant death.

        if (Input.GetAxisRaw ("Horizontal") > 0.0f && _Direction != Vector3.left)
            _Direction = Vector3.right;
        else if (Input.GetAxisRaw ("Horizontal") < 0.0f && _Direction != Vector3.right)
            _Direction = Vector3.left;
        else if (Input.GetAxisRaw ("Vertical") > 0.0f && _Direction != Vector3.down)
            _Direction = Vector3.up;
        else if (Input.GetAxisRaw ("Vertical") < 0.0f && _Direction != Vector3.up)
            _Direction = Vector3.down;
    }

    private IEnumerator Move ()
    {
        yield return new WaitForSeconds (_Speed);

        var currentPos = _Transform.position;
        var endPiece = _Tail[_Tail.Count - 1];
        _Tail.RemoveAt (_Tail.Count - 1);

        endPiece.position = currentPos;
        _Tail.Insert (0, endPiece);

        _Rigidbody2D.MovePosition (_Rigidbody2D.position + (Vector2)_Direction);

        StopCoroutine (Move ());
        StartCoroutine (Move ());
    }
}

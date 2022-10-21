using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Drag))]
public class CounterMovement : MonoBehaviour
{
    [SerializeField] private float _force;

    public Vector3 Direction { get; set; }

    public Vector3 Destination { get; set; }

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ComparePoints(transform.position, Destination, 0.1f))
        {
            Stop();
            return;
        }

        if (Direction != Vector3.zero)
        {
            Move();
        }
    }

    public void Move()
    {
        _rigidbody.AddForce(Direction * _force * Time.fixedDeltaTime);
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
        Direction = Vector3.zero;
    }

    public static bool ComparePoints(Vector3 point1, Vector3 point2, float tolerance)
    {
        return Mathf.Abs(Vector3.Distance(point1, point2)) <= tolerance;
    }
}
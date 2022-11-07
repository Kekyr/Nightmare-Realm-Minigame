using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;

    public RectTransform CounterToMove { get; set; }
    public RectTransform Destination { get; set; }
    public bool IsMoving { get; private set; }

    private Tween tween;
    private Vector3 _direction;
    private float _magnitude;
    private RaycastHit2D _raycastHit;

    private void Update()
    {
        if (CounterToMove != null && Destination != null && !IsMoving)
        {
            _direction = (Destination.position - CounterToMove.position).normalized;
            _magnitude = (Destination.position - CounterToMove.position).magnitude;


            if (ComparePoints(_direction, Vector3.up) ||
                ComparePoints(_direction, Vector3.down) ||
                ComparePoints(_direction, Vector3.left) ||
                ComparePoints(_direction, Vector3.right))
            {
                _raycastHit = Physics2D.Raycast(CounterToMove.position, _direction, _magnitude);

                if (_raycastHit.collider == null) 
                    StartCoroutine(Move());
            }
        }
    }


    private IEnumerator Move()
    {
        IsMoving = true;

        CounterToMove.SetParent(CounterToMove.parent.parent);
        tween = CounterToMove.DOMove(Destination.position, 1f);

        yield return tween.WaitForCompletion();

        CounterToMove.SetParent(Destination);
        _gridManager.CheckColumns();

        Destination = null;
        IsMoving = false;
    }

    private bool ComparePoints(Vector3 point1, Vector3 point2, float tolerance=0.1f)
    {
        return Mathf.Abs(Vector3.Distance(point1, point2)) <= tolerance;
    }
}
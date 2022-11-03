using System;
using System.Collections;
using DG.Tweening;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private GridManager _gridManager;
    [SerializeField] private GameObject _winWindow;

    public RectTransform CounterToMove { get; set; }

    public RectTransform Destination { get; set; }

    public bool IsMoving { get; private set; }
    private Tween tween;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (CounterToMove != null && Destination !=null && !IsMoving)
        {
            Vector3 direction = (Destination.position-CounterToMove.position).normalized;
            float magnitude = (Destination.position - CounterToMove.position).magnitude;
            

            if (ComparePoints(direction,Vector3.up,0.1f)|| 
                ComparePoints(direction,Vector3.down,0.1f)||
                ComparePoints(direction,Vector3.left,0.1f)||
                ComparePoints(direction,Vector3.right,0.1f))
            {
                CounterToMove.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                
                Ray2D ray = new Ray2D(CounterToMove.position, direction);
                
                RaycastHit2D raycastHit=Physics2D.Raycast(ray.origin,ray.direction,magnitude);
                
                Debug.DrawRay(ray.origin,ray.direction*magnitude,Color.red,5f);
                
                CounterToMove.gameObject.GetComponent<BoxCollider2D>().enabled = true;

                
                //Debug.Log(raycastHit.collider);
                
                if (raycastHit.collider == null)
                {
                    StartCoroutine(Move());
                }
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

    public void TurnOnWinWindow()
    {
        _winWindow.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    private bool ComparePoints(Vector3 point1, Vector3 point2, float tolerance)
    {
        return Mathf.Abs(Vector3.Distance(point1, point2)) <= tolerance;
    }


}

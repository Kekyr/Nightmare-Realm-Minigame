using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private RectTransform[,] _slots = new RectTransform[5, 5];

    private int _childIndex;
    private int _numberOfValidCounters;
    private bool _yellowColumn;
    private bool _orangeColumn;
    private bool _redColumn;


    private void Awake()
    {
        GetSlots();
    }

    private void GetSlots()
    {
        for (int i = 0; i <= _slots.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= _slots.GetUpperBound(1); j++)
            {
                _slots[i, j] = (RectTransform)transform.GetChild(_childIndex);
                //Debug.Log(_slots[i, j]);
                _childIndex++;
            }
        }
    }

    public void CheckColumns()
    {
        for (int i = 0; i <= _slots.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= _slots.GetUpperBound(1); j++)
            {
                if (_slots[i, j].childCount > 0)
                {
                    /*Debug.Log("i:" +i);
                    Debug.Log("j:" +j);

                    Debug.Log(_slots[i, j].GetChild(0).tag);*/
                    
                    if (i == 0 && _slots[i, j].GetChild(0).CompareTag("Yellow"))
                    {
                        FoundValidCounter(ref _yellowColumn,i);
                    }
                    else if (i == 2 && _slots[i, j].GetChild(0).CompareTag("Orange"))
                    {
                        FoundValidCounter(ref _orangeColumn,i);

                    }
                    else if (i == 4 && _slots[i, j].GetChild(0).CompareTag("Red"))
                    {
                        FoundValidCounter(ref _redColumn,i);
                    }
                }
            }

            _numberOfValidCounters = 0;
        }

        if (_redColumn && _yellowColumn && _orangeColumn)
        {
            GameManager.instance.TurnOnWinWindow();
        }
    }

    private void FoundValidCounter(ref bool column, int columnNumber)
    {
        _numberOfValidCounters++;
        //Debug.Log("ValidCounters: "+_numberOfValidCounters);
        if (_numberOfValidCounters == 5)
        {
            column = true;
            Debug.Log("columnNumber: "+columnNumber);
        }
    }
}
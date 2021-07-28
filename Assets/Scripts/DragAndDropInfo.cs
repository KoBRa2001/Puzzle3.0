using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInfo : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _childDiamonds;
    public List<Vector2Int> ChildPosition;

    private void Awake()
    {
        foreach(var child in _childDiamonds)
        {            
            Vector2Int newPosition = Vector2Int.FloorToInt(child.transform.position - transform.position);
            ChildPosition.Add(newPosition);
        }
    }
}

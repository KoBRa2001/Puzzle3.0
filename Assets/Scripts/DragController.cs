using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private Transform _dragParent;

    private DragAndDrop _selectedItem;

    public DragAndDrop SelectedItem => _selectedItem;
    public DiamandItem DiamondPrefab;
    
    public void SetItem(DragAndDrop item)
    {
        if (_selectedItem != null)
            _selectedItem.Reset();

        _selectedItem = item;
        _selectedItem.transform.SetParent(_dragParent);        
        //DiamondPrefab = item.DiamondPrefab;
    }

    public DragAndDrop TakeSelectedItem()
    {
        var item = _selectedItem;
        _selectedItem = null;
        return item;
    }    

    public bool HasSelectedItem()
    {
        //if(_selectedItem != null)
        //{
        //    return _selectedItem.CheckFreeCells();
        //}
        //return false;
        return _selectedItem != null;
    }
}

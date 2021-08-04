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
    }

    public DragAndDrop TakeSelectedItem()
    {
        var item = _selectedItem;
        //_selectedItem = null;
        return item;
    }    

    public void DropItem()
    {
        _selectedItem = null;
    }

    public bool HasSelectedItem()
    {        
        return _selectedItem != null;
    }
}

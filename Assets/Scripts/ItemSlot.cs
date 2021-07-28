using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour//, IDropHandler
{   
    [SerializeField] private DiamandItem _diamondPrefab;
    [SerializeField] private ScoreSystem _scoreSystem;

    private DragController _dragController;
    private GridController _gridController;

    private DiamandItem _itemInSlot;

    private bool isEmpty => _itemInSlot == null;

    public Sprite icon;

    public int x; 
    public int y;


    public void SetupPrefab(DiamandItem newPrefab)
    {
        _diamondPrefab = newPrefab;
    }

    public bool CheckSlot()
    {
        return isEmpty;
    }

    public void Setup(DragController dragController, GridController gridController, ScoreSystem scoreSystem)
    {
        _dragController = dragController;
        _gridController = gridController;
        _scoreSystem = scoreSystem;
    }

    public void ClearSlot()
    {
        if (_itemInSlot == null)
            return;

        Destroy(_itemInSlot.gameObject);       
        _itemInSlot = null;
    }
    
    private void OnMouseOver()
    {
        if (!isEmpty)
            return;

        if (Input.GetMouseButtonUp(0) && _dragController.HasSelectedItem())
        {
            var selectedItem = _dragController.TakeSelectedItem();
            if (_gridController.CheckCell(x, y, selectedItem))
            {                             
                SetCell(selectedItem);
                icon = selectedItem.Icon;

                _scoreSystem.UpdateScore(selectedItem.ChildPosition.Count);

                selectedItem.Clear();
                Destroy(selectedItem.gameObject);

                if (_gridController.CheckGameOver())
                    Debug.Log("Game Over");
            }
        }        
    }

    public void SetItemInSlot(DiamandItem item)
    {
        _itemInSlot = item;
        if (icon != null)
            _itemInSlot.SetSprite(icon);       
    }

    public void SetCell(DragAndDrop item)
    {
        //TODO: rewrite this, добавити метод в клас GridController SetItem(int x, int y, List<Vector2Int> positions) і викликати його тут

        foreach(var position in item.ChildPosition)
        {
            _gridController.SetCell(x + position.x, y + position.y, _diamondPrefab);        
        }
    }
}

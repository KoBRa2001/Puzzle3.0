using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private DragController _dragController;
    [SerializeField] private List<DragAndDrop> _itemPrefab;
    [SerializeField] private Transform _parentPanel;
    [SerializeField] private GridController _gridController;

    [Header("Parameters")]
    [SerializeField] private int Count;

    [SerializeField] private List<DragAndDrop> _items;    

    private void Awake()
    {
        _items = new List<DragAndDrop>(Count);        
    }

    private void Start()
    {
        SpawnPack();
    }

    public void Reset()
    {
        List<DragAndDrop> itemsToDelete = new List<DragAndDrop>();
        foreach(var i in _items)
        {
            itemsToDelete.Add(i);
        }                
        foreach(var i in itemsToDelete)
        {
            DeleteItem(i);
        }
        itemsToDelete=null;
    }

    public List<DragAndDrop> GetAvailableItems()
    {
        return _items;
    }

    public void DeleteItem(DragAndDrop objectToDelete)
    {
        Debug.LogWarning("DeleteItem");


        if (_items.Contains(objectToDelete))
        {
            _items.Remove(objectToDelete);
            Destroy(objectToDelete.gameObject);
        }

        if (_items.Count == 0)
            SpawnPack();
    }

    private void SpawnPack()
    {
        for (int i = 0; i < Count; i++)
        {
            int rand = Random.Range(0, _itemPrefab.Count);
            var newItem = Instantiate(_itemPrefab[rand], _parentPanel);
            newItem.Setup(_dragController);
            newItem.SetRand();
            newItem.OnDestroyEvent += () => DeleteItem(newItem);

            _items.Add(newItem);
        }
        if (_gridController.CheckGameOver())
            Debug.Log("Game Over");
    }

}

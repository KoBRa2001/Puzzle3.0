using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int heigth;

    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private DragController _dragController;
    [SerializeField] private ItemController _itemController;
    [SerializeField] private ScoreSystem _scoreSystem;
    [SerializeField] private GameOverPopUp _gameOverPanel;

    private Vector2 currentPosition = Vector2.zero;

    private bool[,] cells;
    private ItemSlot[,] itemCells;

    private HashSet<SpriteRenderer> _highlightedSlots = new HashSet<SpriteRenderer>();

    private Color _defaultSlotColor = new Color32(255, 255, 255, 255);
    private Color _highlightSlotColor = new Color32(100, 100, 100, 255);

    private void Awake()
    {
        cells = new bool[width, heigth];
        itemCells = new ItemSlot[width, heigth];
        currentPosition.x = -width / 2f + 0.5f; ;
        currentPosition.y = -heigth / 2f + 1;
    }

    private void Start()
    {
        InitGrid();
    }

    public void InitGrid()
    {        
        for(int i = 0;  i < width; i++)
        {
            for(int j = 0; j < heigth; j++)
            {
                InitSlot(i, j);               
            }
        }
    }

    public void InitSlot(int i, int j)
    {
        var newSlot = Instantiate(itemSlotPrefab, currentPosition, Quaternion.identity, transform);
        newSlot.Setup(_dragController, this, _scoreSystem);

        //перенести в медот і створити публічні геттери
        newSlot.x = i;
        newSlot.y = j;

        newSlot.transform.localPosition = currentPosition + new Vector2(i, j);
        itemCells[i, j] = newSlot;
    }

    public void HighlightSlot()
    {
        foreach(var i in _highlightedSlots)
        {
            i.color = _highlightSlotColor;
        }
    }

    public void ResetHighlight()
    {
        foreach (var i in _highlightedSlots)
        {
            i.color = _defaultSlotColor;
        }
        _highlightedSlots.Clear();
    }

    public bool CheckCell(int x, int y, DragAndDrop item)
    {        
        if (item == null)
            return false;

        foreach(var position in item.ChildPosition)
        {          
            //If out of bounds
            //if (x + position.x >= Mathf.Sqrt(cells.Length) || y + position.y >= Mathf.Sqrt(cells.Length))
            if (!IsOnBoard(x + position.x, y + position.y))
            {                
                return false;
            }
            if (cells[x + position.x, y + position.y] == true)
            {             
                return false;
            }
            else
            {
                _highlightedSlots.Add(itemCells[x + position.x, y + position.y].GetComponent<SpriteRenderer>());
            }
        }
        return true;
    }

    private bool IsOnBoard(int x, int y)
    {        
        return !(x < 0 || x >= width || y < 0 || y >= heigth);
    }

    public void SetCell(int x, int y, DiamandItem prefab)
    {
        DiamandItem newDiamond = Instantiate(prefab, itemCells[x, y].transform);
        itemCells[x, y].SetItemInSlot(newDiamond);
        cells[x, y] = true;

        CalculateCollapse();
        //if (IsPossibleCollapse(x, y))
        //    VerifyGrid();
    }

    public void CalculateCollapse()
    {
        //if (IsPossibleCollapse())
            VerifyGrid();
    }

    public bool CheckGameOver()
    {
        Debug.LogWarning("CheckGameOver");

        var items = _itemController.GetAvailableItems();

        foreach(var item in items)
        {
            for (int x = 0; x < width; x++)
            {
                for (int i = 0; i < heigth; i++)
                {
                    if (CheckCell(x, i, item))
                    {
                        //Debug.LogError($"Put here {x} {i} + {item.ChildPosition.Count}");

                        foreach (var p in item.ChildPosition)
                        {
                            var t = new Vector2Int(x, i);
                            t += p;
                            //Debug.LogError(cells[t.x, t.y]);

                        }
                        return false;
                    }                 
                }                
            }
                     
        }
        //Debug.LogError($"Cant find pos");        

        AudioManager.Instance.PlayAudio(AudioIndexes.GameOver);

        _gameOverPanel.GameOver();

        return true;
    }   

    public void NewGame()
    {
        Reset();
        _scoreSystem.Clear();
        _itemController.Reset();
    }

    private void VerifyGrid()
    {
        HashSet<Vector2Int> itemIndexes = GetDeletedItems();

        if (itemIndexes.Count > 0)
        {
            _scoreSystem.UpdateScore(itemIndexes.Count);

            foreach (var currentPosition in itemIndexes)
            {
                itemCells[currentPosition.x, currentPosition.y].ClearSlot();
                cells[currentPosition.x, currentPosition.y] = false;
            }
            AudioManager.Instance.PlayAudio(AudioIndexes.ClearRow);
        }
    }

    public void Reset()
    {
        foreach(var i in itemCells)
        {
            i.ClearSlot();            
        }
        cells = new bool[width, heigth];
    }

    public HashSet<Vector2Int> GetDeletedItems()
    {        
        HashSet<Vector2Int> items = new HashSet<Vector2Int>();
        List<Vector2Int> tempItems = new List<Vector2Int>();

        for(int x = 0; x < width; x++)
        {
            for (int i = 0; i < heigth; i++)
            {
                if (!cells[x, i])
                {                    
                    tempItems.Clear();
                    break;
                }
                else
                {
                    tempItems.Add(new Vector2Int(x, i));
                }
            }    
            for(int n = 0; n < tempItems.Count; n++)
            {
                items.Add(tempItems[n]);
            }
            tempItems.Clear();
        }
        for(int y = 0; y < heigth; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!cells[j, y])
                {
                    tempItems.Clear();
                    break;
                }
                else
                {
                    tempItems.Add(new Vector2Int(j, y));
                }
            }
            for (int n = 0; n < tempItems.Count; n++)
            {
                items.Add(tempItems[n]);
            }
        }

        return items;

        //return null;
    }     
}

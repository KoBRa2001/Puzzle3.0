using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private DragController _dragController;    
    [SerializeField] private Image _image; 
    [SerializeField] private Sprite _icon;    


    public event Action OnDestroyEvent = null;

    private CanvasGroup canvasGroup;
    private Camera _camera;

    private Transform startParent;
    //private Vector3 startPosition;

    public Sprite Icon => _icon;


    //[SerializeField] private List<RectTransform> _childDiamonds;
    [SerializeField] private RectTransform _diamond;
    public List<Vector2Int> ChildPosition;
   
    private void Awake()
    {
        _camera = Camera.main;
        
        canvasGroup = GetComponent<CanvasGroup>();

        foreach (var child in ChildPosition)
        {
            RectTransform newDiamond = Instantiate(_diamond, (Vector3Int)child + transform.position, Quaternion.identity, transform);
            //_childDiamonds.Add(newDiamond);            
        }

        //foreach (var child in _childDiamonds)
        //{
        //    Vector2Int newPosition = Vector2Int.FloorToInt(child.transform.position - transform.position);
        //    ChildPosition.Add(newPosition);
        //}
        //ChildPosition.Add(Vector2Int.zero);

    }

    private void Start()
    {
        startParent = transform.parent;
        //startPosition = transform.localPosition;        
    }

    public void Setup(DragController dragController)
    {
        _dragController = dragController;        
    }   

    public void OnBeginDrag(PointerEventData touch)
    {
        _dragController.SetItem(this);        
        canvasGroup.blocksRaycasts = false;        
    }

    internal void SetRand()
    {
        var rand = UnityEngine.Random.Range(1, 4);
        var type = (DiamandType)rand;
        var path = "Icons/" + type.ToString();
        
        _icon = Resources.Load<Sprite>(path);
        _image.sprite = _icon;
    }

    public void OnDrag(PointerEventData touch)
    {
        transform.position = _camera.ScreenPointToRay(touch.position).origin;        
    }    

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragController.DropItem();        

        AudioManager.Instance.PlayAudio(AudioIndexes.Wrong);
        
        Reset();

        canvasGroup.blocksRaycasts = true;
    }
    
    public void Clear()
    {
        OnDestroyEvent?.Invoke();
    }

    public void Reset()
    {
        transform.SetParent(startParent);
        transform.position = startParent.position;
    }
}

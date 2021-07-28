using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Dont use")]
public class RaycastReader : MonoBehaviour
{
    [SerializeField] private DragAndDrop Parent;
    [SerializeField] private DiamandItem _diamondPrefab;
    private Transform parentInstantiate;
    private ItemSlot slot;

    private void Awake()
    {
        //Parent.SetupList(this);
    }

    public void TakeSlot(Sprite icon)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

        if (hit.collider != null)
        {
            //slot = hit.transform.GetComponent<ItemSlot>();
            if (slot != null)
            {
                CreateDiamond();
                slot.icon = icon;
                parentInstantiate = hit.transform;
            }
        }
    }

    public DiamandItem CreateDiamond()
    {
        DiamandItem current = Instantiate(_diamondPrefab, parentInstantiate);
        slot.SetItemInSlot(current);
        return current;
    }

    public bool IsOverSlot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

        if (hit.collider != null)
        {
            slot = hit.transform.GetComponent<ItemSlot>();
            if (slot != null)
            {
                if (slot.CheckSlot())
                {                                        
                    parentInstantiate = hit.transform;
                    Debug.Log("Raycast work");
                    return true;
                }
                else return false;
            }
            else
                return false;
        }
        else
        {
            Debug.Log("Raycast work no collider");
            return false;
        }
    }
}

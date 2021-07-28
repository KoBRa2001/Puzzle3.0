using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Type type;

    private void Awake()
    {        
        string path = "Icons/" + type.ToString();
        Sprite icon = Resources.Load<Sprite>(path);


        if (gameObject.TryGetComponent<Image>(out var result))
            result.sprite = icon;

        if (gameObject.GetComponent<Image>())
        {
            gameObject.GetComponent<Image>().sprite = icon;
        }
        else if (gameObject.GetComponent<SpriteRenderer>())
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = icon;
        }
    }  
   
    public enum Type
    {
        Red = 0,
        Green = 1,
        Yellow = 2,
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GridController _gridController;
    [SerializeField] Button _retryButton;

    private void Awake()
    {
        Close();
        _retryButton.onClick.AddListener(Retry);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        Close();
        _gridController.NewGame();        
    }
}

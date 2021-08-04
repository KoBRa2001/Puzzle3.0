using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        if (_playButton != null)
        {
            _playButton.onClick.AddListener(Play);
        }
        if (_backButton != null)
        {
            _backButton.onClick.AddListener(Back);
        }
        if (_quitButton != null)
        {
            _quitButton.onClick.AddListener(QuitGame);
        }        
    }    

    public void Play()
    {
        AudioManager.Instance.PlayAudio(AudioIndexes.Click);
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        AudioManager.Instance.PlayAudio(AudioIndexes.Click);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayAudio(AudioIndexes.Click);
        Debug.Log("Quit");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] GridController _gridController;
    [SerializeField] Button _retryButton;
    
    
    [SerializeField] ScoreSystem _scoreSystem;

    [SerializeField] GameObject _score;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _bestScoreText;

    [SerializeField] GameObject _newBestScore;
    [SerializeField] Text _newBestScoreText;    

    private int _bestScore;

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
        _newBestScore.SetActive(false);
        _score.SetActive(false);

        gameObject.SetActive(false);
    }    

    public void GameOver()
    {
        //_bestScore = PlayerPrefs.GetInt("Best", 0);
        Open();
        
        if (_scoreSystem.TryToSaveBestScore())
        {
            _newBestScore.SetActive(true);
            _newBestScoreText.text = _scoreSystem.Score.ToString();
        }
        else
        {
            _score.SetActive(true);
            _scoreText.text = _scoreSystem.Score.ToString();
            _bestScoreText.text = _scoreSystem.BestScore.ToString();
        }
        //if (_scoreSystem._score > _bestScore)
        //{
        //    _bestScore = _scoreSystem._score;
        //    PlayerPrefs.SetInt("Best", _bestScore);
        //    _newBestScore.SetActive(true);
        //    _newBestScoreText.text = _bestScore.ToString();
        //}
        //else
        //{            
        //    _score.SetActive(true);
        //    _scoreText.text = _scoreSystem.score.ToString();
        //    _bestScoreText.text = _bestScore.ToString();
        //}
        //_scoreSystem._score = 0;
    }

    public void Retry()
    {
        AudioManager.Instance.PlayAudio(AudioIndexes.Click);
        Close();
        _gridController.NewGame();
        _scoreSystem.Clear();
    }
}

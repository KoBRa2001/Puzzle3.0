using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int _score = 0;
    private int _bestScore;

    private void Awake()
    {
        _bestScore = PlayerPrefs.GetInt("Best", 0);
    }

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChange?.Invoke(_score);
        }
    }
    
    public int BestScore
    {
        get => _bestScore;        
    }

    public event Action<int> OnScoreChange = null;

    private void Start()
    {
        Clear();
    }

    public void UpdateScore(int points)
    {
        Score += points;
    }

    public bool TryToSaveBestScore()
    {
        //TODO: check and save best score;        

        if (_bestScore < _score)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("Best", _bestScore);
            return true;
        }


        return false;

    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("PlayerPrefs DeleteAll");
            PlayerPrefs.DeleteAll();
        }
    }

    public void Clear()
    {
        Score = 0;
    }

}

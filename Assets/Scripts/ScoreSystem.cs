using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    public int score;

    public void UpdateScore(int points)
    {
        score += points;
        _scoreText.text = score.ToString();//(points + int.Parse(_scoreText.text.ToString())).ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("PlayerPrefs DeleteAll");
            PlayerPrefs.DeleteAll();
        }
    }

    public void Reset()
    {
        _scoreText.text = "0";
        score = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreSystem _scoreSystem = null;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();

        _scoreSystem.OnScoreChange += OnScoreChangeHandler;
    }

    private void OnDestroy()
    {
        _scoreSystem.OnScoreChange -= OnScoreChangeHandler;
    }

    private void OnScoreChangeHandler(int score)
    {
        _text.text = score.ToString();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float _timeLeft;
    [SerializeField] public bool isAnswering;
    [SerializeField] public bool canLoadNextQuestion;

    [SerializeField] private float displayQuestionTime = 7f, displayAnswerTime = 4f;
    [SerializeField] private Image timerImage;

    private void Start()
    {
        timerImage = GetComponent<Image>();
        _timeLeft = displayQuestionTime;
        isAnswering = true;
        canLoadNextQuestion = false;
    }

    public void UpdateTimer()
    {
        _timeLeft -= Time.deltaTime;
        timerImage.fillAmount = (_timeLeft / (isAnswering ? displayQuestionTime : displayAnswerTime));

        if (_timeLeft > 0) return;

        // Timer has run out


        // Toggle state from answering to viewing or vice versa
        if (isAnswering)
        {
            _timeLeft = displayAnswerTime;
            isAnswering = false;
        }
        else
        {
            _timeLeft = displayQuestionTime;
            isAnswering = true;
            canLoadNextQuestion = true;
        }

        // Refill the timer
        timerImage.fillAmount = 1f;
    }

    public void CancelTimer()
    {
        _timeLeft = 0;
    }
}
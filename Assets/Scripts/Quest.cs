using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class Quest : MonoBehaviour
{
    [Header("Questions")] [SerializeField] private List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] private QuestionSO currentQuestion;
    [SerializeField] private TextMeshProUGUI questionText;

    [Header("Answers")] [SerializeField] private GameObject[] options;
    [SerializeField] private int correctOptionIndex;

    [Header("Sprites")] [SerializeField] private Sprite defaultOptionSprite;
    [SerializeField] private Sprite correctOptionSprite;

    [Header("Timer")] [SerializeField] private Timer timer;

    [Header("Score")] [SerializeField] private TextMeshProUGUI score;

    [Header("Progress Bar")] [SerializeField]
    private Slider progressBar;

    private bool _hasAnswered;
    private float _correctAnswers, _questionsSeenSoFar, _totalQuestions;
    [SerializeField] public bool hasSeenAllQuestions;


    // getters
    public int GetNumberOfCorrectAnswers()
    {
        return (int)_correctAnswers;
    }

    public int GetTotalNumberOfQuestions()
    {
        return (int)_totalQuestions;
    }

    private void Start()
    {
        _questionsSeenSoFar = 0;
        _correctAnswers = 0;
        _totalQuestions = questions.Count;
        _hasAnswered = false;
        progressBar.minValue = 0;
        progressBar.maxValue = questions.Count;
        progressBar.wholeNumbers = true;
        progressBar.interactable = false;

        PickRandomQuestion();
        InitializeQuestion();
    }

    private void Update()
    {
        timer.UpdateTimer();
        if (timer.canLoadNextQuestion)
        {
            _hasAnswered = false;
            GetNextQuestion();
            timer.canLoadNextQuestion = false;
        }
        else if (!_hasAnswered && !timer.isAnswering)
        {
            OnSelectOption(-1);
        }

        if (_questionsSeenSoFar > 0)
            score.text = "Score: " + Math.Floor(_correctAnswers / _questionsSeenSoFar * 100) + "%";

        if (!timer.isAnswering && progressBar.value == progressBar.maxValue)
        {
            hasSeenAllQuestions = true;
        }
    }

    private void InitializeQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        questionText.color = Color.cyan;
        for (var i = 0; i < options.Length; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetOption(i);
        }

        correctOptionIndex = currentQuestion.GetCorrectOptionIndex();
        progressBar.value++;
    }

    public void OnSelectOption(int selectedOptionIndex)
    {
        _questionsSeenSoFar++;
        if (selectedOptionIndex == correctOptionIndex)
        {
            DisplayResult(0);
            _correctAnswers++;
        }
        else if (selectedOptionIndex != -1)
        {
            DisplayResult(1);
        }
        else
        {
            DisplayResult(2);
            return;
        }

        _hasAnswered = true;
        timer.CancelTimer();
    }

    private void DisplayResult(int result)
    {
        if (result == 0)
        {
            // correct answer
            questionText.text = "Correct!";
            questionText.color = Color.green;
        }
        else if (result == 1)
        {
            // incorrect answer
            questionText.text = "Incorrect!\n";
            questionText.text +=
                "Answer is: " + options[correctOptionIndex].GetComponentInChildren<TextMeshProUGUI>().text;
            questionText.color = Color.red;
        }
        else
        {
            // no answer
            questionText.text = "Timer ran out!\n";
            questionText.text +=
                "Answer is: " + options[correctOptionIndex].GetComponentInChildren<TextMeshProUGUI>().text;
            questionText.color = Color.magenta;
        }

        Image correctButtonImage = options[correctOptionIndex].GetComponent<Image>();
        correctButtonImage.sprite = correctOptionSprite;
        SetButtonState(false);
    }

    private void SetButtonState(bool canEnable)
    {
        foreach (var buttonObject in options)
        {
            buttonObject.GetComponent<Button>().interactable = canEnable;
        }
    }

    private void GetNextQuestion()
    {
        if (questions.Count == 0) return;
        SetButtonState(true);
        SetDefaultButtonSprites();
        PickRandomQuestion();
        InitializeQuestion();
    }

    private void PickRandomQuestion()
    {
        int index = new Random().Next(0, questions.Count);
        currentQuestion = questions[index];
        questions.RemoveAt(index);
    }

    private void SetDefaultButtonSprites()
    {
        foreach (var buttonObjects in options)
        {
            buttonObjects.GetComponent<Button>().image.sprite = defaultOptionSprite;
        }
    }
}
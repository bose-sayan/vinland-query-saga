using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [Header("Question")] [SerializeField] private QuestionSO question;
    [SerializeField] private TextMeshProUGUI questionText;
    [Header("Answer")] [SerializeField] private GameObject[] options;
    [SerializeField] private int correctOptionIndex;
    [Header("Sprite")] [SerializeField] private Sprite defaultOptionSprite;
    [SerializeField] private Sprite correctOptionSprite;
    [Header("Timer")] [SerializeField] private Timer timer;

    private bool _hasAnswered;


    private void Start()
    {
        InitializeQuestion();
        _hasAnswered = false;
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
    }

    private void InitializeQuestion()
    {
        questionText.text = question.GetQuestion();
        questionText.color = Color.cyan;
        for (var i = 0; i < options.Length; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetOption(i);
        }

        correctOptionIndex = question.GetCorrectOptionIndex();
    }

    public void OnSelectOption(int selectedOptionIndex)
    {
        if (selectedOptionIndex == correctOptionIndex)
        {
            DisplayResult(0);
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
        InitializeQuestion();
        SetButtonState(true);
        SetDefaultButtonSprites();
    }

    private void SetDefaultButtonSprites()
    {
        foreach (var buttonObjects in options)
        {
            buttonObjects.GetComponent<Button>().image.sprite = defaultOptionSprite;
        }
    }
}
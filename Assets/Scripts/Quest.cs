using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private QuestionSO question;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private GameObject[] options;
    [SerializeField] private int correctOption;

    void Start()
    {
        questionText.text = question.GetQuestion();
        for (var i = 0; i < options.Length; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetOption(i);
        }

        correctOption = question.GetCorrectOptionIndex();
    }
}
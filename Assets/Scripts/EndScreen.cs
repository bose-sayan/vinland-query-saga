using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button replayButton;

    void Start()
    {
        var questObject = FindObjectOfType<Quest>();
        scoreText.text += questObject.GetNumberOfCorrectAnswers() + "/" + questObject.GetTotalNumberOfQuestions();
    }
}
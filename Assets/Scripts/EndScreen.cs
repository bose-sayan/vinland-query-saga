using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Quest questObject;

    void Awake()
    {
        questObject = FindObjectOfType<Quest>();
    }

    public void UpdateScore()
    {
        scoreText.text = "Your Score: " + questObject.GetNumberOfCorrectAnswers() + "/" +
                         questObject.GetTotalNumberOfQuestions();
    }

    public void OnReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
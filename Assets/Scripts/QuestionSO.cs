using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest Questions", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)] [SerializeField] private string question = "Enter New Question";
    [SerializeField] private string[] options = new string[4];
    [SerializeField] private int correctOption;

    public string GetQuestion()
    {
        return question;
    }

    public string GetOption(int index)
    {
        return options[index];
    }

    public int GetCorrectOptionIndex()
    {
        return correctOption;
    }
}
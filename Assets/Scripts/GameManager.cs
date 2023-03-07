using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Quest _quest;
    private EndScreen _endScreen;

    private void Awake()
    {
        _quest = FindObjectOfType<Quest>();
        _endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        _quest.gameObject.SetActive(true);
        _endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_quest.hasSeenAllQuestions)
        {
            _quest.gameObject.SetActive(false);
            _endScreen.gameObject.SetActive(true);
            _endScreen.UpdateScore();
        }
    }
}
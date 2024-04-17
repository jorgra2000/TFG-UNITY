using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestQuestion : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject panelTestQuestion;
    public TMP_Text questionText;
    public TMP_Text response1Text;
    public TMP_Text response2Text;
    private Chrono chronoScript;
    private QuestionList questionsData = new QuestionList();


    private float currentTime = 0f;
    private float panelActiveTime = 0f;
    private float randomTestQuestTime;
    private int randomQuestion;

    void Start()
    {
        chronoScript = GameObject.Find("Controller").GetComponent<Chrono>();
        randomTestQuestTime = UnityEngine.Random.Range(20,50);

        if (jsonFile != null)
        {
            questionsData = JsonUtility.FromJson<QuestionList>(jsonFile.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!chronoScript.GetFinishedRace())
        {
            if(panelTestQuestion.activeSelf == false)
            {
                if(currentTime > randomTestQuestTime)
                {
                    randomQuestion = (int)UnityEngine.Random.Range(0,questionsData.testQuestions.Length);
                    questionText.text = questionsData.testQuestions[randomQuestion].question;
                    response1Text.text = questionsData.testQuestions[randomQuestion].response1Text;
                    response2Text.text = questionsData.testQuestions[randomQuestion].response2Text;
                    panelTestQuestion.SetActive(true);
                    panelActiveTime = 0f;
                    currentTime = 0;
                }
                else
                {
                    currentTime += Time.deltaTime;
                }
            }
            else
            {
                panelActiveTime += Time.deltaTime;

                if (panelActiveTime > 10f)
                {
                    panelTestQuestion.SetActive(false);
                }
            }           
        }
    }

    public void CheckResponse1()
    {
        if(questionsData.testQuestions[randomQuestion].response1)
        {
            print("Correcto");
            //BUFO DE VELOCIDAD
        }

        panelTestQuestion.SetActive(false);
    }

        public void CheckResponse2()
    {
        if(questionsData.testQuestions[randomQuestion].response2)
        {
            print("Correcto");
            //BUFO DE VELOCIDAD
        }

        panelTestQuestion.SetActive(false);
    }


    [System.Serializable]
    public class Question
    {
        public string question;
        public string response1Text;
        public string response2Text;
        public bool response1;
        public bool response2;
    }

    [System.Serializable]
    public class QuestionList
    {
        public Question[] testQuestions;
    }
}

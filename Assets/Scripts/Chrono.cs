using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chrono : MonoBehaviour
{
    private float currentTime = 0f;
    private int secs, min;
    private bool finishedRace = false;
    private TMP_Text timeText;

    private void Start()
    {
        timeText = GameObject.Find("TimeText").GetComponent<TMP_Text>();
    }

    public void setFinishedRace(bool finishedRace)
    {
        this.finishedRace = finishedRace;
    }

    public bool getFinishedRace()
    {
        return finishedRace;
    }

    void Update()
    {
        if(!finishedRace)
        {
            advanceChrono();
        }

    }

    private void UpdateChrono()
    {
        string formatedTime = string.Format("{0:00}:{1:00}", min, secs);

        timeText.text = formatedTime;
    }

    private void advanceChrono()
    {
        currentTime += Time.deltaTime;

        secs = (int)(currentTime % 60);
        min = (int)((currentTime / 60) % 60);

        UpdateChrono();
    }

}

using UnityEngine;
using TMPro;

public class Chrono : MonoBehaviour
{
    public TMP_Text timeTextFinished;

    private float currentTime = 0f;
    private int secs, min;
    private bool finishedRace = true;
    private TMP_Text timeText;


    private void Start()
    {
        timeText = GameObject.Find("TimeText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(!finishedRace)
        {
            AdvanceChrono();
        }
        else
        {
            timeTextFinished.text = timeText.text;
        }

    }

    private void UpdateChrono()
    {
        string formatedTime = string.Format("{0:0}:{1:00}.{2:00}", min, secs, (int)(currentTime * 100 % 100));

        timeText.text = formatedTime;
    }

    private void AdvanceChrono()
    {
        currentTime += Time.deltaTime;

        secs = (int)(currentTime % 60);
        min = (int)(currentTime / 60 % 60);

        UpdateChrono();
    }

    public void SetFinishedRace(bool finishedRace)
    {
        this.finishedRace = finishedRace;
    }

    public bool GetFinishedRace()
    {
        return finishedRace;
    }

}

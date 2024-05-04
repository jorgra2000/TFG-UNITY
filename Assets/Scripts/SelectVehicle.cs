using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectVehicle : MonoBehaviour
{
    public GameObject car;
    public GameObject bodyCar;
    public Material[] colors;

    private float rotationSpeed = 10f;

    void Start()
    {
        PlayerPrefs.SetString("Color", "Yellow");
    }

    void Update()
    {
        car.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ColorYellow()
    {
        bodyCar.GetComponent<Renderer>().material = colors[0];
        PlayerPrefs.SetString("Color", "Yellow");
    }

    public void ColorBlue()
    {
        bodyCar.GetComponent<Renderer>().material = colors[1];
        PlayerPrefs.SetString("Color", "Blue");
    }

    public void ColorRed()
    {
        bodyCar.GetComponent<Renderer>().material = colors[2];
        PlayerPrefs.SetString("Color", "Red");
    }
}

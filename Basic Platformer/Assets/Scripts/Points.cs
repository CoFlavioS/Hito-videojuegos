using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public Text waveCountdownText;
    public float puntuacion = 0f;

    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("addPoints", 2f, 1f);
    }

    void addPoints()
    {
        puntuacion += 100f;
        waveCountdownText.text = Mathf.Round(puntuacion).ToString();
    }

}

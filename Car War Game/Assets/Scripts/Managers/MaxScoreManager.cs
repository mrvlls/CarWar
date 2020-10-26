using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScoreManager : MonoBehaviour
{
    Text text;                      // Обращаемся к компоненту текст.

    void Awake()
    {
        // Находим компонент текста.
        text = GetComponent<Text>();

        // Устанавливаем текст.
        text.text = "Score: " + PlayerPrefs.GetInt("maxScore");
    }



}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;        // Очки игрока.


    Text text;                      // Обращаемся к компоненту текст.


    void Awake()
    {
        // Находим компонент текста.
        text = GetComponent<Text>();

        // Обнуляем очки.
        score = 0;
    }


    void Update()
    {
        // Устанавливаем текст.
        text.text = "Score: " + score;
    }
}

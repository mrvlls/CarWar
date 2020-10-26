using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}

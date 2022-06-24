using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI _resultOfGame;
    public static TextMeshProUGUI resultOfGame;
    
    [SerializeField] GameObject endScreen;

    public static void RenderText(string result)
    {
        if (result == "Win")
        {
            resultOfGame.text = "YOU COOL, UWU";
        }
        else
        {
            resultOfGame.text = "YOU WOMANIZER >:(";
        }
    }

    public static void End(string total)
    {
        endScreen.SetActive(true);
        RenderText(total);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    private void Start()
    {
        resultOfGame = _resultOfGame;
    }
}

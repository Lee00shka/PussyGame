using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI _resultOfGame;
    private static TextMeshProUGUI resultOfGame;
    
    [SerializeField] GameObject _endScreen;
    private static GameObject endScreen;
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

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        Global.numOfEnchanted = 0;
		PlayerController._glasses = false;
    }

    private void Start()
    {
        resultOfGame = _resultOfGame;
        endScreen = _endScreen;
    }
}

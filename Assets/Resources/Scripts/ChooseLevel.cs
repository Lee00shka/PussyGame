using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public int whatLevel;
    public void LevelDownloader(int whatLevel)
    {
        SceneManager.LoadScene(whatLevel);
    }
    
}

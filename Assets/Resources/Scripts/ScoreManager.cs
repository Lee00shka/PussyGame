using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int numOfPussies;
    public static int numOfEnchanted = 0;
    private TextMeshPro score;

    private void Start()
    {
        score = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        score.text = "CHARMED: " + numOfEnchanted.ToString() + "/" + numOfPussies.ToString();
    }
}

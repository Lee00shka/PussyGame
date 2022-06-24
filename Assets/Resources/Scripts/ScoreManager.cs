using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int numOfPussies;
    public static int numOfEnchanted = 0;
    public TextMesh score;

    private void Start()
    {
        score = GetComponent<TextMesh>();
    }
    private void Update()
    {
        score.text = "CHARMED: " + numOfEnchanted.ToString() + "/" + numOfPussies.ToString();
    }
}

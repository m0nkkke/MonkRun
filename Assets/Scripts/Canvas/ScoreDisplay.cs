using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.Instance.Score.ToString() + " ì.";
    }
}

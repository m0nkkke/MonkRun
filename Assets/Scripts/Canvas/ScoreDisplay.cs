using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.Instance.Score.ToString() + "ì";
    }
}

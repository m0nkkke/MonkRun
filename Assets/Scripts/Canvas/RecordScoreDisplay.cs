using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.Instance.gameData.MaxScore.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettings : MonoBehaviour
{
    public GameObject PausePanel;

    public void PauseButtonPessed()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ContinueButtonPressed()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ExitInMenu()
    {
        GameManager.Instance.Restart();
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}

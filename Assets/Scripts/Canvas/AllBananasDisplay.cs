using TMPro;
using UnityEngine;

public class AllBananasDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bananaText;

    // ������� ��� ����� 
    public static AllBananasDisplay Instance;

    private void Awake()
    {
        Instance = this;
    }

    // ��������� ����� � ����������� �������
    public void Update()
    {
        bananaText.text = GameManager.Instance.gameData.AllBananas.ToString(); // ���������� ���������� �������
    }

    // ����� ��� ���������� ������� (���� ��� ����� �������� ���������� �������)
    public void UpdateBananasDisplay()
    {
        // ��������� ����� � ���������� ����������� �������
        bananaText.text = GameManager.Instance.gameData.AllBananas.ToString();

        // ��������� ���������� ������� � PlayerPrefs, ����� ��� �����������
        PlayerPrefs.SetInt("Bananas", GameManager.Instance.gameData.AllBananas);
        PlayerPrefs.Save(); // ��������� ��������� � PlayerPrefs
    }
}

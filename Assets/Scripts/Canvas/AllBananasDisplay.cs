using TMPro;
using UnityEngine;

public class AllBananasDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bananaText;

    // Добавил эти штуки 
    public static AllBananasDisplay Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Обновляем текст с количеством бананов
    public void Update()
    {
        bananaText.text = GameManager.Instance.gameData.AllBananas.ToString(); // Отображаем количество бананов
    }

    // Метод для обновления бананов (если вам нужно вызывать обновление вручную)
    public void UpdateBananasDisplay()
    {
        // Обновляем текст с актуальным количеством бананов
        bananaText.text = GameManager.Instance.gameData.AllBananas.ToString();

        // Сохраняем количество бананов в PlayerPrefs, чтобы оно сохранялось
        PlayerPrefs.SetInt("Bananas", GameManager.Instance.gameData.AllBananas);
        PlayerPrefs.Save(); // Сохраняем изменения в PlayerPrefs
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public static ShopUIManager Instance;

    [Header("UI")]
    public GameObject confirmWindow; // Окно подтверждения
    public GameObject notEnoughWindow; // Окно недостаточно средств

    [Header("Confirm Window Elements")]
    public TMP_Text confirmText; // Текст в окне подтверждения
    public Button yesButton; // Кнопка "Да"
    public Button noButton; // Кнопка "Нет"
    public Button ponButton; // Кнопка "Пон" для закрытия окна недостаточности средств

    private ShopItemView _currentItem; // Текущий выбранный скин

    private void Awake()
    {
        Instance = this;

        // Привязываем обработчики к кнопкам
        yesButton.onClick.AddListener(OnConfirmPurchase); // Нажатие "Да"
        noButton.onClick.AddListener(CloseConfirmWindow); // Нажатие "Нет"
        ponButton.onClick.AddListener(HideNotEnoughWindow); // Нажатие на "Пон"
    }

    // Показываем окно подтверждения покупки
    public void ShowConfirmWindow(ShopItemView itemView)
    {
        _currentItem = itemView;
        confirmText.text = $"Приобрести {_currentItem.Item.Name} за {_currentItem.Price} бананов?"; // Задаем текст
        confirmWindow.SetActive(true); // Показываем окно
    }

    // Метод подтверждения покупки
    public void OnConfirmPurchase()
    {
        int currentBananas = GameManager.Instance.gameData.AllBananas;

        if (currentBananas >= _currentItem.Price) // Проверяем, хватает ли бананов
        {
            // Если хватает бананов, покупаем скин
            GameManager.Instance.gameData.AllBananas -= _currentItem.Price; // Списываем бананы из базы данных

            // Сохраняем обновленное количество бананов в PlayerPrefs
            PlayerPrefs.SetInt("Bananas", GameManager.Instance.gameData.AllBananas);
            PlayerPrefs.Save(); // Сохраняем изменения

            _currentItem.Unlock(); // Разблокируем скин
            _currentItem.ToggleSelection(ShopPanel.Instance.GetShopItems()); // Выбираем скин

            // Обновляем отображение количества бананов
            AllBananasDisplay.Instance.Update();

            // Сохраняем информацию о том, что скин куплен
            SavePurchasedSkin(_currentItem);
        }
        else
        {
            ShowNotEnoughWindow(); // Если не хватает средств, показываем окно с ошибкой
        }

        CloseConfirmWindow(); // Закрываем окно подтверждения
    }

    // Закрытие окна подтверждения
    public void CloseConfirmWindow() => confirmWindow.SetActive(false);

    // Показываем окно о нехватке бананов
    public void ShowNotEnoughWindow()
    {
        notEnoughWindow.SetActive(true); // Показываем окно
    }

    // Скрытие окна о нехватке бананов
    public void HideNotEnoughWindow() => notEnoughWindow.SetActive(false);

    // Метод для сохранения информации о покупке скина
    private void SavePurchasedSkin(ShopItemView itemView)
    {
        if (itemView.Item is CharacterSkinsItem characterSkinItem)
        {
            PlayerPrefs.SetString("PurchasedSkin_" + characterSkinItem.SkinType.ToString(), "true");
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Не удается привести объект к типу CharacterSkinsItem");
        }
    }
}

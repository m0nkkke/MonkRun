using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopPanel : MonoBehaviour
{
    public static ShopPanel Instance;  // Добавляем Instance

    public event Action<ShopItemView> ItemViewClicked; // Событие для клика на скин
    private List<ShopItemView> _shopItems = new List<ShopItemView>(); // Список скинов

    [SerializeField] private Transform _itemsParent; // Место, где будут размещаться скины
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory; // Фабрика для создания скинов

    private OpenSkinsChecker _openSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    // Инициализация с проверками
    public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _openSkinsChecker = openSkinsChecker;
        _selectedSkinChecker = selectedSkinChecker;
    }

    private void Awake()
    {
        Instance = this; // Инициализация Instance
    }

    // Метод для отображения скинов
    public void Show(IEnumerable<ShopItem> items)
    {
        Debug.Log("Отображаем скины на панели...");

        Clear(); // Очищаем панель от старых скинов

        foreach (ShopItem item in items)
        {
            if (item == null)
            {
                Debug.LogError("Найден пустой элемент в списке скинов.");
                continue; // Пропускаем пустые элементы
            }

            if (item is CharacterSkinsItem characterSkinItem)
            {
                ShopItemView spawnedItem = _shopItemViewFactory.Get(characterSkinItem, _itemsParent);
                spawnedItem.Click += OnItemViewClick; // Привязываем событие клика
                spawnedItem.Unselect();  // Убираем выделение скина
                spawnedItem.UnHighlight(); // Убираем подсветку скина

                // Проверяем, что item правильно инициализирован
                if (spawnedItem.Item != null)
                {
                    try
                    {
                        // Проверяем скин
                        if (spawnedItem.Item is CharacterSkinsItem characterSkinsItemInner)
                        {
                            Debug.Log($"Скин {characterSkinsItemInner.SkinType} проверяется.");
                            _openSkinsChecker.Visit(characterSkinsItemInner);

                            if (_openSkinsChecker.IsOpened)
                            {
                                spawnedItem.Unlock(); // Разблокируем скин
                            }
                            else
                            {
                                spawnedItem.Lock(); // Скин заблокирован
                            }
                        }
                        else
                        {
                            Debug.LogError($"Item в spawnedItem не является типом CharacterSkinsItem. Тип: {spawnedItem.Item.GetType().Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Ошибка при проверке скина: {ex.Message}");
                    }
                }
                else
                {
                    Debug.LogError("Item в spawnedItem равен null.");
                }

                _shopItems.Add(spawnedItem); // Добавляем скин в список
            }
        }

        LoadSelectedSkin(); // Загружаем выбранный скин
    }

    // Метод для выделения скина
    public void Select(ShopItemView itemView)
    {
        // Снимаем выделение с предыдущего скина, если он был выбран
        foreach (var item in _shopItems)
        {
            if (item != itemView)
            {
                item.Unselect(); // Сбрасываем выбор с других скинов
            }
        }

        itemView.ToggleSelection(_shopItems); // Переключаем состояние галочки и фона
        SaveSelectedSkin(itemView); // Сохраняем выбранный скин
    }

    // Метод для обработки кликов по скинам
    private void OnItemViewClick(ShopItemView itemView)
    {
        Select(itemView); // Вызываем выделение скина
        ItemViewClicked?.Invoke(itemView); // Вызываем событие для выбранного скина
    }

    // Сохранение выбранного скина
    private void SaveSelectedSkin(ShopItemView itemView)
    {
        if (itemView.Item is CharacterSkinsItem characterSkinItem)
        {
            // Сохраняем тип скина в PlayerPrefs
            PlayerPrefs.SetString("SelectedSkin", characterSkinItem.SkinType.ToString());
            PlayerPrefs.Save();
        }
    }

    // Загрузка состояния выбранного скина
    private void LoadSelectedSkin()
    {
        // Получаем тип выбранного скина из PlayerPrefs
        string selectedSkinType = PlayerPrefs.GetString("SelectedSkin", string.Empty);

        if (string.IsNullOrEmpty(selectedSkinType))
        {
            // Если сохраненного состояния нет, выбираем BaseMonk по умолчанию
            SetDefaultSkin();
        }
        else
        {
            foreach (var item in _shopItems)
            {
                if (item.Item is CharacterSkinsItem characterSkinItem && characterSkinItem.SkinType.ToString() == selectedSkinType)
                {
                    item.ToggleSelection(_shopItems); // Выбираем сохраненный скин
                    break;
                }
            }
        }
    }

    // Устанавливаем BaseMonk по умолчанию
    private void SetDefaultSkin()
    {
        foreach (var item in _shopItems)
        {
            if (item.Item is CharacterSkinsItem characterSkinItem && characterSkinItem.SkinType == CharacterSkins.BaseMonk)
            {
                item.ToggleSelection(_shopItems); // Выбираем BaseMonk по умолчанию
                SaveSelectedSkin(item); // Сохраняем выбранный скин
                break;
            }
        }
    }

    // Очистка старых скинов
    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.Click -= OnItemViewClick; // Убираем обработчик клика
            Destroy(item.gameObject); // Удаляем старый скин
        }

        _shopItems.Clear(); // Очищаем список скинов
    }

    public List<ShopItemView> GetShopItems()
    {
        return _shopItems;
    }
}

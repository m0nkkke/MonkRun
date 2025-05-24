using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;
    [SerializeField] private IntValueView _priceView;
    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _selectionItem; // Галочка
    [SerializeField] private Image _bananaIcon; // Иконка банана
    [SerializeField] private TextMeshProUGUI _priceText; // Текст с ценой
    [SerializeField] private GameObject _priceContainer; // Контейнер для цены и иконки банана
    [SerializeField] private Renderer _renderer; // Рендер для изменения материала

    private Image _backgroundImage;
    private GameObject _unamedObject; // Ссылка на объект unamed

    public ShopItem Item { get; private set; }
    public bool IsLock { get; private set; }
    public int Price => Item.Price;
    public bool IsSelected { get; private set; } // Флаг выбранного скина

    public event Action<ShopItemView> Click;

    public void Initialize(ShopItem item)
    {
        Item = item;
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;

        _contentImage.sprite = item.Icon;
        _priceView.Show(item.Price);
        _priceText.text = item.Price.ToString();


        IsLock = !item.IsUnlocked;
        _lockImage.gameObject.SetActive(IsLock);

        IsSelected = false; // Изначально не выбран
        _selectionItem.enabled = false; // Галочка скрыта

        if (IsLock)
        {
            _priceContainer.SetActive(true); // Контейнер для иконки и цены виден, если скин заблокирован
        }
        else
        {
            _priceContainer.SetActive(false); // Контейнер скрыт, если скин разблокирован
        }

        // Найдем объект unamed для обновления материала
        _unamedObject = GameObject.Find("Background/monkOnMenu/unamed");
        if (_unamedObject != null)
        {
            _renderer = _unamedObject.GetComponent<Renderer>(); // Получаем рендер объекта unamed
        }
    }

    // Метод срабатывает при нажатии на скин
    // Метод срабатывает при нажатии на скин
    public void OnPointerClick(PointerEventData eventData)
    {
        // Если скин уже выбран, не делаем никаких изменений
        if (IsSelected)
        {
            return;
        }

        if (IsLock)
        {
            ShopUIManager.Instance.ShowConfirmWindow(this); // Показываем окно подтверждения, если скин заблокирован
        }
        else
        {
            Click?.Invoke(this); // Вызываем событие клика для разблокированного скина
        }
    }

    public void ToggleSelection(List<ShopItemView> allItems)
    {
        if (IsSelected)
        {
            Unselect(); // Если уже выбран, отменяем выбор
        }
        else
        {
            UnselectAllOthers(allItems); // Снимаем выделение с других скинов
            Highlight(); // Меняем фон на выделенный
            _selectionItem.enabled = true; // Показываем галочку

            // Применяем материал только если скин выбран
            if (_renderer != null && Item.SkinMaterial != null)
            {
                _renderer.material = Item.SkinMaterial; // Применяем материал при выборе
                GameManager.Instance.SaveSkinMaterial(Item.SkinMaterial);
                if (_renderer != null)
                {
                    // Загружаем материал по имени из PlayerPrefs или из ресурсов
                    string materialName = PlayerPrefs.GetString("SelectedSkinMaterial", string.Empty);
                    if (!string.IsNullOrEmpty(materialName))
                    {
                        Material material = Resources.Load<Material>("Materials/" + materialName);
                        if (material != null)
                        {
                            _renderer.material = material; // Применяем материал при выборе
                        }
                        else
                        {
                            Debug.LogError("Не удалось найти материал: " + materialName);
                        }
                    }
                }
            }
        }

        IsSelected = !IsSelected; // Переключаем флаг выбранного скина
    }

    // Снимаем выделение с других скинов
    public void UnselectAllOthers(List<ShopItemView> allItems)
    {
        foreach (var item in allItems)
        {
            if (item != this) // Если это не текущий скин
            {
                item.Unselect(); // Снимаем выделение
            }
        }
    }

    // Снимаем выделение с других скинов
    private void UnselectAllOthers()
    {
        foreach (var item in ShopPanel.Instance.GetShopItems())
        {
            if (item != this) // Если это не текущий скин
            {
                item.Unselect(); // Снимаем выделение
            }
        }
    }

    public void Unlock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(false);
        Item.Unlock(); // Разблокируем скин

        // Скрываем весь контейнер с ценой и бананом, если скин разблокирован
        _priceContainer.SetActive(false);
    }

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(true);

        // Показываем весь контейнер с ценой и бананом, если скин заблокирован
        _priceContainer.SetActive(true);
    }

    public void Highlight()
    {
        _backgroundImage.sprite = _highlightBackground;
    }

    public void UnHighlight()
    {
        _backgroundImage.sprite = _standartBackground;
    }

    public void Unselect()
    {
        IsSelected = false;
        _selectionItem.enabled = false; // Убираем галочку
        _backgroundImage.sprite = _standartBackground; // Возвращаем обычный фон
    }
}

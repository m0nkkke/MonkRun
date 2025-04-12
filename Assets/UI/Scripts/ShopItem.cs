using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string Name;                // �������� �����
    public int Price;                  // ���� �����
    public bool IsUnlocked;            // ������������� �� ����
    public Sprite Icon;                // ������ �����
    public Material SkinMaterial;      // �������� �����, ������� �� ����� �����������

    // ����� ��� ������������� �����
    public void Unlock() => IsUnlocked = true;
}


//public abstract class ShopItem : ScriptableObject
//{
//    public string DisplayName => _name;

//    [SerializeField] private string _name;
//    [field: SerializeField] public GameObject Model { get; private set; }
//    [field: SerializeField] public Sprite Image { get; private set; }
//    [field: SerializeField, Range(0, 10000)] public int Price { get; private set; }
//}

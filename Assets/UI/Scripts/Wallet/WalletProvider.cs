using UnityEngine;

public class WalletProvider : MonoBehaviour
{
    public static WalletProvider Instance;

    public Wallet Wallet;  // Текущий кошелек

    [SerializeField] private PersistentData _persistentData;

    private void Awake()
    {
        Instance = this;
        Wallet = new Wallet(_persistentData);
    }
}

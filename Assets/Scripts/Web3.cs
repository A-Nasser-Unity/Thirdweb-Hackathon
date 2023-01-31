using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Web3 : MonoBehaviour
{
    private ThirdwebSDK sdk;

    public static string selectedWallet;

    public static string balance;

    async void OnEnable()
    {
        sdk = new ThirdwebSDK("mumbai");

        await Metamask();

        if (await sdk.wallet.IsConnected())
        {
            LoadInfo();
        }
    }

    private void LoadInfo()
    {
        LoadBalance();
    }

    public async void ConnectWallet(string provider)
    {
        await Metamask();
        LoadInfo();
    }

    public async Task<string> Metamask()
    {
        string address =
            await sdk
                .wallet
                .Connect(new WalletConnection()
                {
                    provider = WalletProvider.MetaMask,
                    chainId = 80001 // Switch the wallet Goerli network on connection
                });

        selectedWallet = "Metamask";
        return address;
    }

    public async void LoadBalance()
    {
        var bal = await GetTokenDrop().ERC20.Balance();

        // Set balance text
        balance = bal.displayValue + " " + bal.symbol;
    }

    private Contract GetTokenDrop()
    {
        return sdk.GetContract("0xDcf4E5f969c69F54Ef761c7A4AD21315F1281AA6");
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Thirdweb;
using System;
using TMPro;
using UnityEngine.UI;
using PlayMaker;

public enum Wallet
{
    MetaMask,
    CoinbaseWallet,
    WalletConnect,
    MagicAuth,
}

[Serializable]
public struct WalletButton
{
    public Wallet wallet;
    public GameObject walletButton;
    public Sprite icon;
}

[Serializable]
public struct NetworkSprite
{
    public Chain chain;
    public Sprite sprite;
}

public class Prefab_ConnectWallet : MonoBehaviour
{

    [Header("PLAYMAKER")]
    public PlayMakerFSM myFsm;

    [Header("SETTINGS")]
    public List<Wallet> supportedWallets = new List<Wallet> { Wallet.MetaMask, Wallet.CoinbaseWallet, Wallet.WalletConnect };
    public bool supportSwitchingNetwork = false;

    [Header("UI ELEMENTS (DO NOT EDIT)")]
    // Connecting
    public GameObject connectButton;
    public GameObject connectDropdown;
    public List<WalletButton> walletButtons;
    // Connected
    public GameObject connectedButton;
    public GameObject connectedDropdown;
    public TMP_Text balanceText;
    public TMP_Text walletAddressText;
    public TMP_Text goldTokenBalance;
    public TMP_Text nftSkinPrice;
    public Image walletImage;
    public TMP_Text currentNetworkText;
    public Image currentNetworkImage;
    public Image chainImage;
    // Network Switching
    public GameObject networkSwitchButton;
    public GameObject networkDropdown;
    public GameObject networkButtonPrefab;
    public List<NetworkSprite> networkSprites;

    string address;
    Wallet wallet;


    // UI Initialization

    private void Start()
    {
        address = null;

        if (supportedWallets.Count == 1)
            connectButton.GetComponent<Button>().onClick.AddListener(() => OnConnect(supportedWallets[0]));
        else
            connectButton.GetComponent<Button>().onClick.AddListener(() => OnClickDropdown());


        foreach (WalletButton wb in walletButtons)
        {
            if (supportedWallets.Contains(wb.wallet))
            {
                wb.walletButton.SetActive(true);
                wb.walletButton.GetComponent<Button>().onClick.AddListener(() => OnConnect(wb.wallet));
            }
            else
            {
                wb.walletButton.SetActive(false);
            }
        }

        connectButton.SetActive(true);
        connectedButton.SetActive(false);

        connectDropdown.SetActive(false);
        connectedDropdown.SetActive(false);

        networkSwitchButton.SetActive(supportSwitchingNetwork);
        networkDropdown.SetActive(false);
    }

    public async void setGoldBalance()
    {
        goldTokenBalance.text = await getGoldBalance();
    }

    // get Gold Token Balance

    public async Task<string> getGoldBalance()
    {
        var bal = await GetTokenDrop().ERC20.Balance();
        return bal.displayValue;
    }

    public async void giveGoldBalance(string amount)
    {
        await GetTokenDrop().ERC20.Claim(amount);
        setGoldBalance();
    }

    private Contract GetTokenDrop()
    {
        return ThirdwebManager.Instance.SDK.GetContract("0xDcf4E5f969c69F54Ef761c7A4AD21315F1281AA6");
    }

    public async Task<bool> ownSkins(string tokenId)
    {
        // First, check to see if the you own the NFT
        var owned = await GetEdition().ERC1155.GetOwned();

        // if owned contains a token with the same ID as the listing, then you own it
        bool ownsNft = owned.Exists(nft => nft.metadata.id == tokenId);

        PlayMakerGlobals.Instance.Variables.FindFsmBool("PLAYERHAVESKIN1").Value = ownsNft;

        return ownsNft;
        
    }

    private Contract GetEdition()
    {
        return ThirdwebManager.Instance.SDK.GetContract("0xF21331109Bc1CC89cF2841D70E1349F05047408B");
    }

    public async void setNftSkinPrice(string listingId)
    {
        nftSkinPrice.text = await getNftSkinPrice(listingId);
    }

    public async Task<string> getNftSkinPrice(string listingId)
    {
        var price = await GetMarketplace().GetListing(listingId);
        return price.buyoutCurrencyValuePerToken.displayValue;
    }

    public async void buySkin(string listingId)
    {
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING").Value = true;

        var result = await GetMarketplace().BuyListing(listingId, 1);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful").Value = false;
        }

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING").Value = false;
    }

    private Marketplace GetMarketplace()
    {
        return ThirdwebManager.Instance.SDK
            .GetContract("0xe54e380E4Eeab4FD34CB8cBcD285613e2a84f0C2")
            .marketplace;
    }

    // Connecting

    public async void OnConnect(Wallet _wallet)
    {
        try
        {
            address = await ThirdwebManager.Instance.SDK.wallet.Connect(
               new WalletConnection()
               {
                   provider = GetWalletProvider(_wallet),
                   chainId = (int)ThirdwebManager.Instance.chain,
               });

            wallet = _wallet;
            OnConnected();
            print($"Connected successfully to: {address}");
        }
        catch (Exception e)
        {
            print($"Error Connecting Wallet: {e.Message}");
        }
    }

    async void OnConnected()
    {
        try
        {
            Chain _chain = ThirdwebManager.Instance.chain;
            CurrencyValue nativeBalance = await ThirdwebManager.Instance.SDK.wallet.GetBalance();
            balanceText.text = $"{nativeBalance.value.ToEth()} {nativeBalance.symbol}";
            walletAddressText.text = address.ShortenAddress();

            setGoldBalance();
            setNftSkinPrice("2");
            ownSkins("0");

            currentNetworkText.text = ThirdwebManager.Instance.chainIdentifiers[_chain];
            currentNetworkImage.sprite = networkSprites.Find(x => x.chain == _chain).sprite;
            connectButton.SetActive(false);
            connectedButton.SetActive(true);
            connectDropdown.SetActive(false);
            connectedDropdown.SetActive(false);
            networkDropdown.SetActive(false);
            walletImage.sprite = walletButtons.Find(x => x.wallet == wallet).icon;
            chainImage.sprite = networkSprites.Find(x => x.chain == _chain).sprite;
        }
        catch (Exception e)
        {
            print($"Error Fetching Native Balance: {e.Message}");
        }

    }

    // Disconnecting

    public async void OnDisconnect()
    {
        try
        {
            await ThirdwebManager.Instance.SDK.wallet.Disconnect();
            OnDisconnected();
            print($"Disconnected successfully.");

        }
        catch (Exception e)
        {
            print($"Error Disconnecting Wallet: {e.Message}");
        }
    }

    void OnDisconnected()
    {
        address = null;
        connectButton.SetActive(true);
        connectedButton.SetActive(false);
        connectDropdown.SetActive(false);
        connectedDropdown.SetActive(false);
    }

    // Switching Network

    public async void OnSwitchNetwork(Chain _chain)
    {

        try
        {
            ThirdwebManager.Instance.chain = _chain;
            await ThirdwebManager.Instance.SDK.wallet.SwitchNetwork((int)_chain);
            OnConnected();
            print($"Switched Network Successfully: {_chain}");

        }
        catch (Exception e)
        {
            print($"Error Switching Network: {e.Message}");
        }
    }

    // UI

    public void OnClickDropdown()
    {
        if (String.IsNullOrEmpty(address))
            connectDropdown.SetActive(!connectDropdown.activeInHierarchy);
        else
            connectedDropdown.SetActive(!connectedDropdown.activeInHierarchy);
    }

    public void OnClickNetworkSwitch()
    {
        if (networkDropdown.activeInHierarchy)
        {
            networkDropdown.SetActive(false);
            return;
        }

        networkDropdown.SetActive(true);

        foreach (Transform child in networkDropdown.transform)
            Destroy(child.gameObject);

        foreach (Chain chain in Enum.GetValues(typeof(Chain)))
        {
            if (chain == ThirdwebManager.Instance.chain || !ThirdwebManager.Instance.supportedNetworks.Contains(chain))
                continue;

            GameObject networkButton = Instantiate(networkButtonPrefab, networkDropdown.transform);
            networkButton.GetComponent<Button>().onClick.RemoveAllListeners();
            networkButton.GetComponent<Button>().onClick.AddListener(() => OnSwitchNetwork(chain));
            networkButton.transform.Find("Text_Network").GetComponent<TMP_Text>().text = ThirdwebManager.Instance.chainIdentifiers[chain];
            networkButton.transform.Find("Icon_Network").GetComponent<Image>().sprite = networkSprites.Find(x => x.chain == chain).sprite;
        }
    }

    // Utility

    WalletProvider GetWalletProvider(Wallet _wallet)
    {
        switch (_wallet)
        {
            case Wallet.MetaMask:
                return WalletProvider.MetaMask;
            case Wallet.CoinbaseWallet:
                return WalletProvider.CoinbaseWallet;
            case Wallet.WalletConnect:
                return WalletProvider.WalletConnect;
            case Wallet.MagicAuth:
                return WalletProvider.MagicAuth;
            default:
                throw new UnityException($"Wallet Provider for wallet {_wallet} unimplemented!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Thirdweb;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

public class User
{
    public string address;
    public int DIFFICULTY;
    public int DOUBLEAMMO;
    public int POPULATION;
    public int TOWERUPGRADE;
    public int MINEAMMO;
    public int WAVES;
}

public class Web3 : MonoBehaviour
{
    string adminAddress = "0x0de82DCC40B8468639251b089f8b4A4400022e04";
    string baseUrl = "https://rp-3-next-mongo.vercel.app/api/";
    // difficulty
    public async void getDifficulty()
    {
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "USERS/" + address;
        using(var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                User user = JsonUtility.FromJson<User>(json);
                if(user != null)
                {
                    int value = user.DIFFICULTY;
                    PlayMakerGlobals.Instance.Variables.FindFsmInt("DIFFICULTY").Value = value;
                }
            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    public async void updateDifficulty()
    {
        int value = PlayMakerGlobals.Instance.Variables.FindFsmInt("DIFFICULTY").Value;
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "DIFFICULTY";

        var data = new Dictionary<string, dynamic>{
            {"address", address },
            {"DIFFICULTY",value}
        };
        var content = JsonConvert.SerializeObject(data);
        var contentData = new StringContent(content, Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync(url, contentData);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    // double ammo
    public async void getDoubleAmmo()
    {
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "USERS/" + address;
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                User user = JsonUtility.FromJson<User>(json);
                if (user != null)
                {
                    int value = user.DOUBLEAMMO;
                    PlayMakerGlobals.Instance.Variables.FindFsmInt("DOUBLEAMMO").Value = value;
                }
            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    public async void updateDoubleAmmo()
    {
        int value = PlayMakerGlobals.Instance.Variables.FindFsmInt("DOUBLEAMMO").Value;
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "DOUBLEAMMO";

        var data = new Dictionary<string, dynamic>{
            {"address", address },
            {"DOUBLEAMMO",value}
        };
        var content = JsonConvert.SerializeObject(data);
        var contentData = new StringContent(content, Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync(url, contentData);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    // population
    public async void getPopulation()
    {
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "USERS/" + address;
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                User user = JsonUtility.FromJson<User>(json);
                if (user != null)
                {
                    int value = user.POPULATION;
                    PlayMakerGlobals.Instance.Variables.FindFsmInt("POPULATION").Value = value;
                }
            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    public async void updatePopulation()
    {
        int value = PlayMakerGlobals.Instance.Variables.FindFsmInt("POPULATION").Value;
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "POPULATION";

        var data = new Dictionary<string, dynamic>{
            {"address", address },
            {"POPULATION",value}
        };
        var content = JsonConvert.SerializeObject(data);
        var contentData = new StringContent(content, Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync(url, contentData);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    // tower upgrade
    public async void getTowerUpgrade()
    {
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "USERS/" + address;
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                User user = JsonUtility.FromJson<User>(json);
                if (user != null)
                {
                    int value = user.TOWERUPGRADE;
                    PlayMakerGlobals.Instance.Variables.FindFsmInt("TOWERUPGRADE").Value = value;
                }
            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    public async void updateTowerUpgrade()
    {
        int value = PlayMakerGlobals.Instance.Variables.FindFsmInt("TOWERUPGRADE").Value;
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "TOWERUPGRADE";

        var data = new Dictionary<string, dynamic>{
            {"address", address },
            {"TOWERUPGRADE",value}
        };
        var content = JsonConvert.SerializeObject(data);
        var contentData = new StringContent(content, Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync(url, contentData);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    // mine ammo
    public async void getMineAmmo()
    {
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "USERS/" + address;
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                User user = JsonUtility.FromJson<User>(json);
                if (user != null)
                {
                    int value = user.MINEAMMO;
                    PlayMakerGlobals.Instance.Variables.FindFsmInt("MINEAMMO").Value = value;
                }
            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    public async void updateMineAmmo()
    {
        int value = PlayMakerGlobals.Instance.Variables.FindFsmInt("MINEAMMO").Value;
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "MINEAMMO";

        var data = new Dictionary<string, dynamic>{
            {"address", address },
            {"MINEAMMO",value}
        };
        var content = JsonConvert.SerializeObject(data);
        var contentData = new StringContent(content, Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync(url, contentData);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    // Waves
    public async void getWaves()
    {
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "USERS/" + address;
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                User user = JsonUtility.FromJson<User>(json);
                if (user != null)
                {
                    int value = user.WAVES;
                    PlayMakerGlobals.Instance.Variables.FindFsmInt("WAVES").Value = value;
                }
            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }

    public async void updateWaves()
    {
        int value = PlayMakerGlobals.Instance.Variables.FindFsmInt("WAVES").Value;
        string address = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        string url = baseUrl + "WAVES";

        var data = new Dictionary<string, dynamic>{
            {"address", address },
            {"WAVES",value}
        };
        var content = JsonConvert.SerializeObject(data);
        var contentData = new StringContent(content, Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.PostAsync(url, contentData);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                print(response.ReasonPhrase);
            }
        }
    }
    public void initializeVariables()
    {
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING0").Value = true;
        setGoldBalance();
        setDiamondBalance();
        setOwnSkin1();
        setOwnSkin2();
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING0").Value = false;
    }
    public async void setGoldBalance()
    {
        PlayMakerGlobals.Instance.Variables.FindFsmFloat("GOLD").Value = await getGoldBalance();
    }

    public async Task<float> getGoldBalance()
    {
        var bal = await GetGoldTokenDrop().ERC20.Balance();
        float balance = float.Parse(bal.displayValue, System.Globalization.CultureInfo.InvariantCulture);
        return balance;

    }

    public async void giveGoldBalance(string amount)
    {
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING3").Value = true;
        var result = await GetGoldTokenDrop().ERC20.Claim(amount);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful7").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful7").Value = false;
        }

        setGoldBalance();
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING3").Value = false;
    }

    private Contract GetGoldTokenDrop()
    {
        return ThirdwebManager.Instance.SDK.GetContract("0xDcf4E5f969c69F54Ef761c7A4AD21315F1281AA6");
    }
    public async void setDiamondBalance()
    {
        PlayMakerGlobals.Instance.Variables.FindFsmFloat("DIAMOND").Value = await getGoldBalance();
    }

    public async Task<float> getDiamondBalance()
    {
        var bal = await GetDiamondTokenDrop().ERC20.Balance();
        float balance = float.Parse(bal.displayValue, System.Globalization.CultureInfo.InvariantCulture);
        return balance;

    }

    public async void buyDiamondToken(string amount)
    {
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING6").Value = true;
        var result = await GetDiamondTokenDrop().ERC20.Claim(amount);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful8").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful8").Value = false;
        }

        setDiamondBalance();
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING6").Value = false;
    }

    private Contract GetDiamondTokenDrop()
    {
        return ThirdwebManager.Instance.SDK.GetContract("0x27f342e6914733f3D0874156BbDC1016E6C3b2f6");
    }
    public async void decreaseGoldToken(string amount)
    {
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING7").Value = true;
        var result = await GetGoldTokenDrop().ERC20.Transfer(adminAddress, amount);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful5").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful5").Value = false;
        }

        setGoldBalance();
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING7").Value = false;
    }

    public async void decreaseDiamondToken(string amount)
    {
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING8").Value = true;
        var result = await GetDiamondTokenDrop().ERC20.Transfer(adminAddress, amount);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful6").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful6").Value = false;
        }

        setDiamondBalance();
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING8").Value = false;
    }

    public async void setOwnSkin1()
    {
        string tokenId = "0";
        // First, check to see if the you own the NFT
        var owned = await GetEdition().ERC1155.GetOwned();

        // if owned contains a token with the same ID as the listing, then you own it
        bool ownsNft = owned.Exists(nft => nft.metadata.id == tokenId);

        PlayMakerGlobals.Instance.Variables.FindFsmBool("PLAYERHAVESKIN1").Value = ownsNft;
    }
    public async void setOwnSkin2()
    {
        string tokenId = "1";
        // First, check to see if the you own the NFT
        var owned = await GetEdition().ERC1155.GetOwned();

        // if owned contains a token with the same ID as the listing, then you own it
        bool ownsNft = owned.Exists(nft => nft.metadata.id == tokenId);

        PlayMakerGlobals.Instance.Variables.FindFsmBool("PLAYERHAVESKIN2").Value = ownsNft;
    }

    private Contract GetEdition()
    {
        return ThirdwebManager.Instance.SDK.GetContract("0xa3ADb006C64ea277227Da13a6B86df3E6B05bfD2");
    }

    public async void buySkin1Gold()
    {
        string listingId = "0";

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

        setOwnSkin1();

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING").Value = false;
    }

    public async void buySkin2Gold()
    {
        string listingId = "1";

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING2").Value = true;

        var result = await GetMarketplace().BuyListing(listingId, 1);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful2").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful2").Value = false;
        }

        setOwnSkin2();

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING2").Value = false;
    }

    public async void buySkin1Diamond()
    {
        string listingId = "2";

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING4").Value = true;

        var result = await GetMarketplace().BuyListing(listingId, 1);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful3").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful3").Value = false;
        }

        setOwnSkin1();

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING4").Value = false;
    }

    public async void buySkin2Diamond()
    {
        string listingId = "3";

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING5").Value = true;

        var result = await GetMarketplace().BuyListing(listingId, 1);

        var isSuccess = result.isSuccessful();

        if (isSuccess)
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful4").Value = true;
        }
        else
        {
            PlayMakerGlobals.Instance.Variables.FindFsmBool("WasTransactionSuccessful4").Value = false;
        }

        setOwnSkin2();

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING5").Value = false;
    }

    private Marketplace GetMarketplace()
    {
        return ThirdwebManager.Instance.SDK
            .GetContract("0x0b096A838178D22B784599cE6eC23281a71a8430")
            .marketplace;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using System.Threading.Tasks;

public class Web3 : MonoBehaviour
{
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
        await GetGoldTokenDrop().ERC20.Claim(amount);
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
        await GetDiamondTokenDrop().ERC20.Claim(amount);
        setDiamondBalance();
        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING6").Value = false;
    }

    private Contract GetDiamondTokenDrop()
    {
        return ThirdwebManager.Instance.SDK.GetContract("0x27f342e6914733f3D0874156BbDC1016E6C3b2f6");
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

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING2").Value = false;
    }

    public async void buySkin1Diamond()
    {
        string listingId = "0";

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

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING4").Value = false;
    }

    public async void buySkin2Diamond()
    {
        string listingId = "1";

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

        PlayMakerGlobals.Instance.Variables.FindFsmBool("LOADING5").Value = false;
    }

    private Marketplace GetMarketplace()
    {
        return ThirdwebManager.Instance.SDK
            .GetContract("0x0b096A838178D22B784599cE6eC23281a71a8430")
            .marketplace;
    }
}

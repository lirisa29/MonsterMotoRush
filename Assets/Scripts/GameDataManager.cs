using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shop Data Holder
[System.Serializable]
public class CharacterShopData
{
    public List<int> purchasedCharactersIndexes = new List<int>();
}

// Player Data Holder
[System.Serializable]
public class PlayerData
{
    public int coins = 0;
    public int selectedCharacterIndex = 0;
}

public static class GameDataManager
{
    private static PlayerData playerData = new PlayerData();
    private static CharacterShopData characterShopData = new CharacterShopData();

    private static Character selectedCharacter;

    static GameDataManager()
    {
        LoadPlayerData();
        LoadCharacterShopData();
    }
    
    // Player Data Methods
    public static Character GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public static void SetSelectedCharacter(Character character, int index)
    {
        selectedCharacter = character;
        playerData.selectedCharacterIndex = index;
        SavePlayerData();
    }
    
    public static int GetSelectedCharacterIndex()
    {
        return playerData.selectedCharacterIndex;
    }
    
    public static int GetCoins()
    {
        return playerData.coins;
    }

    public static void AddCoins(int amount)
    {
        playerData.coins += amount;
        SavePlayerData();
    }

    public static void LostCoins(int amount)
    {
        playerData.coins -= amount;
        SavePlayerData();
    }
    
    public static bool CanSpendCoins(int amount)
    {
        return (playerData.coins >= amount);
    }

    public static void SpendCoins(int amount)
    {
        playerData.coins -= amount;
        SavePlayerData();
    }

    static void LoadPlayerData()
    {
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
        UnityEngine.Debug.Log("<color=green>[PlayerData] Loaded.</color>");
    }

    static void SavePlayerData()
    {
        BinarySerializer.Save(playerData, "player-data.txt");
        UnityEngine.Debug.Log("<color=magenta>[PlayerData] Saved.</color>");
    }
    
    //Shop Data Methods
    public static void AddPurchasedCharacter(int characterIndex)
    {
        characterShopData.purchasedCharactersIndexes.Add(characterIndex);
        SaveCharacterShopData();
    }
    
    public static List<int> GetAllPurchasedCharacter()
    {
        return characterShopData.purchasedCharactersIndexes;
    }
    
    public static int GetPurchasedCharacter(int index)
    {
        return characterShopData.purchasedCharactersIndexes [index];
    }
    
    static void LoadCharacterShopData()
    {
        characterShopData = BinarySerializer.Load<CharacterShopData>("character-shop-data.txt");
        UnityEngine.Debug.Log("<color=green>[CharacterShopData] Loaded.</color>");
    }

    static void SaveCharacterShopData()
    {
        BinarySerializer.Save(characterShopData, "character-shop-data.txt");
        UnityEngine.Debug.Log("<color=magenta>[CharacterShopData] Saved.</color>");
    }
}

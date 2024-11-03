using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Data Holder
[System.Serializable]
public class PlayerData
{
    public int coins = 0;
}

public static class GameDataManager
{
    private static PlayerData playerData = new PlayerData();

    static GameDataManager()
    {
        LoadPlayerData();
    }
    
    // Player Data Methods
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
}

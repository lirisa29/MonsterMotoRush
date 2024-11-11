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
    public int unlockedLevel = 1;
    public Dictionary<int, float> bestTimes = new Dictionary<int, float>();
}
// Audio Data Holder
[System.Serializable]
public class AudioData
{
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;
    public float bikeVolume = 1.0f;
}

public static class GameDataManager
{
    private static PlayerData playerData = new PlayerData();
    private static CharacterShopData characterShopData = new CharacterShopData();
    private static AudioData audioData = new AudioData();

    private static Character selectedCharacter;

    static GameDataManager()
    {
        LoadPlayerData();
        LoadCharacterShopData();
        LoadAudioVolume();
    }

    // Player Data Methods
    public static int GetUnlockedLevel()
    {
        return playerData.unlockedLevel;
    }

    public static void SetUnlockedLevel(int level)
    {
        playerData.unlockedLevel = level;
        SavePlayerData();
    }

    public static Character GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public static void SetSelectedCharacter(Character character, int index)
    {
        if (character == null)
        {
            Debug.LogError("Attempted to set a null character.");
            return;
        }

        selectedCharacter = character;
        playerData.selectedCharacterIndex = index;
        SavePlayerData();
        Debug.Log($"Character set: {selectedCharacter}, Index: {index}");
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

    public static float GetBestTimeForLevel(int levelIndex)
    {
        if (playerData.bestTimes.ContainsKey(levelIndex))
        {
            return playerData.bestTimes[levelIndex];
        }

        return float.MaxValue;
    }

    public static void SetBestTimeForLevel(int levelIndex, float time)
    {
        if (!playerData.bestTimes.ContainsKey(levelIndex) || time > playerData.bestTimes[levelIndex])
        {
            playerData.bestTimes[levelIndex] = time;
            SavePlayerData();
        }
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
    
    // Audio Data Methods
    public static float GetMusicVolume()
    {
        return audioData.musicVolume;
    }

    public static void SetMusicVolume(float volume)
    {
        audioData.musicVolume = volume;
        SaveAudioVolume();
    }
    
    public static float GetSFXVolume()
    {
        return audioData.sfxVolume;
    }

    public static void SetSFXVolume(float volume)
    {
        audioData.sfxVolume = volume;
        SaveAudioVolume();
    }
    
    public static float GetBikeVolume()
    {
        return audioData.bikeVolume;
    }

    public static void SetBikeVolume(float volume)
    {
        audioData.bikeVolume = volume;
        SaveAudioVolume();
    }

    private static void LoadAudioVolume()
    {
        audioData = BinarySerializer.Load<AudioData>("music-data.txt");
        UnityEngine.Debug.Log("<color=green>[AudioData] Loaded.</color>");
    }

    private static void SaveAudioVolume()
    {
        BinarySerializer.Save(audioData, "music-data.txt");
        UnityEngine.Debug.Log("<color=magenta>[AudioData] Saved.</color>");
    }
}

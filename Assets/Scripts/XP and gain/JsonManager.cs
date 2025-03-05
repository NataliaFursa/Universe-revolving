using System.IO;
using UnityEngine;
using static Upgrade;

public static class JsonManager
{
    public static PlayerData playerData;
    public static PlayerData loadedData;
    public static UpgradeData j_upgrateData;
    
    
    public class PlayerData
    {
        public int XP;
        public int MS; 
        public float HP;
        public int StartMoney;

    }
    
    static void MetaToData(PlayerData data)
    {
        data.XP = Meta.PlayerXP;
        data.MS = Meta.baseMS; 
        data.HP = Meta.baseHP;
        data.StartMoney = Meta.baseMoney;
    }
    static public void UpgradeSaveToJson(UpgradeData upgrateData)
    {
        string json = JsonUtility.ToJson(upgrateData, true); // Сериализация в JSON с отступами
        string path = Path.Combine(Application.persistentDataPath, "upgrateData.json"); // Путь к файлу
    }
    static public UpgradeData UpgradeLoadToJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "upgrateData.json"); // Путь к файлу
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // Чтение JSON строки из файла
            j_upgrateData = JsonUtility.FromJson<UpgradeData>(json); // Десериализация JSON в объект PlayerData
            return j_upgrateData;
        }
        else
        {
            Debug.LogError("Файл не найден: " + path);
            //Meta.SetDefault();
            return null;
        }
    }
    static public void SaveToJson()
    {
        playerData = new PlayerData();
        MetaToData(playerData);
        string json = JsonUtility.ToJson(playerData, true); // Сериализация в JSON с отступами
        string path = Path.Combine(Application.persistentDataPath, "playerData.json"); // Путь к файлу

        File.WriteAllText(path, json); // Запись JSON строки в файл
        Debug.Log("Данные сохранены в: " + path);
    }

    static public void LoadFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerData.json"); // Путь к файлу
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // Чтение JSON строки из файла
            loadedData = JsonUtility.FromJson<PlayerData>(json); // Десериализация JSON в объект PlayerData
            Meta.SetStats(loadedData);
        }
        else
        {
            Debug.LogError("Файл не найден: " + path);
            Meta.SetDefault();
        }
    }
}

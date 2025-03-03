using System.IO;
using UnityEngine;

public static class JsonManager
{
    public static PlayerData playerData;
    public static PlayerData loadedData;
    public class PlayerData
    {
        public int XP = Meta.PlayerXP;
        public int MS = Meta.baseMS; 
        public float HP = Meta.baseHP;
        public int Money = Meta.baseMoney;

    }
    static void SaveToJson(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true); // Сериализация в JSON с отступами
        string path = Path.Combine(Application.persistentDataPath, "playerData.json"); // Путь к файлу

        File.WriteAllText(path, json); // Запись JSON строки в файл
        Debug.Log("Данные сохранены в: " + path);
    }

    static void LoadFromJson()
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
        }
    }
}

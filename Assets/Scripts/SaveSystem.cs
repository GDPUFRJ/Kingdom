using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static bool newGame = true;

    private static string path = Application.persistentDataPath + "/SaveData.kin";

    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(GameManager.Instance, MonoBehaviour.FindObjectOfType<TimerPanel>(), PropertyManager.Instance, KEventManager.Instance, StartingKingdomController.Instance, Camera.main.transform);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found in path: '" + path + "'");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        SaveData saveData = formatter.Deserialize(stream) as SaveData;
        stream.Close();

        return saveData;
    }

    public static void ResetSaveData()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static bool CheckSaveDataExistence()
    {
        return File.Exists(path);
    }
}

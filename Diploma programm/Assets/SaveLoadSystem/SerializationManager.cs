using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;
 
public class SerializationManager: MonoBehaviour
{
    string filePath;

    public List<GameObject> EnemySaves = new List<GameObject>();

    string sceneName;

    LevelManager levelManager;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    public void GameSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);

        SaveData save = new SaveData();

        save.SaveEnemies(EnemySaves);
        save.savedScene = sceneName;
        bf.Serialize(fs, save);

        fs.Close();
    }

    public void Load()
    {
        
        if(File.Exists(filePath))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);

        SaveData save = (SaveData)bf.Deserialize(fs);
        fs.Close();
        levelManager.LoadLevel(sceneName);
        int i = 0;

        foreach(var enemy in save.EnemyData)
        {
            EnemySaves[i].GetComponent<Enemy>().LoadData(enemy);
            i++;
        }

    }

 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{

    Scene scene = SceneManager.GetActiveScene();
    public string savedScene;

    [System.Serializable]
    public struct Vector2
    {
        public float x, y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
    //[System.Serializable]
    //public struct SceneNameToSave
    //{
    //    public string currentScene;

    //    public SceneName()

    //}
    

    [System.Serializable]
    public struct EnemySaveData
    {
        public Vector2 positioneEnemy;
            
        public EnemySaveData(Vector2 enempos)
        {
            positioneEnemy = enempos;
        }
    }

    public List<EnemySaveData> EnemyData = new List<EnemySaveData>();

    public void SaveEnemies(List<GameObject> enemies)
    {
        foreach(var go in enemies)
        {
           // var em = go.GetComponent<List<Enemy>>();

            Vector2 enempos = new Vector2(go.transform.position.x, go.transform.position.y);

            EnemyData.Add(new EnemySaveData(enempos));
        }
    }

}

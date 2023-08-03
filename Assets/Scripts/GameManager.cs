using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public int CurrentScore;
  public string CurrentUserName;

  public MenuUIController menuUIController;

  private void Awake()
  {
    Debug.Log("persistentDataPath: " + Application.persistentDataPath);

    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }

  public void SaveGameFile()
  {
    SaveData data = LoadGameFile();
    if (data == null) { return; }

    data.Infomations.Add(new GameInfo(this.CurrentUserName, this.CurrentScore));
    string json = JsonUtility.ToJson(data);

    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    Debug.Log("SaveGameFile.invoke!");
  }

  public SaveData LoadGameFile()
  {
    string path = Application.persistentDataPath + "/savefile.json";

    if (File.Exists(path))
    {
      string json = File.ReadAllText(path);
      SaveData data = JsonUtility.FromJson<SaveData>(json);
      return data;
    }
    return null;
  }

  [System.Serializable]
  public class GameInfo
  {
    public string UserName;
    public int UserScore;

    public GameInfo(string UserName, int UserScore)
    {
      this.UserName = UserName;
      this.UserScore = UserScore;
    }
  }

  [System.Serializable]
  public class SaveData
  {
    public List<GameInfo> Infomations;
    public SaveData()
    {
      this.Infomations = new List<GameInfo>();
    }
  }
}

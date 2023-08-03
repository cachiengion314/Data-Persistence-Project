using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public int CurrentScore;
  public string CurrentUserName;

  public string BestScore;

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

    BestScore = _FindBestScore();
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

  private string _FindBestScore()
  {
    SaveData data = LoadGameFile();

    List<GameInfo> Infomation = data.Infomations;
    GameInfo game = null;
    for (int i = 0; i < Infomation.Count; ++i)
    {
      GameInfo curr_game = Infomation[i];
      if (game == null)
      {
        game = curr_game;
        continue;
      }
      if (curr_game.UserScore > game?.UserScore)
      {
        game = curr_game;
      }
    }
    string _text = $"Best: {game?.UserName}: {game?.UserScore}";
    return _text;
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

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static GameManager;
using System.Collections.Generic;

public class MenuUIController : MonoBehaviour
{
  // injectshit here
  public TMP_InputField UserName;
  public TMP_Text BestScore;

  private void Start()
  {
    SaveData data = GameManager.Instance.LoadGameFile();

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
    string _text = $"best: {game?.UserName}: {game?.UserScore}";
    Debug.Log(_text);
    BestScore.text = _text;
  }

  public void QuitGame()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.ExitPlaymode();
#else
  Application.Quit();
#endif
  }
  public void PlayGame()
  {
    SceneManager.LoadScene(1);
    GameManager.Instance.CurrentUserName = UserName.text;
  }
}

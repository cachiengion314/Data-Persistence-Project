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
    BestScore.text = GameManager.Instance.BestScore;
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

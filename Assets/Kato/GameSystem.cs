using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
  public void StartGame()
  {
    Debug.Log("c");
    SceneManager.LoadScene("SelectStageScene");
  }
}

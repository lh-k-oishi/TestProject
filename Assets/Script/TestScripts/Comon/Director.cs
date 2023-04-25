using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Director : MonoBehaviour
{
    public enum SceneState { title, tutorial, gameScene, gameOver, gameClear }

    public void LoadNextScene(SceneState nextScene)
    {
        switch (nextScene)
        {
            case SceneState.title:
                SceneManager.LoadScene("Title");
                break;
            case SceneState.tutorial:
                SceneManager.LoadScene("Tutorial");
                break;
            case SceneState.gameScene:
                SceneManager.LoadScene("GameScene");
                break;
            case SceneState.gameOver:
                SceneManager.LoadScene("GameOver");
                break;
            case SceneState.gameClear:
                SceneManager.LoadScene("GameClear");
                break;
            default:
                break;
        }
    }

    // このスクリプトをインスタンス化
    static Director instance;
    public static Director Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Director();
            }
            return instance;
        }
        set { instance = value; }
    }


    private void Awake()
    {
        instance = this;
    }
}

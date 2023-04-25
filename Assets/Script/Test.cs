using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField]
    SceneControl sceneControl;


    public void OnText(InputAction.CallbackContext obj)
    {
        sceneControl.nextScene = Director.SceneState.gameClear;
    }

    public void OnText2(InputAction.CallbackContext obj)
    {
        sceneControl.nextScene = Director.SceneState.gameOver;
    }
}

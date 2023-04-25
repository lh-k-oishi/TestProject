using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneControl : MonoBehaviour
{
    [SerializeField] public Director.SceneState nextScene;
    PlayerInput _input;
    Director loadNextScene;
    private void Awake()
    {
        TryGetComponent(out _input);
    }
    private void Start()
    {
        loadNextScene = GetComponent<Director>();
    }
    private void OnEnable()
    {
        _input.actions["Next"].started += OnNext;
    }
    private void OnDisable()
    {
        _input.actions["Next"].started -= OnNext;
    }
    public void OnNext(InputAction.CallbackContext obj)
    {
        // 現在のシーン状態をもとに、次のシーンを読み込む
        loadNextScene.LoadNextScene(nextScene);
    }

}

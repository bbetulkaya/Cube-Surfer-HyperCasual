using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance => instance ?? (instance = FindObjectOfType<GameManager>());
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] public GameState currentState;
    private void OnEnable()
    {
        Observer.UpdateGameState += UpdateGameState;
    }
    private void OnDisable()
    {
        Observer.UpdateGameState += UpdateGameState;
    }

    private void UpdateGameState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.START:
                break;
            case GameState.GAMEPLAY:
                break;
            case GameState.FINALGAME:
                break;
            case GameState.FINISH:
                break;
            default:
                Debug.LogError(this);
                break;
        }
    }
}

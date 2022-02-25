using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeStack : MonoBehaviour
{
    #region Singleton
    private static CubeStack instance;
    public static CubeStack Instance => instance ?? (instance = FindObjectOfType<CubeStack>());
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }
    #endregion

    [SerializeField] private List<Transform> cubeStack;
    [SerializeField] private Transform stackParent;
    [SerializeField] private Transform cubeParent;
    [SerializeField] private Transform prevCube;

    private void Start()
    {
        cubeStack.Add(prevCube);
    }

    public void AddCube(Transform newCube)
    {
        // TODO ADD OF CUBE ANIMATIONS
        cubeStack.Add(newCube);

        var parentPos = transform.position;
        parentPos.y = cubeStack.Count + 1;
        transform.position = parentPos;

        newCube.transform.SetParent(stackParent);

        var newPos = newCube.position;
        newPos.y = cubeStack.Count;
        newPos.z = stackParent.position.z;
        newPos.x = stackParent.position.x;
        newCube.position = newPos;

    }

    public void RemoveCube()
    {
        if (cubeStack.Count <= 0) return;

        var tmpCube = cubeStack[0];
        tmpCube.SetParent(cubeParent);
        cubeStack.Remove(tmpCube);
        StartCoroutine(HandleRemoveAnimation());
    }

    public void FinalGame()
    {
        if (cubeStack.Count <= 0)
        {
            Debug.Log("LOSE");
            Observer.UpdateGameState?.Invoke(GameState.FINISH);
        }
        else
        {
            StartCoroutine(HandleFinalGameStack());
        }
    }

    private IEnumerator HandleFinalGameStack()
    {
        while (cubeStack.Count > 0)
        {
            yield return new WaitForSeconds(0.7f);
            RemoveCube();
        }

        yield return new WaitForSeconds(0.3f);
        Observer.UpdateGameState?.Invoke(GameState.FINISH);
    }

    private IEnumerator HandleRemoveAnimation()
    {
        transform.DOMoveY(cubeStack.Count + 1, 0.6f, true).SetEase(Ease.InBounce);

        foreach (Transform cube in cubeStack)
        {
            cube.DOLocalMoveY(cube.localPosition.y - 1, 0.2f, false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

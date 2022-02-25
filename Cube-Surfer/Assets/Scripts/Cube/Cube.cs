using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Transform parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            other.gameObject.tag = "Untagged";
            CubeStack.Instance.AddCube(other.gameObject.GetComponent<Cube>().parent);
        }
        if (other.CompareTag("Obstacle"))
        {
            other.enabled = false;
            Observer.HIT?.Invoke();
            CubeStack.Instance.RemoveCube();
        }
        if (other.CompareTag("FinalGame"))
        {
            Observer.UpdateGameState?.Invoke(GameState.FINALGAME);
            CubeStack.Instance.FinalGame();
        }
    }
}

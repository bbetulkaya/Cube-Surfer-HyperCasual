using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private BoxCollider obstacleCollider;
    private void OnEnable()
    {
        Observer.HIT += HandleHit;
    }
    private void OnDestroy()
    {
        Observer.HIT -= HandleHit;
    }

    private void HandleHit()
    {
        StartCoroutine(HandleCollider());
    }
    private IEnumerator HandleCollider()
    {
        obstacleCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        obstacleCollider.enabled = true;
    }
}

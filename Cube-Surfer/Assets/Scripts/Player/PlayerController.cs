using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    // TODO PLAYER DATA (SCRIPTABLEOBJECT)
    [BoxGroup("Movements Settings"), SerializeField] private Transform leftLimit, rightLimit, sideMovementRoot;
    [BoxGroup("Player Settings"), SerializeField] private float forwardSpeed;
    [BoxGroup("Player Settings"), SerializeField] private float sideMovementSensivity;

    private float leftLimitX;
    private float rightLimitX;

    private void Start()
    {
        leftLimitX = leftLimit.position.x;
        rightLimitX = rightLimit.position.x;
    }

    void Update()
    {
        HandleForwardMovement();
        HandleSideMovement();
    }

    private void HandleForwardMovement()
    {
        if(GameManager.Instance.currentState == GameState.FINISH) return;
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
    }

    private void HandleSideMovement()
    {
        if(GameManager.Instance.currentState != GameState.GAMEPLAY) return;
        var pos = sideMovementRoot.localPosition;
        pos.x += InputManager.Instance.MouseInput.x * sideMovementSensivity;
        pos.x = Mathf.Clamp(pos.x, leftLimitX, rightLimitX);
        sideMovementRoot.localPosition = Vector3.Lerp(sideMovementRoot.localPosition, pos, Time.deltaTime * 20f);

        var moveDirection = Vector3.forward + InputManager.Instance.RawMouseInput.x * Vector3.right;

    }
}

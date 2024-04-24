using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControls : MonoBehaviour
{
    public PlayerController playerController;
    public Gun gun;

    public void StartMovingLeft()
    {
        playerController.SetMovementInput(-1);
    }

    public void StartMovingRight()
    {
        playerController.SetMovementInput(1); 
    }

    public void StopMoving()
    {
        playerController.SetMovementInput(0); 
    }

    public void Jump()
    {
        playerController.Jump();
    }

    public void Shoot()
{
    Gun gunComponent = playerController.GetComponentInChildren<Gun>();
    if (gunComponent != null)
    {
        gunComponent.Shoot();
    }
}

}







using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public Collider2D hitBox;

    public virtual void ActivateWeapon() { }
}


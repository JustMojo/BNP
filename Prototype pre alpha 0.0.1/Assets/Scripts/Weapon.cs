using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    Vector3 newPosition = Vector3.zero;
    Quaternion newRotation;


    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad9) || Input.GetKey(KeyCode.Keypad7)) {
            newPosition.y = firePoint.position.y + 0.400f;
            newRotation.z = firePoint.rotation.z + 45;
        }
        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    void Shoot() {
        Instantiate(bulletPrefab, firePoint.position + newPosition, firePoint.rotation * newRotation);
    }
}

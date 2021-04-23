using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownCameraPositioner : MonoBehaviour
{
    public Transform playerCharacter;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y + 4f, playerCharacter.transform.position.z - 3f);
        transform.LookAt(playerCharacter);
    }
}

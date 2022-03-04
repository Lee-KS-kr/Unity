using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    private Vector3[] positions = { new Vector3(1.0f, 1.0f, 0) , new Vector3(1.0f, -1.0f, 0) ,
    new Vector3(-1.0f, -1.0f, 0),new Vector3(-1.0f, 1.0f, 0)};
    private float moveTime = 1.0f;

    private void Awake()
    {
        StartCoroutine(MoveCube());
    }

    private IEnumerator MoveCube()
    {
        while (true)
        {
            for (int index = 0; index < 4; index++)
            {
                transform.position = positions[index];
                yield return new WaitForSeconds(moveTime);
            }
            yield return null;
        }
    }
}

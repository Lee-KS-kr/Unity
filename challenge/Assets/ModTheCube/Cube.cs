using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    public float turnSpeed;
    private Vector2 cubePosition;
    //private Vector3[] positions = { new Vector3(1.0f, 1.0f, 0) , new Vector3(1.0f, -1.0f, 0) ,
    //new Vector3(-1.0f, -1.0f, 0),new Vector3(-1.0f, 1.0f, 0)};
    //private float moveTime = 1.0f;
    
    void Start()
    {
        //StartCoroutine(MovingCube());
        InvokeRepeating("ChangePosition", 3.0f, 3.0f);
        InvokeRepeating("ChangeColor", 2.0f, 2.5f);
    }
    
    void Update()
    {
        transform.Rotate(turnSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    //private void ChangePosition()
    //{
    //    cubePosition = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
    //    transform.position = cubePosition;
    //    transform.localScale = Vector3.one * 1.3f;
    //}

    private void ChangeColor()
    {
        Material material = Renderer.material;
        material.color = new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    //IEnumerator MovingCube()
    //{
    //    while (true)
    //    {
    //        for (int index = 0; index < 4; index++)
    //        {
    //            transform.position = positions[index];
    //            yield return new WaitForSeconds(moveTime);
    //        }
    //        yield return null;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFSM : MonoBehaviour
{
    public enum eState
    {
        Stop = 0,
        Up = 1,
        Down = -1
    }

    public float height = 1f;
    public float step = 1f;
    public eState dir = eState.Stop;

    void Input()
    {
        if(UnityEngine.Input.GetKey(KeyCode.UpArrow))
        {
            dir = eState.Up;
        }
        else if(UnityEngine.Input.GetKey(KeyCode.DownArrow))
        {
            dir = eState.Down;
        }
    }

    void DoMove()
    {
        if(dir != eState.Stop)
        {
            Vector3 _newPosition = transform.position;
            _newPosition.y = _newPosition.y + (int)dir * Time.deltaTime * step;
            transform.position = _newPosition;
        }
    }

    //private IEnumerator Start()
    //{
    //    dir = eState.Stop;
    //    yield return new WaitForSeconds(1f);
    //    dir = eState.Up;
    //    yield return new WaitForSeconds(2f);
    //    dir = eState.Down;
    //    yield return new WaitForSeconds(2f);
    //    dir = eState.Stop;
    //}

    void Update()
    {
        Input();
        DoMove();
    }
}

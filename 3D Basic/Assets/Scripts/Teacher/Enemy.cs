using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent = null;
    private float targetRecognitionRange = 5;
    // private Vector3 _destination = Vector3.zero;
    // public Transform startPoint = null;
    // public Transform endPoint = null;
    //
    // private void Awake()
    // {
    //     _agent = GetComponent<NavMeshAgent>();
    // }
    //
    // private void Start()
    // {
    //     // Enemy의 초기 위치에서 두 지점까지의 거리를 계산 후 가까운 지점을 초기 목적지로 설정
    //     float toStart = Vector3.Distance(transform.position, startPoint.position); // start지점까지의 거리
    //     float toEnd = Vector3.Distance(transform.position, endPoint.position); // end지점까지의 거리
    //     _destination = toStart < toEnd ?  startPoint.position : endPoint.position; // 각 지점까지의 거리를 계산
    //     _agent.SetDestination(_destination); // 가까운 곳을 목적지로 설정
    // }
    //
    // private void Update()
    // {
    //     // 첫 목적지에 도착한것이 확인되면 다른 목적지로 이동
    //     if (_agent.remainingDistance < _agent.stoppingDistance) // 목적지까지의 거리가 0.1이하이면 도착으로 간주
    //     {
    //         _destination = _destination == startPoint.position ? endPoint.position : startPoint.position;
    //         // 목적지가 start지점이면 end지점으로, end지점이면 start지점으로 설정
    //         _agent.SetDestination(_destination); // 다음 목적지로 이동
    //     }
    // }
    public Transform[] wayPoints = null;
    public Transform target = null;
    private Transform _tempTransform = null;
    private int _index = 0;
    private IEnumerator _enumerator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindObjectOfType<Player>().transform;
        _enumerator = ChasePlayer();
    }

    private void Start()
    {
        _agent.SetDestination(wayPoints[_index].position);
        _tempTransform = wayPoints[_index];
    }

    private void Update()
    {
        if (CheckArrive())
        {
            GoNextWaypoint();
        }
        StartCoroutine(_enumerator);
    }

    bool CheckArrive()
    {
        return _agent.remainingDistance < _agent.stoppingDistance;
    }

    private void GoNextWaypoint()
    {
        _index++;
        _index = _index % wayPoints.Length;
        _agent.SetDestination(wayPoints[_index].position);
        _tempTransform = wayPoints[_index];
    }

    // private void Test()
    // {
    //     _agent.Warp(new Vector3(1, 2, 3)); // 한 지점으로 워프시키고 싶을 때
    //     _agent.speed; // 속도
    //     _agent.acceleration; // 가속
    //     _agent.radius; // 회피 반지름
    //     _agent.avoidancePriority; // 회피 우선도
    //     _agent.remainingDistance;
    //     _agent.stoppingDistance;
    //     _agent.autoRepath;
    //     _agent.pathPending;
    // }

    private void OnDrawGizmos()
    {
        // 목표 인식 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetRecognitionRange);
    }

    private IEnumerator ChasePlayer()
    {
        // Debug.Log("코루틴 발동");
        while (Vector3.Distance(transform.position, target.position) <= targetRecognitionRange)
        {
            // Debug.Log("추적중");
            _agent.SetDestination(target.position);
            yield return null;
        }

        // Debug.Log("돔황챠");
        _agent.SetDestination(_tempTransform.position);
        yield return null;
    }
}

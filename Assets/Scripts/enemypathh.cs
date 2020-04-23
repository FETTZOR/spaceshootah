using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemypathh : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> wayPoints;
    [SerializeField] float moveSpeed = 222f;
    int waypointIndex = 0;

    void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[waypointIndex].transform.position;
    }
    void Update()
    {
        Moove();
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Moove()
    {
        if (waypointIndex <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards
                (transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        var startPosition = path[0].transform.position;
        transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
    }

    void FinishPath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                Vector3 lerpedPosition = Vector3.Lerp(startPosition, endPosition, travelPercent);
                transform.position = new Vector3(lerpedPosition.x, transform.position.y, lerpedPosition.z);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}


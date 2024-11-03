using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class enemyAI : MonoBehaviour
{
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 2;
    public float distant = 10;

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position;

        transform.SetParent(root.transform);

        GameObject waypoints = new GameObject("Waypoint");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;

        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        points = new List<Transform> { p1.transform, p2.transform };
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];

        // ?i?u ch?nh h??ng ??i t??ng d?a trên v? trí c?a ?i?m ??n
        if (goalPoint.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Di chuy?n ??i t??ng ??n v? trí c?a ?i?m ??n
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        // Ki?m tra n?u ??i t??ng ?ã ??n g?n v? trí ?i?m ??n
        if (Vector2.Distance(transform.position, goalPoint.position) < distant)
        {
            if (nextID == points.Count - 1)
            {
                idChangeValue = -5;
            }
            else if (nextID == 0)
            {
                idChangeValue = 5;
            }

            nextID += idChangeValue;
        }
    }
}

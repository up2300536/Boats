using UnityEngine;

public class AIShipController : MonoBehaviour
{
    public Transform[] points;
    public float speed = 5f;
    private int currentPoint = 0;

    void Update()
    {
        Transform target = points[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}

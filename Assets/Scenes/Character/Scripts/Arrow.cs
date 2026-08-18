using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 a_target;
    public float arrowspeed;

    void Update()
    {
        float step = arrowspeed * Time.deltaTime;
        if (a_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, a_target, step);
        }
    }

    public void SetTarget(Vector3 target)
    {
        a_target = target;
    }
}
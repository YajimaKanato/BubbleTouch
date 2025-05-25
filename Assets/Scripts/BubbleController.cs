using UnityEngine;

public class BubbleController : MonoBehaviour
{
    float delta = 0.0f, theta = 0.0f;
    Vector3 basepos;
    private void Start()
    {
        basepos = transform.position;
    }
    void Update()
    {
        delta += Time.deltaTime;
        theta = 2 * Mathf.PI * delta;
        if (theta > 2 * Mathf.PI)
        {
            theta -= 2 * Mathf.PI;
        }
        transform.position = basepos + new Vector3(Mathf.Sin(theta), delta);
    }
}

using UnityEngine;
using DG.Tweening;
using System.Collections;
public class SpawnerMove : MonoBehaviour
{
    float theta = 0.0f;
    private void Start()
    {
        SpawnerMoving();
    }

    private void SpawnerMoving()
    {
        while (true)
        {
            theta += 2 * Mathf.PI * Time.deltaTime;
            transform.position = new Vector2(Mathf.Sin(theta), transform.position.y);
        }
    }
}

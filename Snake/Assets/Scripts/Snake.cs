using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{

    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    [SerializeField] private Transform segmentPrefab;

    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate + 5;
        segments = new List<Transform>();
        segments.Add(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0);
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

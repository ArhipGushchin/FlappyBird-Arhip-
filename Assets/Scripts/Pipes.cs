using UnityEngine;

public class Pipes : MonoBehaviour
{
    public GameManager GameManager;
    public float speed = 5f;
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
  
}

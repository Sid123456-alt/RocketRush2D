using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float maxSpinSpeed = 10f;
    public float minSpeed = 50f;
    public float maxSpeed = 100f;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        float randomsize = Random.Range(minSize,maxSize);
        float randomspeed = Random.Range(minSpeed,maxSpeed)/randomsize;
        transform.localScale = new Vector3 (randomsize ,randomsize ,1);
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomDirection = Random.insideUnitCircle;
        rb.AddForce(randomDirection* randomspeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}

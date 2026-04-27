using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Button restartButton;
    public GameObject explosionEffect;
    private Label scoreText;
    public UIDocument uiDocument;
    private float score = 0f;
    public float scoreMultiplier = 10f;
    private float elapsedTime = 0f;
    public GameObject boosterFlame;
    public float thrustForce = 4f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 540f;
    public float minClickDistance = 0.6f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        Debug.Log("Score: "+ score);
        scoreText.text = "Score: " + score;
    }

    void FixedUpdate()
    {
        if (!Mouse.current.leftButton.isPressed)
        {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        Vector2 toMouse = mousePos - transform.position;

        float nextAngle = rb.rotation;

        if (toMouse.sqrMagnitude >= minClickDistance * minClickDistance)
        {
            Vector2 direction = toMouse.normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            nextAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(nextAngle);
        }

        Vector2 forward = Quaternion.Euler(0f, 0f, nextAngle) * Vector2.up;
        rb.AddForce(forward * thrustForce);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
    }
    void ReloadScene()
{
SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}




}


using System;
using UnityEngine;

public class FlipManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform frontWheel; // Reference to front wheel position
    [SerializeField] private Transform backWheel;  // Reference to back wheel position
    

    private bool isGrounded = false;
    private float totalRotation;
    private int flipCount;
    
    public float flipThreshold = 360f;
    public int pointsPerFlip = 100;
    public int score = 0;
    public int pointsPerWheelie = 5;
    public float safeAngleThreshold = 45f; // Safe angle range for landing
    
    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) Debug.LogError("Rigidbody2D not found!");
        }
    }

    private void Update()
    {
        // Only track rotation if not grounded
        if (!isGrounded)
        {
            TrackRotation();
        }
        
    }

    private void TrackRotation()
    {
        float currentRotation = transform.eulerAngles.z;
        float rotationDelta = currentRotation - totalRotation;

        // Adjust for angle wrapping around 0-360
        if (rotationDelta > 180) rotationDelta -= 360;
        if (rotationDelta < -180) rotationDelta += 360;

        totalRotation += rotationDelta;

        // Check for full flip and award points
        if (Mathf.Abs(totalRotation) >= flipThreshold)
        {
            flipCount++;
            totalRotation = 0; // Reset rotation
            score += pointsPerFlip;
            audioManager.PlaySFX(audioManager.flip);
            Debug.Log($"Full Flip! Total flips: {flipCount}, Score: {score}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            // Check if the player is within the safe landing angle range
            float angleZ = Mathf.Abs(transform.eulerAngles.z);
            if (angleZ <= safeAngleThreshold || angleZ >= 360 - safeAngleThreshold)
            {
                // Award partial points only if within safe landing angle
                float partialPoints = (Mathf.Abs(totalRotation) / flipThreshold) * pointsPerFlip;
                score += Mathf.RoundToInt(partialPoints);
                audioManager.PlaySFX(audioManager.flip);
                Debug.Log($"Safe Landing! Points awarded: {Mathf.RoundToInt(partialPoints)}, Total Score: {score}");
            }
            else
            {
                Debug.Log("Unsafe Landing. No points awarded.");
            }

            totalRotation = 0;
            flipCount = 0;
        }
        else
        {
            Debug.Log("No ground detected");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

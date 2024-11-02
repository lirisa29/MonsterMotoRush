using System;
using UnityEngine;

public class FlipManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private bool isGrounded = false;
    private float totalRotation;
    private int flipCount;

    public float flipThreshold = 360f;
    public int pointsPerFlip = 100;
    public int score = 0;
    public float safeAngleThreshold = 45f; // Safe angle range for landing

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
        if (!isGrounded)
        {
            TrackRotation();
        }
    }

    private void TrackRotation()
    {
        float currentRotation = transform.eulerAngles.z;
        float rotationDelta = currentRotation - totalRotation;

        if (rotationDelta > 180) rotationDelta -= 360;
        if (rotationDelta < -180) rotationDelta += 360;

        totalRotation += rotationDelta;

        if (Mathf.Abs(totalRotation) >= flipThreshold)
        {
            flipCount++;
            totalRotation = 0;
            score += pointsPerFlip;
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

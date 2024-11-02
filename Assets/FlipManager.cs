using System;
using UnityEngine;

public class FlipDetector : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float totalRotation;
    private int flipCount;

    public float flipThreshold = 360f; // Degrees required for a full flip
    public int pointsPerFlip = 100; // Points for each flip
    public int score = 0; // Total score

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isGrounded)
        {
            DetectFlip();
        }
    }

    private void FixedUpdate()
    {
      isGrounded = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.05f);
    }


    private void DetectFlip()
    {
        // Calculate rotation since airborne
        float currentRotation = transform.eulerAngles.z;
        float rotationDelta = currentRotation - totalRotation;

        // Adjust for 0-360 wraparound
        if (rotationDelta > 180) rotationDelta -= 360;
        if (rotationDelta < -180) rotationDelta += 360;

        totalRotation += rotationDelta;

        // Check if a full flip is completed
        if (Mathf.Abs(totalRotation) >= flipThreshold)
        {
            flipCount++;
            totalRotation = 0; // Reset rotation for next flip
            score += pointsPerFlip; // Update score
            Debug.Log($"Flip detected! Total flips: {flipCount}, Score: {score}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        totalRotation = 0; // Reset rotation count
        flipCount = 0; // Reset flip count for next jump
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When leaving ground, start tracking rotation for flips
        isGrounded = false;
    }
}
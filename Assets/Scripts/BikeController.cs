using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BikeController : MonoBehaviour
{

    [SerializeField] private GameObject gameOverScreen;
    
    public float speed = 1500f;
    public float rotationSpeed = 800f;

    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;

    public Rigidbody2D rb;
    
    private float movement = 0f;
    private float rotation = 0f;
    public float collectedCoins = 0;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        movement = -Input.GetAxisRaw("Vertical") * speed;
        rotation = Input.GetAxisRaw("Horizontal");
        
    }

    private void FixedUpdate()
    {

        if (movement == 0f)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = 10000 };
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }
        
        rb.AddTorque(-rotation * rotationSpeed * Time.fixedDeltaTime);
        
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        if (tag.Equals("coin"))
        {
            //GameDataManager.AddCoins(1);
            collectedCoins = collectedCoins++;
            
            GameSharedUI.Instance.UpdateCoinsUIText();
            
            Destroy(other.gameObject);
        }
    }
}

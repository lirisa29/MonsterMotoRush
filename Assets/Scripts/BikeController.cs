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
    public int collectedCoins = 0;

    [SerializeField] private GameObject[] skins;
    
    private AudioManager audioManager;
    public AudioSource bikeSound;
    [Range(0, 1)] public float minPitch;
    [Range(1, 5)] public float maxPitch;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
        
        ChangePlayerSkin();
    }

    void ChangePlayerSkin()
    {
        Character character = GameDataManager.GetSelectedCharacter();
        
        if (character != null)
        {
            int selectedSkin = GameDataManager.GetSelectedCharacterIndex();
            Debug.Log($"Selected Skin Index: {selectedSkin}");

            if (selectedSkin >= 0 && selectedSkin < skins.Length)
            {
                skins[selectedSkin].SetActive(true);

                for (int i = 0; i < skins.Length; i++)
                {
                    if (i != selectedSkin)
                    {
                        skins[i].SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogError("Selected skin index is out of bounds.");
            }
        }
        else
        {
            Debug.LogError("No character selected. Please ensure a character is set.");
        }
    }

    void Update()
    {
        movement = -Input.GetAxisRaw("Vertical") * speed;
        rotation = Input.GetAxisRaw("Horizontal");
        
        EngineSound();
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

    void EngineSound()
    {
        bikeSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Abs(movement));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        if (tag.Equals("coin"))
        {
            audioManager.PlaySFX(audioManager.coin);
            GameDataManager.AddCoins(50);
            collectedCoins += 50;
            GameSharedUI.Instance.UpdateCoinsUIText();
            Destroy(other.gameObject);
        }
    }
}

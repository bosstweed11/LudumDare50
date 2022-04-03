using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class KeyController : MonoBehaviour
{
    [SerializeField] private Sprite AKey;
    [SerializeField] private Sprite SKey;
    [SerializeField] private Sprite DKey;
    [SerializeField] private GameObject tater;

    [SerializeField] private AudioClip missedKeyPress;
    [SerializeField] private AudioClip madeKeyPress;

    private PlayerMovement player;
    // Start is called before the first frame update
    public int key = -1;

    public List<KeyControl> keys;
    
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        keys = new List<KeyControl>
        {
            Keyboard.current.aKey,
            Keyboard.current.sKey,
            Keyboard.current.dKey,
        };
        key = Random.Range(0, 3);
        switch (key)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = AKey;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = SKey;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = DKey;
                break;
            case 3:
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * FindObjectOfType<NaturalizationController>().moveSpeed;
        if (player.transform.position.x > 10)
        {
            if (keys[key].wasPressedThisFrame)
            {
                if (transform.position.x > 18.5 && transform.position.x < 21.5)
                {
                    Success();
                }
                else if (transform.position.x > 21.5)
                {           
                    Failure();
                }
                else if (transform.position.x < 18.5)
                {
                    Failure();
                }
            }
            else if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                Failure();
            }
        }
        
        if (transform.position.x < 10.5)
        {
            Failure();
        }
    }

    void Failure()
    {
        if (player.transform.position.x > 10)
        {
            AudioSource.PlayClipAtPoint(missedKeyPress, Camera.main.transform.position);
            FindObjectOfType<NaturalizationController>().SpawnKey(1);
        }
        Destroy(gameObject);
    }
    
    void Success(){
        Instantiate(tater, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(madeKeyPress, Camera.main.transform.position);
        FindObjectOfType<GameManager>().PotatoNaturalized();
        
        FindObjectOfType<NaturalizationController>().moveSpeed += .02f;
        if (FindObjectOfType<NaturalizationController>().moveSpeed > .15)
        {
            FindObjectOfType<NaturalizationController>().moveSpeed = .15f;
        }
        FindObjectOfType<NaturalizationController>().SpawnKey(2);
        Destroy(gameObject);
    }
}

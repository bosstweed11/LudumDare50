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
    // Start is called before the first frame update
    public float moveSpeed = 0.03f;
    public int key = -1;

    public List<KeyControl> keys;
    
    void Start()
    {
        keys = new List<KeyControl>
        {
            Keyboard.current.aKey,
            Keyboard.current.sKey,
            Keyboard.current.dKey,
        };
        key = Random.Range(0, 3);
        Debug.Log("Key is : " + key);
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
        if (keys[key].wasPressedThisFrame)
        {
            if (transform.position.x > 18.5 && transform.position.x < 21.5)
            {
                Success();
            }
            else if (transform.position.x > 21.5)
            {           
                FindObjectOfType<NaturalizationController>().SpawnKey();
                Destroy(gameObject);
                Debug.Log("too early!");
            }
            else if (transform.position.x < 18.5)
            {
                FindObjectOfType<NaturalizationController>().SpawnKey();
                Destroy(gameObject);
                Debug.Log("too late!");
            }
        }
        else if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Debug.Log("wrong key");
            FindObjectOfType<NaturalizationController>().SpawnKey();

            Destroy(gameObject);
        }
        else if (transform.position.x < 10.5)
        {
            Debug.Log("way too late");
            FindObjectOfType<NaturalizationController>().SpawnKey();

            Destroy(gameObject);
        }
    }
    
    void Success(){
        Debug.Log("Success");
        Instantiate(tater, transform.position, transform.rotation);
        FindObjectOfType<GameManager>().PotatoNaturalized();
        
        FindObjectOfType<NaturalizationController>().moveSpeed *= 2;
        FindObjectOfType<NaturalizationController>().SpawnKey();
        Destroy(gameObject);
    }
}

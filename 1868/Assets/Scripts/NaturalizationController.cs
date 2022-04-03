using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NaturalizationController : MonoBehaviour
{

    private float spawnTimer = 2.5f;
    private float timeSinceLastSpawn = 0;
    public bool hasStarted = false;
    public float moveSpeed = 0.03f;
    [SerializeField] private GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {

            if (moveSpeed > 0.03f)
            {
                moveSpeed -= 0.005f * Time.deltaTime;
            }
        }
    }

    public void SpawnKey(int source)
    {
        Instantiate(key, new Vector3(30, 2.5f, 0), transform.rotation);
    }
    
    
}

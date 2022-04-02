using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed;
    [SerializeField] private float throwSpeed;
    [SerializeField] private GameObject tater;
    
    private Vector2 moveInput;
    private Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2
        {
            x = moveInput.x * playerSpeed,
            y = _rigidbody2D.velocity.y
        };
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        PrintVector(Mouse.current.position.ReadValue(), "original mouse position");
        Vector3 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        PrintVector(pos, "mouse position");
        Vector3 trans = Camera.main.ScreenToWorldPoint(transform.position);
        var throwVector = pos - transform.position;
        var thisPotato= Instantiate(tater, transform.position, transform.rotation);
        PrintVector(throwVector, "throw vector");
        thisPotato.GetComponent<Rigidbody2D>().velocity = new Vector2
        {
            x = throwVector.x * throwSpeed,
            y = throwVector.y * throwSpeed
        };
    }

    void PrintVector(Vector3 vector, string label)
    {
        Debug.Log(label);
        Debug.Log("x : " + vector.x);
        Debug.Log("y : " + vector.y);
    }
}

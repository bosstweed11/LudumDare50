using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed;

    
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
}

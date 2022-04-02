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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var throwVector = mousePosition - transform.position;
        var thisPotato= Instantiate(tater, transform.position, transform.rotation);
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

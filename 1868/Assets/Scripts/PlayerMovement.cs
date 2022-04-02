using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed;
    [SerializeField] private float throwSpeed;
    [SerializeField] private GameObject tater;
    private int initialPotatoes = 5;
    private List<GameObject> _potatoes = new List<GameObject>();
    
    private Vector2 moveInput;
    private Rigidbody2D _rigidbody2D;
    public GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        for (var i = 0; i < initialPotatoes; i++)
        {
            _potatoes.Add(Instantiate(tater, transform.position, transform.rotation));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Walk();
    }

    void Walk()
    {
        _rigidbody2D.velocity = new Vector2
        {
            x = moveInput.x * playerSpeed,
            y = _rigidbody2D.velocity.y
        };

        if (IsMoving())
            SetSpriteDirection();
    }

    void SetSpriteDirection()
    {
        var localScale = transform.localScale;
        localScale = new Vector2
        {
            x = Mathf.Sign(_rigidbody2D.velocity.x),
            y = localScale.y
        };
        transform.localScale = localScale;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        var getNextPotatoToThrow = GetNextPotato();
        if (getNextPotatoToThrow != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var throwVector = mousePosition - transform.position;
            getNextPotatoToThrow.GetComponent<PotatoMovement>()._potatoState = PotatoState.InAir;
            getNextPotatoToThrow.GetComponent<Rigidbody2D>().velocity = new Vector2
            {
                x = throwVector.x * throwSpeed,
                y = throwVector.y * throwSpeed
            };
            _gameManager.SentPotatoToVote();
            Debug.Log("Threw potato!");
        }
        
    }

    private PotatoMovement GetNextPotato()
    {
        var potatoes = GameObject.FindObjectsOfType<PotatoMovement>();
        
        return potatoes.FirstOrDefault(potato => potato._potatoState.Equals(PotatoState.InHand));
    }

    void PrintVector(Vector3 vector, string label)
    {
        Debug.Log(label);
        Debug.Log("x : " + vector.x);
        Debug.Log("y : " + vector.y);
    }
    
    bool IsMoving()
    {
        return Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
    }
}

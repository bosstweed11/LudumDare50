using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PotatoMovement : MonoBehaviour
{
    public PotatoState _potatoState;
    public GameManager _gameManager;
    private Transform _player;

    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite sprite3;

    private float returnSpeed = 0.005f;
    private float followSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        _potatoState = PotatoState.InHand;
        _player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        var random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (_potatoState)
        {
            case PotatoState.InHand:
                UpdateInHand();
                break;
            case PotatoState.InAir:
                UpdateInAir();
                break;
            case PotatoState.Returning:
                UpdateReturn();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void UpdateInHand()
    {
        transform.position = Vector2.Lerp(transform.position, _player.position, followSpeed);
    }

    public void UpdateReturn()
    {
        var distance = Vector2.Distance(transform.position, _player.position);
        var direction = (_player.position - transform.position).normalized;

        transform.position += direction * returnSpeed;
        if (_potatoState.Equals(PotatoState.Returning))
        {
            if (distance < 1f)
            {
                _potatoState = PotatoState.InHand;
                Debug.Log("In hand!");
                _gameManager.PotatoInHand();
            }
        }
    }

    public void UpdateInAir()
    {
        var distance = Vector2.Distance(transform.position, _player.position);
        if (distance > 20f)
        {
            Debug.Log("Went home!");
            _gameManager.PotatoWentHome();
            Destroy(gameObject);
        }
    }

    public void Return()
    {
        Debug.Log("Voted!");
        _potatoState = PotatoState.Returning;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}

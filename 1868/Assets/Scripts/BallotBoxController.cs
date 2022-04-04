using TMPro;
using UnityEngine;

public class BallotBoxController : MonoBehaviour
{
    [SerializeField] public string label;
    [SerializeField] private TextMeshProUGUI democraticVoteUI;
    [SerializeField] private TextMeshProUGUI republicanVoteUI;
    // Start is called before the first frame update
    public int democraticVoteCount = 0;
    public int republicanVoteCount = 0;
    public float counter = 0;
    private GameManager _gameManager;

    public float voteCounter = 0;

    private bool isOpen = false;
    
    [SerializeField] private AudioClip potatoToVote;
    
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void Open()
    {
        isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            counter += Time.deltaTime;
            if (counter > voteCounter)
            {
                republicanVoteCount++;
                UpdateRepublicanText();
                counter = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.gameObject;
        if (col.tag.Equals("Potato"))
        {
            var potato = col.gameObject.GetComponent<PotatoMovement>();
            if (potato._potatoState.Equals(PotatoState.InAir))
            {
                UpdateDemocratText();
                potato.Return();
            }
        }
    }

    private void UpdateRepublicanText()
    {
        republicanVoteUI.text = republicanVoteCount.ToString();
    }
    
    private void UpdateDemocratText()
    {
        democraticVoteCount++;
        democraticVoteUI.text = democraticVoteCount.ToString();
        AudioSource.PlayClipAtPoint(potatoToVote, Camera.main.transform.position);
    }
}

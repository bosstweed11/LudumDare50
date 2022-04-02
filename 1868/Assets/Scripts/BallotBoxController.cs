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
    private float counter = 0;
    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > 1)
        {
            republicanVoteCount++;
            UpdateRepublicanText();
            counter = 0;
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
        //Debug.Log("Voted Republican!");
        republicanVoteCount++;
        republicanVoteUI.text = republicanVoteCount.ToString();
    }
    
    private void UpdateDemocratText()
    {
        Debug.Log("Voted Democrat!");
        democraticVoteCount++;
        democraticVoteUI.text = democraticVoteCount.ToString();
    }
}

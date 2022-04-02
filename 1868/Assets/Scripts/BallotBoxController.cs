using TMPro;
using UnityEngine;

public class BallotBoxController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI democraticVoteUI;
    [SerializeField] private TextMeshProUGUI republicanVoteUI;
    // Start is called before the first frame update
    private int democraticVoteCount = 0;
    private int republicanVoteCount = 0;
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
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Potato"))
        {
            Destroy(col.gameObject);
            
            UpdateDemocratText();
        }
    }

    private void UpdateRepublicanText()
    {
        republicanVoteCount++;
        republicanVoteUI.text = republicanVoteCount.ToString();
        _gameManager.UpdateRepublicanText();
    }
    
    private void UpdateDemocratText()
    {
        democraticVoteCount++;
        democraticVoteUI.text = democraticVoteCount.ToString();
        _gameManager.UpdateDemocratText();
    }
}

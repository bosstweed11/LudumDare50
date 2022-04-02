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
    void Start()
    {
        
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
            democraticVoteCount++;
            UpdateDemocratText();
        }
    }

    private void UpdateRepublicanText()
    {
        republicanVoteUI.text = republicanVoteCount.ToString();
    }
    
    private void UpdateDemocratText()
    {
        democraticVoteUI.text = democraticVoteCount.ToString();
    }
}

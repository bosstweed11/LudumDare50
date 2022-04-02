using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI democraticVoteUI;
    [SerializeField] private TextMeshProUGUI republicanVoteUI;
    // Start is called before the first frame update
    private int democraticVoteCount = 0;
    private int republicanVoteCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateRepublicanText()
    {
        republicanVoteCount++;
        republicanVoteUI.text = "Republican: " + republicanVoteCount.ToString();
    }
    
    public void UpdateDemocratText()
    {
        democraticVoteCount++;
        democraticVoteUI.text = "Democrat: " + democraticVoteCount.ToString();
    }
}

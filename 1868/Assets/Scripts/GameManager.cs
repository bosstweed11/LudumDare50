using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI democraticVoteUI;
    [SerializeField] private TextMeshProUGUI republicanVoteUI;
    [SerializeField] private TextMeshProUGUI potatoesInHandUI;
    [SerializeField] private TextMeshProUGUI potatoesVotingUI;
    [SerializeField] private TextMeshProUGUI potatoesAtHomeUI;

    private int potatoesAtHome = 0;

    private int potatoesInHand = 5;

    private int potatoesVoting = 0;
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
    
    public void UpdatePotatoText(PotatoState state, int count)
    {
        switch (state)
        {
            case PotatoState.InHand:
                potatoesInHandUI.text = "In Hand: " + count;
                break;
            case PotatoState.InAir:
            case PotatoState.Returning:
                potatoesInHandUI.text = "Voting: " + count;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void PotatoWentHome()
    {
        potatoesVoting--;
        UpdatePotatoesVotingText();
        potatoesAtHome++;
        UpdatePotatoesAtHomeText();

        if (potatoesVoting == 0 && potatoesInHand == 0)
        {
            Debug.Log("Game Over");
            var ballotBoxes = GameObject.FindObjectsOfType<BallotBoxController>();
            foreach (var ballotBox in ballotBoxes)
            {
                PlayerPrefs.SetInt(ballotBox.label + "Democrat", ballotBox.democraticVoteCount);
                PlayerPrefs.SetInt(ballotBox.label + "Republican", ballotBox.republicanVoteCount);
            }

            SceneManager.LoadScene("Ending");
        }
    }

    public void PotatoInHand()
    {
        potatoesVoting--;
        UpdatePotatoesVotingText();
        potatoesInHand++;
        UpdatePotatoesInHandText();
    }
    
    public void SentPotatoToVote()
    {
        potatoesInHand--;
        UpdatePotatoesInHandText();
        potatoesVoting++;
        UpdatePotatoesVotingText();
    }

    public void UpdatePotatoesAtHomeText()
    {
        potatoesAtHomeUI.text = "Home: " + potatoesAtHome;
    }
    
    public void UpdatePotatoesVotingText()
    {
        potatoesVotingUI.text = "Voting: " + potatoesVoting;
    }
    
    public void UpdatePotatoesInHandText()
    {
        potatoesInHandUI.text = "In Hand: " + potatoesInHand;
    }

}

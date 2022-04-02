using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI potatoesInHandUI;
    [SerializeField] private TextMeshProUGUI potatoesVotingUI;
    [SerializeField] private TextMeshProUGUI potatoesAtHomeUI;

    [SerializeField] private GameObject chatPanel;
    [SerializeField] private TextMeshProUGUI chatText;

    private int potatoesAtHome = 0;
    private int potatoesInHand = 5;
    private int potatoesVoting = 0;

    private List<string> introChatMessages = new List<string>
    {
        "Hey I'm Boss Greed, leader of Tammany Hall, the leading social group for the Democratic Party in New York City.",
        "If we can win these 4 seats, we'll have complete control of the city government!",
        "I need your help, since I'm a character in a game, I need you to control me. Here's my user manual.",
        "Walk Left - A\nWalk Right - D\nAim Potato - Mouse Cursor\nThrow Potato - Click",
        "Potatoes? Yeah here we vote with potatoes. Remember it's just a video game, it doesn't have to make sense!",
        "Be careful with your aim, if you miss the ballot boxes, the potatoes will go home and stop voting, we don't want that!",
        "If you hit a ballot box, the potatoes will come back for more, they love to vote early and often!",
        "Make sure to be leading by the end of the day, or each and every one of my bits will be sad.",
        "Let the voting begin!\n\nPolls are open!",
    };

    private bool readingIntro = true;
    private int introTextIndex = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetIntroText();
    }

    // Update is called once per frame
    void Update()
    {
        if (readingIntro)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                introTextIndex++;
                if (introTextIndex == introChatMessages.Count)
                {
                    readingIntro = false;
                    chatPanel.SetActive(false);
                    FindObjectOfType<PlayerMovement>().canMove = true;
                }
                else
                {
                    SetIntroText();
                }
            }
        }
    }

    void SetIntroText()
    {
        chatText.text = introChatMessages[introTextIndex];
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

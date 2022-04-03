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
    [SerializeField] private TextMeshProUGUI timeLeftUI;
    
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private TextMeshProUGUI chatText;

    private int potatoesAtHome = 0;
    private int potatoesInHand = 5;
    private int potatoesVoting = 0;
    private float gameTimeLeft = 60 * 3;

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

    private bool reading = true;
    private int introTextIndex = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetIntroText();
    }

    // Update is called once per frame
    void Update()
    {
        if (reading)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                introTextIndex++;
                if (introTextIndex == introChatMessages.Count)
                {
                    reading = false;
                    chatPanel.SetActive(false);
                    FindObjectOfType<PlayerMovement>().canMove = true;
                    var ballotBoxes = FindObjectsOfType<BallotBoxController>();
                    foreach (var ballotBox in ballotBoxes)
                    {
                        ballotBox.Open();
                    }
                }
                else
                {
                    SetIntroText();
                }
            }
        }
        else
        {
            gameTimeLeft -= Time.deltaTime;
            if (gameTimeLeft > 10)
            {
                timeLeftUI.text = "Polls Close in: " + (int)(gameTimeLeft / 60) + ":" + (int)(gameTimeLeft % 60);
            }
            else
            {
                timeLeftUI.text = "Polls Close in: 0:0" + (int)(gameTimeLeft % 60);
            }
            if (gameTimeLeft < 0)
            {
                GameOver();
            }
        }
    }

    void SetIntroText()
    {
        chatText.text = introChatMessages[introTextIndex];
    }

    public void PotatoWentHome()
    {
        potatoesVoting--;
        UpdatePotatoesVotingText();
        potatoesAtHome++;
        UpdatePotatoesAtHomeText();

        if (potatoesVoting == 0 && potatoesInHand == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
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

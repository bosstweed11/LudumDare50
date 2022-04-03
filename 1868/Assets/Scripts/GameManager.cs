using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI potatoesInHandUI;
    [SerializeField] private TextMeshProUGUI potatoesVotingUI;
    [SerializeField] private TextMeshProUGUI potatoesAtHomeUI;
    [SerializeField] private TextMeshProUGUI timeLeftUI;
    [SerializeField] private TextMeshProUGUI transportText;
    
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private TextMeshProUGUI chatText;
    [SerializeField] public Sprite bossGreedPicture;
    [SerializeField] public Sprite daJudgePicture;
    [SerializeField] public Image chatImage;

    [SerializeField] public GameObject bossGreed;
    
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
    
    private List<string> judgeMessages = new List<string>
    {
        "We'll never win at this rate, let's get more voters\n\n",
        "Judge, we'll never win the election without more voters!",
        "True, I'll help you get more voters, but I need a promise.",
        "Anything, how can I help?",
        "When you're elected and in power, I want a seat in the New York State Supreme Court",
        "Done! I'll need you there anyway to grow influence outside of the city.",
        "Great, well more voters is easy, they just need documents, you can fill them out here.",
        "Just press the buttons in the order they appear on the screen, remember it's just a game!",
        "Right I almost forgot! Let's get to work!",
    };

    private List<int> greedPicturesJudgeText = new List<int>
    {
        0, 1, 3, 5, 8
    };

    private int judgeTextIndex = 0;

    private bool reading = true;
    private int introTextIndex = 0;

    private int totalVoted = 0;
    private bool introBarnard = false;
    private bool hasEnteredCourthouse = false;
    
    // Start is called before the first frame update
    void Start()
    {
        chatImage.sprite = bossGreedPicture;
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
        else if (introBarnard)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                if (judgeTextIndex == 0)
                {
                    GoToCourthouse();
                }
                
                judgeTextIndex++;
                chatImage.sprite = greedPicturesJudgeText.Contains(judgeTextIndex)
                    ? bossGreedPicture
                    : daJudgePicture;
            }

            if (judgeTextIndex == judgeMessages.Count)
            {
                chatPanel.SetActive(false);
                introBarnard = false;
                hasEnteredCourthouse = true;
                FindObjectOfType<NaturalizationController>().hasStarted = true;
                FindObjectOfType<NaturalizationController>().SpawnKey();
            }
            else
            {
                chatText.text = judgeMessages[judgeTextIndex];
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

            if (hasEnteredCourthouse)
            {
                if (bossGreed.transform.position.x < 20)
                {
                    if (Keyboard.current.wKey.wasPressedThisFrame)
                    {
                        GoToCourthouse();
                    }
                }
            }
        }
    }

    void SetIntroText()
    {
        chatText.text = introChatMessages[introTextIndex];
    }

    void GoToCourthouse()
    {
        Camera.main.transform.position = new Vector3(20, 0, -10);
        bossGreed.transform.position = new Vector3(20, 0, 0);
        transportText.text = "Press F to Enter the Voting Hall";
    }

    public void GoToVotingHall()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
        bossGreed.transform.position = new Vector3(0, 0, 0);
        transportText.text = "Press W to Enter the Courthouse";
        
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
    
    public void PotatoNaturalized()
    {
        potatoesInHand++;
        UpdatePotatoesInHandText();
    }
    
    public void SentPotatoToVote()
    {
        potatoesInHand--;
        UpdatePotatoesInHandText();
        potatoesVoting++;
        UpdatePotatoesVotingText();
        totalVoted++;
        if (totalVoted > 10)
        {
            introBarnard = true;
            chatPanel.SetActive(true);
        }
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

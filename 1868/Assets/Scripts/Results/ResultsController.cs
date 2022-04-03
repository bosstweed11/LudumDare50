using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI republicanMayorVotesUI;
    [SerializeField] private TextMeshProUGUI democratMayorVotesUI;
    [SerializeField] private TextMeshProUGUI republicanComptrollerVotesUI;
    [SerializeField] private TextMeshProUGUI democratComptrollerVotesUI;
    [SerializeField] private TextMeshProUGUI republicanPublicWorksVotesUI;
    [SerializeField] private TextMeshProUGUI democratPublicWorksVotesUI;
    [SerializeField] private TextMeshProUGUI republicanChamberlainVotesUI;
    [SerializeField] private TextMeshProUGUI democratChamberlainVotesUI;
    [SerializeField] private TextMeshProUGUI resultTextUI;
    
    
    private int republicanMayorVotes = 0;
    private int democratMayorVotes = 0;
    private int republicanComptrollerVotes = 0;
    private int democratComptrollerVotes = 0;
    private int republicanPublicWorksVotes = 0;
    private int democratPublicWorksVotes = 0;
    private int republicanCityChamberlainVotes = 0;
    private int democratCityChamberlainVotes = 0;
    // Start is called before the first frame update
    void Start()
    {
        republicanMayorVotes = PlayerPrefs.GetInt("MayorRepublican");
        democratMayorVotes = PlayerPrefs.GetInt("MayorDemocrat");
        republicanComptrollerVotes = PlayerPrefs.GetInt("ComptrollerRepublican");
        democratComptrollerVotes = PlayerPrefs.GetInt("ComptrollerDemocrat");
        republicanPublicWorksVotes =  PlayerPrefs.GetInt("WorksRepublican");
        democratPublicWorksVotes = PlayerPrefs.GetInt("WorksDemocrat");
        republicanCityChamberlainVotes = PlayerPrefs.GetInt("CityRepublican");
        democratCityChamberlainVotes = PlayerPrefs.GetInt("CityDemocrat");

        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void SetText()
    {
        republicanMayorVotesUI.text = republicanMayorVotes.ToString();
        democratMayorVotesUI.text = democratMayorVotes.ToString();
        republicanComptrollerVotesUI.text = republicanComptrollerVotes.ToString();
        democratComptrollerVotesUI.text = democratComptrollerVotes.ToString();
        republicanPublicWorksVotesUI.text = republicanPublicWorksVotes.ToString();
        democratPublicWorksVotesUI.text = democratPublicWorksVotes.ToString();
        republicanChamberlainVotesUI.text = republicanCityChamberlainVotes.ToString();
        democratChamberlainVotesUI.text = democratCityChamberlainVotes.ToString();

        CalculateResultText();
    }

    private void CalculateResultText()
    {
        int democraticRacesWon = 0;
        if (democratMayorVotes > republicanMayorVotes)
            democraticRacesWon++;
        
        if (democratComptrollerVotes > republicanComptrollerVotes)
            democraticRacesWon++;

        if (democratPublicWorksVotes > republicanPublicWorksVotes)
            democraticRacesWon++;

        if (democratCityChamberlainVotes > republicanCityChamberlainVotes)
            democraticRacesWon++;

        if (democraticRacesWon == 0)
        {
            resultTextUI.text = "Oh no! All the elections got away from you this time. Try Again!";
        }
        else if (democraticRacesWon == 1)
        {
            resultTextUI.text = "You won 1 election, Try to win them all next time!";
        }
        else if (democraticRacesWon == 2)
        {
            resultTextUI.text = "2 out of 4 ain't bad, but it ain't control! Try Again!";
        }
        else if (democraticRacesWon == 3)
        {
            resultTextUI.text = "So close! You won 3 out of 4! Try to get them all next time!";
        }
        else if (democraticRacesWon == 4)
        {
            resultTextUI.text = "Congratulations! You control the government! Now be nice!";
        }
    }
}

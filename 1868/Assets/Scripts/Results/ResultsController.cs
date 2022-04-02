using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    }
}

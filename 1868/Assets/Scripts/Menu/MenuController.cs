using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("VotingHall");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim;
    //int lastLevel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        //SceneManager.LoadScene(lastLevel);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Credits()
    {
        anim.SetBool("Credits", true);
    }
    public void BackToMainMenu()
    {
        anim.SetBool("Credits", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        StartCoroutine(Starting());
        IEnumerator Starting()
        {

            // fadePanelM.DOFade(1, 1);
            
            yield return new WaitForSecondsRealtime(1.1f);
            SceneManager.LoadScene(1);
        }
    }
    public void QuitGame()
    {
        StartCoroutine(Quitting());
        IEnumerator Quitting()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Application.Quit();
        }
    }
}

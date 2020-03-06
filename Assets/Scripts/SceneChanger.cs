using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Title" && SceneManager.GetActiveScene().name != "Instructions")
        {


            StartCoroutine("LoadScreen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Play()
    {
        SceneManager.LoadScene("Load Screen 1");
    }

    public void Defeat()
    {
        SceneManager.LoadScene("Title");
    }

    public IEnumerator LoadScreen()
    {
        yield return new WaitForSecondsRealtime(7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

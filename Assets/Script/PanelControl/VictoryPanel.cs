using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    public GameObject textOfTime;
    public GameObject textOfCount;
    public GameObject panel;
    public void DoAgain()
    {
        SceneManager.LoadScene(1);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound();
    }

    public void ReMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
       textOfTime.GetComponent<Text>().text = panel.GetComponent<GamePanel>().time.ToString("f2");
        textOfCount.GetComponent<Text>().text = panel.transform.Find("Grid").GetComponent<GridControl>().controlCount.ToString();
    }
}

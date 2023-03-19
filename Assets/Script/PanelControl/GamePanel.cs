using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GamePanel : MonoBehaviour
{
    public List<Sprite> sprites;
    public List<GameObject> grids;
    List<GameObject> image_My;
    public GameObject boxPrefab;
    public GameObject timeText;
    public GameObject minimumTimeText;
    public GameObject controlCount;
    public GameObject minimumControlCount;
    public float time;


    private void Awake()
    {
        
        image_My = new List<GameObject>();      
        minimumTimeText.GetComponent<Text>().text = PlayerPrefs.GetFloat("minimum_time").ToString("f2");
        minimumControlCount.GetComponent<Text>().text = PlayerPrefs.GetInt("minimum_count").ToString();
    }
    
    void Start()
    {
        
        InitGame();     

    }

    
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        timeText.GetComponent<Text>().text = time.ToString("f2");          
    }

    void InitGame()
    {
        for (int i = 1; i < 9; i++)
        {
            image_My.Add(GameObject.Instantiate(boxPrefab, grids[i - 1].transform));
        }
        List<int> number = new List<int>();//抽序号
        for (int i = 0; i < 8; i++)
        {
            number.Add(i);
        }
        for (int i = 1; i < 9; i++)
        {
            int x = Random.Range(0,number.Count);
            image_My[i - 1].transform.GetComponent<Image>().sprite = sprites[number[x]];
            number.RemoveAt(x);
        }
    }

    //重新开始
    public void ReStart()
    {
        SceneManager.LoadScene(1);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound();
 
    }

    public void ExsitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeControlCount(int count)
    {
       controlCount.GetComponent<Text>().text = count.ToString();             
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    private Vector3 pointerDownPos, pointerUpPos;
    private Drection drection;
    private List<GameObject> grids;
    public int controlCount;
    public GameObject finishPanel;
    private List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        grids = transform.parent.transform.GetComponent<GamePanel>().grids;
        sprites = transform.parent.transform.GetComponent<GamePanel>().sprites;
        //PlayerPrefs.SetInt("minimum_count", 0);
        //PlayerPrefs.SetFloat("minimum_time", 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown()
    {
        pointerDownPos = Input.mousePosition;
    }

    public void OnPointerUp()
    {
        pointerUpPos = Input.mousePosition;
        MoveDrection();
        Move();
    }

    private void MoveDrection()
    {
        Vector3 temp = pointerUpPos - pointerDownPos;
        
        if (temp.x > 0 && temp.x > (temp.y > 0 ? temp.y : -temp.y) && temp.x > 50)
        {
            drection = Drection.right;            
        }
       if (temp.x < 0 && temp.x < (temp.y < 0 ? temp.y : -temp.y) && temp.x < -50)
        {
            drection = Drection.left;          
        }
        if (temp.y > 0 && temp.y > (temp.x > 0 ? temp.x : -temp.x) && temp.y > -50)
        {
            drection = Drection.up;           
        }
        if (temp.y < 0 && temp.y < (temp.x < 0 ? temp.x : -temp.x) && temp.y < -50)
        {
            drection = Drection.down;
        }

    }

    private void Move()
    {
        switch (drection)
        {
            case Drection.up:
                MoveUp(FindEmpytGrid());
                break;
            case Drection.down:
                MoveDown(FindEmpytGrid());
                break;
            case Drection.left:
                MoveLeft(FindEmpytGrid());
                break;
            case Drection.right:
                MoveRight(FindEmpytGrid());
                break;
        }
    }

    private int FindEmpytGrid()
    {
        for (int i = 8; i >=0; i--)
        {
            if (grids[i].transform.Find("Image(Clone)") == null)
            {
                return i;
            }
        }
        return -1;
    }

    private void MoveUp(int index)
    {
        if (index + 3 > 8)
        { }
        else
        {
            MoveImage(index + 3, index);          
        }
    }
    private void MoveDown(int index)
    {
        if (index - 3 < 0)
        { }
        else
        {
            MoveImage(index - 3, index);            
        }

    }
    private void MoveLeft(int index)
    {
        if (index == 2 || index == 5 || index == 8)
        { }
        else
        {
            MoveImage(index + 1, index);            
        }
    }
    private void MoveRight(int index)
    {
        if (index == 0 || index == 3 || index == 6)
        { }
        else
        {
            MoveImage(index - 1, index);            
        }
    }

    void MoveImage(int target,int index)
    {
        grids[target].transform.Find("Image(Clone)").transform.SetParent(grids[index].transform);
        grids[index].transform.Find("Image(Clone)").transform.position = grids[index].transform.position;
        controlCount++;
        transform.parent.transform.GetComponent<GamePanel>().ChangeControlCount(controlCount);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySound();
        IsFinishGame();
    }


    void IsFinishGame()
    {
        bool win = ISWin();
        finishPanel.SetActive(win) ;
        if (win)
        {
            if (PlayerPrefs.GetInt("minimum_count") == 0)
            {
                PlayerPrefs.SetInt("minimum_count", controlCount);
            }
            else if (PlayerPrefs.GetInt("minimum_count") > controlCount)
            {
                PlayerPrefs.SetInt("minimum_count", controlCount);
            }
            if (PlayerPrefs.GetFloat("minimum_time") == 0f)
            {
                PlayerPrefs.SetFloat("minimum_time", transform.parent.GetComponent<GamePanel>().time);
            }
            else if (PlayerPrefs.GetFloat("minimum_time") > transform.parent.GetComponent<GamePanel>().time)
            {
                PlayerPrefs.SetFloat("minimum_time", transform.parent.GetComponent<GamePanel>().time);
            }
        }    
    }
    bool ISWin()
    {
        for (int i = 0; i < 8; i++)
        {
            if (grids[i].transform.Find("Image(Clone)") == null)
            {
                return false;
            }
            else if (grids[i].transform.Find("Image(Clone)").transform.GetComponent<Image>().sprite != sprites[i])
            {
                return false;
            }           
        }
        return true;
    }
}
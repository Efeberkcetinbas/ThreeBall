using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public List<GameObject> balls=new List<GameObject>();

    public int index;
    

    private void Start()
    {
        OpenSignal();
    }
    
    public void OpenSignal()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].transform.GetChild(0).gameObject.SetActive(false);
            balls[i].transform.GetComponent<SpriteRenderer>().color=Color.red;
            balls[i].GetComponent<BallControl>().canShoot=false;    
        }
        
        //balls[index].transform.GetChild(0).gameObject.SetActive(true);
        balls[index].transform.GetComponent<SpriteRenderer>().color=Color.green;
        balls[index].GetComponent<BallControl>().canShoot=true;    
        //GameManager.Instance.LineOpenControl(index);
    }

    public void IncreaseIndex()
    {
        balls[index].GetComponent<BallControl>().canShoot=false;
        index++;
    }

    public void CheckIndex()
    {
        if (index == balls.Count) index = 0;
    }

    public IEnumerator ResetBallSpeed(float dragPower)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            yield return new WaitForSeconds(1f);
            balls[i].GetComponent<Rigidbody2D>().drag=dragPower;
        }
    }

    public void StopBall()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].GetComponent<Rigidbody2D>().drag=150;
        }
    }
    
}

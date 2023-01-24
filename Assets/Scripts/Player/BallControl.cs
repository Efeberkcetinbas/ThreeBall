using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;


    public Rigidbody2D rb;

    private Vector3 dragStartPos;
    private Touch touch;

    public BallManager ballManager;
    public LineRendererManager lrManager;

    public bool canShoot=false;

    private Camera cm;

    
    private GameManager gameManager;
    private LevelManager levelManager;

    private void Start()
    {
        cm=Camera.main;
        ballManager=FindObjectOfType<BallManager>();
        lrManager=FindObjectOfType<LineRendererManager>();
        gameManager=GameManager.Instance;
        levelManager=LevelManager.Instance;

    }
   
    private void Update()
    {
        if(!GameManager.Instance.isGameEnd)
        {

            if(gameManager.CheckLife()==false)
            {
                gameManager.isGameEnd=true;
                levelManager.RestartLevel();
                Debug.Log("NO RIGHT TO USE IT");
            }

            
            if(canShoot)
                DragControl();
        }
            
    }

    private void DragControl()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                DragRealease();
            }
        }
    }
    private void DragStart()
    {
        dragStartPos = cm.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lrManager.lr.positionCount = 1;
        lrManager.lr.SetPosition(0, dragStartPos);
        gameManager.canCollide=false;
        gameManager.LineOpenControl(ballManager.index);
    }
    private void Dragging()
    {
        Vector3 draggingPos = cm.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lrManager.lr.positionCount = 2;
        lrManager.lr.SetPosition(1, draggingPos);
    }
    private void DragRealease()
    {
        lrManager.lr.positionCount = 0;

        Vector3 dragReleasePos = cm.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        gameManager.canCollide=true;

        

        if(gameManager.TurnNumber==0 && !gameManager.success) 
        {
            gameManager.isGameEnd=true;
            levelManager.RestartLevel();
            Debug.Log("FAILLLL");
        }
        else
        {
            gameManager.ChangeTurnNumber(-1);
        }
        StartCoroutine(Call());
    }

    private IEnumerator Call()
    {
        yield return null;
        CallBallManager();
    }

    private void CallBallManager()
    {
        ballManager.IncreaseIndex();
        ballManager.CheckIndex();
        ballManager.OpenSignal();

        /*
        Added For Trying Progress and Requirement
        */
        
    }


}

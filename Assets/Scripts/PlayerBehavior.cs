using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private UIBehavior uibehavior;
    public float speed = 2.5f;
    private float XTrans;
    private float YTrans;
    private bool goalReached = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!goalReached)
        {
            Move();
        }
    }

    private void Move()
    {
        XTrans = Input.GetAxis("Horizontal") * speed;
        YTrans = Input.GetAxis("Vertical") * speed;
        //transform.position = new Vector2(XTrans, YTrans);
        transform.Translate(new Vector2(XTrans, YTrans) * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Goal")
        {
            goalReached = true;
            uibehavior.activateGoalUI();
        }
    }
}

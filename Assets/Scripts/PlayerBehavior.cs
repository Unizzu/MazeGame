using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 2.5f;
    private float XTrans;
    private float YTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        XTrans = Input.GetAxis("Horizontal") * speed;
        YTrans = Input.GetAxis("Vertical") * speed;
        //transform.position = new Vector2(XTrans, YTrans);
        transform.Translate(new Vector2(XTrans, YTrans) * Time.deltaTime);
    }
}

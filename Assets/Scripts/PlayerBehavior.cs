using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private UIBehavior uibehavior;
    [SerializeField] private SpawnItems spawnitems;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private TMP_Text keyNumText;
    [SerializeField] private GameObject lighter;
    [SerializeField] private GameObject blinder;
    [SerializeField] private GameObject effectDisplay1;
    [SerializeField] private GameObject effectDisplay2;
    [SerializeField] private Sprite[] effectSprites = new Sprite[4];
    [SerializeField] private float speed = 2f;
    public int keynum = 0;
    private float XTrans;
    private float YTrans;
    private bool goalReached = false;
    private bool isStarted = false;
    private bool warpCooldown = false;
    private bool isLoading = true;
    private bool blueKeyBuff = false;
    private bool yellowKeyBuff = false;
    private bool cursed = false;
    private bool speedDisplayed = false;
    private bool lightDisplayed = false;
    private bool[] keyInventory = new bool[4];

    private Collider2D collid;
    private CircleCollider2D lightcol;
    private Rigidbody2D rb;
    private Transform blindTransform;
    private Image effectIcon1;
    private Image effectIcon2;
    //private Collider2D lightcol;
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = "Loading...";
        collid = GetComponent<Collider2D>();
        collid.enabled = false;
        lightcol = lighter.GetComponentInChildren<CircleCollider2D>();
        lightcol.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        blindTransform = blinder.GetComponent<Transform>();
        effectIcon1 = effectDisplay1.GetComponent<Image>();
        effectIcon2 = effectDisplay2.GetComponent<Image>();
        effectDisplay1.SetActive(false);
        effectDisplay2.SetActive(false);
        StartCoroutine(LoadWait());
    }

    // Update is called once per frame
    void Update()
    {
        keyNumText.text = keynum.ToString();
        DisplayEffectIcon();
        if(!isStarted && Input.GetKeyDown(KeyCode.Space) && !isLoading)
        {
            displayText.text = "Press 'R' to retry.";
            spawnitems.SetWarps();
            collid.enabled = true;
            lightcol.enabled = true;
            isStarted = true;
        }
        if(!goalReached && isStarted)
        {
            Move();
        }
        /*if(cursed)
        {
            blindTransform.localScale = new Vector3(1f, 1f, 1f);
            //lightcol.radius = 1.3f;
            //speed = 1f;
        }
        else
        {
            blindTransform.localScale = new Vector3(1.4f, 1.4f, 1f);
            //lightcol.radius = 1.75f;
            //speed = 2f;
        }*/
    }

    private void Move()
    {
        XTrans = Input.GetAxis("Horizontal") * speed;
        YTrans = Input.GetAxis("Vertical") * speed;
        //transform.position += new Vector3(XTrans, YTrans, 0);
        //transform.Translate(new Vector2(XTrans, YTrans) * Time.deltaTime);
        rb.velocity += new Vector2(XTrans, YTrans);
        
        /*if(Input.GetKeyDown(KeyCode.N))
        {
            blindTransform.localScale = new Vector3(2f, 2f, 2f);
            lightcol.radius = 2.6f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            blindTransform.localScale = new Vector3(1f, 1f, 1f);
            lightcol.radius = 1.3f;
        }*/
    }
    private void DisplayEffectIcon()
    {
        if (speed != 2.0f || lightcol.radius != 1.75f)
        {
            if (!speedDisplayed)
            {
                if(speed != 2.0f)
                {
                    if (!effectDisplay1.activeSelf)
                    {
                        effectDisplay1.SetActive(true);
                        if (speed > 2.0f)
                            effectIcon1.sprite = effectSprites[0];
                        else
                            effectIcon1.sprite = effectSprites[1];
                    }
                    else
                    {
                        effectDisplay2.SetActive(true);
                        if (speed > 2.0f)
                            effectIcon2.sprite = effectSprites[0];
                        else
                            effectIcon2.sprite = effectSprites[1];
                    }
                    speedDisplayed = true;
                }
            }
            if(!lightDisplayed)
            {
                if(lightcol.radius != 1.75f)
                {
                    if (!effectDisplay1.activeSelf)
                    {
                        effectDisplay1.SetActive(true);
                        if (lightcol.radius > 1.75f)
                            effectIcon1.sprite = effectSprites[2];
                        else
                            effectIcon1.sprite = effectSprites[3];
                    }
                    else
                    {
                        effectDisplay2.SetActive(true);
                        if (lightcol.radius > 1.75f)
                            effectIcon2.sprite = effectSprites[2];
                        else
                            effectIcon2.sprite = effectSprites[3];
                    }
                    lightDisplayed = true;
                }
            }
        }
        else
        {
            if (effectDisplay1.activeSelf)
                effectDisplay1.SetActive(false);
            if (effectDisplay2.activeSelf)
                effectDisplay2.SetActive(false);
            speedDisplayed = false;
            lightDisplayed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Key")
        {
            keynum++;
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "BlueKey")
        {
            keyInventory[0] = true;
            speed = 3f;
            blueKeyBuff = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "GreenKey")
        {
            keyInventory[1] = true;
            if (cursed)
            {
                cursed = false;
                speed = 2f;
                blindTransform.localScale = new Vector3(1.4f, 1.4f, 1f);
                lightcol.radius = 1.75f;
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "YellowKey")
        {
            keyInventory[2] = true;
            blindTransform.localScale = new Vector3(1.8f, 1.8f, 1f);
            lightcol.radius = 2.2f;
            yellowKeyBuff = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "RedKey")
        {
            keyInventory[3] = true;
            cursed = true;
            blindTransform.localScale = new Vector3(1f, 1f, 1f);
            lightcol.radius = 1.3f;
            speed = 1f;
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "Goal")
        {
            goalReached = true;
            displayText.text = " ";
            uibehavior.activateGoalUI();
        }
        if (col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
        {
            displayText.text = "Press Space to Warp.";
        }
        if(col.gameObject.tag == "Button")
        {
            ButtonBehavior button = col.GetComponent<ButtonBehavior>();
            button.Pressed();
        }
        if (col.gameObject.tag == "SpeedEffect")
        {
            SpeedTileBehavior stb = col.GetComponent<SpeedTileBehavior>();
            if(stb.checkActivation())
            {
                if(stb.giveEffect() || !blueKeyBuff)
                {
                    speed += 0.5f;
                    StartCoroutine(tileSpeedUp());
                }
                else
                {
                    speed -= 0.5f;
                    StartCoroutine(tileSpeedDown());
                }
            }
            
        }
        if(col.gameObject.tag == "LightEffect")
        {
            LightTileBehavior ltb = col.GetComponent<LightTileBehavior>();
            if(ltb.checkActivation())
            {
                if(ltb.giveEffect() && !yellowKeyBuff)
                {
                    blindTransform.localScale = new Vector3(1.6f, 1.6f, 1f);
                    lightcol.radius += 0.225f;
                    StartCoroutine(tileLightUp());
                }
                else
                {
                    blindTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
                    lightcol.radius -= 0.225f;
                    StartCoroutine(tileLightDown());
                }
            }
        }
    }

    

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
        {
            WarpBehavior wp = col.GetComponent<WarpBehavior>();
            if (Input.GetKeyDown(KeyCode.Space) && !warpCooldown)
            {
                collid.enabled = false;
                wp.Teleport(gameObject.transform);
                collid.enabled = true;
                warpCooldown = true;
                StartCoroutine(WarpingCooldown());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "RedWarp" || col.gameObject.tag == "BlueWarp")
        {
            displayText.text = "Press 'R' to retry.";
        }
        if (col.gameObject.tag == "Button")
        {
            ButtonBehavior button = col.GetComponent<ButtonBehavior>();
            button.Released();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" && keynum >= 1)
        {
            displayText.text = "Press Space to Unlock.";
        }
        if (col.gameObject.tag == "BulkLock")
        {
            if(keynum >= 3)
                displayText.text = "Press Space to Unlock.";
            else
                displayText.text = "Not Enough Keys.";
        }
        if(col.gameObject.tag == "BlueLock")
        {
            if (keyInventory[0])
            {
                displayText.text = "Press Space to Unlock.";
            }
        }
        if (col.gameObject.tag == "GreenLock")
        {
            if (keyInventory[1])
            {
                displayText.text = "Press Space to Unlock.";
            }
        }
        if (col.gameObject.tag == "YellowLock")
        {
            if (keyInventory[2])
            {
                displayText.text = "Press Space to Unlock.";
            }
        }
        if (col.gameObject.tag == "RedLock")
        {
            if(keyInventory[3])
            {
                displayText.text = "Press Space to Unlock.";
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" && keynum >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keynum--;
                displayText.text = "Press 'R' to retry.";
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "BulkLock" && keynum >= 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keynum -= 3;
                displayText.text = "Press 'R' to retry.";
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "BlueLock" && keyInventory[0])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInventory[0] = false;
                displayText.text = "Press 'R' to retry.";
                RemoveColoredLocks(col.gameObject.tag);
                speed = 2f;
                blueKeyBuff = false;
            }
        }
        if (col.gameObject.tag == "GreenLock" && keyInventory[1])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInventory[1] = false;
                displayText.text = "Press 'R' to retry.";
                RemoveColoredLocks(col.gameObject.tag);
            }
        }
        if (col.gameObject.tag == "YellowLock" && keyInventory[2])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInventory[2] = false;
                displayText.text = "Press 'R' to retry.";
                RemoveColoredLocks(col.gameObject.tag);
                blindTransform.localScale = new Vector3(1.4f, 1.4f, 1f);
                lightcol.radius = 1.75f;
                yellowKeyBuff = false;
            }
        }
        if (col.gameObject.tag == "RedLock" && keyInventory[3])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInventory[3] = false;
                displayText.text = "Press 'R' to retry.";
                RemoveColoredLocks(col.gameObject.tag);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Lock" || col.gameObject.tag == "BulkLock" || col.gameObject.tag == "BlueLock" || col.gameObject.tag == "YellowLock" || col.gameObject.tag == "GreenLock" || col.gameObject.tag == "RedLock")
        {
            displayText.text = "Press 'R' to retry.";
        }
    }

    private void RemoveColoredLocks(string tag)
    {
        foreach (GameObject locks in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(locks);
        }
    }

    IEnumerator WarpingCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        warpCooldown = false;
    }

    IEnumerator LoadWait()
    {
        yield return new WaitForSeconds(3.0f);
        isLoading = false;
        displayText.text = "Press Space to Start.";
    }

    IEnumerator tileSpeedUp()
    {
        yield return new WaitForSeconds(5.0f);
        speed -= 0.5f;
    }

    IEnumerator tileSpeedDown()
    {
        yield return new WaitForSeconds(5.0f);
        speed += 0.5f;
    }

    IEnumerator tileLightUp()
    {
        yield return new WaitForSeconds(5.0f);
        blindTransform.localScale = new Vector3(1.4f, 1.4f, 1f);
        lightcol.radius -= 0.225f;
    }

    IEnumerator tileLightDown()
    {
        yield return new WaitForSeconds(5.0f);
        blindTransform.localScale = new Vector3(1.4f, 1.4f, 1f);
        lightcol.radius += 0.225f;
    }
}

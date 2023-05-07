using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject loadStart;
    [SerializeField] private GameObject loadEnd;
    [SerializeField] private GameObject goalTile;
    [SerializeField] private GameObject pathTile;
    [SerializeField] private GameObject blockTile;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject lockTile;
    [SerializeField] private GameObject bulkLockTile;
    [SerializeField] private GameObject lightBlock;
    [SerializeField] private GameObject cursedBlockOn;
    [SerializeField] private GameObject cursedBlockOff;
    [SerializeField] private GameObject[] warps = new GameObject[4];
    [SerializeField] private GameObject[] buttons = new GameObject[4];
    [SerializeField] private GameObject[] coloredBlocks = new GameObject[8];
    [SerializeField] private GameObject[] coloredKeys = new GameObject[4];
    [SerializeField] private GameObject[] coloredLocks = new GameObject[4];
    [SerializeField] private GameObject[] effectTiles = new GameObject[4];
    [SerializeField] private Tilemap tm;
    [SerializeField] private Transform tileGroup;
    [SerializeField] private Transform itemGroup;
    //[SerializeField] private TileBase newTile;


    private Vector3Int StartPos;
    private Vector3Int EndPos;
    private Vector3 SpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = tm.WorldToCell(loadStart.transform.position);
        EndPos = tm.WorldToCell(loadEnd.transform.position);
        Spawn();
        Destroy(loadStart);
        Destroy(loadEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        for (int x = StartPos.x; x <= EndPos.x; x++)
        {
            for (int y = StartPos.y; y >= EndPos.y; y--)
            {
                TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                
                if (tile != null)
                {
                    string tileName = tile.name;
                    /*if (tile.name == "goal 1")
                    {
                        //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                        SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                        Instantiate(goalTile, SpawnPos, Quaternion.identity);
                        tm.SetTile(new Vector3Int(x, y, 0), null);
                    }*/
                    switch (tileName)
                    {
                        case "player":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            player.position = SpawnPos;
                            break;
                        case "goal 1":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(goalTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "tile 1":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(blockTile, SpawnPos, Quaternion.identity, tileGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "path":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "key":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(key, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "keyblue":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredKeys[0], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "keygreen":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredKeys[1], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "keyyellow":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredKeys[2], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "keyred":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredKeys[3], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "locktile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(lockTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "3locktile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(bulkLockTile, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "bluelock":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredLocks[0], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "greenlock":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredLocks[1], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "yellowlock":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredLocks[2], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "redlock":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredLocks[3], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "warpred":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(warps[0], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "warpblue":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(warps[1], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "warpyellow":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(warps[2], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "warpgreen":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(warps[3], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonred":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(buttons[0], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblue":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(buttons[1], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonyellow":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(buttons[2], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttongreen":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(buttons[3], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockred":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[0], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockblue":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[1], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "lightwall":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[2], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockgreen":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[3], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockredoff":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[4], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockblueoff":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[5], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockyellowoff":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[6], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "buttonblockgreenoff":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(coloredBlocks[7], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "lightblock":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(lightBlock, SpawnPos, Quaternion.identity, tileGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "curseblockon":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(cursedBlockOn, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "curseblockoff":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(pathTile, SpawnPos, Quaternion.identity, tileGroup);
                            Instantiate(cursedBlockOff, SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "speeduptile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(effectTiles[0], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "speeddowntile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(effectTiles[1], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "lightuptile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(effectTiles[2], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        case "lightdowntile":
                            SpawnPos = tm.GetCellCenterWorld(new Vector3Int(x, y, 0));
                            Instantiate(effectTiles[3], SpawnPos, Quaternion.identity, itemGroup);
                            tm.SetTile(new Vector3Int(x, y, 0), null);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public void SetWarps()
    {
        foreach (GameObject warps in GameObject.FindGameObjectsWithTag("RedWarp"))
        {
            WarpBehavior wp = warps.GetComponent<WarpBehavior>();
            wp.findOtherWarp();
        }
        foreach (GameObject warps in GameObject.FindGameObjectsWithTag("BlueWarp"))
        {
            WarpBehavior wp = warps.GetComponent<WarpBehavior>();
            wp.findOtherWarp();
        }
        foreach (GameObject warps in GameObject.FindGameObjectsWithTag("GreenWarp"))
        {
            WarpBehavior wp = warps.GetComponent<WarpBehavior>();
            wp.findOtherWarp();
        }
        foreach (GameObject warps in GameObject.FindGameObjectsWithTag("YellowWarp"))
        {
            WarpBehavior wp = warps.GetComponent<WarpBehavior>();
            wp.findOtherWarp();
        }
    }

    public void LightUpBlocks()
    {
        foreach (GameObject lightblock in GameObject.FindGameObjectsWithTag("Lightblock"))
        {
            Animator blockanim = lightblock.GetComponent<Animator>();
            SpriteRenderer blocksr = lightblock.GetComponent<SpriteRenderer>();
            blockanim.SetBool("IsActivated", true);
            blocksr.sortingLayerName = "Items";
        }
    }

    public void CurseCall()
    {
        foreach (GameObject blocks in GameObject.FindGameObjectsWithTag("Curseblock"))
        {
            ButtonBlockBehavior blockScript = blocks.GetComponent<ButtonBlockBehavior>();
            blockScript.ButtonSwitch();
        }
    }
}

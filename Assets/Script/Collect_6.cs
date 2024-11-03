using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Collect6 : MonoBehaviour
{
    public Tilemap tilemap;
    public bool hasDragonBall = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector3 hitPosition = contact.point;
                Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    UnityEngine.Debug.Log("Tile name: " + tile.name);

                    if (tile.name == "red_apple")
                    {
                        tilemap.SetTile(cellPosition, null);
                    }
                    else if (tile.name == "treasure")
                    {
                        hasDragonBall = true;
                        tilemap.SetTile(cellPosition, null);
                        UnityEngine.Debug.Log(hasDragonBall);
                    }
                }
            }
        }
    }
}
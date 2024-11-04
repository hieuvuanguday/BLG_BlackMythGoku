using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Collect : MonoBehaviour
{
    public Tilemap tilemap;
    public bool hasDragonBall = false;
    public HealthBar healthBar;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public ManaBar manaBar;
    public TileBase emptyTile;

    void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        manaBar = FindObjectOfType<ManaBar>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector3 hitPosition = contact.point;
                Vector3Int cellPosition = tilemap?.WorldToCell(hitPosition) ?? Vector3Int.zero;

                TileBase tile = tilemap?.GetTile(cellPosition);
                if (tile != null)
                {
                    if (tile.name == "red_apple")
                    {
                        playerMovement.currentStamina = Mathf.Min(playerMovement.currentStamina + 5, playerMovement.maxStamina);
                        manaBar.HandleManaBar(playerMovement.currentStamina, playerMovement.maxStamina);
                        Score.scoreValue += 5;
                        tilemap.SetTile(cellPosition, emptyTile);
                    }
                    else if (tile.name == "treasure")
                    {
                        hasDragonBall = true;
                        tilemap.SetTile(cellPosition, null);
                    }
                    else if (tile.name == "dead")
                    {
                        playerMovement.currentStamina = 0;
                        manaBar.HandleManaBar(0, playerMovement.maxStamina);

                        healthBar.HandleHealthBar(0, playerHealth.maxHealth);
                        Score.scoreValue = 0;
                        yield return new WaitForSeconds(1f);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
        }
    }
}

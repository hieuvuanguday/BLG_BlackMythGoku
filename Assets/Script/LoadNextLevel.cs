using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public float delaySecond = 2;
    private string[] sceneOrder = { "1st_floor", "2nd_floor", "score_board" };
    public Collect collect;
    private Enemy enemy;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        collect = FindObjectOfType<Collect>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player" && collect.hasDragonBall == true)
        {
            collision.gameObject.SetActive(false);
            ModeSelect();
        }
    }

    public void ModeSelect()
    {
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delaySecond);


        string currentSceneName = SceneManager.GetActiveScene().name;

        int currentIndex = System.Array.IndexOf(sceneOrder, currentSceneName);

        if (currentIndex >= 0 && currentIndex < sceneOrder.Length - 1)
        {
            string nextSceneName = sceneOrder[currentIndex + 1];
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Không có scene tiếp theo hoặc đã đến scene cuối cùng.");
        }
    }
}
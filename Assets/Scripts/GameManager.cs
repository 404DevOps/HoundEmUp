using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float spawnRange = 4.5f;
    public float fenceRange = 3.0f;
    public int totalSheep = 3;
    public GameObject sheepPrefab;
    public GameObject fencePrefab;
    public GameObject fence;

    public GameObject endScreen;
    public GameObject startScreen;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    int timer;
    int score;

    public float centerXOffset;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateScore(int Score)
    {
        score += Score;
        scoreText.SetText("Score: " + score);
    }

    public void GameOver()
    {
        isGameActive = false;
        endScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        isGameActive = true;
        timerText.gameObject.SetActive(true);
        timer = 120;
        StartCoroutine(countDown());

        startScreen.SetActive(false);
       // SpawnFence();
        SpawnSheep();
    }

    IEnumerator countDown()
    {
        while (isGameActive)
        {
            timer--;
            timerText.SetText("Time: " + timer);

            if (timer == 0)
            {
                GameOver();
            }

            yield return new WaitForSeconds(1);
        }
    }

    Vector3 RandomSpawnPosition()
    {
        //TODO, cant be InsideGutter
        var randX = Random.Range(-spawnRange, spawnRange);
        var randZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(randX, 0, randZ);
    }

    //void SpawnFence()
    //{
    //    fence = Instantiate(fencePrefab, fencePrefab.transform.position, fencePrefab.transform.rotation);
    //}
    public void SpawnSheep()
    {
        Vector3 validPos = RandomSpawnPosition(); ;
        while (IsPosInFence(validPos))
        {
            validPos = RandomSpawnPosition();
        }

        Instantiate(sheepPrefab, validPos, sheepPrefab.transform.rotation);
    }

    public bool IsPosInFence(Vector3 pos)
    {
        //TODO Fix Center Point
        var center = GetParentCenter(fence);
        center.x -= centerXOffset;
        var fenceBounds = new Bounds(center, new Vector3(3, 3, 3));

        if (fenceBounds.Contains(pos))
        {
            return true;
        }
        return false;

    }

    Vector3 GetParentCenter(GameObject obj)
    {
        Vector3 sumVector = new Vector3(0f, 0f, 0f);

        foreach (Transform child in obj.transform)
        {
            sumVector += child.position;
        }

        Vector3 groupCenter = sumVector / obj.transform.childCount;

        return groupCenter;
    }
}

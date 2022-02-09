using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float spawnRange = 4.5f;
    public float fenceRange = 3.0f;
    public int totalSheep = 3;
    public GameObject sheepPrefab;
    public GameObject fencePrefab;
    private GameObject fence;

    public GameObject endScreen;
    public GameObject startScreen;

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
        startScreen.SetActive(false);
        SpawnFence(); 
        SpawnSheep(totalSheep);
        isGameActive = true;
    }

    Vector3 RandomFencePosition()
    {
        //TODO, cant be InsideGutter
        float randX = Random.Range(-5, 1);
        float randZ = Random.Range(-4, 2.4f);

        return new Vector3(randX, fencePrefab.transform.position.y, randZ);
    }

    Vector3 RandomSpawnPosition()
    {
        //TODO, cant be InsideGutter
        var randX = Random.Range(-spawnRange, spawnRange);
        var randZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(randX, 0, randZ);
    }

    void SpawnFence()
    {
        fence = Instantiate(fencePrefab, RandomFencePosition(), fencePrefab.transform.rotation);
    }
    void SpawnSheep(int amount)
    {
        for (int i = 0; i < amount; i++)
		{
            Vector3 validPos = RandomSpawnPosition(); ;
            while (IsPosInFence(validPos))
            {
                validPos = RandomSpawnPosition();
            }

            Instantiate(sheepPrefab, validPos, sheepPrefab.transform.rotation);
	    }
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

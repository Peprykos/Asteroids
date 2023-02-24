using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    CameraScript cs;
    //odleglosc spawnowania w pionie i poziomie od srodka
    float verticalDistance, horizontalDistance;
    //licznik do nastepnego spawanu
    float spawnTimer;
    //co ile ma byc spawn
    public float spawnInterval = 5;
    //prefab kamulec
    public GameObject kamulecPrefab;
    //ekran konca gry
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        cs = Camera.main.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnInterval)
        {
            //spawnujemy kamien
            Vector3 spawnPosition = getRandomSpawnPosition();
            GameObject asteroid = Instantiate(kamulecPrefab, spawnPosition, Quaternion.identity);
            //liczymy wektor w kierunku gracza
            //resetujemy timer
            spawnTimer = 0;
        }
    }
    Vector3 getRandomSpawnPosition()
    {
        //policz odlegosc spawnowania
        verticalDistance = 0.55f * cs.gameHeight;
        horizontalDistance = 0.55f * cs.gameWidth;
        //losowanie liczby miedzy <1;4>
        int randomSpawnLine = Random.Range(1, 5);
        Vector3 randomSpawnLocation = Vector3.zero;
        switch (randomSpawnLine)
        {
            case 1:
                //gorna linia
                randomSpawnLocation = new Vector3(Random.Range(-horizontalDistance, horizontalDistance),
                                                                0,
                                                                verticalDistance);
                break;
            case 2:
                //prawa linia
                randomSpawnLocation = new Vector3(horizontalDistance,
                                                    0,
                                                    Random.Range(-verticalDistance, verticalDistance));
                break;
            case 3:
                //dolna linia
                randomSpawnLocation = new Vector3(Random.Range(-horizontalDistance, horizontalDistance),
                                                                0,
                                                                -verticalDistance);
                break;
            case 4:
                //lewa linia
                randomSpawnLocation = new Vector3(-horizontalDistance,
                                                    0,
                                                    Random.Range(-verticalDistance, verticalDistance));
                break;
        }
        return randomSpawnLocation;
    }
    public void GameOver()
    {
        //zatrzymaj czas
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}

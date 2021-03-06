using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    public GameObject basicEnemy;
    public Text waveNum;
    
    public float timeBetweenWaves;
    public float timeBeforeRoundStarts;
    public float timeVariable;

    public bool isRoundGoing;
    public bool isIntermission;
    public bool isStartOfRound;

    public int round;

    private void Start()
    {
        isRoundGoing = false;
        isIntermission = false;
        isStartOfRound = true;
        timeVariable = Time.time + timeBeforeRoundStarts;
        round = 1;
    }

    private void spawnEnemies()
    {
        StartCoroutine("ISpawnEnemies");
    }

    IEnumerator ISpawnEnemies()
    {
        for (int i = 0; i < round; i++)
        {
            GameObject newEnemy = Instantiate(basicEnemy, MapGenerator.startTile.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        waveNum.text = "Wave: " + round.ToString();

        if (isStartOfRound)
        {
            if (Time.time >= timeVariable)
            {
                isStartOfRound = false;
                isRoundGoing = true;

                spawnEnemies();
            }
        } 
        else if (isIntermission)
        {
            // If timeBetweenWaves amount of time has passed
            // from previous round
            if (Time.time >= timeVariable)
            {
                isIntermission = false;
                isStartOfRound = true;
            }
        }
        else if (isRoundGoing)
        {
            if (Enemies.enemies.Count <= 0)
            {
                isIntermission = true;
                isRoundGoing = false;

                timeVariable = Time.time + timeBetweenWaves;
                round++;
            }
        }
    }
}

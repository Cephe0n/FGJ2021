using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
using TMPro;


public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    public List<GameObject> PooledMeleeEnemies, PooledRangedEnemies;
    GameObject[] SpawnPoints;
    public GameObject MeleeEnemy, RangeEnemy;
    public int PoolAmount, MaxRangedEnemies, MaxMeleeEnemies, EnemiesKilled;
    int currentMelee, currentRanged, targetFPS;
    public TMP_Text KillsText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetFPS = 80;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
        MasterAudio.PlaySound("begin");
        MasterAudio.PlaySound("AmbientWindLoop");

        PooledMeleeEnemies = new List<GameObject>();
        PooledRangedEnemies = new List<GameObject>();
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        GameObject me, re;

        for (int i = 0; i < PoolAmount; i++)
        {
            re = Instantiate(RangeEnemy);
            me = Instantiate(MeleeEnemy);

            re.SetActive(false);
            me.SetActive(false);

            PooledRangedEnemies.Add(re);
            PooledMeleeEnemies.Add(me);
        }

        StartCoroutine(SpawnEnemies());
    }

    public GameObject GetPooledEnemy(int pType)
    {
        for (int i = 0; i < PoolAmount; i++)
        {
            if (pType == 1 && PooledMeleeEnemies[i] != null)
            {
                if (!PooledMeleeEnemies[i].activeInHierarchy)
                    return PooledMeleeEnemies[i];
            }
            else if (pType == 2 && PooledRangedEnemies[i] != null)
            {
                if (!PooledRangedEnemies[i].activeInHierarchy)
                    return PooledRangedEnemies[i];
            }
        }
        return null;
    }

    IEnumerator SpawnEnemies()
    {
        if (currentMelee < MaxMeleeEnemies)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));

            Transform mSpawn = SpawnPoints[Random.Range(1, SpawnPoints.Length)].transform;
            GameObject currEnemy = GetPooledEnemy(1);
            if (currEnemy != null)
            {
                currEnemy.transform.position = mSpawn.position;
                currEnemy.SetActive(true);
            }

        }

        if (currentRanged < MaxRangedEnemies)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));

            Transform rSpawn = SpawnPoints[Random.Range(1, SpawnPoints.Length)].transform;
            GameObject currRenemy = GetPooledEnemy(2);
            if (currRenemy != null)
            {
                currRenemy.transform.position = rSpawn.position;
                currRenemy.SetActive(true);
            }

        }

        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        KillsText.text = "Enemies Killed: " + EnemiesKilled;
    }
}

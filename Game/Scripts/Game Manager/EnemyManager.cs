using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private GameObject soldier_prefab,boar_prefab;

    public Transform[] soldier_spawn_points, boar_spawn_points, soldier_spawn_points_town;

    [SerializeField] private int soldier_enemy_count, boar_enemy_count, soldier_enemy_count_town;

    private int initial_soldier_count, initial_boar_count, initial_soldier_count_town;

    public float wait_before_spawn_enemies_time = 10f;

    private int number_of_died_soldiers=0, number_of_died_boars=0;


    // Start is called before the first frame update
    private void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        initial_soldier_count = soldier_enemy_count;
        initial_boar_count = boar_enemy_count;
        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
        //Invoke("SpawnEnemies",10f);


    }

    void SpawnEnemies()
    {
        SpawnSoldiers();
        SpawnBoars();
        SpawnSoldiers_town();
    }

    void SpawnSoldiers()
    {
        Debug.Log("Spawning soldiers  : ");
        
        int index = 0;
        for (int i=0; i< soldier_enemy_count;i++)
        {
            if (index >=soldier_spawn_points.Length)
            {
                index = 0;
            }

            Instantiate(soldier_prefab,soldier_spawn_points[index].position,Quaternion.identity);
            index++;
        }
        soldier_enemy_count = 0;


    }

    void SpawnSoldiers_town()
    {
        Debug.Log("Spawning soldiers town");
        int index = 0;
        for (int i = 0; i < soldier_enemy_count_town; i++)
        {
            if (index >= soldier_spawn_points_town.Length)
            {
                index = 0;
            }

            Instantiate(soldier_prefab, soldier_spawn_points_town[index].position, Quaternion.identity);
            index++;
        }
        soldier_enemy_count_town = 0;


    }

    void SpawnBoars()
    {
        Debug.Log("Spawning boars");
        int index = 0;
        for (int i = 0; i < boar_enemy_count; i++)
        {
            if (index >= boar_spawn_points.Length)
            {
                index = 0;
            }

            Instantiate(boar_prefab, boar_spawn_points[index].position, Quaternion.identity);
            index++;
        }
        boar_enemy_count = 0;


    }


    public void EnemyDied(string enemyType)
    {
        if (enemyType.Equals("soldier"))
        {
            number_of_died_soldiers++;

            if (number_of_died_soldiers >= initial_soldier_count)
            {
                number_of_died_soldiers = 0;
            }

            Debug.Log("Calling Enemy manager");

            soldier_enemy_count++;
            if (soldier_enemy_count > initial_soldier_count)
            {
                soldier_enemy_count = initial_soldier_count;

            }

            Invoke("SpawnSoldierAfterDie", 10f);
        }else if (enemyType.Equals("boar"))
        {
            number_of_died_boars++;

            if (number_of_died_boars >= initial_boar_count)
            {
                number_of_died_boars = 0;
            }

            Debug.Log("Calling Enemy manager");

            boar_enemy_count++;
            if (boar_enemy_count > initial_boar_count)
            {
                boar_enemy_count = initial_boar_count;

            }

            Invoke("SpawnBoarAfterDie", 10f);
        }


        //StartCoroutine("CheckToSpawnEnemies");


    }


    private void SpawnSoldierAfterDie()
    {
        if (number_of_died_soldiers>=3)
        {
            Instantiate(soldier_prefab, soldier_spawn_points[Random.Range(0, soldier_spawn_points.Length-1)].position, Quaternion.identity);
        }
        
    }

    private void SpawnBoarAfterDie()
    {
        if (number_of_died_soldiers >= 0)
        {
            Instantiate(soldier_prefab, soldier_spawn_points[Random.Range(0, boar_spawn_points.Length-1)].position, Quaternion.identity);
        }

    }


    IEnumerator CheckToSpawnEnemies()
    {
        Debug.Log("Waiting for spawn !");
        yield return new WaitForSeconds(wait_before_spawn_enemies_time);
        SpawnEnemies();
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }


}//class

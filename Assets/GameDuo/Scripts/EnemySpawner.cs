using GameDuo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    void Awake()
    {
        GameManager.Enemy.InitEnemySpawn(this);
    }

    public void Spawn(int index)
    {
        var spawnedObject = Instantiate(enemies[index].gameObject, transform);
        var enemy = spawnedObject.GetComponent<Enemy>();
        enemy.InitEnemy(index, player);
    }

}

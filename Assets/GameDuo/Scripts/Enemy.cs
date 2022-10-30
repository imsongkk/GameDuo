using GameDuo.Data;
using GameDuo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    EnemyInfo enemyInfo;
    Player player;

    [SerializeField] Image hpBarBackground;
    [SerializeField] Image hpBar;

    int currentHeart, maxHeart;

    void Start()
    {
        player.EnemyQueue.Enqueue(this);
        MoveHpBar();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while(true)
        {
            transform.position += new Vector3(-1,0,0) * 0.5f * Time.deltaTime;
            MoveHpBar();
            if (IsAttackable())
            {
                StartCoroutine(Attack());
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        var cached = new WaitForSeconds(1f);
        while(true)
        {
            player.OnDamage(enemyInfo.attack);
            if (player.IsDied())
                yield break;
            yield return cached;
        }
    }

    public bool IsDied() => currentHeart <= 0;

    public void OnDamge(int attack)
    {
        currentHeart -= (attack - enemyInfo.defense);
        RefreshHpBar();
        if (IsDied())
        {
            player.EnemyQueue.Dequeue();
            player.isAttacking = false;
        }
    }

    private bool IsAttackable()
    {
        var dist = Vector2.Distance(transform.position, player.transform.position);
        return dist <= 0.7f;
    }

    public void InitEnemy(int index, Player player)
    {
        enemyInfo = GameManager.Data.EnemyData.enemies[index];
        this.player = player;
        currentHeart = enemyInfo.heart;
        maxHeart = currentHeart;
    }

    private void RefreshHpBar()
    {
        hpBar.fillAmount = currentHeart / (float)maxHeart;
    }

    private void MoveHpBar()
    {
        hpBarBackground.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0,0.2f, 0));
    }
}

using GameDuo.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image hpBarBackground;
    [SerializeField] Image hpBar;
    
    public Queue<Enemy> EnemyQueue { get; set; } = new Queue<Enemy>();

    int attack, defense, heart;
    int maxHeart;

    public bool isAttacking = false;

    public void Start()
    {
        attack = GameManager.Data.UserData.Attack.value;
        defense = GameManager.Data.UserData.Defense.value;
        heart = GameManager.Data.UserData.Heart.value;
        maxHeart = GameManager.Data.UserData.Heart.GetMaxHp(GameManager.Data.UserData.Heart.level);
        RefreshHP();

        hpBarBackground.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.15f, 0));
    }

    public void Update()
    {
        if (isAttacking) return;

        if(EnemyQueue.Count > 0)
        {
            var enemy = EnemyQueue.Peek();
            isAttacking = true;
            StartCoroutine(Attack(enemy));
            Debug.Log("A");
        }
    }

    IEnumerator Attack(Enemy target)
    {
        var cached = new WaitForSeconds(1f);
        while(true)
        {
            target.OnDamge(attack);
            Debug.Log(attack);
            if (target.IsDied())
            {
                Destroy(target.gameObject);
                yield break;
            }
            yield return cached;
        }
    }

    public void OnDamage(int damage)
    {
        heart -= (damage - defense);
        RefreshHP();
        if (heart <= 0)
            Die();
    }

    public bool IsDied() => heart <= 0;

    private void Die()
    {
        Debug.Log("DIe");
    }

    private void RefreshHP()
    {
        hpBar.fillAmount = heart / (float)maxHeart;
    }
}

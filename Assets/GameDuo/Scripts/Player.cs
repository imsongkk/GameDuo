using GameDuo.Managers;
using GameDuo.UI.Popup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image hpBarBackground;
    [SerializeField] Image hpBar;
    
    public Queue<Enemy> EnemyQueue { get; set; } = new Queue<Enemy>();

    public bool isAttacking = false;

    public void Start()
    {
        GameManager.User.Player = this;
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
            var damage = GetRealAttackDamage();
            target.OnDamge(damage);
            if (target.IsDied())
            {
                var xp = target.EnemyInfo.xp;
                GameManager.User.EnemyKill(xp);
                Destroy(target.gameObject);
                yield break;
            }
            yield return cached;
        }
    }

    public void OnDamage(int damage)
    {
        GameManager.Data.UserData.Heart.value -= (damage - GameManager.Data.UserData.Defense.value);
        RefreshHP();
        if (GameManager.Data.UserData.Heart.value <= 0)
            Die();
    }

    private int GetRealAttackDamage()
    {
        var inventory = GameManager.Data.UserData.Item;
        if (inventory.IsExistsItem())
            return inventory.damage + GameManager.Data.UserData.Attack.value;
        else
            return GameManager.Data.UserData.Attack.value;
    }

    public bool IsDied() => GameManager.Data.UserData.Heart.value <= 0;

    private void Die()
    {
        GameManager.UI.ShowPopupUI<UI_GameOverPopup>();
    }

    public void RefreshHP()
    {
        hpBar.fillAmount = GameManager.Data.UserData.Heart.value / (float)GameManager.Data.UserData.Heart.GetMaxHp(GameManager.Data.UserData.Heart.level);
    }
}

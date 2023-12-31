using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private int enemyLife = 10;
    private int enemyMaxLife = 10;
    public int EnemyLife
    {
        get
        {
            return enemyLife;
        }
        set
        {
            enemyLife = value;
            if (OnLifeChanged != null)
            {
                OnLifeChanged.Invoke(); //着火しまーす！
            }
        }
    }
    public int EnemyMaxLife { get => enemyMaxLife; set => enemyMaxLife = value; }
    public delegate void OnLifeChangedDelegate();
    public event OnLifeChangedDelegate OnLifeChanged;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyAction(int turnCount)
    {
        EnemyLife--;
        EnemyLife--;
        Debug.Log($"{turnCount}ターン目のアクションだよ");
        Debug.Log("EnemyActionだよよよ!");
        Debug.Log("勝手に2点食らうよ");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum EnemyKindDebugTest
{
    //! 職業Enum
    ant,        //! アリ
    mantis,     //! カマキリ
    bee,        //! ハチ
    beetle,     //! カブトムシ
}

public abstract class EnemyParentDebugTest : MonoBehaviour
{
    //! enemyステータス群
    private string enemyName;               //! 名前
    private int enemyAttack;                //! 攻撃力
    private int enemyLife;                  //! 現在HP
    private int enemyMaxLife;               //! 最大HP(超過回復防止用)

    //! Enemy詳細ステータス群
    private bool enemyChargeFlag = false;   //!「溜める」管理フラグ
    public int enemyHealValue = 100;        //!回復値
    public int enemyChargeMagnification = 2;//!「溜める」倍率


    //! 各getter,setter
    public string EnemyName { get => enemyName; set => enemyName = value; }
    public int EnemyAttack { get => enemyAttack; set => enemyAttack = value; }
    public int EnemyLife
    {
        get => enemyLife;
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

    //! Enemy初期化時のステータス管理
    public void Initialize(EnemyKindDebugTest enemyKind)
    {
        switch (enemyKind)
        {
            case EnemyKindDebugTest.ant:
                EnemyName = "ant";
                EnemyLife = 500;
                EnemyAttack = 100;
                break;

            case EnemyKindDebugTest.mantis:
                EnemyName = "mantis";
                EnemyLife = 1000;
                EnemyAttack = 200;
                break;

            case EnemyKindDebugTest.bee:
                EnemyName = "bee";
                EnemyLife = 1500;
                EnemyAttack = 300;
                break;

            case EnemyKindDebugTest.beetle:
                EnemyName = "beetle";
                EnemyLife = 2000;
                EnemyAttack = 400;
                break;

            default:
                Debug.Log("その敵は存在しない");
                break;
        }
    }

    //! 「行動パターン」分岐 -> 各クラスに記述を投げる
    public abstract void EnemyActionDebugTest(int turnCount);

    //! 「溜める」行動
    public void EnemyChargeDebugTest()
    {
        enemyAttack *= enemyChargeMagnification; //* 2倍Atk
        enemyChargeFlag = true;
        Debug.Log("EnemyAtk: " + enemyAttack);//*debug
        Debug.Log("EnemyChargedFlag: " + enemyChargeFlag);//*debug
    }

    //! 「溜める」解除_ヘルパー関数
    private void EnemyChargeInvalidDebugTest()
    {
        enemyAttack /= enemyChargeMagnification; //* 2倍Atkを1倍に戻す
        enemyChargeFlag = false;
        Debug.Log("EnemyAtk: " + enemyAttack);//*debug
        Debug.Log("EnemyChargedFlag: " + enemyChargeFlag);//*debug
    }

    //! 「回復」行動
    public void EnemyHealDebugTest()
    {
        int tempHpHeal = EnemyLife + enemyHealValue;
        if (tempHpHeal > EnemyMaxLife)
        {   //! 回復超過。HPMAXLifeを代入
            EnemyLife = EnemyMaxLife;
        }
        else
        {   //! 回復未超過。HPに回復量を加算
            EnemyLife += enemyHealValue;
        }
        Debug.Log("EnemyLife: " + EnemyLife);//*debug
    }

    //!「最前列ランダム単体攻撃」行動
    public void EnemySingleAttackDebugTest(List<PlayerChildTest> characters)
    {
        List<PlayerChildTest> targetGroup = SelectTargetGroupsDebugTest(characters);

        //* debugStart
        int tGcount = 0;
        foreach (PlayerChildTest items in targetGroup)
        {
            Debug.Log("targetGroup" + tGcount + ": " + items);
            tGcount++;
        }
        //* debugEnd

        PlayerChildTest targetCharacter = SelectCharacterFromRowDebugTest(targetGroup);

        Debug.Log("targetCharacter: " + targetCharacter);//*debug

        enemyAttack -= targetCharacter.PlayerDaux;
        targetCharacter.PlayerLife -= enemyAttack;

        //? 攻撃行動後にEnemyが「溜める」状態だった場合、「溜める」解除。
        if (enemyChargeFlag == true)
        {
            EnemyChargeInvalidDebugTest();
        }

        Debug.Log("enemyAttack: " + enemyAttack);//*debug
        Debug.Log("playerLife: " + targetCharacter.PlayerLife);//*debug
        Debug.Log("playerDaux" + targetCharacter.PlayerDaux);//*debug
    }

    public void EnemyGroupAttackDebugTest(List<PlayerChildTest> characters)
    {
        List<PlayerChildTest> targetGroup = SelectTargetGroupsDebugTest(characters);

        //* debugStart
        int tG2Count = 0;
        foreach (PlayerChildTest items in targetGroup)
        {
            Debug.Log("before" + tG2Count + ": " + items);
            tG2Count++;
        }
        //* debugEnd

        foreach (PlayerChildTest Group in targetGroup)
        {
            Group.PlayerLife -= enemyAttack;
        }

        //? 攻撃行動後にEnemyが「溜める」状態だった場合、「溜める」解除。
        if (enemyChargeFlag == true)
        {
            EnemyChargeInvalidDebugTest();
        }

        //* debugStart
        int tG3Count = 0;
        foreach (PlayerChildTest items in targetGroup)
        {
            Debug.Log("after" + tG3Count + ": " + items);
            tG3Count++;
        }
        //* debugEnd
    }

    //! 全キャラクターのリストから最前列のみのリストを新規作成する関数
    public List<PlayerChildTest> SelectTargetGroupsDebugTest(List<PlayerChildTest> characters)
    {
        //? 前列にいるキャラクターだけを含む新しいリストを作成
        List<PlayerChildTest> Groups = characters.Where(c => c.PlayerPosition <= 3).ToList();
        if (Groups.Count == 0)
        {
            //? 中列にいるキャラクターだけを含む新しいリストを作成
            Groups = characters.Where(c => c.PlayerPosition >= 4 && c.PlayerPosition <= 6).ToList();
        }
        if (Groups.Count == 0)
        {
            //? 後列にいるキャラクターだけを含む新しいリストを作成
            Groups = characters.Where(c => c.PlayerPosition >= 7 && c.PlayerPosition <= 9).ToList();
        }
        return Groups; //* toListで生成したいずれかのListを返す
    }

    //! 最前列内のランダムなキャラクタークラスを返す関数_ヘルパー関数
    private PlayerChildTest SelectCharacterFromRowDebugTest(List<PlayerChildTest> Groups)
    {
        int minPlayerPosition = Groups.Min(c => c.PlayerPosition); //* 最前列リスト内の最小値を取得
        int maxPlayerPosition = Groups.Max(c => c.PlayerPosition); //* 最前列リスト内の最大値を取得

        Debug.Log("乱数下限: " + minPlayerPosition);//*debug
        Debug.Log("乱数上限: " + maxPlayerPosition);//*debug

        while (true)
        {
            int randPlayerPosition = UnityEngine.Random.Range(minPlayerPosition, maxPlayerPosition + 1); //* 最小値と最大値の間で乱数を生成
            Debug.Log("乱数選定: " + randPlayerPosition);//*debug

            foreach (PlayerChildTest character in Groups)
            {
                if (character.PlayerPosition == randPlayerPosition) //* 生成した乱数がキャラクターの位置と一致する場合
                {
                    return character; //* 合致したキャラクタークラスを返す
                }
            }
        }
    }
}
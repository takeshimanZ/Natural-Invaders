using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : CharacterScript
{
    private GameObject Sword; // Swordオブジェクト
    private GameObject Shield; // Shieldオブジェクト
    private Vector3 startPos;
    [SerializeField] ParticleSystem potetohit;
    // private bool isSwordOn = false; // Swordオブジェクトのオン/オフの状態を管理
    // private bool isShieldOn = false; // Shieldオブジェクトのオン/オフの状態を管理

    public Potato() : base("Potato", 25, 5, 0, 0, 15, 0)
    {
        // 親クラス(CharacterScript)のコンストラクタを呼び出す
        // ここで追加の初期化などを行うこともできます
    }

    public override void FrontAction()
    {
        base.FrontAction();
        // ToggleSword();
        // 固有キャラの前列行動の処理を追加
    }
    IEnumerator Move()
    {
        Vector3 newEnemyPos = enemyObject.transform.position;
        newEnemyPos.y += 2.0f;
        newEnemyPos.z -= 2.0f;
        float duration = 0.4f; // 移動時間
        float waitTime = 0.2f; // 待機時間
        float elapsed = 0;     // 経過時間

        // 最初の位置からEnemyの位置へ移動
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(startPos, newEnemyPos, elapsed / duration);
            yield return null;
        }

        // 0.2秒待機
        potetohit.Play();
        yield return new WaitForSeconds(waitTime);
        SoundManager.instance.PlaySE(SoundManager.SE_Type.Se17TomatoAttac4);

        // Enemyの位置から最初の位置へ移動
        elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(newEnemyPos, startPos, elapsed / duration);
            yield return null;
        }
    }

    // void ToggleSword()
    // {
    //     // オンの場合はオフに、オフの場合はオンにする
    //     Sword.SetActive(!isSwordOn);
    //     // 状態を反転
    //     isSwordOn = !isSwordOn;
    // }
    private void FrontActionSound()
    {
        // サウンド再生のロジック
        // audioSource.PlayOneShot(sampleSound);  //固有キャラのAudioClip
    }
    private void FrontActionEffect()
    {
        // エフェクトプレハブの生成と再生
        // GameObject effectInstance = Instantiate(attackEffectPrefab, transform.position, Quaternion.identity);
        // Destroy(effectInstance, particleSystem.main.duration);  // エフェクトが終了したら削除
    }

    public override void MiddleAction()
    {
        base.MiddleAction();
        // ToggleShield();
        // Potato独自の中列行動の処理を追加
    }

    // void ToggleShield()
    // {
    //     // オンの場合はオフに、オフの場合はオンにする
    //     Shield.SetActive(!isShieldOn);
    //     // 状態を反転
    //     isShieldOn = !isShieldOn;
    // }
    public override void BackAction()
    {
        base.BackAction();
        // Potato独自の後列行動の処理を追加
    }

    public override void AutoHeal()
    {
        base.AutoHeal();
    }


    private void MiddleActionSound()
    {
        // サウンド再生のロジック
        // audioSource.PlayOneShot(sampleSound);  // 固有キャラのAudioClip
    }
    private void MiddleActionEffect()
    {
        // エフェクトプレハブの生成と再生
        // GameObject effectInstance = Instantiate(attackEffectPrefab, transform.position, Quaternion.identity);
        // Destroy(effectInstance, particleSystem.main.duration);  // エフェクトが終了したら削除
    }

    private void BackActionSound()
    {
        // サウンド再生のロジック
        // audioSource.PlayOneShot(sampleSound);  // 固有キャラのAudioClip
    }
    private void BackActionnEffect()
    {
        // エフェクトプレハブの生成と再生
        // GameObject effectInstance = Instantiate(attackEffectPrefab, transform.position, Quaternion.identity);
        // Destroy(effectInstance, particleSystem.main.duration);  // エフェクトが終了したら削除
    }

    public override void Death()
    {
        base.Death();
        // キャラの生死判定
    }




    // Start is called before the first frame update
    void Start()
    {
        // SwordとShieldオブジェクトを取得
        // Sword = transform.Find("Sword").gameObject;
        // Shield = transform.Find("Shield").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // // マウスの左クリックがされたら
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // オンとオフを切り替えるメソッドを呼び出す
        //     ToggleSword();
        //     ToggleShield();
        // }
    }
}

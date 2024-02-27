// ---------------------------------------------------------  
// IsHitBoss.cs  
//   
// ボスのダメージ処理
//
// 作成日: 2024/2/14
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class BossDamage : MonoBehaviour, IDamaged
{

    #region 変数  

    // アニメーター取得用
    private Animator _bossAnimator = default;

    // ボスのHP
    private int _bossHP = 1;

    private bool isDeath = false;

    #endregion

    #region プロパティ 

    public bool IsDeath 
    { 
        get => isDeath; 
        set => isDeath = value; 
    }
    
    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        // アニメーター取得
        _bossAnimator = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="playerOffensive">プレイヤーの攻撃力</param>
    public void IsHitJudgment(int playerOffensive)
    {
        // ボスのHPを減らす
        _bossHP -= playerOffensive;

        // HPがまだあるか判定
        if (_bossHP <= 0)
        {
            // 撃破アニメーション再生
            _bossAnimator.SetBool("IsCrushing", true);
        }
        else
        {
            // 被弾アニメーション再生
            _bossAnimator.SetBool("IsHit", true);
        }
    }

    /// <summary>
    /// 被弾アニメーション終了時の処理
    /// </summary>
    public void IsHitBall()
    {
        // アニメーション終了
        _bossAnimator.SetBool("IsHit", false);
    }

    /// <summary>
    /// 撃破アニメーション終了時の処理
    /// </summary>
    public void IsCrushing()
    {
        IsDeath = true;
        // 非アクティブ化
        this.gameObject.SetActive(false);
    }

    #endregion

}
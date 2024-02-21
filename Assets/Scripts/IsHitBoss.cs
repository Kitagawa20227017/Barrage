// ---------------------------------------------------------  
// IsHitBoss.cs  
//   
// ボスのダメージ処理
//
// 作成日: 2024/2/14
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class IsHitBoss : MonoBehaviour, IDamaged
{

    #region 変数  

    // アニメーター取得用
    private Animator _bossAnimator = default;

    // ボスのHP
    private int _bossHP = 50;

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
        // ボスのHPがあったらダメージ処理
        // なかったら撃破
        if (_bossHP > 0)
        {
            // HPを減らす
            _bossHP -= playerOffensive;

            // 被弾アニメーション再生
            _bossAnimator.SetBool("IsHit", true);
        }
        else if (_bossHP <= 0)
        {
            // 撃破アニメーション再生
            _bossAnimator.SetBool("IsCrushing", true);
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
        // 非アクティブ化
        this.gameObject.SetActive(false);
    }

    #endregion

}
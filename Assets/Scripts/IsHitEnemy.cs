// ---------------------------------------------------------  
// IsHitEnemy.cs  
//   
// 敵のダメージ処理
//
// 作成日: 2024/2/14
// 作成者: 北川 稔明  
// ---------------------------------------------------------  
using UnityEngine;

public class IsHitEnemy : MonoBehaviour, IDamaged
{

    #region 変数  

    [SerializeField, Header("HP")]
    private int _enemyHP = 1;

    // アニメーター取得用
    private Animator _enemyAnimator = default;


    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        // 初期設定
        _enemyAnimator = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="playerOffensive">プレイヤーの攻撃力</param>
    public void IsHitJudgment(int playerOffensive)
    {
        // HPを減らす
        _enemyHP -= playerOffensive;

        // HPがまだあるか判定
        if (_enemyHP <= 0)
        {
            // アニメーション再生
            _enemyAnimator.SetBool("isHit", true);
        }
        else
        {
            // アニメーション再生
            _enemyAnimator.SetBool("IsHitBall", true);
        }
    }

    /// <summary>
    /// 撃破アニメーション終了時処理
    /// </summary>
    public void OnAnimationEnd()
    {
        // 非アクティブ化
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 被弾アニメーション終了時処理
    /// </summary>
    public void HitBall()
    {
        // アニメーション再生
        _enemyAnimator.SetBool("IsHitBall", false);
    }

    #endregion

}
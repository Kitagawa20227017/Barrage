// ---------------------------------------------------------  
// IsHitEnemy.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class IsHitEnemy : MonoBehaviour,IDamaged
{

    #region 変数  

    Animator _enemyAnimator = default;

    #endregion

    #region プロパティ  
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
    /// 弾が当たったときの処理
    /// </summary>
    /// <param name="isHitTga"></param>
    public void IsHitJudgment()
    {
        _enemyAnimator.SetBool("isHit", true);
    }

    public void OnAnimationEnd()
    {
        this.gameObject.SetActive(false);
    }

    #endregion

}

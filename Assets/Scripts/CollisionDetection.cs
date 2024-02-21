// ---------------------------------------------------------  
// CollisionDetection.cs  
//   
// 当たり判定処理
//
// 作成日: 2024/2/5
// 作成者: 北川 稔明 
// ---------------------------------------------------------  
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    #region 変数  

    // プレイヤーの攻撃力
    private int _playerDamage = 1;

    // どの弾が当たったとき判定するか
    private enum Classification
    {
        PlayerBall,
        EnemyBall
    }

    [SerializeField, Header("判定する弾")]
    private Classification _classification = default;

    // インターフェース用
    private IDamaged _damaged = default;

    // _classificationを文字列化して記憶
    private string _selection = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        // 初期設定
        _selection = _classification.ToString();
        _damaged = this.gameObject.GetComponent<IDamaged>();
    }

    /// <summary>
    /// 弾の当たり判定処理
    /// </summary>
    /// <param name="collision">当たった弾</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 特定のタグのオブジェクトが当たったとき
        if (collision.tag == _selection)
        {
            // ダメージ処理の呼び出し
            _damaged.IsHitJudgment(_playerDamage);
        }
    }

    #endregion

}
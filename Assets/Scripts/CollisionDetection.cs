// ---------------------------------------------------------  
// CollisionDetection.cs  
//   
// 当たり判定処理
//
// 作成日: 2024/2/5
// 作成者: 北川 稔明 
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour
{

    #region 変数  

    #region const定数

    // プレイヤーの撃った弾のタグ
    private const string PLAYER_BALL = "PlayerBall";

    #endregion

    [SerializeField,Header("無敵時間の長さ(敵なら関係なし)")]
    private float _invincibleTime = 2f;

    // 時間計測のタイマー
    private float _timeMemory = 0;

    // 弾が当たったかの判定
    private bool _isHitBall = false;

    // どの弾が当たったとき判定するか
    private enum Classification
    {
        PlayerBall,
        EnemyBall
    }

    [SerializeField,Header("判定する弾")]
    private Classification _classification = default;

    // _classificationを文字列化して記憶
    private string _selection = default; 

    #endregion

    #region メソッド  
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        _selection = _classification.ToString();
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // プレイヤーの無敵判定処理
        if(_isHitBall && _selection != PLAYER_BALL)
        {
            _timeMemory += Time.deltaTime;
            if(_timeMemory >= _invincibleTime)
            {
                _isHitBall = false;
                _timeMemory = 0;
            }
        }
    }

    /// <summary>
    /// 弾の当たり判定処理
    /// </summary>
    /// <param name="collision">当たった弾</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _selection && !_isHitBall)
        {
            _isHitBall = true;
        }
    }

    #endregion

}

// ---------------------------------------------------------  
// IsHitPlayer.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class IsHitPlayer : MonoBehaviour,IDamaged
{

    #region 変数  

    #region consr定数

    // アルファ値の最大
    private const float COLOE_ALPHA = 255f;

    // 点滅時間
    private const float IS_HIT_TIMER = 3f;

    #endregion
    
    // プレイヤーのスプライト
    private SpriteRenderer _target = default;

    // プレイヤーのスプライトカラー
    private Color _targetColor = default;

    // 残機
    private int _remainingLives = 5;

    // 弾が当たったか
    private bool _isHit = false;

    // 時間計測用のタイマー
    private float _timer = 0;

    // 
    private float repeatValue = default;

    // 
    private float _cycle = 0.5f;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        _target = this.gameObject.GetComponent<SpriteRenderer>();
        _targetColor = _target.color;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        if(_isHit)
        {
            _timer += Time.deltaTime;

            // 
            repeatValue = Mathf.Repeat((float)_timer, _cycle);

            // デューティ比を指定して代入
            _targetColor.a = repeatValue >= _cycle * 0.5f ? 1 : 0;
            _target.color = _targetColor;

            if(_timer >= IS_HIT_TIMER)
            {
                _targetColor.a = COLOE_ALPHA;
                _target.color = _targetColor;
                _timer = 0;
                _isHit = false;
            }
        }
    }

    public void IsHitJudgment()
    {
        if(_remainingLives <= 0)
        {
            Debug.Log("Game Over");
            return;
        }

        if(!_isHit)
        {
            _remainingLives--;
            _isHit = true;
        }
    }

    #endregion

}

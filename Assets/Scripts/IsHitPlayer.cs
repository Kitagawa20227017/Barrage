// ---------------------------------------------------------  
// IsHitPlayer.cs  
//   
// プレイヤーのダメージ処理
//
// 作成日: 2024/2/14
// 作成者: 北川 稔明  
// ---------------------------------------------------------  
using UnityEngine;
using TMPro;

public class IsHitPlayer : MonoBehaviour, IDamaged
{

    #region 変数  

    #region consr定数

    // 最大値
    private const int MAX_VALUE = 1;

    // アルファ値の最大
    private const float COLOE_ALPHA = 1f;

    // Mathf.Repeatの上限値
    private const float UPPER_VALUE = 0.5f;

    // 点滅時間
    private const float IS_HIT_TIMER = 2f;

    // 点滅の周期
    private const float CYCLE = 0.25f;

    #endregion

    // 残機の表示
    private TextMeshProUGUI _uGUI = default;

    // プレイヤーのスプライト
    private SpriteRenderer _target = default;

    // プレイヤーのスプライトカラー
    private Color _targetColor = default;

    // 残機
    private int _playerStocks = 5;

    // 弾が当たったか
    private bool _isHit = false;

    // 時間計測用のタイマー
    private float _timer = 0;

    // デューティ比の格納用
    private float _repeatValue = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        // 初期設定
        _target = this.gameObject.GetComponent<SpriteRenderer>();
        _uGUI = GameObject.Find("PlayerStock").GetComponent<TextMeshProUGUI>();
        _targetColor = _target.color;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        // 弾が当たったとき
        if (_isHit)
        {
            _timer += Time.deltaTime;

            // 値の取得
            _repeatValue = Mathf.Repeat((float)_timer, UPPER_VALUE);

            // デューティ比を指定して代入
            _targetColor.a = _repeatValue >= CYCLE ? MAX_VALUE : 0;
            _target.color = _targetColor;

            // 一定時間過ぎたとき
            if (_timer >= IS_HIT_TIMER)
            {
                // 点滅を止める
                _targetColor.a = COLOE_ALPHA;
                _target.color = _targetColor;
                _timer = 0;
                _isHit = false;
            }
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="playerOffensive">プレイヤーの攻撃力</param>
    public void IsHitJudgment(int playerOffensive)
    {
        // 残機がないとき
        if (_playerStocks <= 0)
        {
            Debug.Log("Game Over");
            return;
        }

        // 無敵時間ではないとき
        if (!_isHit && _playerStocks > 0)
        {
            // 残機を減らして無敵付与
            _playerStocks--;
            _uGUI.text = "×" + _playerStocks;
            _isHit = true;
        }
    }

    #endregion

}
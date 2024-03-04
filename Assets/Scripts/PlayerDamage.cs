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

public class PlayerDamage : MonoBehaviour, IDamaged
{

    #region 変数  

    #region consr定数

    // 最大値
    private const int MAX_VALUE = 1;

    // アルファ値の最大
    private const float COLOE_ALPHA_HIDDEN = 0f;

    private const float COLOE_ALPHA_DISPLAY = 1f;

    // Mathf.Repeatの上限値
    private const float UPPER_VALUE = 0.5f;

    // 点滅時間
    private const float IS_HIT_TIMER = 2f;

    // 点滅の周期
    private const float CYCLE = 0.25f;

    #endregion

    [SerializeField,Header("残機")]
    private int _playerStocks = 5;

    [SerializeField, Header("ヒット音")]
    private AudioClip _hitAudio = default;

    [SerializeField, Header("撃墜音")]
    private AudioClip _shootingAudio = default;

    // AudioSource格納用
    private AudioSource _audioSource = default;

    // アニメーター取得用
    private Animator _playerAnimator = default;

    // 残機の表示
    private TextMeshProUGUI _uGUI = default;

    // プレイヤーのスプライト
    private SpriteRenderer _target = default;

    // プレイヤーのスプライトカラー
    private Color _targetColor = default;

    // 時間計測用のタイマー
    private float _timer = 0;

    // デューティ比の格納用
    private float _repeatValue = default;

    // 弾が当たったか
    private bool _isHit = false;

    // 死亡判定
    private bool _isDeath = false;

    // ゲームが止まっているかの判定
    private bool _isStop = false;

    #endregion

    #region プロパティ

    public bool IsDeath 
    { 
        get => _isDeath; 
        set => _isDeath = value; 
    }

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
        _playerAnimator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _targetColor = _target.color;
        _uGUI.text = "×" + _playerStocks;
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
                _targetColor.a = COLOE_ALPHA_DISPLAY;
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
        // ゲームが止まっているとき処理しない
        if(_isStop)
        {
            return;
        }

        // 無敵時間ではないとき
        if (!_isHit && _playerStocks > 0)
        {
            _playerStocks--;
            // 無敵付与
            _uGUI.text = "×" + _playerStocks;
            _isHit = true;
        }

        // 残機がないとき
        if (_playerStocks <= 0)
        {
            _isHit = false;
            IsDeath = true;
            _playerAnimator.SetBool("isShootingDown", true);
            // 音再生
            _audioSource.PlayOneShot(_shootingAudio);
            return;
        }
        else
        {
            // 音再生
            _audioSource.PlayOneShot(_hitAudio);
        }
    }

    /// <summary>
    /// 爆破アニメーション終了処理
    /// </summary>
    public void isShootingDown()
    {
        _targetColor.a = COLOE_ALPHA_HIDDEN;
        _target.color = _targetColor;
    }

    /// <summary>
    /// GameTransitionが呼ぶ処理
    /// </summary>
    public void IsStop()
    {
        _isStop = !_isStop;
    }

    #endregion

}
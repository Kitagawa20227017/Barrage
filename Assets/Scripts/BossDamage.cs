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

    [SerializeField, Header("HP")]
    private int _bossHP = 1;

    [SerializeField, Header("ヒット音")]
    private AudioClip _hitAudio = default;

    [SerializeField,Header("撃墜音")]
    private AudioClip _shootingAudio = default;

    // AudioSource格納用
    private AudioSource _audioSource = default;

    // アニメーター取得用
    private Animator _bossAnimator = default;

    // 撃墜判定
    private bool _isDeath = false;

    // ゲームオーバー判定
    private bool _isGameOver = false;

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
        _bossAnimator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="playerOffensive">プレイヤーの攻撃力</param>
    public void IsHitJudgment(int playerOffensive)
    {
        if(_isGameOver)
        {
            return;
        }

        // ボスのHPを減らす
        _bossHP -= playerOffensive;

        // HPがまだあるか判定
        if (_bossHP <= 0)
        {
            // 撃破アニメーション再生
            _bossAnimator.SetBool("IsCrushing", true);

            // 音再生
            _audioSource.PlayOneShot(_shootingAudio);
        }
        else
        {
            // 被弾アニメーション再生
            _bossAnimator.SetBool("IsHit", true);

            // 音再生
            _audioSource.PlayOneShot(_hitAudio);
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

    public void IsGameOver()
    {
        _isGameOver = !_isGameOver;
    }

    #endregion

}
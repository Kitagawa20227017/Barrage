// ---------------------------------------------------------  
// GameTransition.cs  
//   
// 作成日: 2024/2/22  
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class GameTransition : MonoBehaviour
{

    #region 変数  

    // スクリプト取得用
    private MoveCamera _moveCamera = default;
    private PlayerInput _playerInput = default;
    private PlayerDamage _playerDamage = default;
    private BossDamage _bossDamage = default;

    // オブジェクト取得用
    private GameObject _enemys = default;
    private GameObject _gameOverUI = default;
    private GameObject _gameClearUI = default;
    
    // ループ防止用フラグ
    private bool _isStop = false;

    #endregion

    #region メソッド  
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        // スクリプト取得
        _moveCamera = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
        _playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        _playerDamage = GameObject.Find("Player").GetComponent<PlayerDamage>();
        _bossDamage = GameObject.Find("Boss").GetComponent<BossDamage>();

        // オブジェクト取得
        _enemys = GameObject.Find("Enemys");
        _gameClearUI = GameObject.Find("ClearUI");
        _gameOverUI = GameObject.Find("GamaOverUI");

        // 初期設定
        _gameOverUI.SetActive(false);
        _gameClearUI.SetActive(false);
        _moveCamera.IsMoveCamera();
        _playerInput.IsPlayerInput();
        _isStop = false;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // ゲームクリア処理
        if (_bossDamage.IsDeath && !_isStop)
        {
            // 動きを停止する
            _moveCamera.IsMoveCamera();
            _playerInput.IsPlayerInput();
            _playerDamage.IsStop();

            // 敵の非アクティブ化
            _enemys.SetActive(false);

            // UIのアクティブ
            _gameClearUI.SetActive(true);

            // ループ防止用フラグをたてる
            _isStop = true;
        }

        // ゲームオーバー処理
        if (_playerDamage.IsDeath && !_isStop)
        {
            // 動きを停止する
            _moveCamera.IsMoveCamera();
            _playerInput.IsPlayerInput();
            _playerDamage.IsStop();

            // 敵の非アクティブ化
            _enemys.SetActive(false);

            // UIのアクティブ
            _gameOverUI.SetActive(true);

            // ループ防止用フラグをたてる
            _isStop = true;
        }
    }

    #endregion

}

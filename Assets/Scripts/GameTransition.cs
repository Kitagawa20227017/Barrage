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
    private IsHitPlayer _isHitPlayer = default;

    // オブジェクト取得用
    private GameObject _enemys = default;
    private GameObject _gameOverUI = default;
    private GameObject _gameClearUI = default;
    
    // 
    private bool _isStop = false;

    #endregion

    #region メソッド  
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        // 初期設定
        _moveCamera = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
        _playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        _isHitPlayer = GameObject.Find("Player").GetComponent<IsHitPlayer>();

        _enemys = GameObject.Find("Enemys");
        _gameClearUI = GameObject.Find("ClearUI");
        _gameOverUI = GameObject.Find("GamaOverUI");
        _moveCamera.IsMoveCamera();
        _playerInput.IsPlayerInput();
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // ゲームオーバー処理
        if(_isHitPlayer.IsDeath && !_isStop)
        {
            _moveCamera.IsMoveCamera();
            _playerInput.IsPlayerInput();
            _enemys.SetActive(false);
            _gameOverUI.SetActive(true);
            _isStop = true;
        }
    }

    #endregion

}

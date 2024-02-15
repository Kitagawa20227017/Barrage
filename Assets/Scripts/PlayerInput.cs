// ---------------------------------------------------------  
// PlayerMove.cs  
//   
// プレイヤーの入力処理
//
// 作成日:  2024/2/1
// 作成者:  北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    #region 変数  

    #region const変数

    // 横軸の移動量   
    private const float PLUS_HORIZONMOVE = 5f;
    private const float MINUS_HORIZONMOVE = -5f;

    // 縦軸の移動量
    private const float PLUS_VERTICAL = 4f;
    private const float MINUS_VERTICAL = -4f;

    // キーの入力値の補正
    private const float PLUS = 1f;
    private const float MINUS = -1f;

    // 横軸の移動制限
    private const float PLAYER_MIN_POX_X = -14.5f;
    private const float PLAYER_MAX_POX_X = 14.5f;

    // 縦軸の移動制限
    private const float PLAYER_MIN_POX_Y = -9f;
    private const float PLAYER_MAX_POX_Y = 9f;

    #endregion

    [SerializeField]
    private GameObject _playerBall = default;

    // Animator格納用
    private Animator _playerAnimator = default;

    // プレイヤーの入力方向の格納場所
    private float _horizontal = default;
    private float _vertical = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary> 
    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.SetBool("isLeft",false);
        _playerAnimator.SetBool("isRight", false);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {
        // 入力値の代入
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        #region 入力値の補正

        // 横軸の移動
        if (_horizontal > 0)
        {
            _horizontal = PLUS;
        }
        else if (_horizontal < 0)
        {
            _horizontal = MINUS;
        }
        else
        {
            _horizontal = 0;
        }


        // 縦軸の移動
        if (_vertical > 0)
        {
            _vertical = PLUS;
        }
        else if (_vertical < 0)
        {
            _vertical = MINUS;
        }
        else
        {
            _vertical = 0;
        }

        #endregion

        #region 移動処理

        // 横軸の移動処理
        if (_horizontal == PLUS)
        {
            // アニメーション再生
            _playerAnimator.SetBool("isLeft", false);
            _playerAnimator.SetBool("isRight", true);

            if (gameObject.transform.position.x < PLAYER_MAX_POX_X)
            {
                gameObject.transform.position = 
                    new Vector2(gameObject.transform.position.x + (PLUS_HORIZONMOVE * Time.deltaTime), gameObject.transform.position.y);
            }
        }
        else if (_horizontal == MINUS)
        {
            // アニメーション再生
            _playerAnimator.SetBool("isLeft", true);
            _playerAnimator.SetBool("isRight", false);

            if (gameObject.transform.position.x > PLAYER_MIN_POX_X)
            {
                gameObject.transform.position = 
                    new Vector2(gameObject.transform.position.x + (MINUS_HORIZONMOVE * Time.deltaTime), gameObject.transform.position.y);
            }
        }
        else if (_horizontal == 0)
        {
            // アニメーション再生
            _playerAnimator.SetBool("isLeft", false);
            _playerAnimator.SetBool("isRight", false);

            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }


        // 縦軸の移動処理 
        if (_vertical == PLUS)
        {
            if (gameObject.transform.position.y < PLAYER_MAX_POX_Y)
            {
                gameObject.transform.position = 
                    new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + (PLUS_VERTICAL * Time.deltaTime));
            }
        }
        else if (_vertical == MINUS)
        {
            if (gameObject.transform.position.y > PLAYER_MIN_POX_Y)
            {
                gameObject.transform.position = 
                    new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + (MINUS_VERTICAL * Time.deltaTime));
            }
        }
        else if (_vertical == 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }

        #endregion
    }

    #endregion

}

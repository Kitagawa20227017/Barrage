// ---------------------------------------------------------  
// PlayerMove.cs  
//   
// プレイヤーの入力処理
//
// 作成日:  2024/2/1
// 作成者:  北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
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

    private const float PLAYER_MIN_POX_X = -12.63f;
    private const float PLAYER_MAX_POX_X = 12.63f;

    private const float PLAYER_MIN_POX_Y = 12.63f;
    private const float PLAYER_MAX_POX_Y = 12.63f;



    #endregion

    // プレイヤーの入力方向の格納場所
    private float _horizontal = default;
    private float _vertical = default;


    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {
        // 入力値の代入
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        #region 入力値の補正

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

        if (_horizontal == PLUS )
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (PLUS_HORIZONMOVE * Time.deltaTime), gameObject.transform.position.y);
        }
        else if(_horizontal == MINUS)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (MINUS_HORIZONMOVE * Time.deltaTime), gameObject.transform.position.y);
        }
        else if(_horizontal == 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }

        if (_vertical == PLUS)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y + (PLUS_VERTICAL * Time.deltaTime));
        }
        else if (_vertical == MINUS)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y + (MINUS_VERTICAL * Time.deltaTime));

        }
        else if(_vertical == 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }

        #endregion
    }

    #endregion

}

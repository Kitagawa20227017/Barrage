// ---------------------------------------------------------  
// PlayerMove.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

    #region 変数  

    float _player = 0;
    float _player1 = 0;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
    {
    }
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start ()
    {
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {
        _player = Input.GetAxis("Horizontal");
        _player1 = Input.GetAxis("Vertical");

        if(0 < _player)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (5f * Time.deltaTime), gameObject.transform.position.y);
        }
        else if(_player < 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (-5f * Time.deltaTime), gameObject.transform.position.y);
        }
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }

        if (0 < _player1)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y + (2.5f * Time.deltaTime));
        }
        else if (_player1 < 0)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y + (-2.5f * Time.deltaTime));

        }
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        }
    }

    #endregion

}

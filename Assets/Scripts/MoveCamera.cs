// ---------------------------------------------------------  
// MoveCamera.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{

    #region 変数

    #region const定数

    // カメラの移動スピード
    private const float MOVE_SPEED = 5f;

    #endregion

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
    }
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        if(transform.position.y <= 100f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + MOVE_SPEED * Time.deltaTime, transform.position.z);
        }
    }

    #endregion

}

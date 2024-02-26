// --------------------------------------------------------- 
// MoveCamera.cs                                                
//   
// カメラの移動処理
//
// 作成日: 2024/2/15
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    #region 変数

    // カメラの移動位置
    private const float STOP_CAMERA = 100f;

    // カメラの移動スピード
    [SerializeField, Header("移動スピード")]
    private float _moveSpeed = 4.5f;

    private bool _isMove = false;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        // 特定の位置まで移動する
        if (transform.position.y <= STOP_CAMERA && _isMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _moveSpeed * Time.deltaTime, transform.position.z);
        }
    }

    public void IsMoveCamera()
    {
        _isMove = !_isMove;
        Debug.Log(_isMove);
    }

    #endregion

}
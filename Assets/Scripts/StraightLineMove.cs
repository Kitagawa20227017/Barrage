// ---------------------------------------------------------  
// StraightLine.cs  
//   
// 真っ直ぐ進む弾の処理
//
// 作成日:  2024/2/5
// 作成者:  北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class StraightLineMove : MonoBehaviour
{

    #region 変数  

    #region const定数

    // Directionの選んだ内容の判定用
    private const string UP_MOVE = "Up";
    private const string DOWN_MOVE = "Down";

    #endregion

    [SerializeField,Header("弾の速さ")]
    private float _moveSpeed = 7.5f;

    // 弾の進む方向の計算
    private int _moveDirection = default;

    // Transform格納用
    private Transform _transform = default;

    // 弾の進む方向
    private enum Direction
    {
        Up,
        Down
    }

    [SerializeField,Header("弾の進む方向")]
    private Direction _direction = default;

    // Directionのstring化
    private string _directionMemory = default;

    #endregion

    #region メソッド  
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        _directionMemory = _direction.ToString();
        if(_directionMemory == UP_MOVE)
        {
            _moveDirection = -1;
        }
        else if(_directionMemory == DOWN_MOVE)
        {
            _moveDirection = 1;
        }
        _transform = this.transform; 
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        _transform.Translate(0,  _moveDirection * _moveSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// 画面外処理
    /// </summary>
    private void OnBecameInvisible()
    {
        // 画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    #endregion

}

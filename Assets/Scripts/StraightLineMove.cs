// ---------------------------------------------------------  
// StraightLine.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class StraightLineMove : MonoBehaviour
{

    #region 変数  

    #region const定数

    private const string UP_MOVE = "Up";
    private const string DOWN_MOVE = "Down";

    #endregion

    [SerializeField]
    private float _moveSpeed = 7.5f;

    // 
    private Transform _transform = default;

    // 
    private int _moveDirection = default;

    // 
    private enum Direction
    {
        Up,
        Down
    }

    [SerializeField]
    private Direction _direction = default;

    // 
    private string _directionMemory = default;

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

    private void OnBecameInvisible()
    {
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    #endregion

}

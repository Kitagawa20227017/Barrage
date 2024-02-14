// ---------------------------------------------------------  
// EnemyMove.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    #region 変数

    [SerializeField,Header("曲がり始めるまでの時間")]
    private float _curveTimer = 2f;

    [SerializeField,Header("敵の動くスピード")]
    private float _moveSpeed = 5f;

    // カメラ内に入ったかの判定
    private bool _isInCamera = false;

    // 
    private float _timer = 0;

    //
    private float n = 0;

    //
    private float _angleEnemy = default;

    //
    private Transform _transform = default;

    //
    private int _curveDirections = default ;

    //
    private string _direction = default;

    private enum RotationDirection
    {
        Right,
        Left
    }

    [SerializeField,Header("曲がる方向")]
    private RotationDirection _rotationDirection;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _transform = this.transform;
        _angleEnemy = _transform.eulerAngles.z;
        _direction = _rotationDirection.ToString();
        if (_direction == "Right")
        {
            _curveDirections = 120;
        }
        else if(_direction == "Left")
        {
            _curveDirections = -120;
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        // カメラ内に入っているときのみ動く
        if (_isInCamera)
        {
            _timer += Time.deltaTime;
            if (_timer >= _curveTimer && Mathf.Round(_transform.eulerAngles.z) != 180)
            {
                n += _curveDirections * Time.deltaTime;
                _transform.rotation = Quaternion.Euler(0, 0, _angleEnemy + n);
            }
            _transform.Translate(0, -_moveSpeed * Time.deltaTime, 0);
        }
    }
    private void OnBecameVisible()
    {
        // カメラ内に入ったとき
        _isInCamera = true;
    }

    private void OnBecameInvisible()
    {
        // 画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    #endregion
}

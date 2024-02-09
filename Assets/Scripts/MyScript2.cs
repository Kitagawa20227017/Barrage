// ---------------------------------------------------------  
// MyScript2.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MyScript2 : MonoBehaviour
{
    #region 変数

    // 角度が変わるまでの時間
    [SerializeField]
    private float _directionsTimer = 2f;

    // どの角度まで変わるか
    [SerializeField]
    private int _angle = 90;

    // 角度が変わるスピード
    [SerializeField]
    private int _angleMoveSpeed = 100;

    // 移動速度
    [SerializeField]
    private float _moveSpeed = 5f;

    // 角度の調整用
    private int _storageAngle = default;

    // 角度が変わるスピードを変換して保存する用
    private int _storageAnglSpeed = default;

    // タイマー用
    private float time = 0;

    // 現在の角度
    private float _objAngleNow = default;

    // 初期の角度
    private float _objAngle = default;

    // transform格納用
    private Transform _transform = default;

    // 曲がる向き
    private enum RotationDirection
    {
        Right,
        Left
    }

    // inspectorで設定できるようにする
    [SerializeField]
    private RotationDirection _rotationDirection;

    // RotationDirectionをstring変換しておく
    private string _direction = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        // 初期設定
        _transform = this.transform;
        _objAngle = _transform.eulerAngles.z;
        _direction = _rotationDirection.ToString();
        if (_direction == "Right")
        {
            _storageAnglSpeed = _angleMoveSpeed;
        }
        else if (_direction == "Left")
        {
            _storageAnglSpeed = _angleMoveSpeed * -1;
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= _directionsTimer /*&& Mathf.Round(_transform.eulerAngles.z) != _angle*/)
        {
            _objAngleNow += _storageAnglSpeed * Time.deltaTime;
            //_transform.rotation = Quaternion.Euler(0, 0, _objAngle + _objAngleNow);
            //_transform.rotation = Quaternion.Euler(0, 0, _transform.eulerAngles.z + _objAngleNow);
        }
        _transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
    }

    private void OnBecameInvisible()
    {
        // 初期化
        time = 0;
        _objAngleNow = 0;

        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }
    #endregion

}

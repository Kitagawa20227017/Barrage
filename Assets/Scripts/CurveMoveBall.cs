// ---------------------------------------------------------  
// CurveMoveBall.cs  
//   
// 曲がる弾の処理
// 
// 作成日: 2024/2/6 
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CurveMoveBall : MonoBehaviour
{

    #region 変数

    #region const定数

    // 曲がる方向の判定用
    private const string LEFT = "Left";
    private const string RIGHT = "Right";

    #endregion

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

    // transform格納用
    private Transform _transform = default;

    // 角度が変わるスピードを変換して保存する用
    private int _storageAnglSpeed = default;

    // タイマー用
    private float _timer = 0;

    // 現在の角度
    private float _objAngleNow = default;

    // アクティブになった時の角度
    private float _objAngle = default;

    // アクティブ時の判定
    private bool _isActiveObj = false;

    // 曲がる向き
    private enum RotationDirection
    {
        Right,
        Left
    }

    // inspectorで設定できるようにする
    [SerializeField,Header("曲がる向き")]
    private RotationDirection _rotationDirection = default;

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

        // 右に曲がるか左に曲がるかの判定
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
        // アクティブになったときに角度の記録
        if(this.gameObject.activeSelf  && !_isActiveObj)
        {
            _objAngle = _transform.eulerAngles.z;

            // 重複して更新しないようにする
            _isActiveObj = true;
        }

        // 時間を測る
        _timer += Time.deltaTime;

        // 計測時間がタイマーを超えたら曲がり始める
        if (_timer >= _directionsTimer)
        {
            _objAngleNow += _storageAnglSpeed * Time.deltaTime;
            _transform.rotation = Quaternion.Euler(0, 0, _objAngle + _objAngleNow);
        }

        // 前に進む
        _transform.Translate(0, -_moveSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// 画面外処理
    /// </summary>
    private void OnBecameInvisible()
    {
        // 初期化
        _timer = 0;
        _objAngleNow = 0;
        _isActiveObj = false;

        // 画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 当たり判定処理
    /// </summary>
    /// <param name="collision">当たったオブジェクト</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 非アクティブ化
        gameObject.SetActive(false);
    }

    #endregion

}

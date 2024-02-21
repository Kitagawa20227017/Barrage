// ---------------------------------------------------------  
// EnemyMove.cs  
//   
// 敵の行動処理
//
// 作成日:  2024/2/6
// 作成者:  北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    #region 変数

    #region const定数

    // 反対側の角度
    private const int OPPOSITION = 180;

    // 曲がる方向とスピード
    private const float ANGLE_SPEED_RIGHT = 120;
    private const float ANGLE_SPEED_LEFT = -120;

    // _directionの判定用
    private const string RIGHT = "Right";
    private const string LEFT = "Left";

    #endregion

    [SerializeField,Header("曲がり始めるまでの時間")]
    private float _curveTimer = 2f;

    [SerializeField,Header("敵の動くスピード")]
    private float _moveSpeed = 5f;

    // Transform格納用
    private Transform _transform = default;

    // 曲がるスピード
    private float _curveDirections = default;

    // 時間計測のタイマー
    private float _timer = 0;

    // 角度の合計値
    private float _angleSum = 0;

    // アクティブ時の角度
    private float _angleEnemy = default;

    // カメラ内に入ったかの判定
    private bool _isInCamera = false;

    // 曲がる方向
    private enum RotationDirection
    {
        Right,
        Left
    }

    [SerializeField,Header("曲がる方向")]
    private RotationDirection _rotationDirection = default;

    // _rotationDirectionの文字列化
    private string _direction = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        // 初期処理
        _transform = this.transform;
        _angleEnemy = _transform.eulerAngles.z;
        _direction = _rotationDirection.ToString();

        // 曲がる方向の決定
        if (_direction == RIGHT)
        {
            _curveDirections = ANGLE_SPEED_RIGHT;
        }
        else if(_direction == LEFT)
        {
            _curveDirections = ANGLE_SPEED_LEFT;
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
            // 時間計測開始
            _timer += Time.deltaTime;

            // 特定時間過ぎていて180度回転してないとき
            if (_timer >= _curveTimer && Mathf.Round(_transform.eulerAngles.z) != OPPOSITION)
            {
                // 角度を増やしていく
                _angleSum += _curveDirections * Time.deltaTime;
                _transform.rotation = Quaternion.Euler(0, 0, _angleEnemy + _angleSum);
            }

            // 縦軸方向に進む
            _transform.Translate(0, -_moveSpeed * Time.deltaTime, 0);
        }
    }

    /// <summary>
    /// 画面内処理
    /// </summary>
    private void OnBecameVisible()
    {
        // カメラ内に入ったとき
        _isInCamera = true;
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

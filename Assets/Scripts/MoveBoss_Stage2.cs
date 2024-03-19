// ---------------------------------------------------------  
// MoveBoss_Stage2.cs  
// 
// ステージ2のボスの行動処理
//   
// 作成日: 2024/2/10
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class MoveBoss_Stage2 : MonoBehaviour
{

    #region 変数  

    #region const定数

    // 弾の種類
    private const int BALL_TYPE = 3;

    // 攻撃する回数
    private const int BALL_COUNT = 7;

    // 弾と弾の間隔
    private const float ATTACK_INTERVAL = 0.35f;

    // 動ける範囲
    private const float MOVE_SCOPE_MIN_X = -10.5f;
    private const float MOVE_SCOPE_MAX_X = 11f;
    private const float MOVE_SCOPE_MIN_Y = 102f;

    // 攻撃の時間
    private const float ATTACK_TIME = 3f;

    // 攻撃の弾数、角度
    private const int BALL_COUNT_PATRN = 10;
    private const float ANGLE_PATRN = 10;

    #endregion

    [SerializeField, Header("動く速さ")]
    private float _moveSpeed = 4f;

    // ランダムな位置の格納
    private Vector3 _randomPos = default;

    // オブジェクトプールの呼び出し
    private BossLaunchControl _control = default;

    // ランダムな弾の格納
    private int _randomBall = 0;

    // 攻撃した回数
    private int _ballConut = 0;

    // 時間計測用のタイマー
    private float _attackIntervalTimer = 0;
    private float _attackTimer = 0;

    // Yの最大値格納
    private float _yMaxPos = default;

    // カメラ内に入ったかの判定
    private bool _isInCamera = false;

    // 弾を選ぶかどうかの判定
    private bool _isRandom = false;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        // 初期設定
        _yMaxPos = transform.position.y;
        _randomPos = new Vector3(Random.Range(MOVE_SCOPE_MIN_X, MOVE_SCOPE_MAX_X), Random.Range(MOVE_SCOPE_MIN_Y, _yMaxPos), transform.position.z);

        // スクリプト取得
        _control = gameObject.GetComponent<BossLaunchControl>();
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        // 画面外のとき処理しない
        if (!_isInCamera)
        {
            return;
        }

        // 計測開始
        _attackIntervalTimer += Time.deltaTime;

        // 一定時間たったとき
        if (_attackIntervalTimer >= ATTACK_TIME)
        {
            // 弾を選ぶ
            if (!_isRandom)
            {
                _randomBall = Random.Range(0, BALL_TYPE);
                _isRandom = true;
            }

            // 計測開始
            _attackTimer += Time.deltaTime;

            // 一定時間たったとき攻撃
            if (_attackTimer >= ATTACK_INTERVAL)
            {
                _control.ObjectPool(transform.position, BALL_COUNT_PATRN, ANGLE_PATRN, _randomBall);
                _ballConut++;
                _attackTimer = 0;
            }

            // 攻撃回数に達したら攻撃終了
            if (_ballConut >= BALL_COUNT)
            {
                _attackIntervalTimer = 0;
                _attackTimer = 0;
                _isRandom = false;
                _ballConut = 0;
            }
        }

        // 選ばれた位置と同じ位置ならもう一度選ぶ。それ以外なら移動
        if (transform.position == _randomPos)
        {
            _randomPos = new Vector3(Random.Range(MOVE_SCOPE_MIN_X, MOVE_SCOPE_MAX_X), Random.Range(MOVE_SCOPE_MIN_Y, _yMaxPos), transform.position.z);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _randomPos, _moveSpeed * Time.deltaTime);
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

    #endregion

}

// ---------------------------------------------------------  
// MoveBoss_Stage2.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MoveBoss_Stage2 : MonoBehaviour
{

    #region 変数  

    #region const定数

    // 真っ直ぐの弾 
    private const int STRAIGH_BALL = 0;

    // 左曲がりの弾
    private const int LEFT_BALL = 1;

    // 右曲がりの弾
    private const int RIGHT_BALL = 2;

    // 往復回数
    private const int ROUND_TRIP = 2;

    // 弾と弾の間隔
    private const float ATTACK_INTERVAL = 0.35f;

    // 動ける範囲
    private const float MOVE_SCOPE_MIN_X = -10.5f;
    private const float MOVE_SCOPE_MAX_X = 11f;

    // 動く速さ
    private const float MOVE_SPEED = 7.5f;

    // 攻撃の時間
    private const float ATTACK_TIME = 4f;

    // 攻撃の弾数、角度
    private const int BALL_COUNT_PATRN = 4;
    private const float ANGLE_PATRN = 15;

    #endregion

    // オブジェクトプールの呼び出し
    private BossLaunchControl _control = default;

    // 行動パターン
    private int _patrn = 0;

    // 往復回数のカウント
    private int _roundTripCount = 0;

    // 時間計測用のタイマー
    private float _attackIntervalTimer = 0;
    private float _attackTimer = 0;

    // 行動が終わった判定
    private bool _isActionFin = false;

    // 往復判定
    private bool _isMove = false;

    // 止まる判定
    private bool _isStop = false;

    // カメラ内に入ったかの判定
    private bool _isInCamera = false;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
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

        // 行動パターンごとに呼び出し
        switch (_patrn)
        {
            case 0:
                ActionPatrn0();
                break;

            case 1:
                ActionPatrn1();
                break;

            case 2:
                ActionPatrn2();
                break;

            case 3:
                ActionPatrn3();
                break;

            case 4:
                ActionPatrn4();
                break;
        }

        // 行動が終わったら次の行動を選ぶ
        if (_isActionFin)
        {
            _patrn = Random.Range(0, 5);

            // 初期化
            _isActionFin = false;
            _isStop = false;
            _attackTimer = 0;
            _attackIntervalTimer = 0;
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
    /// 行動パターン1
    /// </summary>
    private void ActionPatrn0()
    {
        // 特定の位置まで移動したとき
        if (transform.position.x <= MOVE_SCOPE_MIN_X && !_isMove)
        {
            _isMove = true;
        }
        else if (transform.position.x >= MOVE_SCOPE_MAX_X && _isMove)
        {
            // 動きを止めて攻撃
            _isStop = true;

            // 時間計測開始 
            _attackIntervalTimer += Time.deltaTime;
            _attackTimer += Time.deltaTime;

            // 攻撃処理
            if (_attackTimer > ATTACK_INTERVAL && !_isActionFin)
            {
                _control.ObjectPool(transform.position, BALL_COUNT_PATRN, ANGLE_PATRN, STRAIGH_BALL);
                _attackTimer = 0;
            }

            // 攻撃終了
            if (_attackIntervalTimer > ATTACK_TIME)
            {
                _isActionFin = true;
            }
        }

        // 移動処理
        if (_isMove && !_isStop)
        {
            gameObject.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        else if (!_isMove && !_isStop)
        {
            gameObject.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
    /// 行動パターン2
    /// </summary>
    private void ActionPatrn1()
    {
        // 特定の位置まで移動したとき
        if (transform.position.x >= MOVE_SCOPE_MAX_X && !_isMove)
        {
            _isMove = true;
        }
        else if (transform.position.x <= MOVE_SCOPE_MIN_X && _isMove)
        {
            // 動きを止めて攻撃
            _isStop = true;

            // 時間計測開始
            _attackIntervalTimer += Time.deltaTime;
            _attackTimer += Time.deltaTime;

            // 攻撃処理
            if (_attackTimer > ATTACK_INTERVAL && !_isActionFin)
            {
                _control.ObjectPool(transform.position, BALL_COUNT_PATRN, ANGLE_PATRN, STRAIGH_BALL);
                _attackTimer = 0;
            }

            // 攻撃終了
            if (_attackIntervalTimer > ATTACK_TIME)
            {
                _isActionFin = true;
            }
        }

        // 移動処理
        if (_isMove && !_isStop)
        {
            gameObject.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        else if (!_isMove && !_isStop)
        {
            gameObject.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
    /// 行動パターン3
    /// </summary>
    private void ActionPatrn2()
    {
        // 特定の位置まで移動したとき
        if (Mathf.Round(transform.position.x) == 0 && _roundTripCount >= ROUND_TRIP)
        {
            // 動きを止めて攻撃
            _isStop = true;

            // 時間計測開始
            _attackIntervalTimer += Time.deltaTime;
            _attackTimer += Time.deltaTime;

            // 攻撃処理
            if (_attackTimer > ATTACK_INTERVAL && !_isActionFin)
            {
                _control.ObjectPool(transform.position, BALL_COUNT_PATRN, ANGLE_PATRN, STRAIGH_BALL);
                _attackTimer = 0;
            }

            // 攻撃終了
            if (_attackIntervalTimer > ATTACK_TIME)
            {
                _roundTripCount = 0;
                _isActionFin = true;
            }
        }
        else if (transform.position.x >= MOVE_SCOPE_MAX_X && !_isMove)
        {
            _isMove = true;
            _roundTripCount++;
        }
        else if (transform.position.x <= MOVE_SCOPE_MIN_X && _isMove)
        {
            _isMove = false;
            _roundTripCount++;
        }

        // 移動処理
        if (_isMove && !_isStop)
        {
            gameObject.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        else if (!_isMove && !_isStop)
        {
            gameObject.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
    /// 行動パターン4
    /// </summary>
    private void ActionPatrn3()
    {
        // 特定の位置まで移動したとき
        if (transform.position.x <= MOVE_SCOPE_MIN_X && !_isMove)
        {
            _isMove = true;
        }
        else if (transform.position.x >= MOVE_SCOPE_MAX_X && _isMove)
        {
            // 動きを止めて攻撃
            _isStop = true;

            // 時間計測開始
            _attackIntervalTimer += Time.deltaTime;
            _attackTimer += Time.deltaTime;

            // 攻撃処理
            if (_attackTimer > ATTACK_INTERVAL && !_isActionFin)
            {
                _control.ObjectPool(transform.position, BALL_COUNT_PATRN, ANGLE_PATRN, LEFT_BALL);
                _attackTimer = 0;
            }

            // 攻撃終了
            if (_attackIntervalTimer > ATTACK_TIME)
            {
                _isActionFin = true;
            }
        }

        // 移動処理
        if (_isMove && !_isStop)
        {
            gameObject.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        else if (!_isMove && !_isStop)
        {
            gameObject.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
    /// 行動パターン5
    /// </summary>
    private void ActionPatrn4()
    {
        // 特定の位置まで移動したとき
        if (transform.position.x >= MOVE_SCOPE_MAX_X && !_isMove)
        {
            _isMove = true;
        }
        else if (transform.position.x <= MOVE_SCOPE_MIN_X && _isMove)
        {
            // 動きを止めて攻撃
            _isStop = true;

            // 時間計測開始
            _attackIntervalTimer += Time.deltaTime;
            _attackTimer += Time.deltaTime;

            // 攻撃処理
            if (_attackTimer > ATTACK_INTERVAL && !_isActionFin)
            {
                _control.ObjectPool(transform.position, BALL_COUNT_PATRN, ANGLE_PATRN, RIGHT_BALL);
                _attackTimer = 0;
            }

            // 攻撃終了
            if (_attackIntervalTimer > ATTACK_TIME)
            {
                _isActionFin = true;
            }
        }

        // 移動処理
        if (_isMove && !_isStop)
        {
            gameObject.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        else if (!_isMove && !_isStop)
        {
            gameObject.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
    }

    #endregion
}

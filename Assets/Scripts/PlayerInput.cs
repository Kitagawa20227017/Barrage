// ---------------------------------------------------------  
// PlayerInput.cs  
//   
// プレイヤーの入力処理
//
// 作成日: 2024/2/1
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    #region 変数  

    #region const変数

    // 最初に生成する弾の数
    private const int INITIAL_INSTANTIATE = 15;

    // 横軸の移動量   
    private const float PLUS_HORIZONMOVE = 5f;
    private const float MINUS_HORIZONMOVE = -5f;

    // 縦軸の移動量
    private const float PLUS_VERTICAL = 5f;
    private const float MINUS_VERTICAL = -5f;

    // キーの入力値の補正
    private const float PLUS = 1f;
    private const float MINUS = -1f;

    // 横軸の移動制限
    private const float PLAYER_MIN_POX_X = -11.5f;
    private const float PLAYER_MAX_POX_X = 12f;

    // 縦軸の移動制限
    private const float PLAYER_MIN_POX_Y = -9f;
    private const float PLAYER_MAX_POX_Y = 9f;

    #endregion

    [SerializeField]
    private GameObject _playerBall = default;

    // 弾の親オブジェクトのTransform
    private Transform _playerBullets = default;

    // Animator格納用
    private Animator _playerAnimator = default;

    // 時間計測用のタイマー
    private float _timer = 5f;

    // 弾の撃てない時間
    private float _coolTime = 0.25f;

    // プレイヤーの入力方向の格納場所
    private float _horizontal = default;
    private float _vertical = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary> 
    private void Start()
    {
        // 初期設定
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.SetBool("isLeft", false);
        _playerAnimator.SetBool("isRight", false);
        _playerBullets = new GameObject("PlayerBullets").transform;

        // 弾の生成
        for (int i = 0; i <= INITIAL_INSTANTIATE; i++)
        {
            Instantiate(_playerBall, gameObject.transform.position, Quaternion.Euler(0, 0, 0), _playerBullets);
        }

        // 弾の非アクティブ化
        foreach (Transform bullets in _playerBullets)
        {
            bullets.gameObject.SetActive(false);
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {
        // 入力値の代入
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        #region 入力値の補正

        // 横軸の移動
        if (_horizontal > 0)
        {
            _horizontal = PLUS;
        }
        else if (_horizontal < 0)
        {
            _horizontal = MINUS;
        }
        else
        {
            _horizontal = 0;
        }


        // 縦軸の移動
        if (_vertical > 0)
        {
            _vertical = PLUS;
        }
        else if (_vertical < 0)
        {
            _vertical = MINUS;
        }
        else
        {
            _vertical = 0;
        }

        #endregion

        #region 移動処理

        // 横軸の移動処理
        if (_horizontal == PLUS)
        {
            // アニメーション再生
            _playerAnimator.SetBool("isLeft", false);
            _playerAnimator.SetBool("isRight", true);

            // 移動可能範囲なら移動
            if (gameObject.transform.localPosition.x < PLAYER_MAX_POX_X)
            {
                gameObject.transform.localPosition =
                    new Vector3(gameObject.transform.localPosition.x + (PLUS_HORIZONMOVE * Time.deltaTime), gameObject.transform.localPosition.y, transform.localPosition.z);
            }
        }
        else if (_horizontal == MINUS)
        {
            // アニメーション再生
            _playerAnimator.SetBool("isLeft", true);
            _playerAnimator.SetBool("isRight", false);

            // 移動可能範囲なら移動
            if (gameObject.transform.localPosition.x > PLAYER_MIN_POX_X)
            {
                gameObject.transform.localPosition =
                    new Vector3(gameObject.transform.localPosition.x + (MINUS_HORIZONMOVE * Time.deltaTime), gameObject.transform.localPosition.y, transform.localPosition.z);
            }
        }
        else if (_horizontal == 0)
        {
            // アニメーション再生
            _playerAnimator.SetBool("isLeft", false);
            _playerAnimator.SetBool("isRight", false);

            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, transform.localPosition.z);
        }


        // 縦軸の移動処理 
        if (_vertical == PLUS)
        {
            // 移動可能範囲なら移動
            if (gameObject.transform.localPosition.y < PLAYER_MAX_POX_Y)
            {
                gameObject.transform.localPosition =
                    new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + (PLUS_VERTICAL * Time.deltaTime), transform.localPosition.z);
            }
        }
        else if (_vertical == MINUS)
        {
            // 移動可能範囲なら移動
            if (gameObject.transform.localPosition.y > PLAYER_MIN_POX_Y)
            {
                gameObject.transform.localPosition =
                    new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + (MINUS_VERTICAL * Time.deltaTime), transform.localPosition.z);
            }
        }
        else if (_vertical == 0)
        {
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, transform.localPosition.z);
        }

        #endregion

        #region 弾の発射処理

        // スペースが押されたとき
        if (Input.GetButton("Attack"))
        {
            _timer += Time.deltaTime;
            if (_timer > _coolTime)
            {
                ObjPool(this.transform.position);
                _timer = 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 弾の生成処理
    /// </summary>
    /// <param name="pos">プレイヤーの現在位置</param>
    private void ObjPool(Vector3 pos)
    {
        // 弾の親オブジェクトを探索
        foreach (Transform bullets in _playerBullets)
        {
            if (!bullets.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                bullets.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, this.transform.eulerAngles.z));
                //アクティブにする
                bullets.gameObject.SetActive(true);
                return;
            }
        }

        //非アクティブなオブジェクトがない場合新規生成
        Instantiate(_playerBall, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), _playerBullets);
    }

    #endregion

}
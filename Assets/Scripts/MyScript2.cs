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

    #region const定数 

    // 最初に生成する弾の数
    private const int INITIAL_INSTANTIATE = 30;

    private const string BULLET_NAME = "BossBullets";

    #endregion

    [SerializeField, Header("発射する弾")]
    private GameObject[] _gameObject = default;

    [SerializeField, Header("弾と弾の発射間隔")]
    private float _timer = 0.25f;

    [SerializeField, Header("撃ち始めるまでの時間")]
    private float _time = 0.25f;

    [SerializeField, Range(0, 90), Header("斜めの弾の数(左右対称)")]
    private int _ballQuantity = 1;

    [SerializeField, Range(0, 90f), Header("弾と弾の間隔")]
    private float _ballInterval = 1;

    [SerializeField, Range(0, 100), Header("弾の数")]
    private int _conutBall = 5;

    // 弾の親オブジェクトのTransform
    private Transform[] _bullets = default;

    // 弾を撃った回数のカウント
    private int _conut = 0;

    // 撃ち始めのタイマー
    private float _mainTimer = 0;

    // 弾の発射間隔のタイマー
    private float _subtime = 0;

    // 初期角度の保存先
    private float _initialAngle = default;

    // カメラ内に入ったかの判定
    private bool _isInCamera = false;

    private bool random = false;
    private int patrn = 0;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _bullets = new Transform[_gameObject.Length];
        for (int i = 0; i < _gameObject.Length; i++)
        {
            _bullets[i] = new GameObject(BULLET_NAME + i).transform;
            for (int j = 0; j < 100; j++)
            {
                Instantiate(_gameObject[i], gameObject.transform.position, Quaternion.Euler(0, 0, 0), _bullets[i]);
            }

            foreach (Transform ball in _bullets[i])
            {
                ball.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {
        // 画面外なら処理しない
        if (!_isInCamera)
        {
            return;
        }

        // 角度が変わったとき弾を撃たない
        if (Mathf.Round(transform.eulerAngles.z) != _initialAngle)
        {
            return;
        }

        // 時間計測
        _mainTimer += Time.deltaTime;

        // 弾を撃ち始める
        if (_mainTimer >= _time)
        {
            if(!random)
            {
                patrn = Random.Range(0, _gameObject.Length);
                random = true;
            }

            // 時間計測
            _subtime += Time.deltaTime;

            // 弾を連続で撃つ
            if (_subtime >= _timer && _conut < _conutBall)
            {
                ObjectPool(gameObject.transform.position);
                _conut++;
                _subtime = 0;
            }

            // 一定回数撃ったら初期化
            if (_conut >= _conutBall)
            {
                _mainTimer = 0;
                _conut = 0;
                random = false;
            }
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
    /// 弾の生成(オブジェクトプール)
    /// </summary>
    /// <param name="pos">敵の現在位置</param>
    private void ObjectPool(Vector3 pos)
    {
        float settingAngle = default;
        bool isActive = false;

        #region 正面の弾

        // 子オブジェクト探索
        foreach (Transform bulletsTrans in _bullets)
        {
            if (!bulletsTrans.gameObject.activeSelf)
            {
                isActive = false;

                // 非アクティブなオブジェクトの位置と回転を設定
                bulletsTrans.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, this.transform.eulerAngles.z));

                // アクティブにする
                bulletsTrans.gameObject.SetActive(true);

                // 非アクティブの弾があった
                isActive = true;
                break;
            }
        }

        // 非アクティブなオブジェクトがない場合新規生成
        if (!isActive)
        {
            Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), _bullets[patrn]);
        }

        #endregion

        #region 左側の弾

        // 弾数分ループ
        for (int i = 1; i <= _ballQuantity; i++)
        {
            // 非アクティブの弾があるかどうか
            isActive = false;

            // 角度計算
            settingAngle = _ballInterval * i;

            // 子オブジェクト探索
            foreach (Transform bulletsTrans in _bullets)
            {
                if (!bulletsTrans.gameObject.activeSelf)
                {
                    // 非アクティブなオブジェクトの位置と回転を設定
                    bulletsTrans.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, this.transform.eulerAngles.z + settingAngle));

                    // アクティブにする
                    bulletsTrans.gameObject.SetActive(true);

                    // 非アクティブの弾があった
                    isActive = true;
                    break;
                }
            }

            // 非アクティブなオブジェクトがない場合新規生成
            if (!isActive)
            {
                Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + settingAngle), _bullets[patrn]);
            }
        }

        #endregion

        #region 右側の弾

        // 弾数分ループ
        for (int i = 1; i <= _ballQuantity; i++)
        {
            // 非アクティブの弾があるかどうか
            isActive = false;

            // 角度計算
            settingAngle = _ballInterval * i;

            // 子オブジェクト探索
            foreach (Transform bulletsTrans in _bullets)
            {
                if (!bulletsTrans.gameObject.activeSelf)
                {
                    // 非アクティブなオブジェクトの位置と回転を設定
                    bulletsTrans.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - settingAngle));

                    // アクティブにする
                    bulletsTrans.gameObject.SetActive(true);

                    // 非アクティブの弾があった
                    isActive = true;

                    break;
                }

            }

            // 非アクティブなオブジェクトがない場合新規生成
            if (!isActive)
            {
                Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - settingAngle), _bullets[patrn]);
            }
        }

        #endregion

    }

    #endregion

}

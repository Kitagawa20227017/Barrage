// ---------------------------------------------------------  
// SmallFryOnlyControl.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class SmallFryOnlyControl : MonoBehaviour
{

    #region 変数  

    #region const定数 

    private const int AA = 1;


    #endregion

    [SerializeField, Header("発射する弾")]
    private GameObject _gameObject = default;

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

    // 弾の親オブジェクトのトランスフォーム
    private Transform bullets = default;

    // 弾を撃った回数のカウント
    private int _conut = 0;

    // 撃ち始めのタイマー
    private float _mainTimer = 0;

    // 弾の発射間隔のタイマー
    private float _subtime = 0;

    // 初期角度の保存先
    private float _initialAngle = default;

    // 
    float kau = default;

    // 曲がる向き
    private enum RotationDirection
    {
        Right,
        Left
    }

    // inspectorで設定できるようにする
    [SerializeField, Header("曲がる向き")]
    private RotationDirection _rotationDirection;

    // RotationDirectionをstring変換しておく
    private string _direction = default;


    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _direction = _rotationDirection.ToString();
        _initialAngle = transform.eulerAngles.z;
        bullets = new GameObject("SmallFryBullets").transform;
        for (int i = 0; i < _conutBall * (_ballQuantity + 1) * 2; i++)
        {
            Instantiate(_gameObject, gameObject.transform.position, Quaternion.Euler(0, 0, 270), bullets);
        }

        foreach (Transform t in bullets)
        {
            t.gameObject.SetActive(false);
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {

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
            // 時間計測
            _subtime += Time.deltaTime;

            // 弾を連続で撃つ
            if (_subtime >= _timer && _conut < _conutBall)
            {
                if(_direction == "Right")
                {
                    RightMove(gameObject.transform.position);
                }
                else if(_direction == "Left")
                {
                    LeftMove(gameObject.transform.position);
                }
                _conut++;
                _subtime = 0;
            }

            // 一定回数撃ったら初期化
            if (_conut >= _conutBall)
            {
                _mainTimer = 0;
                _conut = 0;
            }
        }
    }

    /// <summary>
    /// 弾の生成(オブジェクトプール)
    /// </summary>
    /// <param name="pos">敵の現在位置</param>
    private void LeftMove(Vector3 pos)
    {
        #region 正面の弾

        bool isflag = false;
        foreach (Transform t in bullets)
        {
            if (!t.gameObject.activeSelf)
            {
                isflag = false;
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, this.transform.eulerAngles.z));
                //アクティブにする
                t.gameObject.SetActive(true);
                isflag = true;
                break;
            }
        }
        //非アクティブなオブジェクトがない場合新規生成

        if (!isflag)
        {
            //生成時にbulletsの子オブジェクトにする
            Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), bullets);
        }

        #endregion

        #region 左側の弾

        for (int i = 1; i <= _ballQuantity; i++)
        {
            isflag = false;
            kau = _ballInterval * i;
            foreach (Transform t in bullets)
            {
                if (!t.gameObject.activeSelf)
                {
                    // 非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, this.transform.eulerAngles.z + kau));
                    // アクティブにする
                    t.gameObject.SetActive(true);
                    isflag = true;
                    break;
                }
            }

            // 非アクティブなオブジェクトがない場合新規生成
            if (!isflag)
            {
                //生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau), bullets);
            }
        }

        #endregion
    }

    /// <summary>
    /// 弾の生成(オブジェクトプール)
    /// </summary>
    /// <param name="pos">敵の現在位置</param>
    private void RightMove(Vector3 pos)
    {
        #region 正面の弾

        bool isflag = false;
        foreach (Transform t in bullets)
        {
            if (!t.gameObject.activeSelf)
            {
                isflag = false;
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, this.transform.eulerAngles.z));
                //アクティブにする
                t.gameObject.SetActive(true);
                isflag = true;
                break;
            }
        }
        //非アクティブなオブジェクトがない場合新規生成

        if (!isflag)
        {
            //生成時にbulletsの子オブジェクトにする
            Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), bullets);
        }

        #endregion

        #region 右側の弾

        for (int i = 1; i <= _ballQuantity; i++)
        {
            isflag = false;
            kau = _ballInterval * i;
            foreach (Transform t in bullets)
            {
                if (!t.gameObject.activeSelf)
                {
                    //非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau));
                    //アクティブにする
                    t.gameObject.SetActive(true);
                    isflag = true;
                    break;
                }

            }
            //非アクティブなオブジェクトがない場合新規生成

            if (!isflag)
            {
                //生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau), bullets);
            }
        }

        #endregion
    }
    #endregion

}

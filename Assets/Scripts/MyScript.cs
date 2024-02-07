// ---------------------------------------------------------  
// MyScript.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MyScript : MonoBehaviour
{

    #region 変数  

    [SerializeField] 
    private GameObject _gameObject;

    [SerializeField, Range(0, 90),Header("斜めの弾の数(左右対称)")]
    private int s = 0;

    [SerializeField, Range(0, 90f), Header("弾と弾の間隔")]
    private float sa = 0;

    [SerializeField, Range(0, 100), Header("弾の数")]
    private int _conutBall = 5;

    [SerializeField, Range(0, 10f), Header("弾の発射間隔")]
    private float _timer = 0.25f;


    private Transform bullets;
    private int _conut = 5;
    private float _nowTime = 0;
    private float _time = 1f;
    private float _nowtime = 0;
    private float n = -45f;

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
        bullets = new GameObject("PlayerBullets").transform;
        for(int i = 0; i < 100; i++)
        {
            Instantiate(_gameObject, gameObject.transform.position, Quaternion.Euler(0, 0, -90), bullets);
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {
        _nowTime += Time.deltaTime;
        if (_nowTime >= _time)
        {
            _nowtime += Time.deltaTime;
            if (_nowtime >= _timer && _conut <= _conutBall)
            {
                test_2(gameObject.transform.position);
                _conut++;
                _nowtime = 0;
            }

            if(_conut >= _conutBall)
            {
                _nowTime = 0;
                _conut = 0;
            }
        }
    }

    private void test_2(Vector3 pos)
    {
        bool isflag = false;
        foreach (Transform t in bullets)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z));
                //アクティブにする
                t.gameObject.SetActive(true);
                break;
            }
            else
            {
                isflag = true;
            }
        }
        //非アクティブなオブジェクトがない場合新規生成

        if (isflag)
        {
            //生成時にbulletsの子オブジェクトにする
            Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), bullets);
        }

        for(int i = 1; i <= s; i++)
        {
            isflag = false;
            float kau = sa * i;
            foreach (Transform t in bullets)
            {
                if (!t.gameObject.activeSelf)
                {
                    //非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau));
                    //アクティブにする
                    t.gameObject.SetActive(true);
                    break;
                }
                else
                {
                    isflag = true;
                }
            }
            //非アクティブなオブジェクトがない場合新規生成

            if (isflag)
            {
                //生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau), bullets);
            }
        }

        for (int i = 1; i <= s; i++)
        {
            isflag = false;
            float kau = sa * i;
            foreach (Transform t in bullets)
            {
                if (!t.gameObject.activeSelf)
                {
                    //非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau));
                    //アクティブにする
                    t.gameObject.SetActive(true);
                    break;
                }
                else
                {
                    isflag = true;
                }
            }
            //非アクティブなオブジェクトがない場合新規生成

            if (isflag)
            {
                //生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau), bullets);
            }
        }
    }

    #endregion

}

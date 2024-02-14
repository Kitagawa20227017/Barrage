// ---------------------------------------------------------  
// MyScript.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;

public class MyScript : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private GameObject[] _gameObject;

    [SerializeField, Range(0, 90), Header("斜めの弾の数(左右対称)")]
    private int s = 0;

    [SerializeField, Range(0, 90f), Header("弾と弾の間隔")]
    private float sa = 0;

    [SerializeField, Range(0, 100), Header("弾の数")]
    private int _conutBall = 5;

    [SerializeField, Range(0, 10f), Header("弾の発射間隔")]
    private float _timer = 0.25f;

    [SerializeField]
    private float _time = 0.25f;

    private Transform[] bullets;
    private int _conut = 0;
    private float _nowTime = 0;
    private float _nowtime = 0;
    private float _angle = default;

    private int _patrn = 0;
    private string ppppp = "BossBullets";

    private bool isflag = false;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _angle = transform.eulerAngles.z;
        bullets = new Transform[_gameObject.Length];
        for (int i = 0; i < _gameObject.Length; i++)
        {
            bullets[i] = new GameObject(ppppp + i).transform;
            for (int j = 0; j < 100; j++)
            {
                Instantiate(_gameObject[i], gameObject.transform.position, Quaternion.Euler(0, 0, -90), bullets[i]);
            }

            foreach(Transform ball in bullets[i])
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
        if (Mathf.Round(transform.eulerAngles.z) != _angle)
        {
            return;
        }
        _nowTime += Time.deltaTime;
        if (_nowTime >= _time)
        {
            if(!isflag)
            {
                _patrn = Random.Range(0, _gameObject.Length);
                isflag = true;
            }
            _nowtime += Time.deltaTime;
            if (_nowtime >= _timer && _conut < _conutBall)
            {
                ObjectPool(gameObject.transform.position);
                _conut++;
                _nowtime = 0;
            }

            if (_conut >= _conutBall)
            {
                _nowTime = 0;
                _conut = 0;
                isflag = false;
            }
        }
    }

    private void ObjectPool(Vector3 pos)
    {
        bool isflag = false;
        foreach (Transform t in bullets[_patrn])
        {
            if (!t.gameObject.activeSelf)
            {
                // 非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z));
                // アクティブにする
                t.gameObject.SetActive(true);

                isflag = true;
                break;
            }
            else
            {
                isflag = false;
            }
        }

        // 非アクティブなオブジェクトがない場合新規生成
        if (!isflag)
        {
            // 生成時にbulletsの子オブジェクトにする
            Instantiate(_gameObject[_patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), bullets[_patrn]);
        }

        for (int i = 1; i <= s; i++)
        {
            isflag = false;
            float kau = sa * i;
            foreach (Transform t in bullets[_patrn])
            {
                if (!t.gameObject.activeSelf)
                {
                    // 非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau));
                    // アクティブにする
                    t.gameObject.SetActive(true);
                    isflag = true;
                    break;
                }
                else
                {
                    isflag = false;
                }
            }

            // 非アクティブなオブジェクトがない場合新規生成
            if (!isflag)
            {
                // 生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject[_patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau), bullets[_patrn]);
            }
        }

        for (int i = 1; i <= s; i++)
        {
            isflag = false;
            float kau = sa * i;
            foreach (Transform t in bullets[_patrn])
            {
                if (!t.gameObject.activeSelf)
                {
                    // 非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau));
                    // アクティブにする
                    t.gameObject.SetActive(true);
                    isflag = true;
                    break;
                }
                else
                {
                    isflag = false;
                }
            }

            // 非アクティブなオブジェクトがない場合新規生成
            if (!isflag)
            {
                //生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject[_patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau), bullets[_patrn]);
            }
        }
    }

    #endregion

}

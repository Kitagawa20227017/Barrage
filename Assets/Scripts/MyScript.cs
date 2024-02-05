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

    [SerializeField] private GameObject _gameObject;
    Transform bullets;

    private float _nowTime = 0;
    private float _time = 3f;
    private float _nowtime = 0;
    private float _timer = 0.5f;
    private int _conut = 0;

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
        for(int i = 0; i < 10; i++)
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
            if (_nowtime >= _timer && _conut <= 5)
            {
                test(gameObject.transform.position);
                test_2(gameObject.transform.position);
                test_3(gameObject.transform.position);
                _conut++;
                _nowtime = 0;
            }

            if(_conut >= 5)
            {
                Debug.Log("A");
                _nowTime = 0;
                _conut = 0;
            }
        }
    }
    private void test(Vector3 pos)
    {
        foreach (Transform t in bullets)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos,Quaternion.Euler(0,0,-90));
                //アクティブにする
                t.gameObject.SetActive(true);
                return;
            }
        }
        //非アクティブなオブジェクトがない場合新規生成

        //生成時にbulletsの子オブジェクトにする
        Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, -90), bullets);
    }

    private void test_2(Vector3 pos)
    {
        foreach (Transform t in bullets)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, -135));
                //アクティブにする
                t.gameObject.SetActive(true);
                return;
            }
        }
        //非アクティブなオブジェクトがない場合新規生成

        //生成時にbulletsの子オブジェクトにする
        Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, -135), bullets);
    }

    private void test_3(Vector3 pos)
    {
        foreach (Transform t in bullets)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, -45));
                //アクティブにする
                t.gameObject.SetActive(true);
                return;
            }
        }
        //非アクティブなオブジェクトがない場合新規生成

        //生成時にbulletsの子オブジェクトにする
        Instantiate(_gameObject, pos, Quaternion.Euler(0, 0, -45), bullets);
    }
    #endregion

}

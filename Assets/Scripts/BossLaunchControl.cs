// ---------------------------------------------------------  
// BossLaunchControl.cs  
//   
// ボスの攻撃処理
//
// 作成日:  2024/2/9
// 作成者:  北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class BossLaunchControl : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private GameObject[] _gameObject;

    private Transform[] bullets;
    private string ppppp = "BossBullets";

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        bullets = new Transform[_gameObject.Length];
        for (int i = 0; i < _gameObject.Length; i++)
        {
            bullets[i] = new GameObject(ppppp + i).transform;
            for (int j = 0; j < 100; j++)
            {
                Instantiate(_gameObject[i], gameObject.transform.position, Quaternion.Euler(0, 0, 0), bullets[i]);
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
        
    }

    public void ObjectPool(Vector3 pos,int qty,float angle,int patrn)
    {
        bool isflag = false;
        foreach (Transform t in bullets[patrn])
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
            Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), bullets[patrn]);
        }

        for (int i = 1; i <= qty; i++)
        {
            isflag = false;
            float kau = angle * i;
            foreach (Transform t in bullets[patrn])
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
                Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau), bullets[patrn]);
            }
        }

        for (int i = 1; i <= qty; i++)
        {
            isflag = false;
            float kau = angle * i;
            foreach (Transform t in bullets[patrn])
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
                Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau), bullets[patrn]);
            }
        }
    }

    #endregion

}

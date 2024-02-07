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

    private float a = 0;
    private bool flag = false;
    private float time = 0;

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
        a = transform.position.x;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {
        if (!flag)
        {
            a += 1.5f * Time.deltaTime;
            if (a >= 10f)
            {
                flag = true;
            }
        }
        else if (flag)
        {
            a -= 1.5f * Time.deltaTime;
            if (a <= -10f)
            {
                flag = false;
            }
        }
        gameObject.transform.position = new Vector3(a, transform.position.y, transform.position.z);
    }

    #endregion

}

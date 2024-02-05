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
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {
        if(!flag)
        {
            a += 0.01f;
            if (a >= 10f)
            {
                flag = true;
            }
        }
        else if(flag)
        {
            a -= 0.01f;
            if (a <= -10f)
            {
                flag = false;
            }
        }
        gameObject.transform.position = new Vector3(transform.position.x - a * Time.deltaTime, transform.position.y, transform.position.z);
    }

    #endregion

}

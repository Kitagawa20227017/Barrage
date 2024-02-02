// ---------------------------------------------------------  
// CollisionDetection.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour
{

    #region 変数  

    private bool flag = false;
    private float _time = 0;
    private float _timer = 2f;

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
        if(flag)
        {
            _time += Time.deltaTime;
            if(_time >= _timer)
            {
                flag = false;
                _time = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball" && !flag)
        {
            flag = true;
        }
    }

    #endregion

}

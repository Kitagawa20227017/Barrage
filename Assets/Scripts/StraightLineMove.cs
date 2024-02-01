// ---------------------------------------------------------  
// StraightLine.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class StraightLineMove : MonoBehaviour
{

    #region 変数  

    private const float MOVESPEED = 2f;
    private Transform _transform = default;

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
        _transform = this.transform;
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        _transform.Translate(MOVESPEED * Time.deltaTime,0, 0);
    }

    private void OnBecameInvisible()
    {
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    #endregion

}

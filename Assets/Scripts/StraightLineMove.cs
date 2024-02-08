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

    [SerializeField]
    private float _a = 0f;

    [SerializeField]
    private float _moveSpeed = 7.5f;

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
        _transform.rotation = Quaternion.Euler(0, 0, _transform.eulerAngles.z + _a);  
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        _transform.Translate(_moveSpeed * Time.deltaTime,0, 0);
    }

    private void OnBecameInvisible()
    {
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    #endregion

}

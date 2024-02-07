// ---------------------------------------------------------  
// MyScript4.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MyScript4 : MonoBehaviour
{
    #region 変数

    private bool flag = false;
    private const float MOVESPEED = 5f;
    private float time = 0;
    private float n = 0;
    private float a = 0;
    private Transform _transform = default;
    private int aaa = default ;
    private string _direction = default;

    private enum RotationDirection
    {
        Right,
        Left
    }

    [SerializeField]
    private RotationDirection _rotationDirection;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {
        _transform = this.transform;
        a = _transform.eulerAngles.z;
        _direction = _rotationDirection.ToString();
        if (_direction == "Right")
        {
            aaa = 120;
        }
        else if(_direction == "Left")
        {
            aaa = -120;
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update()
    {
        if (flag)
        {
            time += Time.deltaTime;
            if (time >= 2f && Mathf.Round(_transform.eulerAngles.z) != 90)
            {
                n += aaa * Time.deltaTime;
                _transform.rotation = Quaternion.Euler(0, 0, a + n);
            }
            _transform.Translate(MOVESPEED * Time.deltaTime, 0, 0);
        }
    }
    private void OnBecameVisible()
    {
        flag = true;
    }

    private void OnBecameInvisible()
    {
        //画面外に行ったら非アクティブにする
        gameObject.SetActive(false);
    }

    #endregion
}

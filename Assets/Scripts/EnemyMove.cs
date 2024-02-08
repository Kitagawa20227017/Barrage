// ---------------------------------------------------------  
// MyScript4.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    #region 変数

    [SerializeField]
    private float timer = 2f;

    [SerializeField]
    private int aa = 90;

    [SerializeField]
    private float _moveSpeed = 5f;

    private bool flag = false;
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
            //if (time >= timer && Mathf.Round(_transform.eulerAngles.z) != 90)
            //{
            //    n += aaa * Time.deltaTime;
            //    _transform.rotation = Quaternion.Euler(0, 0, a + n);
            //}
            _transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
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

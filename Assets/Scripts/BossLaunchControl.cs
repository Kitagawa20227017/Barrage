// ---------------------------------------------------------  
// BossLaunchControl.cs  
//   
// ボスの攻撃処理
//
// 作成日: 2024/2/9
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class BossLaunchControl : MonoBehaviour
{

    #region 変数  

    #region const定数

    // 親オブジェクトの名前
    private const string BULLET_NAME = "BossBullets";

    // 最初に生成する弾の数
    private const int INITIAL_INSTANTIATE = 30;

    #endregion

    [SerializeField, Header("射撃音")]
    private AudioClip _shotAudio = default;

    [SerializeField, Header("弾")]
    private GameObject[] _gameObject;

    // 弾生成時の親オブジェクトのトランスフォーム
    private Transform[] _bullets = default;

    // AudioSource格納用
    private AudioSource _audioSource = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        // 初期設定
        _bullets = new Transform[_gameObject.Length];
        _audioSource = gameObject.GetComponent<AudioSource>();

        for (int i = 0; i < _gameObject.Length; i++)
        {
            _bullets[i] = new GameObject(BULLET_NAME + i).transform;
            for (int j = 0; j < INITIAL_INSTANTIATE; j++)
            {
                Instantiate(_gameObject[i], gameObject.transform.position, Quaternion.Euler(0, 0, 0), _bullets[i]);
            }

            foreach (Transform ball in _bullets[i])
            {
                ball.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// オブジェクトプール
    /// </summary>
    /// <param name="pos">現在位置</param>
    /// <param name="qty">弾数</param>
    /// <param name="angle">角度</param>
    /// <param name="patrn">弾の種類</param>
    public void ObjectPool(Vector3 pos, int qty, float angle, int patrn)
    {
        bool isActive = false;
        _audioSource.PlayOneShot(_shotAudio);
        foreach (Transform bulletsTrans in _bullets[patrn])
        {
            if (!bulletsTrans.gameObject.activeSelf)
            {
                // 非アクティブなオブジェクトの位置と回転を設定
                bulletsTrans.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z));
                // アクティブにする
                bulletsTrans.gameObject.SetActive(true);

                isActive = true;
                break;
            }
            else
            {
                // 非アクティブのオブジェクトがない
                isActive = false;
            }
        }

        // 非アクティブなオブジェクトがない場合新規生成
        if (!isActive)
        {
            // 生成時にbulletsの子オブジェクトにする
            Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z), _bullets[patrn]);
        }

        for (int i = 1; i <= qty; i++)
        {
            isActive = false;
            float kau = angle * i;
            foreach (Transform bulletsTrans in _bullets[patrn])
            {
                if (!bulletsTrans.gameObject.activeSelf)
                {
                    // 非アクティブなオブジェクトの位置と回転を設定
                    bulletsTrans.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau));
                    // アクティブにする
                    bulletsTrans.gameObject.SetActive(true);
                    isActive = true;
                    break;
                }
                else
                {
                    isActive = false;
                }
            }

            // 非アクティブなオブジェクトがない場合新規生成
            if (!isActive)
            {
                // 生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z + kau), _bullets[patrn]);
            }
        }

        for (int i = 1; i <= qty; i++)
        {
            isActive = false;
            float kau = angle * i;
            foreach (Transform bulletsTrans in _bullets[patrn])
            {
                if (!bulletsTrans.gameObject.activeSelf)
                {
                    // 非アクティブなオブジェクトの位置と回転を設定
                    bulletsTrans.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau));
                    // アクティブにする
                    bulletsTrans.gameObject.SetActive(true);
                    isActive = true;
                    break;
                }
                else
                {
                    isActive = false;
                }
            }

            // 非アクティブなオブジェクトがない場合新規生成
            if (!isActive)
            {
                //生成時にbulletsの子オブジェクトにする
                Instantiate(_gameObject[patrn], pos, Quaternion.Euler(0, 0, transform.eulerAngles.z - kau), _bullets[patrn]);
            }
        }
    }

    #endregion

}
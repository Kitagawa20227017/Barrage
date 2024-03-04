// ---------------------------------------------------------  
// TitleMenu.cs  
//   
// 作成日: 2024/2/22
// 作成者: 北川 稔明 
// ---------------------------------------------------------  
using UnityEngine;
using TMPro;

public class TitleMenu : MonoBehaviour
{

    #region 変数  

    #region const定数

    // キーの入力値の補正
    private const float PLUS = 1f;
    private const float MINUS = -1f;

    // 選択中のUIの位置
    private const float SELECT_UI_POS_X = 50f;

    #endregion

    [SerializeField,Header("StageUIオブジェクト")]
    private GameObject _stageSelectUI = default;

    [SerializeField, Header("ExitUIオブジェクト")]
    private GameObject _exitUI = default;

    [SerializeField,Header("ステージ選択UI")]
    private GameObject _stageSummary = default;

    [SerializeField, Header("選択音")]
    private AudioClip _selectAudio = default;

    // TextMesh格納用
    private TextMeshProUGUI _stageSelectText = default;
    private TextMeshProUGUI _exitText = default;

    // プレイヤーの入力方向の格納場所
    private float _vertical = default;

    // AudioSource格納用
    private AudioSource _audioSource = default;

    // Exitにカーソルが合ってるかどうか
    private bool _isExit = false;

    // 複数回処理をしないようにするためのフラグ
    private bool _isNotLoop = false;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        // 初期設定
        _stageSelectText = _stageSelectUI.GetComponent<TextMeshProUGUI>();
        _exitText = _exitUI.GetComponent<TextMeshProUGUI>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        gameObject.SetActive(true);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // 入力値の代入
        _vertical = Input.GetAxis("Vertical");

        #region 入力値の補正

        // 縦軸の移動
        if (_vertical > 0)
        {
            _vertical = PLUS;
        }
        else if (_vertical < 0)
        {
            _vertical = MINUS;
        }
        else
        {
            _vertical = 0;
        }

        #endregion

        if(_vertical == PLUS)
        {
            _isExit = false;
        }
        else if(_vertical == MINUS)
        {
            _isExit = true;
        }

        if(Input.GetButtonDown("Enter") && _isExit)
        {
            // ゲーム終了
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
            #else
            Application.Quit();//ゲームプレイ終了
            #endif
        }
        else if (Input.GetButtonDown("Enter") && !_isExit)
        {
            // ステージ選択UIをアクティブ
            _stageSummary.SetActive(true);
            gameObject.SetActive(false);
        }

        // 選択中のUIならこれ以上処理しない
        if(_isExit == _isNotLoop)
        {
            return;
        }

        // 選択中のUIを保持
        _isNotLoop = _isExit;

        if (_isExit)
        {
            // 音再生
            _audioSource.PlayOneShot(_selectAudio);

            // ステージを選択していない状態にする
            _stageSelectUI.transform.localPosition =
                new Vector3(SELECT_UI_POS_X, _stageSelectUI.transform.localPosition.y, _stageSelectUI.transform.localPosition.x);
            _stageSelectText.color = Color.white;

            // Exitを選択している状態にする
            _exitUI.transform.localPosition = new Vector3(0, _exitUI.transform.localPosition.y, _exitUI.transform.localPosition.x);
            _exitText.color = Color.red;
        }
        else
        {
            // 音再生
            _audioSource.PlayOneShot(_selectAudio);

            // ステージを選択している状態にする
            _stageSelectUI.transform.localPosition = new Vector3(0, _stageSelectUI.transform.localPosition.y, _stageSelectUI.transform.localPosition.x);
            _stageSelectText.color = Color.red;

            // Exitを選択していない状態にする
            _exitUI.transform.localPosition = new Vector3(SELECT_UI_POS_X, _exitUI.transform.localPosition.y, _exitUI.transform.localPosition.x);
            _exitText.color = Color.white;
        }
    }

    #endregion

}

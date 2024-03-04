// ---------------------------------------------------------  
// StageSelectMenu.cs  
//   
// ステージ選択メニュー
//
// 作成日: 2024/2/24
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelectMenu : MonoBehaviour
{

    #region 変数  

    // 配列の前後を見る
    private const int ONE = 1;

    [SerializeField,Header("ステージ名UI")]
    private GameObject[] _stageUI = default;

    [SerializeField,Header("TitleUI")]
    private GameObject _titleUI = default;

    [SerializeField, Header("選択音")]
    private AudioClip _selectAudio = default;

    // AudioSource格納用
    private AudioSource _audioSource = default;

    // TextMesh格納用
    private TextMeshProUGUI[] _stageTextMeshs = default;

    // 選択中のUIの配列指標
    private int _stageUIIndex = 0;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        // 初期設定
        _stageTextMeshs = new TextMeshProUGUI[_stageUI.Length];
        foreach(GameObject gameObject in _stageUI)
        {
            _stageTextMeshs[_stageUIIndex] = gameObject.GetComponent<TextMeshProUGUI>();
            _stageUIIndex++;
        }
        _stageUIIndex = 0;
        _stageTextMeshs[_stageUIIndex].color = Color.red;
        _audioSource = gameObject.GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // Sキーを押したとき
        if(Input.GetButtonDown("MenuCursorUp") && _stageUIIndex != 0)
        {
            // 音再生
            _audioSource.PlayOneShot(_selectAudio);

            // 選択中のUIを変える
            _stageUIIndex--;
            _stageTextMeshs[_stageUIIndex].color = Color.red;
            _stageTextMeshs[_stageUIIndex + 1].color = Color.white;
        }
        // Wキーを押したとき
        else if(Input.GetButtonDown("MenuCursorDown") && _stageUIIndex < _stageUI.Length - ONE)
        {
            // 音再生
            _audioSource.PlayOneShot(_selectAudio);

            // 選択中のUIを変える
            _stageUIIndex++;
            _stageTextMeshs[_stageUIIndex].color = Color.red;
            _stageTextMeshs[_stageUIIndex - 1].color = Color.white;
        }

        // Enterを押したとき
        if(Input.GetButtonDown("Enter"))
        {
            Scene();
        }
    }

    /// <summary>
    /// 画面遷移処理
    /// </summary>
    private void Scene()
    {
        if (_stageUIIndex != _stageUI.Length - ONE)
        {
            // UIと同じシールに遷移
            SceneManager.LoadScene(_stageUI[_stageUIIndex].name); 
        }
        // 「Back」が選択されているとき
        else
        {
            // 選択中のUIを一番上にする
            for (int i = 0; i < _stageUI.Length; i++)
            {
                if (i == 0)
                {
                    _stageTextMeshs[i].color = Color.red;
                }
                else
                {
                    _stageTextMeshs[i].color = Color.white;
                }
            }

            // 初期化
            _stageUIIndex = 0;

            // UIのアクティブ制御
            _titleUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    #endregion

}

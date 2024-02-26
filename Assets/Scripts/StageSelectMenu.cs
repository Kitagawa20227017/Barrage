// ---------------------------------------------------------  
// StageSelectMenu.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageSelectMenu : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private GameObject[] _stageUI = default;

    [SerializeField]
    private GameObject _gameObject1 = default;

    private TextMeshProUGUI[] _stageTextMeshs = default;

    private int _stageUIIndex = 0;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        _stageTextMeshs = new TextMeshProUGUI[_stageUI.Length];
        foreach(GameObject gameObject in _stageUI)
        {
            _stageTextMeshs[_stageUIIndex] = gameObject.GetComponent<TextMeshProUGUI>();
            _stageUIIndex++;
        }
        _stageUIIndex = 0;
        _stageTextMeshs[_stageUIIndex].color = Color.red;
        gameObject.SetActive(false);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        if(Input.GetButtonDown("MenuCursorUp") && _stageUIIndex >= 1)
        {
            _stageUIIndex--;
            _stageTextMeshs[_stageUIIndex].color = Color.red;
            _stageTextMeshs[_stageUIIndex + 1].color = Color.white;
        }
        else if(Input.GetButtonDown("MenuCursorDown") && _stageUIIndex < _stageUI.Length - 1)
        {
            _stageUIIndex++;
            _stageTextMeshs[_stageUIIndex].color = Color.red;
            _stageTextMeshs[_stageUIIndex - 1].color = Color.white;
        }

        if(Input.GetButtonDown("Enter"))
        {
            Scene();
        }
    }

    private void Scene()
    {
        if (_stageUIIndex != _stageUI.Length - 1)
        {
            SceneManager.LoadScene(_stageUI[_stageUIIndex].name);
        }
        else
        {
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
            _stageUIIndex = 0;
            this.gameObject.SetActive(false);
            _gameObject1.SetActive(true);
        }
    }

    #endregion

}

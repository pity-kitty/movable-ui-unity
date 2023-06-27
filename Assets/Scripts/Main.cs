using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate
{
    /// <summary>
    /// The main.
    /// </summary>
    public class Main : MonoBehaviour
    {
        [SerializeField] private string customizationSceneName;
        [SerializeField] private Button customizationSceneButton;

        private void Start()
        {
            InitializeSubscriptions();
        }

        private void InitializeSubscriptions()
        {
            customizationSceneButton.onClick.AddListener(OnLoadScene);
        }
        
        private void OnLoadScene()
        {
            SceneManager.LoadScene(customizationSceneName, LoadSceneMode.Single);
        }
    }
}

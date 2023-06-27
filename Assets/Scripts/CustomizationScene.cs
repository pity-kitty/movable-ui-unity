using EdCon.MiniGameTemplate.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EdCon.MiniGameTemplate
{
    public class CustomizationScene : MonoBehaviour
    {
        private const string SaveMessage = "Layout Scheme Saved";
        
        [SerializeField] private string mainSceneName;
        [SerializeField] private ToastService toastService;
        [SerializeField] private UISettings uiSettings;
        
        [Header("Panel buttons")]
        [SerializeField] private Button saveButton;
        [SerializeField] private Button defaultButton;
        [SerializeField] private Button backButton;

        private void Start()
        {
            InitializeSubscriptions();
        }

        private void InitializeSubscriptions()
        {
            backButton.onClick.AddListener(OnLoadScene);
            saveButton.onClick.AddListener(SaveButtonClick);
        }
        
        private void OnLoadScene()
        {
            SceneManager.LoadScene(mainSceneName, LoadSceneMode.Single);
        }

        private void SaveButtonClick()
        {
            uiSettings.SaveLayout();
            toastService.ShowToast(SaveMessage);
        }
    }
}

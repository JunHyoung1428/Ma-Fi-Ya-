using UnityEngine;
using LoginSystem;

namespace LoginSystem
{
    public class LoginManager : MonoBehaviour
    {

        public enum Panel { Login, SignUp, Verify, Reset, Main, Edit }

        [SerializeField] InfoPanel infoPanel;
        [SerializeField] LoginSystem.LoginPanel loginPanel;
        [SerializeField] SignUpPanel signUpPanel;
        [SerializeField] ResetPanel resetPanel;
        [SerializeField] VerifyPanel verifyPanel;
        [SerializeField] LobbyManager mainPanel;

        private void OnEnable()
        {
            SetActivePanel(Panel.Login);
        }

        public void SetActivePanel( Panel panel )
        {
            loginPanel.gameObject.SetActive(panel == Panel.Login);
            signUpPanel.gameObject.SetActive(panel == Panel.SignUp);
            resetPanel.gameObject.SetActive(panel == Panel.Reset);
            mainPanel.gameObject.SetActive(panel == Panel.Main);
            verifyPanel.gameObject.SetActive(panel == Panel.Verify);
        }

        public void ShowInfo( string message )
        {
            infoPanel.ShowInfo(message);
        }

        public void SignUpToLogin( string id, string pass )
        {
            loginPanel.FromSignUpPanel(id, pass);
        }
    }
}


using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : MonoBehaviour
{
    [SerializeField] LoginSystem.LoginManager loginCanvas;
    [SerializeField] TMP_Text text;
    [SerializeField] Button buttonConnectAgain;

    private bool isDisconnect;

    void Awake() 
    {
        CheckConnectedToInternet();
        buttonConnectAgain.onClick.AddListener(CheckConnectedToInternet);
    }

    private void Update()
    {
        if ( isDisconnect )
            return;

        if ( Input.anyKey )
        {
            loginCanvas.gameObject.SetActive(true);
            VCamController.Instance.SetVCam(VCamController.VCam.Login);
            this.gameObject.SetActive(false);
        }
    }

    //인터넷 연결 확인 메서드
    //연결 확인시에만 로그인 가능
    private void CheckConnectedToInternet()
    {
        if ( Application.internetReachability == NetworkReachability.NotReachable )
        {
            isDisconnect = true;
            buttonConnectAgain.gameObject.SetActive(true);
            text.text = "Connect Internet Again";

        }
        else
        {
            buttonConnectAgain.gameObject.SetActive(false);
            isDisconnect = false;
            text.text = "Press Any Key to Login";
        }
    }
}

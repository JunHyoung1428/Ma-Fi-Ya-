using Photon.Pun;
using TMPro;
using UnityEngine;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField idInputField;

    private void Start()
    {
        idInputField.text = $"Player {Random.Range(1000, 10000)}";
    }

    public void Login()
    {
        if (idInputField.text == "")
        {
            Debug.LogError("Empty nickname : Please input name");
            return;
        }

        PhotonNetwork.LocalPlayer.NickName = idInputField.text; // �� ������ �г���
        PhotonNetwork.ConnectUsingSettings();   // ���� ������Ʈ���� ������ ���� ������ ���� �õ�
        
        // ������ ����, ��Ʈ��ũ�� ������ �����ϴ� �������� ������ �� (Callback)
        // Ŭ���̾�Ʈ�� �������� ��û : PhotonNetwork.
        // �׿� ���� ����� Callback���� ������ Ŭ���̾�Ʈ����
    }
}

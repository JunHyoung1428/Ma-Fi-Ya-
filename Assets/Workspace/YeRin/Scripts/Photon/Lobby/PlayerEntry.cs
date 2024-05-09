using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;  // ���� ���� �ؽ����̺� ����ؾ� �� (�����ϰ� �̸����� ���� �����ϸ� ����)

public class PlayerEntry : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playerReady;
    [SerializeField] Button playerReadyButton;

    private Player player;

    public Player Player { get { return player; } }

    public void SetPlayer(Player player)
    {
        this.player = player;
        playerName.text = player.NickName;
        playerReady.text = player.GetReady() ? "Ready" : "";
        playerReadyButton.gameObject.SetActive(player.IsLocal);
    }

    public void Ready()
    {
        /*PhotonHashTable customProperty = player.CustomProperties;
        bool ready;
        if (customProperty.TryGetValue("Ready", out object value))
        {
            ready = (bool)value;
        }
        else
        {
            ready = false;
        }

        PhotonHashTable ht = new PhotonHashTable();
        ht.Add("Ready", !ready);  // ������, �� ���� �����ϰ� ���ָ� ��
        player.SetCustomProperties(ht);*/

        // but. ���ٺ��� �Ǽ��� �򰥸� �� ����. -> CustomProperties�� Ȯ�� �޼ҵ�� ����� �� ����

        bool ready = player.GetReady();
        player.SetReady(!ready);
    }

    public void ChangeCustomProperty(PhotonHashTable property)
    {
        bool ready = player.GetReady();
        playerReady.text = ready ? "Ready" : "";
    }
}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Programmer : Yerin
/// 
/// About player control in Knife Game Mode
/// </summary>
public class PlayerController : MonoBehaviourPun
{
    [Header("Components")]
    [SerializeField] TMP_Text nickNameText;
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource walkAudio;

    public TMP_Text Name => nickNameText;

    //[SerializeField] GameObject speechBubble;
    //[SerializeField] TMP_Text bubbleText;

    [SerializeField] float movePower;
    [SerializeField] float rotateSpeed;

    [Header("States")]
    [SerializeField] private bool isWalking;

    [Header("Knife")]
    [SerializeField] GameObject shortKnife;
    [SerializeField] GameObject middleKnife;
    [SerializeField] GameObject longKnife;

    [SerializeField] LayerMask layerMask;
    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;

    private float preAngle;
    private float cosAngle;
    private float CosAngle;

    Collider[] colliders = new Collider[20];

    private Vector3 moveDir;

    private void Start()
    {
        walkAudio.Stop();
        SetWeaponLength();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Accelate();
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Rotate();
        }
    }

    #region Move
    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;

        if (photonView.IsMine)
        {
            photonView.RPC("WalkStart", RpcTarget.All);
        }
    }

    private void Accelate()
    {

        if (moveDir.x == 0 && moveDir.z == 0 && isWalking)
        {
            photonView.RPC("WalkStop", RpcTarget.All);
        }

        controller.Move(transform.forward * moveDir.z * movePower * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * 100f * Time.deltaTime);
    }

    [PunRPC]
    private void WalkStart()
    {
        animator.Play("Walk");
        isWalking = true;
        walkAudio.Play();
    }

    [PunRPC]
    private void WalkStop()
    {
        animator.Play("Idle");
        isWalking = false;
        walkAudio.Stop();
    }
    #endregion

    #region Dance
    private void OnHipHopDance(InputValue value)
    {
        if (photonView.IsMine)
            photonView.RPC("HipHop", RpcTarget.All);
    }

    private void OnRumbaDance(InputValue value)
    {
        if (photonView.IsMine)
            photonView.RPC("Rumba", RpcTarget.All);
    }

    private void OnSillyDance(InputValue value)
    {
        if (photonView.IsMine)
            photonView.RPC("Silly", RpcTarget.All);
    }

    [PunRPC]
    private void HipHop()
    {
        animator.SetTrigger("hipHop");
    }

    [PunRPC]
    private void Rumba()
    {
        animator.SetTrigger("rumba");
    }

    [PunRPC]
    private void Silly()
    {
        animator.SetTrigger("silly");
    }
    #endregion

    #region Attack
    private void SetWeaponLength()
    {
        if (shortKnife.activeSelf)
        {
        }
        else if (middleKnife.activeSelf)
        {
            range = range * 2;
        }
        else if (longKnife.activeSelf)
        {
            range = range * 3;
        }
        else
        {
            Debug.Log("No Weapon");
        }
    }
    private void OnAttack()
    {
        if (photonView.IsMine)
            photonView.RPC("Attack", RpcTarget.All);
    }

    [PunRPC]
    private void Attack()
    {
        if (shortKnife.activeSelf)
        {
            animator.SetTrigger($"shortAttack{Random.Range(1, 3)}");
        }
        else if (middleKnife.activeSelf)
        {
            animator.SetTrigger($"middleAttack{Random.Range(1, 3)}");
        }
        else if (longKnife.activeSelf)
        {
            animator.SetTrigger($"longAttack{Random.Range(1, 3)}");
        }
        else
        {
            Debug.Log("No Weapon");
        }

        AttackRange();
    }

    private void AttackRange()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, layerMask);
        for (int i = 0; i < size; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirToTarget) < CosAngle)
                continue;

            PlayerController player = colliders[i].GetComponent<PlayerController>();
            if (player.gameObject == gameObject)
            {
                continue;
            }
            // 바로 죽음
            Debug.Log($"{player.Name.text} die");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion


    [PunRPC]
    private void NickName(string nickName)
    {
        nickNameText.text = nickName;
    }

    public void SetNickName(string nickName)   // PhotonNetwork.PlayerList[playerNumber].NickName
    {
        photonView.RPC("NickName", RpcTarget.All, nickName);
    }
}

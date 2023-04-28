using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DetailWindow : MonoBehaviour
{
    public float alphaChangeSpeed = 5.0f;
    float alphaTarget = 0.0f;

    bool isPause = false;
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            if (isPause)
            {
                Close();
            }
        }
    }

    Image itemIcon;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemPrice;
    TextMeshProUGUI itemDescription;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        Transform child = transform.GetChild(0);
        itemIcon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        itemName = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        itemPrice = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(3);
        itemDescription = child.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (alphaTarget > 0.0f)
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
        }
        else
        {
            canvasGroup.alpha -= Time.deltaTime * alphaChangeSpeed;
        }
        canvasGroup.alpha = Mathf.Clamp(canvasGroup.alpha, 0.0f, 1.0f);
    }

    /// <summary>
    /// ��â�� ���� �Լ�
    /// </summary>
    /// <param name="data">���鼭 ǥ���� ������</param>
    public void Open(ItemData data)
    {
        if (!isPause && data != null)        // �Ͻ� ���� ���°� �ƴϰ� �����Ͱ� �������� ����
        {
            itemIcon.sprite = data.itemIcon;                    // ������ �̹��� ����
            itemName.text = data.itemName;                      // �̸� ����
            itemPrice.text = data.price.ToString();             // ���� ����
            itemDescription.text = data.itemDescription;        // �� ���� ����

            alphaTarget = 1;                // ���̰Բ� alpha ��ǥġ ����

            MovePosition(Mouse.current.position.ReadValue());   // ���� �� ���콺 ��ġ �������� ����
        }
    }

    /// <summary>
    /// ��â�� �ݴ� �Լ�
    /// </summary>
    public void Close()
    {
        alphaTarget = 0;    // �Ⱥ��̰Բ� alpha ��ǥġ ����
    }

    /// <summary>
    /// �� ����â�� ��ġ�� �ű�� �Լ�
    /// </summary>
    /// <param name="screenPos">�� ��ġ(��ũ�� ��ǥ)</param>
    public void MovePosition(Vector2 screenPos)
    {
        if (alphaTarget > 0)                // ���϶��� �����̰� �ϱ�
        {
            RectTransform rect = (RectTransform)transform;

            int diffX = (int)(screenPos.x + rect.sizeDelta.x) - Screen.width;
            diffX = Mathf.Max(0, diffX);    // ��ģ�κи�ŭ�� �������� ������
            screenPos.x -= diffX;           // ȭ���� �ȹ���� �����

            //if (screenPos.x + rect.sizeDelta.x > Screen.width)   // ���η� ��� ���
            //{
            //    screenPos.x -= rect.sizeDelta.x;                // ������â ���� ũ�⸸ŭ �������� ������
            //}
            //if (screenPos.y - rect.sizeDelta.y < 0)             // ���η� ��� ���
            //{
            //    screenPos.y += rect.sizeDelta.y;                // ������â ���� ũ�⸸ŭ ���� ������
            //}

            transform.position = screenPos;     // ��ġ ����
        }
    }
}

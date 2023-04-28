using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpliterUI : MonoBehaviour
{
    /// <summary>
    /// ������ ���� �ּ� ����(�����⸦ �õ��ϸ� �ּ�1���� ������)
    /// </summary>
    const int itemCountMin = 1;

    /// <summary>
    /// �������� ������ �и��� ����
    /// </summary>
    uint itemSplitCount = itemCountMin;
    uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            if(itemSplitCount != value)
            {
                itemSplitCount = (uint)Mathf.Clamp((int)value,itemCountMin,(int)(targetSlot.ItemCount-1));

                inputField.text = itemSplitCount.ToString();
                slider.value = itemSplitCount;
            }
        }
    }
    /// <summary>
    /// �������� ���� ����
    /// </summary>
    ItemSlot targetSlot;

    /// <summary>
    /// �������� �и��� ������ �����Է��� �� �ִ� ��ǲ �ʵ�
    /// </summary>
    TMP_InputField inputField;

    /// <summary>
    /// ������ �и� ������ ������ �� �ִ� �����̴�
    /// </summary>
    Slider slider;

    /// <summary>
    /// �и��� �������� ������
    /// </summary>
    Image itemImage;

    /// <summary>
    /// ok��ư�� ������ �� ����Ǵ� �Լ�
    /// �Ķ��Ÿ(������ �ε���, ��������)
    /// </summary>
    public Action<uint, uint> onOkClick;


    // 1. awake���� �ʿ��� ������Ʈ ã��
    // 2. ��ǲ�ʵ�� �����̴��� ������Ű��(�ϳ��� �ٲ�� �ٸ� �ϳ��� ���� ����Ǿ�� �Ѵ�.)
    // 3. OK ��ư�� ������ ItemSplitCount�� ����� â�� ����ϱ�
    // 4. Cancel ��ư�� ������ "���"��� ����� â�� ����ϱ�
    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        slider = GetComponentInChildren<Slider>();
        //inputField.onValueChanged.AddListener((text)) =>
            //if(uint.TryParse())


        Transform child = transform.GetChild(0);
        
        itemImage= child.GetComponent<Image>();
        
    }

    
    private void Start()
    {
        //slider.onValueChanged.AddListener(delegate { valueChanged(slider, inputField)});
    }

    private void Update()
    {
        
        slider.value = int.Parse(inputField.text);
    }

    public void Open(ItemSlot target)
    {
        targetSlot = target;
        ItemSplitCount = 1;
        itemImage.sprite = targetSlot.ItemData.itemIcon;
        slider.minValue = itemCountMin;
        slider.maxValue = target.ItemCount -1;
        gameObject.SetActive(true);
    }
        
    public void valueChanged(Slider slider , TMP_InputField inputField)
    {
        //int value = (int)(diff * slider.value);
    }






}

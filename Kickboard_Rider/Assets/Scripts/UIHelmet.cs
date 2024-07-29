using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelmet : MonoBehaviour
{
    public PlayerMovement player; // �÷��̾� ��ũ��Ʈ ����
    public Image uiImage; // ������ UI �̹���
    public Sprite sprite1; // ù ��° ��������Ʈ
    public Sprite sprite2; // �� ��° ��������Ʈ

    void Update()
    {
        if (player != null && uiImage != null)
        {
            // �÷��̾��� bool ���� ���� �̹��� ����
            if (player.isHelmet)
            {
                uiImage.sprite = sprite1;
            }
            else
            {
                uiImage.sprite = sprite2;
            }
        }
    }
}

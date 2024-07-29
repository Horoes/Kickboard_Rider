using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelmet : MonoBehaviour
{
    public PlayerMovement player; // 플레이어 스크립트 참조
    public Image uiImage; // 변경할 UI 이미지
    public Sprite sprite1; // 첫 번째 스프라이트
    public Sprite sprite2; // 두 번째 스프라이트

    void Update()
    {
        if (player != null && uiImage != null)
        {
            // 플레이어의 bool 값에 따라 이미지 변경
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

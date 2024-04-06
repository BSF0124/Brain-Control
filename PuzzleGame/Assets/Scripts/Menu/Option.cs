using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    // 해상도 선택 드롭다운 메뉴
    public TMP_Dropdown resolutionDropdown;
    
    // 선택 가능한 해상도 목록 리스트
    List<Resolution> resolutions = new List<Resolution>();
    
    // 선택한 해상도 인덱스
    int resolutionNum;
    
    void Start()
    {
        InitUI();
    }

    // UI 초기화 메서드드
    void InitUI()
    {
        // 60hz인 해상도만 목록에 추가
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            float ratio = Screen.resolutions[i].width / Screen.resolutions[i].height;
            if(Screen.resolutions[i].refreshRateRatio.value == 60 && Math.Abs(ratio - 16/9) < 0.01f)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        // 드롭다운 메뉴 초기화
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        // 각 해상도를 드롭다운 메뉴에 추가
        foreach(Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + " x " + item.height + " " + item.refreshRateRatio + "hz";
            resolutionDropdown.options.Add(option);

            // 현재 화면의 해상도를 기본값으로 설정
            if(item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }

        // 드롭다운 메뉴 갱신
        resolutionDropdown.RefreshShownValue();
    }

    // 해상도 변경 메서드
    public void ResolutionChange(int num)
    {
        // 선택한 해상도의 인덱스 저장
        resolutionNum = num;
    }

    // 변경된 해상도를 적용하는 메서드
    public void Apply()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
        resolutions[resolutionNum].height, Screen.fullScreen);
    }

    // 전체 화면 설정 메서드
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // 배경음 설정
    public void SetBgmVolume()
    {
        float volume = bgmSlider.value;
        AudioManager.instance.bgmVolume = volume;
        AudioManager.instance.SetVolume();

    }

    // 효과음 설정
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        AudioManager.instance.sfxVolume = volume;
        AudioManager.instance.SetVolume();

    }
}

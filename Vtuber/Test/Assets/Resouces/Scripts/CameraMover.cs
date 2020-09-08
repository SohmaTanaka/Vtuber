using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using XInputAssistManager;

public class CameraMover : MonoBehaviour
{
    //カメラの速度の定数
    [SerializeField]
    private const float cameraSpeed = 0.2f;

    //カメラ感度
    [SerializeField, Range(0, 1)]
    private float cameraSensitive;

    //　メインカメラ
    [SerializeField]
    private Camera all_Camera;
    //　切り替える他のカメラ
    [SerializeField]
    private Camera sempai_Camera;
    [SerializeField]
    private Camera kouhai_Camera;

    //カメラのtransform  
    private Transform allCamTransform;
    private Transform sempaiCamTransform;
    private Transform kouhaiCamTransform;
    //初期状態 Rotation
    private Vector3 allCamInitialRotation;
    private Vector3 sempaiCamInitialRotation;
    private Vector3 kouhaiCamInitialRotation;
    //初期状態 Position
    private Vector3 allCamInitialPosition;
    private Vector3 sempaiCamInitialPosition;
    private Vector3 kouhaiCamInitialPosition;

    private SelectCam selectCam;

    int time = 0;

#if UNITY_EDITOR
    public string[] microphoneDeviceNames;

    void Reseter()
    {
        LoadMicrophoneDeviceNames();
    }

    void OnValidate()
    {
        LoadMicrophoneDeviceNames();
    }

    [ContextMenu("Load Microphone Device Names")]
    void LoadMicrophoneDeviceNames()
    {
        microphoneDeviceNames = Microphone.devices;
    }
#endif

    //選択されてるカメラがどれかを表す列挙型
    enum SelectCam
    {
        ALL,
        SEMPAI,
        KOUHAI,
    }

    void Start()
    {
        //カメラの初期設定の取得
        //トランスフォーム
        allCamTransform = all_Camera.transform;
        sempaiCamTransform = sempai_Camera.transform;
        kouhaiCamTransform = kouhai_Camera.transform;
        //回転
        allCamInitialRotation = allCamTransform.localEulerAngles;
        sempaiCamInitialRotation = sempaiCamTransform.localEulerAngles;
        kouhaiCamInitialRotation = kouhaiCamTransform.localEulerAngles;
        //位置
        allCamInitialPosition = allCamTransform.localPosition;
        sempaiCamInitialPosition = sempaiCamTransform.localPosition;
        kouhaiCamInitialPosition = kouhaiCamTransform.localPosition;

        selectCam = SelectCam.ALL;

        InputManager input = InputManager.GetInstance;
    }

    void Update()
    {
        CamControlIsActive();

        CameraPositionControl();

        CameraRotate();

        Reset();
    }

    //カメラ操作の有効無効（深度で映すカメラを変えるが録画は可能になる）
    private void CamControlIsActive()
    {
        //全体カメラの切り替えをする
        if (InputManager.GetInstance.GetKeyDown(InputCode.Main) || Input.GetKeyDown(KeyCode.Z))
        {
            selectCam = SelectCam.ALL;
            all_Camera.depth = 1;
            sempai_Camera.depth = 0;
            kouhai_Camera.depth = 0;
        }
        //先輩カメラの切り替えをする
        if (InputManager.GetInstance.GetKeyDown(InputCode.Sempai) || Input.GetKeyDown(KeyCode.X))
        {
            selectCam = SelectCam.SEMPAI;
            all_Camera.depth = 0;
            sempai_Camera.depth = 1;
            kouhai_Camera.depth = 0;
        }
        //後輩カメラの切り替えをする
        if (InputManager.GetInstance.GetKeyDown(InputCode.Kouhai) || Input.GetKeyDown(KeyCode.C))
        {
            selectCam = SelectCam.KOUHAI;
            all_Camera.depth = 0;
            sempai_Camera.depth = 0;
            kouhai_Camera.depth = 1;
        }
    }

    //初期状態にする
    private void Reset()
    {
        if (InputManager.GetInstance.GetKeyDown(InputCode.RESET))
        {
            switch (selectCam)
            {
                case SelectCam.ALL:
                    allCamTransform.localEulerAngles = allCamInitialRotation;
                    allCamTransform.localPosition = allCamInitialPosition;
                    break;
                case SelectCam.SEMPAI:
                    sempaiCamTransform.localEulerAngles = sempaiCamInitialRotation;
                    sempaiCamTransform.localPosition = sempaiCamInitialPosition;
                    break;
                case SelectCam.KOUHAI:
                    kouhaiCamTransform.localEulerAngles = kouhaiCamInitialRotation;
                    kouhaiCamTransform.localPosition = kouhaiCamInitialPosition;
                    break;
            }
        }
    }

    //カメラの移動
    private void CameraPositionControl()
    {
        if (time * Time.deltaTime > 0.01f)
        {
            if (InputManager.GetInstance.GetKey(InputCode.LOCK)
                || InputManager.GetInstance.GetKeyDown(InputCode.LOCK) 
                || Input.GetKeyDown(KeyCode.LeftShift)
                || Input.GetKey(KeyCode.LeftShift))
            {
                switch (selectCam)
                {
                    case SelectCam.ALL:
                        allCamTransform.localPosition += allCamTransform.right * Input.GetAxis("Horizontal") * cameraSpeed * cameraSensitive;
                        allCamTransform.localPosition += allCamTransform.up * Input.GetAxis("Vertical") * cameraSpeed * cameraSensitive;
                        break;
                    case SelectCam.SEMPAI:
                        sempaiCamTransform.localPosition += sempaiCamTransform.right * Input.GetAxis("Horizontal") * cameraSpeed * cameraSensitive;
                        sempaiCamTransform.localPosition += sempaiCamTransform.up * Input.GetAxis("Vertical") * cameraSpeed * cameraSensitive;
                        break;
                    case SelectCam.KOUHAI:
                        kouhaiCamTransform.localPosition += kouhaiCamTransform.right * Input.GetAxis("Horizontal") * cameraSpeed * cameraSensitive;
                        kouhaiCamTransform.localPosition += kouhaiCamTransform.up * Input.GetAxis("Vertical") * cameraSpeed * cameraSensitive;
                        break;
                }
            }
            else
            {
                switch (selectCam)
                {
                    case SelectCam.ALL:
                        allCamTransform.localPosition += allCamTransform.right * Input.GetAxis("Horizontal") * cameraSpeed * cameraSensitive;
                        allCamTransform.localPosition += allCamTransform.forward * Input.GetAxis("Vertical") * cameraSpeed * cameraSensitive;
                        break;
                    case SelectCam.SEMPAI:
                        sempaiCamTransform.localPosition += sempaiCamTransform.right * Input.GetAxis("Horizontal") * cameraSpeed * cameraSensitive;
                        sempaiCamTransform.localPosition += sempaiCamTransform.forward * Input.GetAxis("Vertical") * cameraSpeed * cameraSensitive;
                        break;
                    case SelectCam.KOUHAI:
                        kouhaiCamTransform.localPosition += kouhaiCamTransform.right * Input.GetAxis("Horizontal") * cameraSpeed * cameraSensitive;
                        kouhaiCamTransform.localPosition += kouhaiCamTransform.forward * Input.GetAxis("Vertical") * cameraSpeed * cameraSensitive;
                        break;
                }
            }
            time = 0;
        }
        time++;
    }

    //カメラの回転
    private void CameraRotate()
    {
        switch (selectCam)
        {
            case SelectCam.ALL:
                allCamTransform.localEulerAngles += new Vector3(Input.GetAxis("RightStickVertical"), Input.GetAxis("RightStickHorizon"), 0) * cameraSensitive;
                break;
            case SelectCam.SEMPAI:
                sempaiCamTransform.localEulerAngles +=  new Vector3(Input.GetAxis("RightStickVertical"), Input.GetAxis("RightStickHorizon"), 0) * cameraSensitive;
                break;
            case SelectCam.KOUHAI:
                kouhaiCamTransform.localEulerAngles += new Vector3(Input.GetAxis("RightStickVertical"), Input.GetAxis("RightStickHorizon"), 0) * cameraSensitive;
                break;
        }
    }
}


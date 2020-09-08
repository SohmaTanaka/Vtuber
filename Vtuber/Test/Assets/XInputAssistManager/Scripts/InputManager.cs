#region Lisence

/*
【English】
BSD 2-Clause License

Copyright (c) 2019, Haku Kaido
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

【日本語】
Copyright (c) <2019>, <Haku Kaido>
All rights reserved.

ソースコード形式かバイナリ形式か、変更するかしないかを問わず、以下の条件を満たす場合に限り、再頒布および使用が許可されます。 

・ソースコードを再頒布する場合、上記の著作権表示、本条件一覧、および下記免責条項を含めること。 
・バイナリ形式で再頒布する場合、頒布物に付属のドキュメント等の資料に、上記の著作権表示、本条件一覧、および下記免責条項を含めること。 
 
本ソフトウェアは、著作権者およびコントリビューターによって「現状のまま」提供されており、明示黙示を問わず、商業的な使用可能性、および特定の目的に対する適合性に関する暗黙の保証も含め、またそれに限定されない、いかなる保証もありません。著作権者もコントリビューターも、事由のいかんを問わず、 損害発生の原因いかんを問わず、かつ責任の根拠が契約であるか厳格責任であるか（過失その他の）不法行為であるかを問わず、仮にそのような損害が発生する可能性を知らされていたとしても、本ソフトウェアの使用によって発生した（代替品または代用サービスの調達、使用の喪失、データの喪失、利益の喪失、業務の中断も含め、またそれに限定されない）直接損害、間接損害、偶発的な損害、特別損害、懲罰的損害、または結果損害について、一切責任を負わないものとします。 

このライセンスはBSD 2-Clause Licenseを利用しています
*/

#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XInputAssistManager
{
    /// <summary>
    /// ※継承禁止、GamePad操作はXInput専用
    /// Inputの管理
    /// 
    /// 作成者:海堂 博
    /// 公開ユーザー:https://github.com/HakuKaido
    /// 作成日時:2018/9/20
    /// </summary>
    public sealed class InputManager : MonoBehaviour
    {
        //インスタンス用フィールド変数
        static InputManager inputManager;

        //JoyStick接続確認用配列
        string[] gamePadNames;

        #region プロパティ

        /// <summary>
        /// ゲームパッドを使用しているか？
        /// </summary>
        public bool IsUseGamePad { get; set; }

        /// <summary>
        /// キーコンフィグの設定を収納するDictionaryプロパティ（ボタン版）
        /// </summary>
        public Dictionary<InputCode, KeyCode> CodeDictionary { get; private set; }

        /// <summary>
        /// キーコンフィグの設定を収納するDictionaryプロパティ（キーボード版）
        /// </summary>
        public Dictionary<InputCode, KeyCode> CodeDictionaryOnKeyBoard { get; private set; }

        /// <summary>
        /// ゲームパッドのボタンの名前を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<ButtonList, KeyCode> ButtonNameDictionary { get; private set; }

        /// <summary>
        /// キーコンフィグ（Trigger）の設定を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<InputCode, TriggerAsButton> CodeTriggerDictionary { get; private set; }

        /// <summary>
        /// ゲームパッドのTriggerの名前を収納するDictionaryプロパティ
        /// </summary>
        public Dictionary<ButtonList, TriggerAsButton> TriggerNameDictionary { get; private set; }

        /// <summary>
        /// 再処理までのカウントをするタイマー
        /// </summary>
        float RestartTimer { get; set; }

        /// <summary>
        /// RT用
        /// </summary>
        TriggerAsButton RightTrigger { get; set; }

        /// <summary>
        /// LT用
        /// </summary>
        TriggerAsButton LeftTrigger { get; set; }

        #region Axis処理

        /// <summary>
        /// 水平方向の入力の大きさ取得
        /// </summary>
        /// <returns>水平方向の入力の大きさ</returns>
        public float GetAxisHorizontal
        {
            get { return Input.GetAxis("Horizontal"); }
        }

        /// <summary>
        /// 垂直方向の入力の大きさ取得
        /// </summary>
        /// <returns>垂直方向の入力の大きさ</returns>
        public float GetAxisVertical
        {
            get { return Input.GetAxis("Vertical"); }
        }

        /// <summary>
        /// ※ProjectSettingsのInputに"RightStickHorizontal"をJoyStickAxisの4th axisで追加しておくこと！
        /// 
        /// 右スティック水平方向の入力の大きさ取得
        /// </summary>
        /// <returns>右スティック水平方向の入力の大きさ</returns>
        public float GetAxisRightHorizontal
        {
            get { return Input.GetAxis("RightStickHorizontal"); }
        }

        /// <summary>
        /// ※ProjectSettingsのInputに"RightStickVertical"をJoyStickAxisの5th axisで追加しておくこと！
        /// 
        /// 右スティック垂直方向の入力の大きさ取得
        /// </summary>
        /// <returns>右スティック垂直方向の入力の大きさ</returns>
        public float GetAxisRightVertical
        {
            get { return Input.GetAxis("RightStickVertical"); }
        }

        #endregion

        #endregion

        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <returns>実体</returns>
        public static InputManager GetInstance
        {
            get
            {
                if (inputManager == null)
                {
                    //GameObjectの生成
                    GameObject gameObject = new GameObject("InputManagerGO");
                    //InputManagerのAdd
                    gameObject.AddComponent<InputManager>();
                    //破壊不可
                    DontDestroyOnLoad(gameObject);

                    inputManager = gameObject.GetComponent<InputManager>();
                }

                return inputManager;
            }
        }

        /// <summary>
        /// 生成直後に一回だけ実行
        /// </summary>
        void Awake()
        {
            //初期化

            RightTrigger = new TriggerAsButton(ButtonList.RightTrigger);
            LeftTrigger = new TriggerAsButton(ButtonList.LeftTrigger);
            CodeDictionary = new Dictionary<InputCode, KeyCode>();
            CodeDictionaryOnKeyBoard = new Dictionary<InputCode, KeyCode>();
            ButtonNameDictionary = new Dictionary<ButtonList, KeyCode>();
            CodeTriggerDictionary = new Dictionary<InputCode, TriggerAsButton>();
            TriggerNameDictionary = new Dictionary<ButtonList, TriggerAsButton>();

            RestartTimer = 0.0f;

            SetButtonName();
            SetCode();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            //TriggerNameDictionary内の値の更新処理
            foreach (TriggerAsButton item in TriggerNameDictionary.Values)
            {
                item.Update();
            }

            //GamePadが接続しているか？
            gamePadNames = Input.GetJoystickNames();

            ////接続している時
            //if (gamePadNames.Length != 0)
            //{
            //    //どれかのボタンを押したらゲームパッド入力を利用する
            //    if (AnyButtonDown()) IsUseGamePad = true;
            //}

            ////キーを押したときはゲームパッド入力を使用しない
            //if (!AnyButtonDown())
            //{
            //    if (Input.anyKeyDown) IsUseGamePad = false;
            //}
        }

        #region Awakeの中身

        /// <summary>
        /// ※Awakeで呼ぶこと
        /// ※事故防止のためfor文などでは回していない
        /// 
        /// ButtonListとKeyCodeを結びつけるメソッド
        /// </summary>
        void SetButtonName()
        {
            //WindowsOS用
            //XInput
            //Button
            ButtonNameDictionary.Add(ButtonList.GamePad_A, KeyCode.Joystick1Button0);   //Aボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_B, KeyCode.Joystick1Button1);   //Bボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_X, KeyCode.Joystick1Button2);   //Xボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Y, KeyCode.Joystick1Button3);   //Yボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_L, KeyCode.Joystick1Button4);   //Lボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_R, KeyCode.Joystick1Button5);   //Rボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Back, KeyCode.Joystick1Button6);   //Backボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Start, KeyCode.Joystick1Button7);   //Startボタン

            ButtonNameDictionary.Add(ButtonList.GamePad_A_2, KeyCode.Joystick2Button0);   //Aボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_B_2, KeyCode.Joystick2Button1);   //Bボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_X_2, KeyCode.Joystick2Button2);   //Xボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Y_2, KeyCode.Joystick2Button3);   //Yボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_L_2, KeyCode.Joystick2Button4);   //Lボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_R_2, KeyCode.Joystick2Button5);   //Rボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Back_2, KeyCode.Joystick2Button6);   //Backボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Start_2, KeyCode.Joystick2Button7);   //Startボタン

            ButtonNameDictionary.Add(ButtonList.GamePad_A_3, KeyCode.Joystick3Button0);   //Aボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_B_3, KeyCode.Joystick3Button1);   //Bボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_X_3, KeyCode.Joystick3Button2);   //Xボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Y_3, KeyCode.Joystick3Button3);   //Yボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_L_3, KeyCode.Joystick3Button4);   //Lボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_R_3, KeyCode.Joystick3Button5);   //Rボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Back_3, KeyCode.Joystick3Button6);   //Backボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Start_3, KeyCode.Joystick3Button7);   //Startボタン

            ButtonNameDictionary.Add(ButtonList.GamePad_A_4, KeyCode.Joystick4Button0);   //Aボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_B_4, KeyCode.Joystick4Button1);   //Bボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_X_4, KeyCode.Joystick4Button2);   //Xボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Y_4, KeyCode.Joystick4Button3);   //Yボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_L_4, KeyCode.JoystickButton4);   //Lボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_R_4, KeyCode.Joystick4Button5);   //Rボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Back_4, KeyCode.Joystick4Button6);   //Backボタン
            ButtonNameDictionary.Add(ButtonList.GamePad_Start_4, KeyCode.Joystick4Button7);   //Startボタン

            //Trigger
            TriggerNameDictionary.Add(ButtonList.LeftTrigger, LeftTrigger);         //LT
            TriggerNameDictionary.Add(ButtonList.RightTrigger, RightTrigger);   //RT
        }

        /// <summary>
        /// ※Awakeで呼ぶこと
        /// ※事故防止のためfor文などでは回していない
        /// 
        /// InputCodeとKeyCodeを結びつけるメソッド
        /// </summary>
        void SetCode()
        {
            //デフォルトのキー設定
            //例（ボタン版）:CodeDictionary.Add(InputCode.操作, ButtonNameDictionary[ButtonList.対応させるボタン名]);

            ////サンプル用
            //CodeDictionary.Add(InputCode.Attack, ButtonNameDictionary[ButtonList.GamePad_X]);
            //CodeDictionary.Add(InputCode.Jump, ButtonNameDictionary[ButtonList.GamePad_A]);

            CodeDictionary.Add(InputCode.Main, ButtonNameDictionary[ButtonList.GamePad_A]);
            CodeDictionary.Add(InputCode.Sempai, ButtonNameDictionary[ButtonList.GamePad_X]);
            CodeDictionary.Add(InputCode.Kouhai, ButtonNameDictionary[ButtonList.GamePad_B]);
            CodeDictionary.Add(InputCode.LOCK, ButtonNameDictionary[ButtonList.GamePad_L]);
            CodeDictionary.Add(InputCode.RESET, ButtonNameDictionary[ButtonList.GamePad_R]);
            CodeTriggerDictionary.Add(InputCode.ZOOMIN, TriggerNameDictionary[ButtonList.RightTrigger]);
            CodeTriggerDictionary.Add(InputCode.ZOOMOUT, TriggerNameDictionary[ButtonList.LeftTrigger]);

            #region だいち

            CodeDictionary.Add(InputCode.DAICHI_ANGRY, ButtonNameDictionary[ButtonList.GamePad_X_2]);
            CodeDictionary.Add(InputCode.DAICHI_FUN, ButtonNameDictionary[ButtonList.GamePad_B_2]);
            CodeDictionary.Add(InputCode.DAICHI_SORROW, ButtonNameDictionary[ButtonList.GamePad_Y_2]);
            CodeDictionary.Add(InputCode.DAICHI_BRINK, ButtonNameDictionary[ButtonList.GamePad_R_2]);

            #endregion

            #region そら

            CodeDictionary.Add(InputCode.SORA_ANGRY, ButtonNameDictionary[ButtonList.GamePad_X_3]);
            CodeDictionary.Add(InputCode.SORA_FUN, ButtonNameDictionary[ButtonList.GamePad_B_3]);
            CodeDictionary.Add(InputCode.SORA_SORROW, ButtonNameDictionary[ButtonList.GamePad_Y_3]);
            //CodeDictionary.Add(InputCode.SORA_BRINK, ButtonNameDictionary[ButtonList.GamePad_R_3]);

            #endregion

            #region あげは

            CodeDictionary.Add(InputCode.AGEHA_JOY, ButtonNameDictionary[ButtonList.GamePad_A_2]);
            CodeDictionary.Add(InputCode.AGEHA_ANGRY, ButtonNameDictionary[ButtonList.GamePad_X_4]);
            CodeDictionary.Add(InputCode.AGEHA_FUN, ButtonNameDictionary[ButtonList.GamePad_B_4]);
            CodeDictionary.Add(InputCode.AGEHA_SORROW, ButtonNameDictionary[ButtonList.GamePad_Y_4]);
            CodeDictionary.Add(InputCode.AGEHA_BRINK, ButtonNameDictionary[ButtonList.GamePad_R_4]);

            #endregion

            //例（キーボード版）:CodeDictionaryOnKeyBoard.Add(InputCode.操作, KeyCode.対応させるキー名);

            ////サンプル用
            //CodeDictionaryOnKeyBoard.Add(InputCode.Attack, KeyCode.Mouse0);
            //CodeDictionaryOnKeyBoard.Add(InputCode.Jump, KeyCode.Space);
        }

        #endregion

        #region キーの状態取得

        /// <summary>
        /// キーを押してない状態から押したときtrue
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyDown(InputCode inputCode)
        {
            //現在の入力キーがあったら
            if (CodeDictionaryOnKeyBoard.ContainsKey(inputCode))
            {
                //キーボード使ってるなら
                return Input.GetKeyDown(CodeDictionaryOnKeyBoard[inputCode]);
            }

            //switch (IsUseGamePad)
            //{
            //    case true:

            //現在の入力キーがCodeDictionaryに入っていなければ
            if (!CodeDictionary.ContainsKey(inputCode))
            {
                //トリガーの状態を返す
                return CodeTriggerDictionary[inputCode].IsTriggerDown;
            }

            //ボタンもしくはキーのKeyCodeを返す
            return Input.GetKeyDown(CodeDictionary[inputCode]);
            //}
        }

        /// <summary>
        /// キーを押し続けている時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKey(InputCode inputCode)
        {
            //現在の入力キーがあったら
            if (CodeDictionaryOnKeyBoard.ContainsKey(inputCode))
            {
                //キーボード使ってるなら
                return Input.GetKey(CodeDictionaryOnKeyBoard[inputCode]);
            }

            //switch (IsUseGamePad)
            //{
            //    case true:

            //現在の入力キーがCodeDictionaryに入っていなければ
            if (!CodeDictionary.ContainsKey(inputCode))
            {
                //トリガーの状態を返す
                return CodeTriggerDictionary[inputCode].GetTriggerState;
            }

            //ボタンもしくはキーのKeyCodeを返す
            return Input.GetKey(CodeDictionary[inputCode]);
            //}

        }

        /// <summary>
        /// キーを押している状態から離した時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyUp(InputCode inputCode)
        {
            //現在の入力キーがあったら
            if (CodeDictionaryOnKeyBoard.ContainsKey(inputCode))
            {
                //キーボード使ってるなら
                return Input.GetKeyUp(CodeDictionaryOnKeyBoard[inputCode]);
            }

            //switch (IsUseGamePad)
            //{
            //    case true:

            //現在の入力キーがCodeDictionaryに入っていなければ
            if (!CodeDictionary.ContainsKey(inputCode))
            {
                //トリガーの状態を返す
                return CodeTriggerDictionary[inputCode].IsTriggerUp;
            }

            //ボタンもしくはキーのKeyCodeを返す
            return Input.GetKeyUp(CodeDictionary[inputCode]);
            //}
        }

        #endregion

        /// <summary>
        /// キーの設定
        /// </summary>
        /// <param name="inputCord">操作の名前</param>
        /// <param name="key">キーコード</param>
        public void SetButton(InputCode inputCode, KeyCode key)
        {
            //現在の入力キーがCodeTriggerDictionaryに入っていたら
            if (CodeTriggerDictionary.ContainsKey(inputCode))
            {
                //消しておく(競合回避)
                CodeTriggerDictionary.Remove(inputCode);
                CodeDictionary.Add(inputCode, key);
            }
            else
            {
                CodeDictionary[inputCode] = key;
            }
        }

        /// <summary>
        /// キーの設定（トリガー版）
        /// 
        /// </summary>
        /// <param name="inputCord">操作の名前</param>
        /// <param name="trigger">キーコード</param>
        public void SetTrigger(InputCode inputCode, TriggerAsButton trigger)
        {
            //現在の入力キーがCodeDictionaryに入っていたら
            if (CodeDictionary.ContainsKey(inputCode))
            {
                //消しておく(競合回避)
                CodeDictionary.Remove(inputCode);
                CodeTriggerDictionary.Add(inputCode, trigger);
            }
            else
            {
                CodeTriggerDictionary[inputCode] = trigger;
            }
        }

        /// <summary>
        /// KeyCodeを返す
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public KeyCode GetButtonCode(ButtonList button)
        {
            return ButtonNameDictionary[button];
        }

        /// <summary>
        /// ボタン（トリガー）名で直接判定する
        /// 前のフレームに押されていなくて現在のフレームで押されていたらtrue
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool GetKeyCodeButtonDown(ButtonList button)
        {
            //ボタンかトリガーかの判別
            if (ButtonNameDictionary.ContainsKey(button)) return Input.GetKeyDown(GetButtonCode(button));
            else if (TriggerNameDictionary.ContainsKey(button)) return TriggerNameDictionary[button].IsTriggerDown;

            return false;
        }

        /// <summary>
        /// ボタン（トリガー）名で直接判定する
        /// キーを押し続けている時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyCodeButton(ButtonList button)
        {
            //ボタンかトリガーかの判別
            if (ButtonNameDictionary.ContainsKey(button)) return Input.GetKey(GetButtonCode(button));
            else if (TriggerNameDictionary.ContainsKey(button)) return TriggerNameDictionary[button].GetTriggerState;

            return false;
        }

        /// <summary>
        /// ボタン（トリガー）名で直接判定する
        /// キーを押している状態から離した時true
        /// </summary>
        /// <param name="inputCode">操作の名前</param>
        /// <returns>キーの状態</returns>
        public bool GetKeyCodeButtonUp(ButtonList button)
        {
            //ボタンかトリガーかの判別
            if (ButtonNameDictionary.ContainsKey(button)) return Input.GetKeyUp(GetButtonCode(button));
            else if (TriggerNameDictionary.ContainsKey(button)) return TriggerNameDictionary[button].IsTriggerUp;

            return false;
        }

        /// <summary>
        /// いずれかのボタン（トリガー）が押されたら
        /// </summary>
        /// <returns></returns>
        public bool AnyButtonDown()
        {
            //ButtonListの数だけ回して、どれかが押されていたらtureを返す
            for (int i = 0; i < (int)ButtonList.End; i++)
            {
                if (inputManager.GetKeyCodeButtonDown((ButtonList)i)) return true;
            }

            return false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputAssistManager;

/// <summary>
/// 表情管理
/// 
/// 作成者:海堂 博
/// </summary>
public class ExpresionManager : MonoBehaviour
{
    enum DaichiExpression
    {
        DEFAULT,
        SORROW,
        ANGRY,
        BRINK,
        FUN,
    }

    enum SoraExpression
    {
        DEFAULT,
        SORROW,
        ANGRY,
        BRINK,
        FUN,
    }

    enum AgehaExpression
    {
        DEFAULT,
        JOY,
        SORROW,
        ANGRY,
        BRINK,
        FUN,
    }

    /****スキンメッシュレンダラーの取得用変数****/
    /****ブレンドシェイプの取得に必要****/
    [SerializeField, Tooltip("先輩のスキンメッシュレンダラー")]
    List<SkinnedMeshRenderer> daichiSkinMeshRenderer;
    [SerializeField, Tooltip("後輩（男）のスキンメッシュレンダラー")]
    List<SkinnedMeshRenderer> soraSkinMeshRenderer;
    [SerializeField, Tooltip("後輩（女）のスキンメッシュレンダラー")]
    List<SkinnedMeshRenderer> agehaSkinMeshRenderer;

    #region ダイチ

    int daichiSorrowMabuta;
    int daichiAngryMabuta;
    int daichiBrinkMabuta;
    int daichiFunMabuta;

    int daichiHa;

    int daichiSorrowMatuge;
    int daichiAngryMatuge;
    int daichiBrinkMatuge;
    int daichiFunMatuge;

    int daichiHeight;
    int daichiRotate;

    DaichiExpression daichiExpression;

    #endregion

    #region ソラ

    int soraSorrowMabuta;
    int soraAngryMabuta;
    int soraBrinkMabuta;
    int soraFunMabuta;

    int soraHa;

    int soraSorrowMatuge;
    int soraAngryMatuge;
    int soraBrinkMatuge;
    int soraFunMatuge;

    int soraHeight;
    int soraRotate;

    SoraExpression soraExpression;

    #endregion

    #region アゲハ

    int agehaSorrowMabuta;
    int agehaBrinkMabuta;
    int agehaFunMabuta;
    int agehaJoyMabuta;

    int agehaAngryMayu;
    int agehaSorrowMayu;
    int agehaFunMayu;

    int agehaJoyMatuge2;

    int agehaSorrowMatuge;
    int agehaBrinkMatuge;
    int agehaFunMatuge;
    int agehaJoyMatuge;

    int agehaTooth;

    AgehaExpression agehaExpression;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        #region 先輩

        daichiExpression = DaichiExpression.DEFAULT;

        #region head.main

        daichiSorrowMabuta = daichiSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.sorrow");
        daichiAngryMabuta = daichiSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.angry");
        daichiBrinkMabuta = daichiSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.brink");
        daichiFunMabuta = daichiSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.fun");

        #endregion _head.main

        #region ha

        daichiHa = daichiSkinMeshRenderer[1].sharedMesh.GetBlendShapeIndex("ha1.hiraki");

        #endregion

        #region matuge1

        daichiSorrowMatuge = daichiSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.sorrow");
        daichiAngryMatuge = daichiSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.angry");
        daichiBrinkMatuge = daichiSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.brink");
        daichiFunMatuge = daichiSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.fun");

        #endregion

        #region mayuge1

        daichiHeight = daichiSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("blendShape1.height");
        daichiRotate = daichiSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("blendShape1.rotation");

        #endregion

        #endregion _先輩

        #region ソラ

        soraExpression = SoraExpression.DEFAULT;

        #region head.main

        soraSorrowMabuta = soraSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("face.sorrow");
        soraAngryMabuta = soraSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("face.angry");
        soraBrinkMabuta = soraSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("face.brink");
        soraFunMabuta = soraSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("face.fun");

        #endregion _head.main

        #region ha

        soraHa = soraSkinMeshRenderer[1].sharedMesh.GetBlendShapeIndex("ha1.hiraki");

        #endregion

        #region matuge1

        soraSorrowMatuge = soraSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.sorrow");
        soraAngryMatuge = soraSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.angry");
        soraBrinkMatuge = soraSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.brink");
        soraFunMatuge = soraSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge6.fun");

        #endregion

        #region mayuge1

        soraHeight = soraSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("blendShape1.height");
        soraRotate = soraSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("blendShape1.rotation");

        #endregion

        #endregion

        #region あげは

        agehaExpression = AgehaExpression.DEFAULT;

        #region head_main1

        agehaSorrowMabuta = agehaSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.sorrow");
        //agehaBrinkMabuta = agehaSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.brink");
        agehaFunMabuta = agehaSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.fun");
        agehaJoyMabuta = agehaSkinMeshRenderer[0].sharedMesh.GetBlendShapeIndex("mabuta.joy");

        #endregion

        #region eyebrow_main

        agehaSorrowMayu = agehaSkinMeshRenderer[1].sharedMesh.GetBlendShapeIndex("mayu.sorrow");
        agehaAngryMayu = agehaSkinMeshRenderer[1].sharedMesh.GetBlendShapeIndex("mayu.angry");
        agehaFunMayu = agehaSkinMeshRenderer[1].sharedMesh.GetBlendShapeIndex("mayu.fun");

        #endregion

        #region eyelashes_joy_main

        agehaJoyMatuge2 = agehaSkinMeshRenderer[2].sharedMesh.GetBlendShapeIndex("matuge2.joy2");

        #endregion

        #region eyelashes_main

        agehaSorrowMatuge = agehaSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("matuge.sorrow");
        //agehaBrinkMatuge = agehaSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("matuge.brink");
        agehaFunMatuge = agehaSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("matuge.fun");
        agehaJoyMatuge = agehaSkinMeshRenderer[3].sharedMesh.GetBlendShapeIndex("matuge.joy");

        #endregion

        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        #region だいち

        if (Input.GetKey(KeyCode.A))//InputManager.GetInstance.GetKey(InputCode.DAICHI_ANGRY))
        {
            daichiExpression = DaichiExpression.ANGRY;
        }
        else if (Input.GetKey(KeyCode.S))//InputManager.GetInstance.GetKey(InputCode.DAICHI_FUN))
        {
            daichiExpression = DaichiExpression.FUN;
        }
        else if (Input.GetKey(KeyCode.D))//InputManager.GetInstance.GetKey(InputCode.DAICHI_SORROW))
        {
            daichiExpression = DaichiExpression.SORROW;
        }
        //else if (InputManager.GetInstance.GetKey(InputCode.DAICHI_BRINK))
        //{
        //    daichiExpression = DaichiExpression.BRINK;
        //}
        else
        {
            daichiExpression = DaichiExpression.DEFAULT;
        }

        switch (daichiExpression)
        {
            case DaichiExpression.DEFAULT:
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiSorrowMabuta, 0);
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiAngryMabuta, 0);
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiBrinkMabuta, 0);
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiFunMabuta, 0);

                daichiSkinMeshRenderer[1].SetBlendShapeWeight(daichiHa, 0);

                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiSorrowMatuge, 0);
                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiAngryMatuge, 0);
                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiBrinkMatuge, 0);
                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiFunMatuge, 0);

                daichiSkinMeshRenderer[3].SetBlendShapeWeight(daichiHeight, 0);
                daichiSkinMeshRenderer[3].SetBlendShapeWeight(daichiRotate, 0);
                break;
            case DaichiExpression.SORROW:
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiSorrowMabuta, 100);
                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiSorrowMatuge, 100);
                break;
            case DaichiExpression.ANGRY:
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiAngryMabuta, 100);
                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiAngryMatuge, 100);
                break;
            case DaichiExpression.BRINK:
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiBrinkMabuta, 100);
                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiBrinkMatuge, 100);
                break;
            case DaichiExpression.FUN:
                daichiSkinMeshRenderer[0].SetBlendShapeWeight(daichiFunMabuta, 100);
                daichiSkinMeshRenderer[2].SetBlendShapeWeight(daichiFunMatuge, 100);
                break;
        }

        #endregion

        #region そら

        if (Input.GetKey(KeyCode.A)/*InputManager.GetInstance.GetKey(InputCode.SORA_ANGRY)*/)
        {
            soraExpression = SoraExpression.ANGRY;
        }
        else if (Input.GetKey(KeyCode.S)/*InputManager.GetInstance.GetKey(InputCode.SORA_FUN)*/)
        {
            soraExpression = SoraExpression.FUN;
        }
        else if (Input.GetKey(KeyCode.D)/*InputManager.GetInstance.GetKey(InputCode.SORA_SORROW)*/)
        {
            soraExpression = SoraExpression.SORROW;
        }
        //else if (InputManager.GetInstance.GetKey(InputCode.SORA_BRINK))
        //{
        //    soraExpression = SoraExpression.BRINK;
        //}
        else
        {
            soraExpression = SoraExpression.DEFAULT;
        }

        switch (soraExpression)
        {
            case SoraExpression.DEFAULT:
                soraSkinMeshRenderer[0].SetBlendShapeWeight(soraSorrowMabuta, 0);
                soraSkinMeshRenderer[0].SetBlendShapeWeight(soraAngryMabuta, 0);
                //soraSkinMeshRenderer[0].SetBlendShapeWeight(soraBrinkMabuta, 0);
                soraSkinMeshRenderer[0].SetBlendShapeWeight(soraFunMabuta, 0);

                soraSkinMeshRenderer[1].SetBlendShapeWeight(soraHa, 0);

                soraSkinMeshRenderer[2].SetBlendShapeWeight(soraSorrowMatuge, 0);
                soraSkinMeshRenderer[2].SetBlendShapeWeight(soraAngryMatuge, 0);
                //soraSkinMeshRenderer[2].SetBlendShapeWeight(soraBrinkMatuge, 0);
                soraSkinMeshRenderer[2].SetBlendShapeWeight(soraFunMatuge, 0);

                soraSkinMeshRenderer[3].SetBlendShapeWeight(soraHeight, 0);
                soraSkinMeshRenderer[3].SetBlendShapeWeight(soraRotate, 0);
                break;
            case SoraExpression.SORROW:
                soraSkinMeshRenderer[0].SetBlendShapeWeight(soraSorrowMabuta, 100);
                soraSkinMeshRenderer[2].SetBlendShapeWeight(soraSorrowMatuge, 100);
                break;
            case SoraExpression.ANGRY:
                soraSkinMeshRenderer[0].SetBlendShapeWeight(soraAngryMabuta, 100);
                soraSkinMeshRenderer[2].SetBlendShapeWeight(soraAngryMatuge, 100);
                break;
            //case SoraExpression.BRINK:
            //    soraSkinMeshRenderer[0].SetBlendShapeWeight(soraBrinkMabuta, 100);
            //    soraSkinMeshRenderer[2].SetBlendShapeWeight(soraBrinkMatuge, 100);
            //    break;
            case SoraExpression.FUN:
                soraSkinMeshRenderer[0].SetBlendShapeWeight(soraFunMabuta, 100);
                soraSkinMeshRenderer[2].SetBlendShapeWeight(soraFunMatuge, 100);
                break;
            default:
                break;
        }

        #endregion

        #region あげは

        if (Input.GetKey(KeyCode.Keypad0))//InputManager.GetInstance.GetKey(InputCode.AGEHA_JOY))
        {
            agehaExpression = AgehaExpression.JOY;
            Debug.Log("JOY入力中");
        }
        else if (Input.GetKey(KeyCode.Keypad1))//InputManager.GetInstance.GetKey(InputCode.DAICHI_ANGRY))
        {
            agehaExpression = AgehaExpression.ANGRY;
        }
        else if (Input.GetKey(KeyCode.Keypad2)) //InputManager.GetInstance.GetKey(InputCode.DAICHI_FUN))
        {
            agehaExpression = AgehaExpression.FUN;
        }
        else if (Input.GetKey(KeyCode.Keypad3)) //InputManager.GetInstance.GetKey(InputCode.DAICHI_SORROW))
        {
            agehaExpression = AgehaExpression.SORROW;
        }
        //else if (InputManager.GetInstance.GetKey(InputCode.DAICHI_BRINK))
        //{
        //    //agehaExpression = AgehaExpression.BRINK;
        //}
        else
        {
            agehaExpression = AgehaExpression.DEFAULT;
        }

        switch (agehaExpression)
        {
            case AgehaExpression.DEFAULT:
                agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaJoyMabuta, 0);
                agehaSkinMeshRenderer[3].SetBlendShapeWeight(agehaJoyMatuge, 0);
                agehaSkinMeshRenderer[2].SetBlendShapeWeight(agehaJoyMatuge2, 0);
                agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaSorrowMabuta, 0);
                agehaSkinMeshRenderer[1].SetBlendShapeWeight(agehaSorrowMayu, 0);
                agehaSkinMeshRenderer[3].SetBlendShapeWeight(agehaSorrowMatuge, 0);
                agehaSkinMeshRenderer[1].SetBlendShapeWeight(agehaAngryMayu, 0);
                //agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaBrinkMabuta, 0);
                //agehaSkinMeshRenderer[3].SetBlendShapeWeight(agehaBrinkMatuge, 0);
                agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaFunMayu, 0);
                agehaSkinMeshRenderer[1].SetBlendShapeWeight(agehaFunMabuta, 0);
                agehaSkinMeshRenderer[3].SetBlendShapeWeight(agehaFunMatuge, 0);
                break;

            case AgehaExpression.JOY:
                agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaJoyMabuta, 100);
                agehaSkinMeshRenderer[3].SetBlendShapeWeight(agehaJoyMatuge, 100);
                agehaSkinMeshRenderer[2].SetBlendShapeWeight(agehaJoyMatuge2, 100);
                break;

            case AgehaExpression.SORROW:
                agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaSorrowMabuta, 100);
                agehaSkinMeshRenderer[1].SetBlendShapeWeight(agehaSorrowMayu, 100);
                agehaSkinMeshRenderer[3].SetBlendShapeWeight(agehaSorrowMatuge, 100);
                break;
            case AgehaExpression.ANGRY:
                agehaSkinMeshRenderer[1].SetBlendShapeWeight(agehaAngryMayu, 100);
                break;
            //case AgehaExpression.BRINK:
            //    agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaBrinkMabuta, 100);
            //    agehaSkinMeshRenderer[2].SetBlendShapeWeight(agehaBrinkMatuge, 100);
                //break;
            case AgehaExpression.FUN:
                agehaSkinMeshRenderer[0].SetBlendShapeWeight(agehaFunMayu, 100);
                agehaSkinMeshRenderer[1].SetBlendShapeWeight(agehaFunMabuta, 100);
                agehaSkinMeshRenderer[3].SetBlendShapeWeight(agehaFunMatuge, 100);
                break;
        }

        #endregion

    }
}

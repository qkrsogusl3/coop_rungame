﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Seok
{

    public class CUIPlayGame : MonoBehaviour
    {

        public Slider mHealthBarSlider;//체력바
        public Slider mBooster;//부스터게이지
        public Slider mJoyStick;//조이스틱
        public Vector3 mBackGround;
        public float mPlayerSpeed = 0.0f;

        public delegate void CallBackBtn();



        CallBackBtn OnJumping = null;
        CallBackBtn OnSliding = null;
        CallBackBtn OnItemBtn_1 = null;
        CallBackBtn OnItemBtn_2 = null;












        // Use this for initialization
        void Start()
        {
            //InvokeRepeating("HealthDown", 1.0f, 1.0f);
            StartCoroutine(HealthBarDown());//체력바깎이는 코루틴시작
            StartCoroutine(Booster());
        }



        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                mJoyStick.value = 2.0f;
            }
        }



        IEnumerator HealthBarDown()//체력바깎이는 코루틴
        {
            while (true)
            {
                mHealthBarSlider.value -= 20;//체력바 밸류값-20
                yield return new WaitForSeconds(1.0f);
            }
        }

        IEnumerator Booster()//체력바깎이는 코루틴
        {
            while (true)
            {
                mBooster.value += 20;
                yield return new WaitForSeconds(1.0f);
            }
        }





        public void OnClickJump()//점프버튼
        {
            if (null != OnJumping)
            {
                OnJumping();
            }
        }
        public void SetOnJumping(CallBackBtn tA)
        {
            OnJumping = tA;
        }



        public void OnClickSlid()
        {
            if (null != OnSliding)
            {
                OnSliding();
            }
        }
        public void SetOnSliding(CallBackBtn tA)
        {
            OnSliding = tA;
        }

        public float GetJoystickDirection()
        {
            float value = mJoyStick.value;

            if (value < 2)
            {
                return -1;
            }
            else if (value > 2)
            {
                return 1;
            }
            else
            {
                return 0;
            }

            //return value == 1 ? -1 : value == 3 ? 1 : 0;

        }


        public void OnClickItemBtn_1()
        {
            if (null != OnItemBtn_1)
            {
                OnItemBtn_1();
            }
        }
        public void SetOnItemBtn_1(CallBackBtn tA)
        {
            OnItemBtn_1 = tA;
        }

        public void OnClickItemBtn_2()
        {
            if (null != OnItemBtn_2)
            {
                OnItemBtn_2();
            }
        }
        public void SetOnItemBtn_2(CallBackBtn tA)
        {
            OnItemBtn_2 = tA;
        }




        /*
        public void OnClickUseItem1()
        {
            Debug.Log("UseItem1");
        }
        public void OnClickUseItem2()
        {
            Debug.Log("UseItem2");
        }
        */
        /*
        public void Booster()
        {
            //게이지가 채워지는 조건을 쓰라
            mBooster.value = 100.0f;
        }
        */



        /*
        public void JoyStickCtl()
        {
            if(mJoyStick.value<2)
            {
                Debug.Log("Left");
                // mJoyStick.value = 2;
                JoyStcikDirection = -1.0f;
            }
            else if(mJoyStick.value>2)
            {
                Debug.Log("Right");
                // mJoyStick.value = 2;
                JoyStcikDirection = 1.0f;
            }
        }
        */


    }
}

﻿using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace Invector.CharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        private bool isSprinting = false;
        #region variables

        protected vThirdPersonCamera tpCamera;                // acess camera info        
        [HideInInspector] public string customCameraState;    // generic string to change the CameraState        
        [HideInInspector] public string customlookAtPoint;    // generic string to change the CameraPoint of the Fixed Point Mode        
        [HideInInspector] public bool changeCameraState;      // generic bool to change the CameraState        
        [HideInInspector] public bool smoothCameraState;      // generic bool to know if the state will change with or without lerp  
        [HideInInspector] public bool keepDirection;          // keep the current direction in case you change the cameraState

        protected vThirdPersonController cc;                  // access the ThirdPersonController component                

        #endregion

        protected virtual void Start()
        {
            CharacterInit();
        }

        protected virtual void CharacterInit()
        {
            cc = GetComponent<vThirdPersonController>();
            if (cc != null)
                cc.Init();

            tpCamera = FindObjectOfType<vThirdPersonCamera>();
            if (tpCamera) tpCamera.SetMainTarget(this.transform);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected virtual void LateUpdate()
        {
            if (cc == null) return;             // returns if didn't find the controller		    
            InputHandle();                      // update input methods
            UpdateCameraStates();               // update camera states
        }

        protected virtual void FixedUpdate()
        {
            cc.AirControl();
            CameraInput();
        }

        protected virtual void Update()
        {
            cc.UpdateMotor();                   // call ThirdPersonMotor methods               
            cc.UpdateAnimator();                // call ThirdPersonAnimator methods		               
        }

        protected virtual void InputHandle()
        {
            ExitGameInput();
            CameraInput();

            if (!cc.lockMovement)
            {
                MoveCharacter();
                SprintInput();
                //StrafeInput();
                JumpInput();
            }
        }

        #region Basic Locomotion Inputs      

        protected virtual void MoveCharacter()
        {
            cc.input.x = InputManager.MainHorizontal();
            cc.input.y = InputManager.MainVertical();
        }

        // protected virtual void StrafeInput()
        // {
        //     if (InputManager.R3Button()){
        //         cc.Strafe();
        //         isSprinting = false;
        //     } //Input.GetKeyDown(strafeInput) 
        // }

        protected virtual void SprintInput()
        {
            if (Input.GetButtonDown("Sprint") && !isSprinting)
            {
                cc.Sprint(true);
                isSprinting = true;
            }

            else if (Input.GetButtonDown("Sprint") && isSprinting)
            {
                cc.Sprint(false);
                isSprinting = false;
            }
        }

        protected virtual void JumpInput()
        {
            if (Input.GetButtonDown("Jump"))
                cc.Jump();
        }

        protected virtual void ExitGameInput()
        {
            // just a example to quit the application 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Cursor.visible)
                    Cursor.visible = true;
                else
                    Application.Quit();
            }
        }

        #endregion

        #region Camera Methods

        protected virtual void CameraInput()
        {
            if (tpCamera == null)
                return;
            var Y = InputManager.CameraVertical(); //Input.GetAxis(rotateCameraYInput);
            var X = InputManager.CameraHorizontal(); //Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);

            // tranform Character direction from camera if not KeepDirection
            if (!keepDirection)
                cc.UpdateTargetDirection(tpCamera != null ? tpCamera.transform : null);
            // rotate the character with the camera while strafing        
            RotateWithCamera(tpCamera != null ? tpCamera.transform : null);
        }

        protected virtual void UpdateCameraStates()
        {
            // CAMERA STATE - you can change the CameraState here, the bool means if you want lerp of not, make sure to use the same CameraState String that you named on TPCameraListData
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void RotateWithCamera(Transform cameraTransform)
        {
            if (cc.isStrafing && !cc.lockMovement && !cc.lockMovement)
            {
                cc.RotateWithAnotherTransform(cameraTransform);
            }
        }

        #endregion     
    }
}
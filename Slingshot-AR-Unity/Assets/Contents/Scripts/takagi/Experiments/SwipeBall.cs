using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Slingshot.tests
{
	public class SwipeBall : MonoBehaviour {

        /// <summary>
        /// ボールを放つ際に発火するイベント
        /// </summary>
        [SerializeField]
        UnityEvent OnRelease;

        /// <summary>
        /// タッチし始めた位置（Screen座標）
        /// </summary>
        private float startPos;

        #region MonoBehaviour
        // Start is called before the first frame update
        void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
#if UNITY_EDITOR
            //マウスを押した瞬間
            if(Input.GetMouseButtonDown(0))
            {
                OnTouch(Input.mousePosition.y);
            }

            //マウスを押している間
            if (Input.GetMouseButton(0))
            {
                ScreenToBallPoint(Input.mousePosition.y);
            }

            //マウスを離した瞬間
            if (Input.GetMouseButtonUp(0))
            {
                Released();
            }
#elif UNITY_IOS
            // タップ時
            if(Input.touchCount > 0)
            {
                //１点目を取得
                Touch touch = Input.GetTouch(0);

                //タッチした瞬間
                if(touch.phase == TouchPhase.Began)
                {
                    OnTouch(touch.position.y);
                }

                //タッチしている間
                if(touch.phase == TouchPhase.Moved)
                {
                    ScreenToBallPoint(touch.position.y);
                }

                //タッチを離した際
                if(touch.phase == TouchPhase.Ended)
                {
                    Released();
                }
            }
#endif

		}

        #endregion

        #region private methods
        /// <summary>
        /// OnReleaseを発火しボールの位置を戻す
        /// </summary>
        void Released()
        {
            OnRelease?.Invoke();
            startPos = 0;
            InitBall();
        }

        /// <summary>
        /// Touch開始時にstartPosを更新
        /// </summary>
        /// <param name="ypos">Screen座標(y)</param>
        void OnTouch(float ypos)
        {
            startPos = ypos;
        }

        /// <summary>
        /// タッチ中のスクリーン位置からパチンコのボールを動かす
        /// </summary>
        /// <param name="ypos"></param>
        void ScreenToBallPoint(float ypos)
        {
            float distance = startPos - ypos;
            if(distance > 0)
            {
                Vector3 pos = gameObject.transform.localPosition;
                gameObject.transform.localPosition = new Vector3(pos.x, -distance/2000, pos.z);
            }

        }

        /// <summary>
        /// パチンコのボールの位置を戻す
        /// </summary>
        void InitBall()
        {
            Vector3 pos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(pos.x, 0, pos.z);
        }
        #endregion
    }
}

﻿using UnityEngine;

/* http://qiita.com/tempura/items/4a5482ff6247ec8873df */
public static class TouchUtility
{
    private static Vector3 _touchPosition = Vector3.zero;

    /// <summary>
    /// タッチ情報を取得(エディタと実機を考慮)
    /// </summary>
    /// <returns>タッチ情報。タッチされていない場合は null</returns>
    public static TouchInfo GetTouch(int id)
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(id))
            {
                return TouchInfo.Began;
            }
            if (Input.GetMouseButton(id))
            {
                return TouchInfo.Moved;
            }
            if (Input.GetMouseButtonUp(id))
            {
                return TouchInfo.Ended;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                return (TouchInfo)((int)Input.GetTouch(id).phase);
            }
        }
        return TouchInfo.None;
    }

    /// <summary>
    /// タッチポジションを取得(エディタと実機を考慮)
    /// </summary>
    /// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3 GetTouchPosition(int id)
    {
        if (Application.isEditor)
        {
            var touch = GetTouch(id);
            if (touch != TouchInfo.None)
            {
                return Input.mousePosition;
            }
        }
        else
        {
            if (Input.touchCount <= 0)
                return Vector3.zero;
            var touch = Input.GetTouch(id);
            _touchPosition.x = touch.position.x;
            _touchPosition.y = touch.position.y;
            return _touchPosition;
        }
        return Vector3.zero;
    }

    /// <summary>
    /// タッチワールドポジションを取得(エディタと実機を考慮)
    /// </summary>
    /// <param name='camera'>カメラ</param>
    /// <param name="id">タッチ番号（Input.Touchesに振られたID）</param>
    /// <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3 GetTouchWorldPosition(Camera camera, int id)
    {
        return camera.ScreenToWorldPoint(GetTouchPosition(id));
    }
}

/// <summary>
/// タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
/// </summary>
public enum TouchInfo
{
    /// <summary>
    /// タッチなし
    /// </summary>
    None = 99,

    // 以下は UnityEngine.TouchPhase の値に対応
    /// <summary>
    /// タッチ開始
    /// </summary>
    Began = 0,
    /// <summary>
    /// タッチ移動
    /// </summary>
    Moved = 1,
    /// <summary>
    /// タッチ静止
    /// </summary>
    Stationary = 2,
    /// <summary>
    /// タッチ終了
    /// </summary>
    Ended = 3,
    /// <summary>
    /// タッチキャンセル
    /// </summary>
    Canceled = 4,
}

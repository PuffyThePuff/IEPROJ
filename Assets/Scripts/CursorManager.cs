using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CursorManager : MonoBehaviour
{
    public static CursorManager instanceRef;
    public enum cursor_state
    {
        up, down
    }
    private cursor_state currentState = cursor_state.up;
    public static CursorManager Instance = null;
    public Texture2D cursorIdleTex;
    public Texture2D cursorPressedTex;


    void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceRef != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCursorState(cursor_state.up);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentState != cursor_state.down)
            {
                SetCursorState(cursor_state.down);
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (currentState != cursor_state.up)
            {
                SetCursorState(cursor_state.up);
            }
        }
    }

    public void SetCursorState(cursor_state newState)
    {
        currentState = newState;
        switch (newState)
        {
            case cursor_state.up: UnityEngine.Cursor.SetCursor(cursorIdleTex, new Vector2(110, 520), CursorMode.Auto); break;
            case cursor_state.down: UnityEngine.Cursor.SetCursor(cursorPressedTex, new Vector2(120, 535), CursorMode.Auto); break;
        }
    }

    public void OnMouseUp()
    {
        SetCursorState(cursor_state.up);
    }

    public void OnMouseDown()
    {
        SetCursorState(cursor_state.down);
    }
}

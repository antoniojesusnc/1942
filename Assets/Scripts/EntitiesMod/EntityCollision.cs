using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to manage the entity collision area.
/// 
/// For know only set the collision size, the idea is to set this by file for allow different sizes, planes that change size, etc
/// </summary>
public class EntityCollision : MonoBehaviour
{
    /// <summary>
    /// size and offset for the collision
    /// </summary>
    [SerializeField]
    private Rect _size;
    public Rect Size
    {
        get
        {
            return _size;
        }
    }

    /// <summary>
    /// when the class is created, setting the offset and the size collision
    /// </summary>
    void Awake()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(Size.x, Size.y);
        GetComponent<BoxCollider2D>().size = new Vector2(Size.width, Size.height);
    }

    /// <summary>
    /// gizmo for check the size and the offset in the editor
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 offset = new Vector3(Size.x, Size.y, 0);
        Gizmos.DrawWireCube(transform.position + offset, new Vector3(_size.width, _size.height, 1));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class used for set the size of the graphic entity.
/// 
/// For know is only used fo the player entity, the idea is to use this class to make the set the plane graphics and anims
/// </summary>
public class EntityGraphic : MonoBehaviour
{
    /// <summary>
    /// graphic size and offset position
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
    /// gizmo for check the size and the offset in the editor
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 offset = new Vector3(Size.x, Size.y, 0);
        Gizmos.DrawWireCube(transform.position + offset, new Vector3(_size.width, _size.height, 1));
    }
}

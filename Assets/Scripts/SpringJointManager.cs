using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
public class SpringJointManager : MonoBehaviour
{
    [SerializeField] Vector2 connectedAnchorOffset;
    SpringJoint2D springJoint;
    private void Awake()
    {
        #region Getting
        springJoint = GetComponent<SpringJoint2D>();
        #endregion
    }
    private void Start()
    {
        springJoint.connectedAnchor = (Vector2)transform.position + connectedAnchorOffset;
    }
}

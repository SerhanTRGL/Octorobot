using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicWeapon : MonoBehaviour{
    private HingeJoint2D joint;
    private CircleCollider2D circleCollider;

    [SerializeField] private bool isAtBase=false;
    [SerializeField] private Transform mountPoint;
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private Rigidbody2D weaponInFront;
    [SerializeField] private ArmController connectedArm;
    [SerializeField] private GameObject weaponFire;

    private void Start() {
        joint = GetComponent<HingeJoint2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        if (IsCollectible()) {
            circleCollider.enabled = false;
        }
    }

    public bool IsMounted() {
        if (!isAtBase) {
            return !(joint.connectedBody == null);
        }
        return true;
    }
    
    public bool CanMountOnThis() {
        return weaponInFront == null;
    }

    public bool IsCollectible() {
        return connectedArm == null;
    }
    
    public void SetConnectedAnchorForJoint(Rigidbody2D anchor) {
        joint.connectedBody = anchor;
    }

    public void SetWeaponInFront(Rigidbody2D weaponInFront) {
        this.weaponInFront = weaponInFront;
    }

    public void SetConnectedArm(ArmController arm) {
        this.connectedArm = arm;
    }

    public ArmController GetConnectedArm() {
        return connectedArm;
    }

    public Transform GetMountPoint() {
        return mountPoint;
    }

    public Transform GetAnchorPoint() {
        return anchorPoint;
    }

    public HingeJoint2D GetHingeJoint2D() {
        return joint;
    }
}

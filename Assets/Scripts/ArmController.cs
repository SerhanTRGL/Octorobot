using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmController : MonoBehaviour {

    const float DISTANCE_BETWEEN_WEAPONS = 0.22f;
    [SerializeField] private BasicWeapon baseWeapon;
    [SerializeField] private BasicWeapon endWeapon;
    [SerializeField] private WeaponSpriteManager spriteManager;
    
    
    public void Mount(BasicWeapon weaponToMount) { 

        //Get new weapon into the arm
        weaponToMount.transform.parent = this.transform;

        //Set the new position
        Rigidbody2D rb= weaponToMount.GetComponent<Rigidbody2D>();
        weaponToMount.transform.position = endWeapon.GetMountPoint().position;
        rb.velocity = Vector3.zero;

        //Set the rotation
        weaponToMount.gameObject.transform.rotation = endWeapon.transform.rotation;
        HingeJoint2D joint = weaponToMount.GetHingeJoint2D();
        JointAngleLimits2D limit = joint.limits;
        limit.min -= weaponToMount.gameObject.transform.rotation.eulerAngles.z - joint.jointAngle;
        limit.max -= weaponToMount.gameObject.transform.rotation.eulerAngles.z - joint.jointAngle;
        joint.limits = limit;
        rb.angularVelocity = 0;
        
        //Set the anchor for the new weapon
        weaponToMount.SetConnectedAnchorForJoint(endWeapon.GetComponent<Rigidbody2D>());
        //Set the connected arm
        weaponToMount.SetConnectedArm(this);

        //Enable capsule collider
        weaponToMount.GetComponent<CircleCollider2D>().enabled = true;

        //Add new weapon at the front of the previous endWeapon
        endWeapon.SetWeaponInFront(weaponToMount.GetComponent<Rigidbody2D>());

        //Change sprites of new weapon and previous one
        if (baseWeapon == endWeapon) {
            spriteManager.SetSpriteOfWeapon(weaponToMount, WeaponSpriteManager.WEAPON_HIERARCHY.End);
        } else {
            spriteManager.SetSpriteOfWeapon(endWeapon, WeaponSpriteManager.WEAPON_HIERARCHY.Middle);
            spriteManager.SetSpriteOfWeapon(weaponToMount, WeaponSpriteManager.WEAPON_HIERARCHY.End);
        }

        //Change endWeapon reference
        this.endWeapon = weaponToMount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteManager : MonoBehaviour
{
    public enum WEAPON_HIERARCHY {Base, Middle, End };
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite middleSprite;
    [SerializeField] private Sprite endSprite;

    public void SetSpriteOfWeapon(BasicWeapon weapon, WEAPON_HIERARCHY hierarchyLevel) {
        SpriteRenderer spriteRenderer = weapon.GetComponentInChildren<SpriteRenderer>();
        if(hierarchyLevel == WEAPON_HIERARCHY.Base) {
            spriteRenderer.sprite = baseSprite;
        }
        else if (hierarchyLevel == WEAPON_HIERARCHY.Middle) {
            spriteRenderer.sprite= middleSprite;
        }
        else if(hierarchyLevel==WEAPON_HIERARCHY.End) { 
            spriteRenderer.sprite= endSprite;
        }
    }
}

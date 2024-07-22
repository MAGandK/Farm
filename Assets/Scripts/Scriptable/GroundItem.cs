using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
   public ItemObject item;
   
   // это для 2D game
   // public void OnBeforeSerialize()
   // {
   //    GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
   //    EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
   // }
   //
   // public void OnAfterDeserialize()
   // {
   //   
   // }  это для 2D game
}

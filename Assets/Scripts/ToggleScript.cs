using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
   //This script is only for the toggle button in the settings men
   //This is not supposed to be used for anything else
   //This isnt a good solution for the full settings menu as that would require
   //individual scripts for each setting

   //However, this is a good example of how to use a toggle button

   [SerializeField] private Toggle toggle;
   void Start()
   {

   }

   public void OnPointerClick(PointerEventData eventData)
   {
      Debug.Log("Click detected on toggle!");
   }
}

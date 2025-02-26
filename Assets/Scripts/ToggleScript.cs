using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
   //This script sets the MotionControlsToggle to be on or off depending on the Value set in the PlayerPrefs
   //This way you can check if its on or not by looking at the toggle instead of playing the game

   [SerializeField] public Toggle toggle;
   void Update()
   {
      if (PlayerPrefs.GetInt("motionControls") == 1)
      {
         toggle.isOn = true;
      }
      else if(PlayerPrefs.GetInt("motionControls") == 0)
      {
         toggle.isOn = false;
      }
      else
      {toggle.isOn = true;}
   }

   public void OnPointerClick(PointerEventData eventData)
   {
      Debug.Log("Click detected on toggle!");
   }
}

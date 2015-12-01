using UnityEngine;
using System.Collections;

public class HUDInfo : MonoBehaviour
{
	
	private TextAnchor anchorAt = TextAnchor.MiddleLeft;
	private static int numberOfLines = 4;
	private int pixelXOffset = 5;
	private int pixelYOffset = -120;
	private GameObject guiObj;
	private static GUIText guiTxt;
	private TextAnchor _anchorAt;
	private float _pixelXOffset;
	private float _pixelYOffset;
	private static ArrayList messageHistory = new ArrayList ();
	private static ArrayList timeMessageHistory = new ArrayList();
	
	void Awake ()
	{
		guiObj = new GameObject ("HUD Info");
		guiObj.AddComponent<GUIText> ();
		guiObj.transform.position = Vector3.zero;
		guiObj.transform.localScale = new Vector3 (0, 0, 1);
		guiObj.name = "HUD Info";
		guiTxt = guiObj.GetComponent<GUIText> ();
		_anchorAt = anchorAt;
		SetPosition ();
	}
	
	void Start ()
	{
		
	}
	
	void Update ()
	{	
		//	if anchorAt or pixelOffset has changed while running, update the text position
		if (_anchorAt != anchorAt || _pixelXOffset != pixelXOffset || _pixelYOffset != pixelYOffset) {
			_anchorAt = anchorAt;
			_pixelXOffset = pixelXOffset;
			_pixelYOffset = pixelYOffset;
			SetPosition ();
		}
		
		checkMessageTime();
	}
	
	public static void insertMessage(string msg){
		messageHistory.Insert(0, msg);
		timeMessageHistory.Insert(0, Time.time);
		
		while (messageHistory.Count > numberOfLines) {
			messageHistory.RemoveAt(messageHistory.Count - 1);
			timeMessageHistory.RemoveAt(timeMessageHistory.Count - 1);
		}
		refreshText ();
	}
	
	private void checkMessageTime(){
		if (timeMessageHistory.Count > 0) {
			float timeMsg = (float) timeMessageHistory[timeMessageHistory.Count - 1];
			if (Time.time > (timeMsg + 20f)){
				messageHistory.RemoveAt(messageHistory.Count - 1);
				timeMessageHistory.RemoveAt(timeMessageHistory.Count - 1);
				refreshText();
			}
		}
	}
	
	private static void refreshText(){
		string displayText = "";
		for (int i = 0; i < messageHistory.Count; i++) {
			if (i == 0)
				displayText = messageHistory[i] as string;
			else
				displayText = (messageHistory [i] as string) + "\n" + displayText;
		}
		guiTxt.text = displayText;
	}
	
	public void SetPosition ()
	{
		switch (anchorAt) {
		case TextAnchor.UpperLeft:
			guiObj.transform.position = new Vector3 (0.0f, 1.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Left;
			break;
		case TextAnchor.UpperCenter:
			guiObj.transform.position = new Vector3 (0.5f, 1.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Center;
			break;
		case TextAnchor.UpperRight:
			guiObj.transform.position = new Vector3 (1.0f, 1.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Right;
			break;
		case TextAnchor.MiddleLeft:
			guiObj.transform.position = new Vector3 (0.0f, 0.5f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Left;
			
			break;
		case TextAnchor.MiddleCenter:
			guiObj.transform.position = new Vector3 (0.5f, 0.5f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Center;
			break;
		case TextAnchor.MiddleRight:
			guiObj.transform.position = new Vector3 (1.0f, 0.5f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Right;
			break;
		case TextAnchor.LowerLeft:
			guiObj.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Left;
			break;
		case TextAnchor.LowerCenter:
			guiObj.transform.position = new Vector3 (0.5f, 0.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Center;
			break;
		case TextAnchor.LowerRight:
			guiObj.transform.position = new Vector3 (1.0f, 0.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Right;
			break;
		}
		guiTxt.pixelOffset = new Vector2 (pixelXOffset, pixelYOffset);
	}
}
using UnityEngine;
using System.Collections;

public class HUDInfo : MonoBehaviour
{

	public static string message;
	public bool showLineMovement = false;
	public TextAnchor anchorAt = TextAnchor.MiddleLeft;
	public int numberOfLines = 2;
	public int pixelXOffset = 5;
	public int pixelYOffset = -140;
	private GameObject guiObj;
	private GUIText guiTxt;
	private TextAnchor _anchorAt;
	private float _pixelXOffset;
	private float _pixelYOffset;
	private string	_message;
	private ArrayList messageHistory = new ArrayList ();
	private int messageHistoryLength;
	private string	displayText;
	// Use this for initialization
	void Awake ()
	{
		guiObj = new GameObject ("HUD Info");
		guiObj.AddComponent<GUIText> ();
		guiObj.transform.position = Vector3.zero;
		guiObj.transform.localScale = new Vector3 (0, 0, 1);
		guiObj.name = "HUD Info";
		guiTxt = guiObj.GetComponent<GUIText> ();
		_anchorAt = anchorAt;
		_message = message;
		SetPosition ();
	}

	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		//	if anchorAt or pixelOffset has changed while running, update the text position
		if (_anchorAt != anchorAt || _pixelXOffset != pixelXOffset || _pixelYOffset != pixelYOffset) {
			_anchorAt = anchorAt;
			_pixelXOffset = pixelXOffset;
			_pixelYOffset = pixelYOffset;
			SetPosition ();
		}
			
		//	if the message has changed, update the display
		if (_message != message) {
			_message = message;
			messageHistory.Insert (0, message);
			messageHistoryLength = messageHistory.Count;

			while (messageHistoryLength>numberOfLines) {
				//messageHistory.Pop();
				messageHistory.RemoveAt (messageHistory.Count - 1);
				messageHistoryLength = messageHistory.Count;
			}
		
			//	create the multi-line text to display
			displayText = "";
			for (int i = 0; i < messageHistory.Count; i++) {
				if (i == 0)
					displayText = messageHistory [i] as string;
				else
					displayText = (messageHistory [i] as string) + "\n" + displayText;
			}
			
			guiTxt.text = displayText;
		}
	}
	
	public void OnDisable ()
	{
		if (guiObj != null)
			GameObject.DestroyImmediate (guiObj.gameObject);
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

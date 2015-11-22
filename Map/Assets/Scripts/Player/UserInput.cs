using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UserInput : MonoBehaviour
{

    private Player player;
	public GameObject SelectedArea;

    // Inicialitzem
    void Start()
    {
        player = GetComponentInParent<Player>();
		createSelectedArea ();
    }

    // Actualitzem a cada frame
    void Update()
    {
        if (player && player.human)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OpenPauseMenu();

            bool leftClick = Input.GetMouseButtonDown(0);
            bool rightClick = Input.GetMouseButtonDown(1);
            if (leftClick || rightClick)
                HandleMouseClick(leftClick, rightClick);

            MoveCamera();
            RotateCamera();
            MouseActivity();
        }
    }

    private void OpenPauseMenu()
    {
    }

	private void createSelectedArea()
	{
		SelectedArea = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		SelectedArea.transform.position = new Vector3(0, 0, 0);
		SelectedArea.transform.localScale = new Vector3(0, 0, 0);
		SelectedArea.name = "SelectedArea";
		Destroy (SelectedArea.GetComponent<Collider> ());
	}

    private void HandleMouseClick(bool leftClick, bool rightClick)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // Click on non-UI element
        {
            RTSObject targetRtsElement = null;

            // Cast a ray in the direction clicked by the user to detect the currently clicked element,
            // and if it is a RTS element, then save its reference
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.collider.gameObject;
                targetRtsElement = objectHit.GetComponent<RTSObject>();
            }

			if (player.SelectedObject && player.SelectedObject.tag == "civil" && player.SelectedObject.GetComponent<CivilUnit>()
			    && player.SelectedObject.GetComponent<CivilUnit>().building == true && player.SelectedObject.GetComponent<CivilUnit>().constructionPoint == Vector3.zero)
			{
				player.SelectedObject.GetComponent<CivilUnit>().constructionPoint = hit.point;
			}
			else if (leftClick)
            {
				object[] obj = GameObject.FindSceneObjectsOfType(typeof (RTSObject));
				foreach (object o in obj)
				{
					RTSObject g = (RTSObject) o;
					g.SetSelection(false);
				}
                // Call the class to change the currently selected RTS element.
                // Note that this will be null if the user clicked on nothing or a non-RTS element object.
                player.ChangeSelectedRtsObject(targetRtsElement);
				if(targetRtsElement && targetRtsElement.IsOwnedBy(player))
				{
					targetRtsElement.SetSelection(true);
					SelectedArea.GetComponent<MeshRenderer>().enabled = true;
				}
				else if (player.SelectedObject != null && player.SelectedObject.IsOwnedBy(player))
				{
					player.SelectedObject.SetSelection(true);
					SelectedArea.GetComponent<MeshRenderer>().enabled = true;
				}
				else if (SelectedArea)
				{
					SelectedArea.GetComponent<MeshRenderer>().enabled = false;
				}
            }
            else if (rightClick && player.SelectedObject != null && player.SelectedObject.IsOwnedBy(player))
            {
                if (player.SelectedObject.GetType().Equals("CivilUnit"))
                {
                    player.SelectedObject.GetComponent<CivilUnit>().harvesting = false;
                    if (player.SelectedObject.CanBuild())
                    {
                        player.SelectedObject.GetComponent<CivilUnit>().building = false;
                    }
                }
				//Atacar
                if (player.SelectedObject.CanAttack() &&
                    targetRtsElement != null && targetRtsElement.owner != null &&
                    targetRtsElement.CanBeAttacked() &&
                    player.Team.IsEnemyOf(targetRtsElement.owner.Team))
                {
                    // If the player clicked over an unit or building and the selected unit
                    // can attack, start the attacking sequence
                    player.SelectedObject.AttackObject(targetRtsElement);
                }
				//Construir un edificio
				else if (player.SelectedObject.CanBuild()&& targetRtsElement != null && targetRtsElement.owner==player && targetRtsElement.CanBeBuilt())
				{
					player.SelectedObject.MoveTo(hit.point);
					player.SelectedObject.GetComponent<CivilUnit>().building=true;
					player.SelectedObject.GetComponent<CivilUnit>().currentProject=targetRtsElement.GetComponent<Building>();
					targetRtsElement.GetComponent<Building>().needsBuilding=true;
				}
                
                //Recolecto
				else if (player.SelectedObject.tag == "civil" && targetRtsElement != null && targetRtsElement.GetComponent<Resource>()) //tag == "wood"
				{
                    //player.SelectedObject.MoveTo(hit.point);
                    player.SelectedObject.GetComponent<CivilUnit>().StartHarvest(targetRtsElement.GetComponent<Resource>());//, Building store)
                    //player.SelectedObject.GetComponent<CivilUnit>().harvesting=true; //el civilunit es recolector
                    //player.SelectedObject.GetComponent<CivilUnit>().resourceDeposit = targetRtsElement.GetComponent<Resource>(); //este es tu recurso
                    //player.SelectedObject.GetComponent<CivilUnit>().harvestType = targetRtsElement.GetComponent<Resource>().GetResourceType();
                    //player.SelectedObject.GetComponent<CivilUnit>().state = 2;
                }
				
                else if (player.SelectedObject.CanMove())
                {
                    // Otherwise, if the unit can move, start the movement sequence
                    player.SelectedObject.MoveTo(hit.point);
                }
            }
        }
    }

    private void MoveCamera()
    {
    }

    private void RotateCamera()
    {
    }

    private void MouseActivity()
    {
    }
}

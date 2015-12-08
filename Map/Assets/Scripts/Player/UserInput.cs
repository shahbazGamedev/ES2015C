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
			bool middleClick = Input.GetMouseButtonDown(2);
            if (leftClick || rightClick || middleClick)
                HandleMouseClick(leftClick, rightClick, middleClick);

            if (Input.GetKey (KeyCode.M)) morirEnemigos(); 
            if (Input.GetKey (KeyCode.P)) morirJugador();
			if (Input.GetKeyUp("k")) demolishBuildings();

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
		SelectedArea.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		SelectedArea.GetComponent<MeshRenderer>().material = Resources.Load("Materials/selectedArea") as Material;
		SelectedArea.name = "SelectedArea";
		Destroy (SelectedArea.GetComponent<Collider> ());
	}

    private void HandleMouseClick(bool leftClick, bool rightClick, bool middleClick)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // Click on non-UI element
        {
            RaycastHit hit = FindMouseTargetHit();
            RTSObject targetRtsElement = (hit.collider != null)
                ? hit.collider.gameObject.GetComponent<RTSObject>() : null;

            if (player.SelectedObject && player.SelectedObject.GetComponent<CivilUnit>()
			    && player.SelectedObject.GetComponent<CivilUnit>().waitingForBuildingLocationSelection)
			{
                player.SelectedObject.GetComponent<CivilUnit>().SetBuildingLocation();
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
            else if (rightClick && player.SelectedObject != null && player.SelectedObject.IsOwnedBy(player) && Input.GetKey(KeyCode.RightControl)==false && Input.GetKey(KeyCode.LeftControl)==false)
            {
				if (player.SelectedObject.GetComponent<CivilUnit>() && player.SelectedObject.GetComponent<CivilUnit>().harvesting)
                {
                    player.SelectedObject.GetComponent<CivilUnit>().harvesting = false;
                }

                // If there is any building project for this object, deassign it before assigning other actions
                if (player.SelectedObject.IsBuilding())
                {
                    player.SelectedObject.AssignBuildingProject(null);
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
                    player.SelectedObject.AssignBuildingProject(targetRtsElement.GetComponent<Building>());
				}
                //Recolecto
				else if (player.SelectedObject.tag == "civil" && targetRtsElement != null && targetRtsElement.GetComponent<Resource>())
				{
                    //player.SelectedObject.MoveTo(hit.point);
                    player.SelectedObject.GetComponent<CivilUnit>().StartHarvest(targetRtsElement.GetComponent<Resource>(),false,null);//, Building store)
                    //player.SelectedObject.GetComponent<CivilUnit>().harvesting=true; //el civilunit es recolector
                    //player.SelectedObject.GetComponent<CivilUnit>().resourceDeposit = targetRtsElement.GetComponent<Resource>(); //este es tu recurso
                    //player.SelectedObject.GetComponent<CivilUnit>().harvestType = targetRtsElement.GetComponent<Resource>().GetResourceType();
                    //player.SelectedObject.GetComponent<CivilUnit>().state = 2;
                }
                else if (player.SelectedObject.CanMove())
                {
                    // Otherwise, if the unit can move, start the movement sequence
                    player.SelectedObject.MoveTo(hit.point, false);
                }
            }
			else if (middleClick && player.SelectedObject != null && player.SelectedObject.IsOwnedBy(player))
			{
				if (player.SelectedObject.CanMove())
                {
                    player.SelectedObject.MoveTo(hit.point, true);
                }
			}
        }
    }

    private RaycastHit FindMouseTargetHit()
    {
        RTSObject targetRtsElement = null;

        // Cast a ray in the direction clicked by the user to detect the currently clicked element,
        // and if it is a RTS element, then save its reference
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        // First, cast a ray without considering the currently selected element
        // (add it temporally to the "ignore raycast" layer), so if the user clicked
        // on a unit that is occluded by the current element, it will be selected instead
        int origSelectedObjectLayer = 0;
        if (player.SelectedObject != null)
        {
            origSelectedObjectLayer = player.SelectedObject.gameObject.layer;
            player.SelectedObject.gameObject.layer = 2; // IgnoreRaycast layer
        }

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            targetRtsElement = objectHit.GetComponent<RTSObject>();
        }

        if (player.SelectedObject != null)
        {
            player.SelectedObject.gameObject.layer = origSelectedObjectLayer; // Restore original layer
        }

        // If we didn't find a collision, repeat the raycast in order to test if the user
        // clicked again on the unit he had already selected
        if (targetRtsElement == null && Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            targetRtsElement = objectHit.GetComponent<RTSObject>();
        }

        return hit;
    }

    //Funcion para que todos los objectos del jugador mueran
    private void morirJugador(){
        GameObject[] games;
        games = GameObject.FindGameObjectsWithTag("civil");
        foreach(var civil in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(civil.GetComponent<CivilUnit>().owner==player){
                Destroy(civil, 2);
            }
        }
        games = GameObject.FindGameObjectsWithTag("townCenter");
        foreach(var town in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(town.GetComponent<TownCenterBuilding>().owner==player){
                Destroy(town, 2);
            }
        }
        games = GameObject.FindGameObjectsWithTag("mility");
        foreach(var mili in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(mili.GetComponent<Unit>().owner==player){
                Destroy(mili, 2);
            }
        }
        games = GameObject.FindGameObjectsWithTag("armyBuilding");
        foreach(var army in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(army.GetComponent<ArmyBuilding>().owner==player){
                Destroy(army, 2);
            }
        }
    }

    //Funcion para que todos los objectos del enemigo mueran
    private void morirEnemigos(){
        GameObject[] games;
        games = GameObject.FindGameObjectsWithTag("civil");
        foreach(var civil in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(civil.GetComponent<CivilUnit>().owner!=player){
                Destroy(civil, 2);
            }
        }
        games = GameObject.FindGameObjectsWithTag("townCenter");
        foreach(var town in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(town.GetComponent<TownCenterBuilding>().owner!=player){
                Destroy(town, 2);
            }
        }
        games = GameObject.FindGameObjectsWithTag("mility");
        foreach(var mili in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(mili.GetComponent<Unit>().owner!=player){
                Destroy(mili, 2);
            }
        }
        games = GameObject.FindGameObjectsWithTag("armyBuilding");
        foreach(var army in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            if(army.GetComponent<ArmyBuilding>().owner!=player){
                Destroy(army, 2);
            }
        }
    }
	
	private void demolishBuildings() {
		
		Building[] buildings = FindObjectsOfType(typeof(Building)) as Building[];
         
        foreach(Building b in buildings)
        {
			if (b.demolishedModel!=null) {
				b.changeModel("demolished");
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

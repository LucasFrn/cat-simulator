using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;

    [SerializeField] private HouseTutorial tutorial;

    [Header("Grid")]
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;
    [SerializeField] private TileBase eraseTile;

    [SerializeField] private LayerMask layer;

    private PlacebleObject objectToPlace;
    private PlacebleObject selectObject;

    [Header("UI")]
    [SerializeField] private GameObject editCanvas;
    [SerializeField] private GameObject buildingCanvas;
    [SerializeField] private GameObject buttonConfirmBuilding;
    [SerializeField] private Text moneyTxt;

    [SerializeField] private GameObject arrow;

    

    bool _IsBuilding = false;

    private void Awake()
    {   
        instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Start()
    {
        moneyTxt.text = GameManager.Instance.petiscos.ToString();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ExitMap();
        }


        if (!objectToPlace)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(objectToPlace) && objectToPlace != null) 
            {         
                LocateObject(objectToPlace);
            }
            else
            {
                Destroy(objectToPlace.gameObject);
            }

            buildingCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!objectToPlace.Placed)
            {
                buildingCanvas.SetActive(true);
                Destroy(objectToPlace.gameObject);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_IsBuilding)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, layer))  
            {
                selectObject = raycastHit.collider.gameObject.GetComponent<PlacebleObject>();

                if (selectObject.Placed)
                {
                    editCanvas.SetActive(true);
                    Vector3 v = selectObject.transform.position;
                    arrow.SetActive(true);
                    arrow.transform.position = new Vector3(v.x, 6f, v.z);
                }
                    
                else
                    selectObject = null;
            }
        }
    }

    public void ConfirmBuilding()
    {
        if (CanBePlaced(objectToPlace) && objectToPlace != null)
        {
            LocateObject(objectToPlace);
        }
        else
        {
            Destroy(objectToPlace.gameObject);
        }

        buildingCanvas.SetActive(true);
    }

    private void LocateObject(PlacebleObject placebleObject)
    {
        if (tutorial)
        {
            tutorial.NextStep();
        }

        buttonConfirmBuilding.SetActive(false);
        placebleObject.StartGameObject();
        placebleObject.Place();
        Vector3Int start = gridLayout.WorldToCell(placebleObject.GetStartPosition());
        TakeArea(start, placebleObject.Size, whiteTile);

        _IsBuilding = false;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTileBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private bool CanBePlaced(PlacebleObject placebleObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placebleObject.Size;

        TileBase[] baseArray = GetTileBlock(area, MainTilemap);

        foreach(var b in baseArray)
        {
            if (b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start,Vector3Int size,TileBase tile)
    {      
        MainTilemap.BoxFill(start, tile, start.x, start.y, start.x + size.x, start.y + size.y);       
    }

    public void InitializeWithObject(StoreItens item, Vector3 vec, Vector3 rotation, bool canDrag)
    {
        if (tutorial)
        {
            tutorial.NextStep();
        }


        if (GameManager.Instance.petiscos < item._cost)
            return;

        GameManager.Instance.petiscos -= item._cost;

        moneyTxt.text = GameManager.Instance.petiscos.ToString();

        editCanvas.SetActive(false);
        buildingCanvas.SetActive(false);

        Vector3 position = SnapCordinateToGrid(vec);

        GameObject obj = Instantiate(item.prefabItem, position, Quaternion.Euler(rotation));
        objectToPlace = obj.GetComponent<PlacebleObject>();

        objectToPlace._name = item.name;

        if (canDrag)
        {
            obj.AddComponent<ObjectDrag>();
            buttonConfirmBuilding.SetActive(true);
            _IsBuilding = true;
            
        }
        else
        {
            LocateObject(objectToPlace);
            buildingCanvas.SetActive(true);
        }
            
    }

    public void RotateObject()
    {
        if (selectObject == null)
            return;

        selectObject.Rotate();
    }

    public void RemoveObject()
    {
        if (selectObject == null)
            return;

        Vector3Int start = gridLayout.WorldToCell(selectObject.GetStartPosition());
        TakeArea(start, selectObject.Size, eraseTile);
        Destroy(selectObject.gameObject);
        editCanvas.SetActive(false);
        arrow.SetActive(false);
        selectObject = null;

        GameManager.Instance.petiscos += 10;

        moneyTxt.text = GameManager.Instance.petiscos.ToString();
    }

    public void ExitMap()
    {
        GameManager.Instance.janelaEmFoco=1;
        SceneManager.LoadScene(1);       
    }

    public void Clear()
    {
        arrow.SetActive(false);
        MainTilemap.ClearAllTiles();
    }
}

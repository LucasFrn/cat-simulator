using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;

    [SerializeField] private LayerMask layer;

    private PlacebleObject objectToPlace;
    private PlacebleObject selectObject;

    [SerializeField] private GameObject editCanvas;
    [SerializeField] private GameObject buildingCanvas;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(0);
        }
 

        if (!objectToPlace)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(objectToPlace) && objectToPlace != null) 
            {
                objectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, layer)) 
            {
                selectObject = raycastHit.collider.gameObject.GetComponent<PlacebleObject>();

                if (selectObject.Placed)
                    editCanvas.SetActive(true);
                else
                    selectObject = null;
            }
        }

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

    public void TakeArea(Vector3Int start,Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
    }

    public void InitializeWithObject(GameObject prefab)
    {
        editCanvas.SetActive(false);
        buildingCanvas.SetActive(false);

        Vector3 position = SnapCordinateToGrid(new Vector3(0, 1, 0));

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlacebleObject>();
        obj.AddComponent<ObjectDrag>();
    }

    public void RotateObject()
    {
        selectObject.Rotate();
    }

    public void RemoveObject()
    {
        Destroy(selectObject.gameObject);
        editCanvas.SetActive(false);
    }
}

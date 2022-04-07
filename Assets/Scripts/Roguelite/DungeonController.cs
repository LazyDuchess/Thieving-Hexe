using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCheckResult
{
    public Vector3 attachPosition;
}


public class DungeonPiece
{
    public const float size = 5;
    public GameObject prefab;
    public Vector3 positionStart;
    public Vector3 positionEnd;
    public int parentDoor = -1;
    public int myParentDoor = -1;
    public List<DungeonPiece> children = new List<DungeonPiece>();
    public DungeonLevel level;

    public DoorComponent[] getDoors()
    {
        return prefab.GetComponentsInChildren<DoorComponent>();
    }

    public DungeonPiece AttachPiece(GameObject piecePrefab, DoorComponent myDoor, DoorComponent toAttachDoor)
    {
        var attachCheck = CheckCanAttach(piecePrefab, myDoor, toAttachDoor);
        var newPiece = level.PlacePiece(piecePrefab, attachCheck.attachPosition);
        children.Add(newPiece);
        newPiece.parentDoor = myDoor.id;
        newPiece.myParentDoor = toAttachDoor.id;
        return newPiece;
    }

    public bool DoorBusy(DoorComponent door)
    {
        var doorID = door.id;
        if (myParentDoor == doorID)
            return true;
        foreach(var element in children)
        {
            if (element.parentDoor == doorID)
                return true;
        }
        return false;
    }

    public AttachCheckResult CheckCanAttach(GameObject piecePrefab, DoorComponent myDoor, DoorComponent toAttachDoor)
    {
        if (myDoor.facing == DoorFace.Backward && toAttachDoor.facing != DoorFace.Forward)
            return null;
        if (myDoor.facing == DoorFace.Forward && toAttachDoor.facing != DoorFace.Backward)
            return null;
        if (myDoor.facing == DoorFace.Left && toAttachDoor.facing != DoorFace.Right)
            return null;
        if (myDoor.facing == DoorFace.Right && toAttachDoor.facing != DoorFace.Left)
            return null;
        var absolutePosition = this.positionStart + myDoor.transform.localPosition - toAttachDoor.transform.localPosition;
        var pieceComponent = piecePrefab.GetComponent<RoomComponent>();
        switch(myDoor.facing)
        {
            case DoorFace.Forward:
                absolutePosition += Vector3.forward;
                break;
            case DoorFace.Backward:
                absolutePosition -= Vector3.forward;
                break;
            case DoorFace.Right:
                absolutePosition += Vector3.right;
                break;
            case DoorFace.Left:
                absolutePosition -= Vector3.right;
                /*
                absolutePosition -= Vector3.right * pieceComponent.xSize * size;
                absolutePosition -= Vector3.right * 0.5f;*/
                break;
        }
        if (!level.PlaceEmpty(absolutePosition, absolutePosition + new Vector3(pieceComponent.xSize * DungeonPiece.size, 0f, pieceComponent.ySize * DungeonPiece.size)))
            return null;
        var result = new AttachCheckResult();
        result.attachPosition = absolutePosition;
        return result;
    }

    public List<GameObject> instantiate(GameObject previous, Transform parent)
    {
        var placedList = new List<GameObject>();
        var placedPrefab = GameObject.Instantiate(prefab, positionStart, Quaternion.identity);
        if (parent != null)
            placedPrefab.transform.parent = parent;
        var placedRoom = placedPrefab.GetComponent<RoomComponent>();
        placedRoom.parentPiece = this;
        placedList.Add(placedPrefab);
        if (myParentDoor != -1)
        {
            var myDoors = placedPrefab.GetComponentsInChildren<DoorComponent>();
            foreach(var element in myDoors)
            {
                if (element.id == myParentDoor)
                {
                    element.empty = false;
                    element.OpenDoor();
                }
            }
        }
        if (previous)
        {
            var doors = previous.GetComponentsInChildren<DoorComponent>();
            foreach(var element in doors)
            {
                if (element.id == parentDoor)
                {
                    element.empty = false;
                    element.OpenDoor();
                }
            }
        }
        foreach(var element in children)
        {
            var newList = element.instantiate(placedPrefab, parent);
            placedList.AddRange(newList);
        }
        return placedList;
    }
}
public class DungeonLevel
{
    public List<DungeonPiece> pieces = new List<DungeonPiece>();

    public DungeonPiece GetPieceAtPosition(Vector3 position)
    {
        foreach (var element in pieces)
        {
            var offset = 0.5f;
            var posStart = element.positionStart + new Vector3(offset,0f,offset);
            var posEnd = element.positionEnd - new Vector3(offset,0f,offset);
            var size = posEnd - posStart;
            var center = posStart + (size / 2f);
            var elementBounds = new Bounds(center, size);
            if (elementBounds.Contains(position))
                return element;
        }
        return null;
    }

    public bool PlaceEmpty(Vector3 positionStart, Vector3 positionEnd)
    {
        var size = positionEnd - positionStart;
        var center = positionStart + (size / 2f);

        var bounds1 = new Bounds(center, size);
        foreach(var element in pieces)
        {
            size = element.positionEnd - element.positionStart;
            center = element.positionStart + (size / 2f);
            var elementBounds = new Bounds(center, size);
            if (bounds1.Intersects(elementBounds))
                return false;
            /*
            if (positionStart.x >= element.positionStart.x && positionStart.y >= element.positionStart.y && positionStart.z >= element.positionStart.z)
            {
                if (positionStart.x <= element.positionEnd.x && positionStart.y <= element.positionEnd.y && positionStart.z <= element.positionEnd.z)
                    return false;
            }
            if (positionEnd.x >= element.positionStart.x && positionEnd.y >= element.positionStart.y && positionEnd.z >= element.positionStart.z)
            {
                if (positionEnd.x <= element.positionEnd.x && positionEnd.y <= element.positionEnd.y && positionEnd.z <= element.positionEnd.z)
                    return false;
            }
            if (element.positionStart.x >= positionStart.x && element.positionStart.y >= positionStart.y && element.positionStart.z >= positionStart.z)
            {
                if (element.positionStart.x <= positionEnd.x && element.positionStart.y <= positionEnd.y && element.positionStart.z <= positionEnd.z)
                    return false;
            }
            if (element.positionEnd.x >= positionStart.x && element.positionEnd.y >= positionStart.y && element.positionEnd.z >= positionStart.z)
            {
                if (element.positionEnd.x <= positionEnd.x && element.positionEnd.y <= positionEnd.y && element.positionEnd.z <= positionEnd.z)
                    return false;
            }*/
        }
        return true;
    }

    public GameObject Instantiate()
    {
        var objectParent = new GameObject("Level");
        pieces[0].instantiate(null, objectParent.transform);
        return objectParent;
    }

    public DungeonPiece PlacePiece(GameObject prefab, Vector3 position)
    {
        var piece = new DungeonPiece();
        var pieceController = prefab.GetComponent<RoomComponent>();
        piece.prefab = prefab;
        piece.positionStart = position;
        piece.positionEnd = position + (new Vector3(pieceController.xSize * DungeonPiece.size, 4f, pieceController.ySize * DungeonPiece.size));
        piece.level = this;
        pieces.Add(piece);
        return piece;
    }
}

[System.Serializable]
public class DungeonDatabase
{
    public List<GameObject> spawnRooms;
    public List<GameObject> enemyRooms;
}

public class PotentialDoor
{
    public AttachCheckResult checkResult;
    public DoorComponent doorComponent;
}

public class DungeonState
{
    public Dictionary<DungeonPiece, bool> alreadyVisited = new Dictionary<DungeonPiece, bool>();
    public RoomComponent[] allRooms;
}

public class DungeonController : MonoBehaviour
{
    public DungeonDatabase database;
    public GameObject level;
    public DungeonLevel dungeonLevel;
    public DungeonState dungeonState;

    

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    public bool AnyAliveEnemies()
    {
        var chars = FindObjectsOfType<CharacterComponent>();
        foreach(var element in chars)
        {
            if (element.GetTeam() != GameController.instance.playerController.GetTeam() && element.IsAlive())
                return true;
        }
        return false;
    }

    private void Update()
    {
        if (dungeonLevel == null)
            return;
        if (!GameController.instance.player)
            return;
        var currentPiece = dungeonLevel.GetPieceAtPosition(GameController.instance.player.transform.position);
        if (currentPiece != null)
        {
            
            foreach(var element in dungeonState.allRooms)
            {
                if (element.parentPiece == currentPiece)
                {
                    if (AnyAliveEnemies())
                    {
                        element.CloseDoors();
                    }
                    else
                    {
                        element.OpenDoors();
                    }
                    if (!dungeonState.alreadyVisited.ContainsKey(currentPiece))
                    {
                        element.EnterRoom();
                        dungeonState.alreadyVisited[currentPiece] = true;
                    }
                    break;
                }
            }
                
        }
    }

    public DungeonLevel GenerateLevel()
    {
        int linearLevelSize = 8;
        var level = new DungeonLevel();
        var spawnPrefabIndex = Random.Range(0, database.spawnRooms.Count);
        var spawnPrefab = database.spawnRooms[spawnPrefabIndex];
        var spawnPiece = level.PlacePiece(spawnPrefab, Vector3.zero);

        var spawnDoors = spawnPiece.getDoors();

        /*
        var nextPiece = database.enemyRooms[0];
        var nextDoors = nextPiece.GetComponentsInChildren<DoorComponent>();
        DoorComponent theDoor = null;
        foreach(var element in nextDoors)
        {
            if (element.id == 3)
                theDoor = element;
        }
        spawnPiece.AttachPiece(nextPiece, spawnDoors[0], theDoor);*/

        var currentPiece = spawnPiece;

        for (var i=0;i<linearLevelSize;i++)
        {
            var currentDoors = currentPiece.getDoors();
            var potentialDoors = new List<DoorComponent>();
            foreach(var element in currentDoors)
            {
                if (!currentPiece.DoorBusy(element))
                {
                    potentialDoors.Add(element);
                }
            }
            var randomDoorIndex = Random.Range(0, potentialDoors.Count);
            var randomDoorComponent = potentialDoors[randomDoorIndex];
            var potentialNextPieces = new List<GameObject>();
            foreach(var element in database.enemyRooms)
            {
                var potentialNextDoors = element.GetComponentsInChildren<DoorComponent>();
                foreach(var pDoor in potentialNextDoors)
                {
                    var res = currentPiece.CheckCanAttach(element, randomDoorComponent, pDoor);
                    if (res != null)
                    {
                        potentialNextPieces.Add(element);
                        /*
                        var potentDoor = new PotentialDoor();
                        potentDoor.checkResult = res;
                        potentDoor.doorComponent = pDoor;
                        finalPotentialNextDoors.Add(potentDoor);*/
                    }
                }
            }
            if (potentialNextPieces.Count > 0)
            {
                var nextPiece = potentialNextPieces[Random.Range(0, potentialNextPieces.Count)];
                var potentialNextDoors = nextPiece.GetComponentsInChildren<DoorComponent>();
                var finalPotentialNextDoors = new List<PotentialDoor>();
                foreach (var pDoor in potentialNextDoors)
                {
                    var res = currentPiece.CheckCanAttach(nextPiece, randomDoorComponent, pDoor);
                    if (res != null)
                    {
                        var potentDoor = new PotentialDoor();
                        potentDoor.checkResult = res;
                        potentDoor.doorComponent = pDoor;
                        finalPotentialNextDoors.Add(potentDoor);
                    }
                }
                var finalPotentDoor = finalPotentialNextDoors[Random.Range(0, finalPotentialNextDoors.Count)];
                currentPiece = currentPiece.AttachPiece(nextPiece, randomDoorComponent, finalPotentDoor.doorComponent);
            }
        }
        dungeonLevel = level;
        this.level = level.Instantiate();
        var spawns = this.level.GetComponentsInChildren<PlayerSpawn>();
        GameController.instance.player.transform.position = spawns[0].transform.position;
        dungeonState = new DungeonState();
        dungeonState.allRooms = this.level.GetComponentsInChildren<RoomComponent>();
        return level;
    }
    /*
    public void GenerateLevel()
    {
        var spawnPrefabIndex = Random.Range(0, database.spawnRooms.Count);
        var spawnPrefab = database.spawnRooms[spawnPrefabIndex];
        var spawnInstance = Instantiate(spawnPrefab);
        var spawnController = spawnInstance.GetComponent<RoomComponent>();
        if (GameController.instance.player)
        {
            var playerSpawn = spawnController.getPlayerSpawn();
            GameController.instance.player.transform.position = playerSpawn.transform.position;
        }
    }*/
}

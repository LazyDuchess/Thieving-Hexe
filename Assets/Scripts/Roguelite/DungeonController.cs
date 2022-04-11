using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachCheckResult
{
    public Vector3 attachPosition;
}

public class GameGlobals
{
    public int survivedDungeons = 0;
    public int currentDungeon = 0;
    public int killedEnemies = 0;
    public int spawnedEnemies = 0;
    public float timeLeft = 0;
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
        if (!level.PlaceEmpty(absolutePosition, absolutePosition + new Vector3(pieceComponent.xSize * DungeonPiece.size, 4f, pieceComponent.ySize * DungeonPiece.size)))
            return null;
        var result = new AttachCheckResult();
        result.attachPosition = absolutePosition;
        return result;
    }

    public List<GameObject> instantiate(GameObject previous, Transform parent, bool instant = true)
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
                    element.OpenDoor(instant);
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
                    element.OpenDoor(instant);
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
    public float wayOutTime = 160f;
    public List<DungeonPiece> pieces = new List<DungeonPiece>();

    public DungeonPiece GetPieceAtPosition(Vector3 position)
    {
        foreach (var element in pieces)
        {
            var offset = 1.0f;
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

    public void DrawBox(Vector3 pos, Quaternion rot, Vector3 scale, Color c)
    {
        // create matrix
        Matrix4x4 m = new Matrix4x4();
        m.SetTRS(pos, rot, scale);

        var point1 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, 0.5f));
        var point2 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, 0.5f));
        var point3 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, -0.5f));
        var point4 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, -0.5f));

        var point5 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, 0.5f));
        var point6 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, 0.5f));
        var point7 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, -0.5f));
        var point8 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, -0.5f));

        Debug.DrawLine(point1, point2, c, 10f);
        Debug.DrawLine(point2, point3, c, 10f);
        Debug.DrawLine(point3, point4, c, 10f);
        Debug.DrawLine(point4, point1, c, 10f);

        Debug.DrawLine(point5, point6, c, 10f);
        Debug.DrawLine(point6, point7, c, 10f);
        Debug.DrawLine(point7, point8, c, 10f);
        Debug.DrawLine(point8, point5, c, 10f);

        Debug.DrawLine(point1, point5, c, 10f);
        Debug.DrawLine(point2, point6, c, 10f);
        Debug.DrawLine(point3, point7, c, 10f);
        Debug.DrawLine(point4, point8, c, 10f);

    }

    public bool PlaceEmpty(Vector3 positionStart, Vector3 positionEnd)
    {
        //kind of a hack, but i ain't too worried.
        var offset = 0.5f;
        positionStart = positionStart + new Vector3(offset, 0f, offset);
        positionEnd = positionEnd - new Vector3(offset, 0f, offset);
        var size = positionEnd - positionStart;
        var center = positionStart + (size / 2f);

        var bounds1 = new Bounds(center, size);
        foreach(var element in pieces)
        {
            size = element.positionEnd - element.positionStart;
            center = element.positionStart + (size / 2f);
            var elementBounds = new Bounds(center, size);
            if (bounds1.Intersects(elementBounds))
            {
                DrawBox(bounds1.center, Quaternion.identity, bounds1.size, Color.red);
                return false;
            }
        }
        DrawBox(bounds1.center, Quaternion.identity, bounds1.size, Color.green);
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
    public List<GameObject> connectorRooms;
    public List<GameObject> lootRooms;
    public List<GameObject> endRooms;
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
    public bool wayOut = false;
    public float timeLeft;
    public int killedEnemies = 0;
    public int spawnedEnemies = 0;
    public bool done = false;
    public float totalTime = 0f;
}

public class PotentialPiece
{
    public DoorComponent myDoorID;
    public DoorComponent otherDoorID;
    public GameObject prefab;
}

public class DungeonGeneratorSettings
{
    public int dungeonLength = 6;
    public int recurseChance = 9;
    public int doorRecurseChance = 6;
    public float wayOutTime = 160f;
}

public class DungeonController : MonoBehaviour
{
    public DungeonDatabase database;
    public GameObject level;
    public DungeonLevel dungeonLevel;
    public DungeonState dungeonState;
    public static DungeonController instance;
    public bool lastOutdoor = true;
    public bool inDangerPrev = false;


    private void Awake()
    {
        instance = this;
    }

    public void SetWayOut()
    {
        dungeonState.wayOut = true;
        dungeonState.alreadyVisited.Clear();
        dungeonState.timeLeft = dungeonLevel.wayOutTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(GenerateSettingsByLevel(0));
    }

    public void RegenerateLevel()
    {
        CleanUpAll();
        GenerateLevel(GenerateSettingsByLevel(GameController.instance.gameGlobals.currentDungeon));
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

    public float GetTimeLeft()
    {
        return dungeonState.timeLeft;
    }

    public float GetTimeElapsed()
    {
        return dungeonState.totalTime;
    }

    private void Update()
    {
        if (dungeonLevel == null)
            return;
        if (!GameController.instance.player)
            return;
        if (!dungeonState.done)
        {
            dungeonState.totalTime += Time.deltaTime;
        }
        if (dungeonState.wayOut && !dungeonState.done)
        {
            dungeonState.timeLeft -= Time.deltaTime;
            if (dungeonState.timeLeft <= 0f)
                GameController.instance.GameOver();
        }
        var currentPiece = dungeonLevel.GetPieceAtPosition(GameController.instance.player.transform.position);
        if (currentPiece != null)
        {
            var outdoo = currentPiece.prefab.GetComponent<RoomComponent>().outdoor;
            if (lastOutdoor != outdoo)
            {
                lastOutdoor = outdoo;
                if (lastOutdoor)
                    GameEventsController.EnterOutdoorArea();
                else
                    GameEventsController.EnterIndoorArea();
            }
            foreach(var element in dungeonState.allRooms)
            {
                if (element.parentPiece == currentPiece)
                {
                    if (AnyAliveEnemies() && !dungeonState.wayOut)
                    {
                        if (inDangerPrev == false)
                        {
                            inDangerPrev = true;
                            GameEventsController.RoomsClose();
                        }
                        element.CloseDoors();
                    }
                    else
                    {
                        if (inDangerPrev == true)
                        {
                            inDangerPrev = false;
                            GameEventsController.RoomsOpen();
                        }
                        element.OpenDoors();
                    }
                    if (!dungeonState.alreadyVisited.ContainsKey(currentPiece))
                    {
                        if (!dungeonState.wayOut)
                            element.EnterRoom();
                        else
                            element.EnterRoomWayOut();
                        dungeonState.alreadyVisited[currentPiece] = true;
                    }
                    break;
                }
            }
                
        }
    }

    //Dead ends, loot rooms.
    public DungeonPiece Recurse(DungeonPiece piece, DoorComponent onDoor = null)
    {
        var recursionSize = Random.Range(1, 4);
        var endInLootRoom = false;
        var endInLotRoomChance = Random.Range(0, 2);
        if (endInLotRoomChance == 0)
            endInLootRoom = true;
        var currentPiece = piece;
        DungeonPiece firstPiece = null;
        for(var i=0;i<recursionSize;i++)
        {
            var pieceDB = database.connectorRooms;
            if (i == recursionSize - 1 && endInLootRoom)
                pieceDB = database.lootRooms;
            var myDoors = currentPiece.getDoors();
            if (i == 0 && onDoor != null)
            {
                myDoors = new DoorComponent[] { onDoor };
            }
            var potentials = new List<PotentialPiece>();
            foreach(var myDoor in myDoors)
            {
                foreach (var newPiece in pieceDB)
                {
                    var otherDoors = newPiece.GetComponentsInChildren<DoorComponent>();
                    foreach(var otherDoor in otherDoors)
                    {
                        var attachCheck = currentPiece.CheckCanAttach(newPiece, myDoor, otherDoor);
                        if (attachCheck != null)
                        {
                            var result = new PotentialPiece();
                            result.myDoorID = myDoor;
                            result.otherDoorID = otherDoor;
                            result.prefab = newPiece;
                            potentials.Add(result);
                        }
                    }
                }
            }
            if (potentials.Count == 0)
            {
                if (i == 0)
                    return null;
                else
                    return firstPiece;
            }
            else
            {
                var potentialIndex = Random.Range(0, potentials.Count);
                var newPiecePotential = potentials[potentialIndex];
                var newPieceFinal = currentPiece.AttachPiece(newPiecePotential.prefab, newPiecePotential.myDoorID, newPiecePotential.otherDoorID);
                if (i != 0)
                {
                    var recurseChance = Random.Range(0, 12);
                    if (recurseChance == 0)
                        Recurse(currentPiece);
                }
                currentPiece = newPieceFinal;
                if (i == 0)
                    firstPiece = currentPiece;
            }
        }
        return firstPiece;
    }

    public DungeonGeneratorSettings GenerateSettingsByLevel(int level)
    {
        var sets = new DungeonGeneratorSettings();
        var lenMin = (int)Mathf.Ceil(level * 0.5f) + 3;
        var lenMax = (int)Mathf.Ceil(level * 0.3f);
        sets.dungeonLength = Random.Range(lenMin, lenMin + lenMax);
        sets.recurseChance = 7;
        if (level >= 6)
            sets.recurseChance = 4;
        if (level >= 10)
            sets.recurseChance = 3;
        sets.doorRecurseChance = 6;

        if (level >= 6)
            sets.doorRecurseChance = 3;

        if (level >= 12)
            sets.doorRecurseChance = 2;

        sets.wayOutTime = 75f + (level * 40f);
        return sets;
    }

    public void CleanUpAll()
    {
        Destroy(level);
        var enemys = FindObjectsOfType<CharacterController>();
        foreach(var element in enemys)
        {
            if (element != GameController.instance.playerController)
                Destroy(element);
        }
        var pickups = FindObjectsOfType<ItemComponent>();
        foreach(var element in pickups)
        {
            if (element.owner == null)
                Destroy(element);
        }
    }

    public DungeonLevel GenerateLevel(DungeonGeneratorSettings settings)
    {
        var linearLevelSize = settings.dungeonLength;

        /*
        int linearLevelSize = Random.Range(6,9);*/
        var connectorRooms = Random.Range(3, (int)(linearLevelSize * 0.7));

        var availableConnectorRooms = new List<int>();
        for(var n=0;n<linearLevelSize;n++)
        {
            availableConnectorRooms.Add(n);
        }

        var toPlaceConnectors = new List<int>();

        for(var n=0;n<connectorRooms;n++)
        {
            var rand = Random.Range(0, availableConnectorRooms.Count);
            toPlaceConnectors.Add(availableConnectorRooms[rand]);
            availableConnectorRooms.RemoveAt(rand);
        }

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

        var recursivable = new List<DungeonPiece>();

        var i = 0;
        Debug.Log("Generating level of size " + linearLevelSize.ToString());
        while(i < linearLevelSize)
        {
            Debug.Log("Working on piece " + i.ToString());
            var currentDoors = currentPiece.getDoors();
            var potentialDoors = new List<DoorComponent>();
            foreach(var element in currentDoors)
            {
                if (!currentPiece.DoorBusy(element))
                {
                    potentialDoors.Add(element);
                }
            }
            //var randomDoorIndex = Random.Range(0, potentialDoors.Count);
            //var randomDoorComponent = potentialDoors[randomDoorIndex];
            var potentialNextPieces = new List<PotentialPiece>();
            var databaseNextPieces = database.enemyRooms;

            if (i == linearLevelSize-1)
            {
                databaseNextPieces = database.endRooms;
            }

            //Place a connector instead of enemy room?
            if (toPlaceConnectors.IndexOf(i) >= 0)
            {
                toPlaceConnectors.Remove(i);
                i = i - 1;
                databaseNextPieces = database.connectorRooms;
                Debug.Log("Placing a connector.");
            }
            foreach(var element in databaseNextPieces)
            {
                var potentialNextDoors = element.GetComponentsInChildren<DoorComponent>();
                foreach(var pDoor in potentialNextDoors)
                {
                    foreach (var myDoor in potentialDoors)
                    {
                        var res = currentPiece.CheckCanAttach(element, myDoor, pDoor);
                        if (res != null)
                        {
                            var potentialPiece = new PotentialPiece();
                            potentialPiece.myDoorID = myDoor;
                            potentialPiece.otherDoorID = pDoor;
                            potentialPiece.prefab = element;
                            potentialNextPieces.Add(potentialPiece);
                        }
                    }
                }
            }
            if (potentialNextPieces.Count > 0)
            {
                var nextPotentialPiece = potentialNextPieces[Random.Range(0, potentialNextPieces.Count)];
                var nextPiece = nextPotentialPiece.prefab;
                var randomDoorComponent = nextPotentialPiece.myDoorID;
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
                if (finalPotentialNextDoors.Count > 0)
                {
                    var finalPotentDoor = finalPotentialNextDoors[Random.Range(0, finalPotentialNextDoors.Count)];
                    var newCurrentPiece = currentPiece.AttachPiece(nextPiece, randomDoorComponent, finalPotentDoor.doorComponent);
                    /*
                    if (i != 0)
                    {
                        var doorz = currentPiece.getDoors().Length;
                        for (var n = 0; n < doorz; n++)
                        {
                            var recurseChance = Random.Range(0, 9);
                            if (recurseChance == 0)
                                Recurse(currentPiece);
                        }

                    }*/
                    recursivable.Add(newCurrentPiece);
                    currentPiece = newCurrentPiece;
                    Debug.Log("Placed piece " + i.ToString());
                }
                else
                {
                    Debug.Log("Couldn't place piece " + i.ToString());
                    i = i - 1;
                }
            }
            else
            {
                Debug.Log("Couldn't place piece " + i.ToString());
            }
            i = i + 1;
        }

        foreach(var element in recursivable)
        {
            var doorz = element.getDoors().Length;
            //var recurseChance = Random.Range(0, 9);
            var recurseChance = Random.Range(0, settings.recurseChance);
            if (recurseChance == 0)
            {
                for (var n = 0; n < doorz; n++)
                {
                    //recurseChance = Random.Range(0, 6);
                    recurseChance = Random.Range(0, settings.doorRecurseChance);
                    if (recurseChance == 0)
                    {
                        Recurse(element);
                        Debug.Log("Recursing!");
                    }
                }
            }

        }
        dungeonLevel = level;
        dungeonLevel.wayOutTime = settings.wayOutTime;
        this.level = level.Instantiate();
        var spawns = this.level.GetComponentsInChildren<PlayerSpawn>();
        GameController.instance.player.transform.position = spawns[0].transform.position;
        dungeonState = new DungeonState();
        dungeonState.allRooms = this.level.GetComponentsInChildren<RoomComponent>();
        inDangerPrev = false;
        GameEventsController.DungeonStart();
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

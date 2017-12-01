using System.Collections;
using UnityEngine;

public class BoardCreator3: MonoBehaviour
{
	// The type of tile that will be laid in a specific position.
	public enum TileType
	{
		Wall, Floor,
	}


	public int columns = 100;                                 // The number of columns on the board (how wide it will be).
	public int rows = 100;                                    // The number of rows on the board (how tall it will be).
	public IntRange numRooms = new IntRange (15, 20);         // The range of the number of rooms there can be.
	public IntRange roomWidth = new IntRange (3, 10);         // The range of widths rooms can have.
	public IntRange roomHeight = new IntRange (3, 10);        // The range of heights rooms can have.
	public IntRange corridorLength = new IntRange (6, 10);    // The range of lengths corridors between rooms can have.
	public GameObject[] floorTiles;                           // An array of floor tile prefabs.
	public GameObject[] wallTiles;                            // An array of wall tile prefabs.
	public GameObject[] outerWallTiles;  					  // An array of outer wall tile prefabs.
	public GameObject[] nStop;
	public GameObject[] sStop;
	public GameObject[] eStop;
	public GameObject[] wStop;
	public GameObject[] vDoublewall;
	public GameObject[] hDoublewall;
	public GameObject[] nwCorner;
	public GameObject[] neCorner;
	public GameObject[] swCorner;
	public GameObject[] seCorner;
	public GameObject[] bnwCorner;
	public GameObject[] bneCorner;
	public GameObject[] bswCorner;
	public GameObject[] bseCorner;
	public GameObject[] westWall;
	public GameObject[] northWall;
	public GameObject[] eastWall;
	public GameObject[] southWall;
	public GameObject[] wallcornerwest_ne; //wall is to the west, smaller corner is to the northeast
	public GameObject[] wallcornerwest_se;
	public GameObject[] wallcornersouth_nw;
	public GameObject[] wallcornersouth_ne;
	public GameObject[] wallcornernorth_se;
	public GameObject[] wallcornernorth_sw;
	public GameObject[] wallcornereast_nw;
	public GameObject[] wallcornereast_sw;
	public GameObject[] doublecornerne; // ne means the smaller corner is to the northeast
	public GameObject[] doublecornernw;
	public GameObject[] doublecornerse;
	public GameObject[] doublecornersw;
	public GameObject[] blockOff;
	public GameObject[] items;
	public int itemCount;
	public GameObject player;
	public GameObject[] enemy;
	public int enemyCount;
	public GameObject exit;

	private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
	private Room[] rooms;                                     // All the rooms that are created for this board.
	private Corridor[] corridors;                             // All the corridors that connect the rooms.
	private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.
	private Vector3[] positions;


	private void Start ()
	{
		// Create the board holder.
		boardHolder = new GameObject("BoardHolder");

		positions = new Vector3[2 + enemyCount + itemCount];

		SetupTilesArray ();

		CreateRoomsAndCorridors ();

		SetTilesValuesForRooms ();
		SetTilesValuesForCorridors ();

		InstantiateTiles ();
		InstantiateOuterWalls ();
		InstantiateExit ();
		InstantiateEnemy ();
		InstantiateItems ();
		InstantiatePlayer ();
	}


	void SetupTilesArray ()
	{
		// Set the tiles jagged array to the correct width.
		tiles = new TileType[columns][];

		// Go through all the tile arrays...
		for (int i = 0; i < tiles.Length; i++)
		{
			// ... and set each tile array is the correct height.
			tiles[i] = new TileType[rows];
		}
	}


	void CreateRoomsAndCorridors ()
	{
		// Create the rooms array with a random size.
		rooms = new Room[numRooms.Random];

		// There should be one less corridor than there is rooms.
		corridors = new Corridor[rooms.Length - 1];

		// Create the first room and corridor.
		rooms[0] = new Room ();
		corridors[0] = new Corridor ();

		// Setup the first room, there is no previous corridor so we do not use one.
		rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

		// Setup the first corridor using the first room.
		corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

		for (int i = 1; i < rooms.Length; i++)
		{
			// Create a room.
			rooms[i] = new Room ();

			// Setup the room based on the previous corridor.
			rooms[i].SetupRoom (roomWidth, roomHeight, columns, rows, corridors[i - 1]);

			// If we haven't reached the end of the corridors array...
			if (i < corridors.Length)
			{
				// ... create a corridor.
				corridors[i] = new Corridor ();

				// Setup the corridor based on the room that was just created.
				corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
			}
		}
	}


	void SetTilesValuesForRooms ()
	{
		// Go through all the rooms...
		for (int i = 0; i < rooms.Length; i++)
		{
			Room currentRoom = rooms[i];

			// ... and for each room go through it's width.
			for (int j = 0; j < currentRoom.roomWidth; j++)
			{
				int xCoord = currentRoom.xPos + j;

				// For each horizontal tile, go up vertically through the room's height.
				for (int k = 0; k < currentRoom.roomHeight; k++)
				{
					int yCoord = currentRoom.yPos + k;

					// The coordinates in the jagged array are based on the room's position and it's width and height.
					tiles[xCoord][yCoord] = TileType.Floor;
				}
			}
		}
	}


	void SetTilesValuesForCorridors ()
	{
		// Go through every corridor...
		for (int i = 0; i < corridors.Length; i++)
		{
			Corridor currentCorridor = corridors[i];

			// and go through it's length.
			for (int j = 0; j < currentCorridor.corridorLength; j++)
			{
				// Start the coordinates at the start of the corridor.
				int xCoord = currentCorridor.startXPos;
				int yCoord = currentCorridor.startYPos;

				// Depending on the direction, add or subtract from the appropriate
				// coordinate based on how far through the length the loop is.
				switch (currentCorridor.direction)
				{
				case Direction.North:
					yCoord += j;
					break;
				case Direction.East:
					xCoord += j;
					break;
				case Direction.South:
					yCoord -= j;
					break;
				case Direction.West:
					xCoord -= j;
					break;
				}

				// Set the tile at these coordinates to Floor.
				tiles[xCoord][yCoord] = TileType.Floor;
			}
		}
	}


	void InstantiateTiles ()
	{
		// Go through all the tiles in the jagged array...
		for (int i = 0; i < tiles.Length; i++)
		{
			for (int j = 0; j < tiles[i].Length; j++)
			{
				// ... and instantiate a floor tile for it.
				if (tiles[i][j] != TileType.Wall) {
					InstantiateFromArray (floorTiles, i, j);
				}

				// If the tile type is Wall...
				if (tiles[i][j] == TileType.Wall) {
					bool north = true;
					bool south = true;
					bool east = true;
					bool west = true;
					bool ne;
					bool nw;
					bool se;
					bool sw;
					//west
					if (i == 0) {
						west = true;	
					} else {
						west = tiles[i - 1][j] == TileType.Wall;
					}
					//east
					if (i == tiles.Length - 1) {
						east = true;	
					} else {
						east = tiles[i + 1][j] == TileType.Wall;
					}
					//south
					if (j == 0) {
						south = true;
					} else {
						south = tiles[i][j - 1] == TileType.Wall;
					}
					//north
					if (j == tiles.Length - 1) {
						north = true;
					} else {
						north = tiles[i][j + 1] == TileType.Wall;
					}
					//sw
					if ((i == 0) || (j == 0)) {
						sw = false;
					} else {
						sw = tiles [i - 1] [j - 1] != TileType.Wall;
					}
					//se
					if ((i == tiles.Length - 1) || (j == 0)) {
						se = false;
					} else {
						se = tiles [i + 1] [j - 1] != TileType.Wall;
					}
					//nw
					if ((i == 0) || (j == tiles.Length - 1)) {
						nw = false;
					} else {
						nw = tiles[i - 1][j + 1] != TileType.Wall;
					}
					//ne
					if ((i == tiles.Length - 1) || (j == tiles.Length - 1)) {
						ne = false;
					} else {
						ne = tiles[i + 1][j + 1] != TileType.Wall;
					}

					// ... instantiate a wall over the top.
					if (!west && east && north && south) {
						if (ne) {
							InstantiateFromArray(wallcornerwest_ne, i, j);
						}
						else if (se) {
							InstantiateFromArray(wallcornerwest_se, i, j);
						}
						else {
							InstantiateFromArray(westWall, i, j);
						}
					}
					else if (west && !east && !north && !south) {
						InstantiateFromArray(eStop, i, j);
					}
					else if (west && !east && north && south) {
						if (sw) {
							InstantiateFromArray(wallcornereast_sw, i, j);
						}
						else if (nw) {
							InstantiateFromArray(wallcornereast_nw, i, j);
						}
						else {
							InstantiateFromArray(eastWall, i, j);
						}
					}
					else if (!west && east && !north && !south) {
						InstantiateFromArray(wStop, i, j);
					}
					else if (west && east && !north && south) {
						if (se) {
							InstantiateFromArray(wallcornernorth_se, i, j);
						}
						else if (sw) {
							InstantiateFromArray(wallcornernorth_sw, i, j);
						}
						else {
							InstantiateFromArray (northWall, i, j);
						}
					}
					else if (!west && !east && !north && south) {
						InstantiateFromArray (nStop, i, j);
					}
					else if (west && east && north && !south) {
						if (ne) {
							InstantiateFromArray(wallcornersouth_ne, i, j);
						}
						else if (nw) {
							InstantiateFromArray(wallcornersouth_nw, i, j);
						}
						else {
							InstantiateFromArray(southWall, i, j);
						}
					}
					else if (!west && !east && north && !south) {
						InstantiateFromArray (sStop, i, j);
					}
					else if (west && !east && north && !south) {
						if (nw) {
							InstantiateFromArray (doublecornernw, i, j);
						} else {
							InstantiateFromArray (seCorner, i, j);
						}
					}
					else if (!west && east && north && !south) {
						if (ne) {
							InstantiateFromArray (doublecornerne, i, j);
						} else {
							InstantiateFromArray (swCorner, i, j);
						}
					}
					else if (!west && east && !north && south) {
						if (se) {
							InstantiateFromArray (doublecornerse, i, j);
						} else {
							InstantiateFromArray (nwCorner, i, j);
						}
					}
					else if (west && !east && !north && south) {
						if (sw) {
							InstantiateFromArray (doublecornersw, i, j);
						} else {
							InstantiateFromArray (neCorner, i, j);
						}
					}
					else if (west && east && !north && !south) {
						InstantiateFromArray (hDoublewall, i, j);
					}
					else if (!west && !east && north && south) {
						InstantiateFromArray (vDoublewall, i, j);
					}
					else if (west && east && north && south) {
						if (nw) {
							InstantiateFromArray (bnwCorner, i, j);
						}
						else if (ne) {
							InstantiateFromArray (bneCorner, i, j);
						}
						else if (se) {
							InstantiateFromArray (bseCorner, i, j);
						} else if (sw) {
							InstantiateFromArray (bswCorner, i, j);
						} else {
							InstantiateFromArray (wallTiles, i, j);
						}
					}
					else {
						InstantiateFromArray (blockOff, i, j);
					}
				}
			}
		}
	}


	void InstantiateOuterWalls ()
	{
		// The outer walls are one unit left, right, up and down from the board.
		float leftEdgeX = -1f;
		float rightEdgeX = columns + 0f;
		float bottomEdgeY = -1f;
		float topEdgeY = rows + 0f;

		// Instantiate both vertical walls (one on each side).
		InstantiateVerticalOuterWall (leftEdgeX, bottomEdgeY, topEdgeY);
		InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

		// Instantiate both horizontal walls, these are one in left and right from the outer walls.
		InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
		InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
	}


	void InstantiateVerticalOuterWall (float xCoord, float startingY, float endingY)
	{
		// Start the loop at the starting value for Y.
		float currentY = startingY;

		// While the value for Y is less than the end value...
		while (currentY <= endingY)
		{
			// ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
			InstantiateFromArray(outerWallTiles, xCoord, currentY);

			currentY++;
		}
	}


	void InstantiateHorizontalOuterWall (float startingX, float endingX, float yCoord)
	{
		// Start the loop at the starting value for X.
		float currentX = startingX;

		// While the value for X is less than the end value...
		while (currentX <= endingX)
		{
			// ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
			InstantiateFromArray (outerWallTiles, currentX, yCoord);

			currentX++;
		}
	}

	void InstantiatePlayer()
	{
		int i = Random.Range (0, rooms.Length);
		Vector3 playerPos = new Vector3 (rooms[i].xPos, rooms[i].yPos, 0);
		while (find (positions, playerPos)) 
		{
			i = Random.Range (0, rooms.Length);
			playerPos = new Vector3 (rooms[i].xPos, rooms[i].yPos, 0);
		}
		Instantiate(player, playerPos, Quaternion.identity);
	}

	void InstantiateExit()
	{
		int i = Random.Range (0, rooms.Length);
		Vector3 Pos = new Vector3 (rooms[i].xPos, rooms[i].yPos, 0);
		positions [0] = Pos;
		Instantiate(exit, Pos, Quaternion.identity);
	}


	void InstantiateEnemy()
	{
		for (int i = 0; i < enemyCount; i++)
		{
			GameObject blob = enemy[Random.Range (0, enemy.Length)];
			int j = Random.Range (0, rooms.Length);
			Vector3 Pos = new Vector3 (rooms[j].xPos, rooms[j].yPos, 0);
			while(find (positions, Pos)) 
			{
				j = Random.Range (0, rooms.Length);
				Pos = new Vector3 (rooms [j].xPos, rooms [j].yPos, 0);
			}
			Instantiate(blob, Pos, Quaternion.identity);
			positions[i + 1] = Pos;
		}
	}

	void InstantiateItems()
	{
		for (int i = 0; i < itemCount; i++)
		{
			GameObject item = items[Random.Range (0, items.Length)];
			int j = Random.Range (0, rooms.Length);
			Vector3 Pos = new Vector3 (rooms[j].xPos, rooms[j].yPos, 0);
			while(find (positions, Pos)) 
			{
				j = Random.Range (0, rooms.Length);
				Pos = new Vector3 (rooms [j].xPos, rooms [j].yPos, 0);
			}
			Instantiate(item, Pos, Quaternion.identity);
			positions[i + enemyCount + 1] = Pos;
		}
	}


	bool find(Vector3[] objects, Vector3 pos) 
	{
		bool retval = false;
		for (int i = 0; i < objects.Length; i++) {
			Vector3 location = objects [i];
			if (pos.Equals (location))
			{
				retval = true;
				break;
			}
		}
		return retval;
	}


	void InstantiateFromArray (GameObject[] prefabs, float xCoord, float yCoord)
	{
		// Create a random index for the array.
		int randomIndex = Random.Range(0, prefabs.Length);

		// The position to be instantiated at is based on the coordinates.
		Vector3 position = new Vector3(xCoord, yCoord, 0f);

		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = boardHolder.transform;
	}
}
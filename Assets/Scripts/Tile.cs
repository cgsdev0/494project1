using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    static Sprite[]         spriteArray;

    public Texture2D        spriteTexture;
	public int				x, y;
	public int				tileNum;
	private BoxCollider		bc;
    private Material        mat;

    private SpriteRenderer  sprend;

	void Awake() {
        if (spriteArray == null) {
            spriteArray = Resources.LoadAll<Sprite>(spriteTexture.name);
        }

		bc = GetComponent<BoxCollider>();

        sprend = GetComponent<SpriteRenderer>();
        //Renderer rend = gameObject.GetComponent<Renderer>();
        //mat = rend.material;
	}

	public void SetTile(int eX, int eY, int eTileNum = -1) {
		if (x == eX && y == eY) return; // Don't move this if you don't have to. - JB

		x = eX;
		y = eY;
		transform.localPosition = new Vector3(x, y, 0);
        gameObject.name = x.ToString("D3")+"x"+y.ToString("D3");

		tileNum = eTileNum;
		if (tileNum == -1 && ShowMapOnCamera.S != null) {
			tileNum = ShowMapOnCamera.MAP[x,y];
			if (tileNum == 0) {
				ShowMapOnCamera.PushTile(this);
			}
		}

        sprend.sprite = spriteArray[tileNum];

		if (ShowMapOnCamera.S != null) SetCollider();
        //TODO: Add something for destructibility - JB

        gameObject.SetActive(true);
		if (ShowMapOnCamera.S != null) {
			if (ShowMapOnCamera.MAP_TILES[x,y] != null) {
				if (ShowMapOnCamera.MAP_TILES[x,y] != this) {
					ShowMapOnCamera.PushTile( ShowMapOnCamera.MAP_TILES[x,y] );
				}
			} else {
				ShowMapOnCamera.MAP_TILES[x,y] = this;
			}
		}
	}


	// Arrange the collider for this tile
	void SetCollider() {
        
        // Collider info from collisionData
        bc.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        char c = ShowMapOnCamera.S.collisionS[tileNum];
        switch (c) {
        case 'D': // Door tile
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
            bc.enabled = false;
            break;
        case 'S': // Solid
            bc.center = Vector3.zero;
            bc.size = Vector3.one;
            break;
        case 'H': //Half solid up ( horizontal doorway)
            bc.center = new Vector3(0, 0.25f, 0);
            bc.size = new Vector3(1, 0.5f, 1);
            break;
        case 'L': //Half solid left (vertical doorway)
            bc.center = new Vector3(-0.25f, 0f, 0);
            bc.size = new Vector3(0.5f, 1, 1);
            break;
        case 'R': //Half solid right (vertical doorway)
            bc.center = new Vector3(0.25f, 0, 0);
            bc.size = new Vector3(0.5f, 1, 1);
            break;
		case '3'://Tiles that should have a higher sorting order
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
            bc.center = Vector3.zero;
			bc.size = Vector3.one;
			break;
        default:
            bc.enabled = false;
            break;
        }
	}	
}
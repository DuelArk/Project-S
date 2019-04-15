using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float moveSpeed;
	public float pixelperunit;

	private Animator ani;
	private SpriteRenderer render;

	private bool front;
	private bool back;
	private bool right;
	private bool left;

	private int hitCount;

	public bool collider;
	public LayerMask layerMask;

    new Transform transform;

    public bool atk;

    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator> ();
		render = GetComponent<SpriteRenderer> ();
		front = true;
		back = false;
		right = false;
		left = false;
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Z))
			StartCoroutine ("Attack");
        if (Input.GetKeyDown(KeyCode.X))
            StartCoroutine("Roll");
		/*if (Input.GetKeyDown (KeyCode.X))
			StartCoroutine ("Hit");
		if (Input.GetKeyDown (KeyCode.C))
			ani.SetBool ("death", true);
		if (Input.GetKeyDown (KeyCode.V))
			ani.SetBool ("death", false);*/
		Moving ();
	}

    IEnumerator Roll()
    {
        ani.SetBool("rollCheck", true);
        StartCoroutine("RollMove");
        yield return new WaitForSeconds(0.1f);
        ani.SetBool("rollCheck", false);
    }

    IEnumerator RollMove()
    {
        Vector2 vdir = Vector2.zero;
        if (front)
            vdir = Vector2.down;
        if (back)
            vdir = Vector2.up;
        if (left)
            vdir = Vector2.left;
        if (right)
            vdir = Vector2.right;
        Vector2 moveDir = PixelPerfectClamp(vdir, pixelperunit);
        for(int i = 0; i < 16; i++)
        {
            transform.Translate(moveDir * 0.25f);
            yield return new WaitForSeconds(0.01f);
        }
    }

	IEnumerator Attack(){
        atk = true;
		ani.SetBool ("swordAtk", true);
		yield return new WaitForSeconds (0.1f);
		ani.SetBool ("swordAtk", false);
	}

	IEnumerator Hit(){
		if (hitCount == 0) {
			ani.SetBool ("hit", true);
			render.color = new Color (1, 0, 0, 1);
			yield return new WaitForSeconds (0.1f);
			ani.SetBool ("hit", false);
		} else {
			render.color = new Color (1, 1, 1, 0);
			yield return new WaitForSeconds (0.1f);
			render.color = new Color (1, 1, 1, 1);
			yield return new WaitForSeconds (0.1f);
		}
		hitCount++;
		if (hitCount < 5)
			StartCoroutine ("Hit");
		if (hitCount == 5)
			hitCount = 0;
	}

	private void Moving(){
        ArrowCheck();
        if (atk)
            return;
		Vector2 moveDir = PixelPerfectClamp (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")), pixelperunit);
		/*if (front || back) {
			moveDir.x = 0;
		} else if (right || left) {
			moveDir.y = 0;
		}*/
		if (left)
			transform.localScale = 2 * Vector3.left + Vector3.one;
		else
			transform.localScale = Vector3.one;
		if (moveDir.x != 0 || moveDir.y != 0) {
			ani.SetFloat ("dirX", moveDir.x);
			ani.SetFloat ("dirY", moveDir.y);
			if (front || back) {
				ani.SetFloat ("dirX", 0);
			} else if (right || left) {
				ani.SetFloat ("dirY", 0);
			}
		}
		ColliderCheck (moveDir.x, moveDir.y);
		if (moveDir.x == 0 && moveDir.y == 0)
			ani.SetBool ("moveCheck", false);
		else
			ani.SetBool ("moveCheck", true);
		if (ani.GetBool ("moveCheck")) {
			if (collider)
				return;
			transform.Translate (moveDir * moveSpeed / 10);
		}
	}
		
	private void ColliderCheck(float dirX, float dirY){
		RaycastHit2D hit;
		RaycastHit2D hit1;
		RaycastHit2D hit2;

		if (dirX > 0)
			dirX = 1;
		else if (dirX < 0)
			dirX = -1;
		else
			dirX = 0;
		if (dirY > 0)
			dirY = 1;
		else if (dirY < 0)
			dirY = -1;
		else
			dirY = 0;
		
		Vector2 start = transform.position;
		Vector2 end = new Vector2 (start.x + dirX * 0.55f, start.y + dirY * 0.6f - 0.45f);
		hit = Physics2D.Linecast (start, end, layerMask);
		if (hit.transform != null)
			collider = true;
		else
			collider = false;

		if (front || back) {
			Vector2 start1 = new Vector2 (start.x + 0.45f, start.y);
			Vector2 start2 = new Vector2 (start.x - 0.45f, start.y);
			Vector2 end1 = new Vector2 (start1.x, start1.y + dirY * 0.6f - 0.45f);
			Vector2 end2 = new Vector2 (start2.x, start2.y + dirY * 0.6f - 0.45f);
			hit1 = Physics2D.Linecast (start1, end1, layerMask);
			hit2 = Physics2D.Linecast (start2, end2, layerMask);
			if (hit1.transform != null || hit2.transform != null)
				collider = true;
		}
		if (right || left) {
			Vector2 start1 = new Vector2 (start.x, start.y - 0.9f);
			Vector2 start2 = start;
			Vector2 end1 = new Vector2 (start1.x + dirX * 0.55f, start1.y);
			Vector2 end2 = new Vector2 (start2.x + dirX * 0.55f, start2.y);
			hit1 = Physics2D.Linecast (start1, end1, layerMask);
			hit2 = Physics2D.Linecast (start2, end2, layerMask);
			if (hit1.transform != null || hit2.transform != null)
				collider = true;
		}
	}

	private Vector2 PixelPerfectClamp(Vector2 moveVector,float pixelsPerUnit){
		Vector2 vectorPixels = new Vector2 (Mathf.RoundToInt (moveVector.x * pixelsPerUnit), Mathf.RoundToInt (moveVector.y * pixelsPerUnit));
		return vectorPixels / pixelsPerUnit;
	}

	private void ArrowCheck(){
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
            if (Input.GetKey(KeyCode.DownArrow))
                return;
			front = false;
			back = true;
			right = false;
			left = false;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
            if (Input.GetKey(KeyCode.UpArrow))
                return;
			front = true;
			back = false;
			right = false;
			left = false;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
            if (Input.GetKey(KeyCode.LeftArrow))
                return;
			front = false;
			back = false;
			right = true;
			left = false;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            if (Input.GetKey(KeyCode.RightArrow))
                return;
			front = false;
			back = false;
			right = false;
			left = true;
		}

		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			if (Input.GetKey (KeyCode.DownArrow)) {
				front = true;
				back = false;
				right = false;
				left = false;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				front = false;
				back = false;
				right = true;
				left = false;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				front = false;
				back = false;
				right = false;
				left = true;
			}
		} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				front = false;
				back = true;
				right = false;
				left = false;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				front = false;
				back = false;
				right = true;
				left = false;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				front = false;
				back = false;
				right = false;
				left = true;
			}
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				front = false;
				back = true;
				right = false;
				left = false;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				front = true;
				back = false;
				right = false;
				left = false;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				front = false;
				back = false;
				right = false;
				left = true;
			}
		} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				front = false;
				back = true;
				right = false;
				left = false;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				front = true;
				back = false;
				right = false;
				left = false;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				front = false;
				back = false;
				right = true;
				left = false;
			}
		}
	}
}

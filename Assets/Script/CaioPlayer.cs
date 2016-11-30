using UnityEngine;
using System.Collections;

public class CaioPlayer : MonoBehaviour {

	private Rigidbody2D playerRb;
	public float velocidade;
	public Animator anime;
	public bool parado;
	public bool pulando;
	public bool morreu;
	public bool grounded;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public bool puloDuplo;
	public bool deslisando;
    

	// Use this for initialization
	void Start () {
		playerRb = GetComponent<Rigidbody2D> ();
		anime = gameObject.GetComponent<Animator> ();
		parado = true;
		morreu = false;
	}

	//verificando se esta no chao
	void OnCollisionEnter2D(Collision2D colisao){
		
		if(colisao.gameObject.tag == "Espinho"){
			morreu = true;
            Application.LoadLevel("InicioCorrer");
            print ("Morreu");
          
        }

		if(colisao.gameObject.tag == "Ganhou"){
           
            if(Application.loadedLevelName == "InicioCorrer")
            {
                Application.LoadLevel("Fase2");
            }
            else
            {
                Application.LoadLevel("Fase3");
            }

            print ("Ganhou");
		}

		if(colisao.gameObject.tag == "groundCheck"){
			//print ("No Chao");
			grounded = true; 
		}
	}

	// Update is called once per frame
	void Update () {
		anime.SetFloat ("velocidade", velocidade);
		anime.SetBool ("parado", parado);
		anime.SetBool ("pulando", pulando);
		anime.SetBool ("morreu", morreu);
		anime.SetBool ("puloDuplo", puloDuplo);
		anime.SetBool ("deslisando", deslisando);

		//verificar se esta no solo
		//grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anime.SetBool ("grounded",grounded);
			
		//Direita
		if (Input.GetKey ("d")) {
			Vector3 esc = transform.localScale;
			Vector3 pos = transform.position;

			velocidade = velocidade * -1; 
			if (esc.x < 0) {
				esc.x = esc.x * -1f;
			}

			if (velocidade > 0) {
				parado = false;
				playerRb.transform.localScale = esc;
				playerRb.velocity = new Vector2 (velocidade, playerRb.velocity.y);
			}

		}else if(Input.GetKeyUp("d")){
			parado = true;
		}

		//Esquerda
		if (Input.GetKey ("a")) {
			Vector3 esc = transform.localScale;

			velocidade *= -1; 
			if (esc.x > 0) {
				esc.x = esc.x * -1f;
			}

			if (velocidade < 0) {
				parado = false;
				//transform.position = velocidade;
				playerRb.velocity = new Vector2 (velocidade, playerRb.velocity.y);
				transform.localScale = esc;
			}
		} else if(Input.GetKeyUp("a")) {
			parado = true;
		}

		//pular
		if((Input.GetKey("space")) && grounded){
			Vector3 pos = transform.position;
			pulando = true;
			grounded = false;
			puloDuplo = true;

			pos.y += 0.4f;
			transform.position = pos;

			pos.x += 0.2f;

			transform.position = pos;

		} else //pulo duplo
			if (pulando && puloDuplo ) {
				Vector3 pos = transform.position;
				pos.y += 1;
				transform.position = pos;
				pos.x += 0.2f;
				transform.position = pos;
				puloDuplo = false;
			} else if(Input.GetKeyUp("space")) {
			
				pulando = false;
		}

		//Deslisar
		if (Input.GetKey("s")) {
			deslisando = true;
		} else if (Input.GetKeyUp("s")) {
			deslisando = false;
		}

	}//fecha Update


}

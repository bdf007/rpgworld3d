using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 6.0f;  // vitesse de déplacement
    public float rotateSpeed = 90.0f; // vitesse de rotation
    public float gravity = 20.0f; // gravité appliquée au joueur

    //SFX
    public AudioClip[] swordSFx;

    private Vector3 moveDirection = Vector3.zero; // direction de déplacement du joueur
    private bool isGrounded = false; // le joueur est-il au sol ?
    // les composants
    private CharacterController controller;
    private Animator anim;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // récupération du CharacterController attaché au joueur
        controller = GetComponent<CharacterController>();
        // récupération de l'Animator attaché au joueur
        anim = GetComponent<Animator>();
        // récupération de l'AudioSource attaché au joueur
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        BaseMovement(); // gestion du déplacement du joueur
        Attack(); // gestion de l'attaque du joueur
    }

    public void BaseMovement()
    {
        if(isGrounded)
        {
            anim.SetFloat("walkSpeed", controller.velocity.magnitude);
            // calcul du vecteur direction de déplacement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") );
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        // application de la gravité sur le joueur
        moveDirection.y -= gravity * Time.deltaTime;
        Physics.SyncTransforms();
        // on applique le mouvement au joueur
        var flags = controller.Move(moveDirection * Time.deltaTime);
        // gestion de la rotation du joueur
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);
        // Detection du sol
        isGrounded = CollisionFlags.CollidedBelow != 0;
    }

    public void Attack()
    {
        // si on appuie sur le bouton gauche de la souris on attaque
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(swordSFx[Random.Range(0,3)]);
            anim.SetTrigger("attack");
        }
    }
}

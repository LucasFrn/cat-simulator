using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNovo : MonoBehaviour,IDataPersistance
{   
    [Header("Movement")]
    [SerializeField] Transform orientation;
    [SerializeField] float groundDrag;
    const float dragFinal = 10f;
    /* [SerializeField] float moveSpeedRegular; //eu gosto de 8
    float moveSpeed;
    [SerializeField]float jumpForce;
    [SerializeField]float jumpCoolDown;
    [SerializeField]bool readyToJump; */
    //float movimentHelper = 10; //faz andar mais rapido, usado por causa que a massa n vai ficar no 1
    [Header("Ground Check")]
    [SerializeField]bool isGrounded;
    /* [SerializeField]float playerHight;
    [SerializeField]LayerMask ignoreMe;
    [SerializeField]ThirdPersonCamera thirdPersonCamera; */
    PosData posDataLoadada;
    float hInput;
    float vInput;
    Vector3 moveDir;
    Rigidbody rb;
    bool ajusteiPos;
    [Header("Movimento Novo Test")]
    [SerializeField]float airMultiplayer;//redução da velocidade quando pulando
    [SerializeField]float speed;//3.5
    float timerPulo;
    const float fallMultiplayer = 1.5f;//multiplicador da Gravidade
    const float lowJumpMuktipkayer = 1;//multiplicador da Gravidade
    const float jumpVelocity = 6f;
    const float maxTimerPulo=0.15f;
    const float speedRegular = 3.5f;
    bool isJumping;

    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerUsesEnergyDrink+=UseEnergetico;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerUsesEnergyDrink-=UseEnergetico;
    }
    void Start()
    {
        ajusteiPos=false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation=true;
        //movimentHelper = rb.mass * 10;
        //readyToJump=true;
        //moveSpeed=moveSpeedRegular;
        timerPulo=0;
        isJumping=false;
        speed=speedRegular;
    }

    // Update is called once per frame
    void Update()
    {
        if(ajusteiPos==false){
            LoadPos();
            ajusteiPos=true;
        }
        if(!GameManager.Instance.jogoPausado){
            if(GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque){
                Entrada();
                isGrounded = Physics.Raycast(transform.position,Vector3.down,0.2f);
                 if(isGrounded){
                    rb.drag=groundDrag;
                }
                else{
                    rb.drag = 0;
                } 
                SpeedControl();

                if(Input.GetKeyDown(KeyCode.Equals)){
                    LoadPos();
                }
                //BetterJump - Fall
                if(rb.velocity.y<0){
                    rb.velocity += Vector3.up* Physics.gravity.y * (fallMultiplayer - 1)* Time.deltaTime;
                } else if(rb.velocity.y> 0 && !Input.GetKey(KeyCode.Space)){
                    rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMuktipkayer-1)*Time.deltaTime;
                }
                //BetterJump - Jump
                if(Input.GetKey(KeyCode.Space)&&isGrounded&&!isJumping){
                    isJumping=true;
                    timerPulo=0;
                }
                if(Input.GetKey(KeyCode.Space)&&isJumping&&timerPulo<maxTimerPulo){
                    timerPulo+=Time.deltaTime;
                    Jump();
                }
                if(Input.GetKeyUp(KeyCode.Space)){
                    isJumping=false;
                }
            }
        }
    }
    void FixedUpdate(){
        if(!GameManager.Instance.jogoPausado){
            if(GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque){
                Mover();
            }
        }
    }
    void Entrada(){
        hInput = Input.GetAxis("Horizontal");
        vInput= Input.GetAxis("Vertical");

        /* if(Input.GetKey(KeyCode.Space)&&readyToJump&&isGrounded){
            readyToJump=false;
            Invoke("ResetJump",jumpCoolDown);
        } */
        /* if(Input.GetKey(KeyCode.Space)&&(timerPulo<maxTimerPulo)){
            Jump();
        } */
    }
    void Mover(){
        moveDir = orientation.forward*vInput + orientation.right*hInput;
        if(isGrounded){//no chao
            //rb.AddForce(moveDir.normalized*moveSpeed*movimentHelper,ForceMode.Force);
            //rb.AddForce(moveDir.normalized,ForceMode.VelocityChange);
            rb.velocity += moveDir.normalized*speed;
        }
        else{//no ar
            //rb.AddForce(moveDir.normalized*moveSpeed*movimentHelper*airMultiplayer,ForceMode.Force);
            //rb.AddForce(moveDir.normalized*airMultiplayer,ForceMode.VelocityChange);
            rb.velocity += moveDir.normalized*speed*airMultiplayer;
        }
    }
    void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x,0,rb.velocity.z);
        if(flatVel.magnitude>speed){
            Vector3 limitedVelocity = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x,rb.velocity.y,limitedVelocity.z);
        }
    }
    void Jump(){
        //rb.velocity=new Vector3(rb.velocity.x,0,rb.velocity.z);
        //rb.AddForce(transform.up*jumpForce*movimentHelper/2, ForceMode.Impulse);
        //rb.AddForce(transform.up*jumpForce, ForceMode.Impulse);
        Vector3 newMove = new Vector3(rb.velocity.x,0,rb.velocity.z);
        rb.velocity =  newMove+ Vector3.up*jumpVelocity;
        
    }
    /* void ResetJump(){
        readyToJump = true;
    } */

    public void UseEnergetico(){
        speed = speedRegular+1f;
        Invoke("ResetMoveSpeed",90f);
    }
    public void ResetMoveSpeed(){
        speed= speedRegular;
    }
    public void ChangeDrag(bool MaisDrag){
        if(MaisDrag){
            groundDrag=dragFinal*2.5f;
        }
        else{
            groundDrag=dragFinal;
        }
    }

    public void LoadData(GameData data)
    {
        posDataLoadada=data.posData;
    }

    public void SaveData(GameData data)
    {
        PosData newPosData = new PosData(transform.position);
        data.posData=newPosData;
    }
    void LoadPos(){
        transform.position=posDataLoadada.gatoPos;
        Debug.Log($"Colocando o gato na pos {transform.position}");
    }
}

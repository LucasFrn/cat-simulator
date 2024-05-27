using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNovo : MonoBehaviour,IDataPersistance
{   
    [Header("Movement")]
    [SerializeField] Transform orientation;
    [SerializeField] float moveSpeedRegular; //eu gosto de 8
    float moveSpeed;
    [SerializeField] float groundDrag;
    [SerializeField]float jumpForce;
    [SerializeField]float jumpCoolDown;
    [SerializeField]float airMultiplayer;
    [SerializeField]bool readyToJump;
    [Header("Ground Check")]
    [SerializeField]float playerHight;
    [SerializeField]bool isGrounded;
    [SerializeField]LayerMask ignoreMe;
    PosData posDataLoadada;
    float hInput;
    float vInput;
    Vector3 moveDir;
    Rigidbody rb;
    bool ajusteiPos;
    float movimentHelper = 10; //faz andar mais rapido, usado por causa que a massa n vai ficar no 1

    // Start is called before the first frame update
    void Start()
    {
        ajusteiPos=false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation=true;
        movimentHelper = rb.mass * 10;
        readyToJump=true;
        moveSpeed=moveSpeedRegular;
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

        if(Input.GetKey(KeyCode.Space)&&readyToJump&&isGrounded){
            readyToJump=false;
            Jump();
            Invoke("ResetJump",jumpCoolDown);
        }
    }
    void Mover(){
        moveDir = orientation.forward*vInput + orientation.right*hInput;
        if(isGrounded){//no chao
            rb.AddForce(moveDir.normalized*moveSpeed*movimentHelper,ForceMode.Force);
        }
        else{//no ar
            rb.AddForce(moveDir.normalized*moveSpeed*movimentHelper*airMultiplayer,ForceMode.Force);
        }
    }
    void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x,0,rb.velocity.z);
        if(flatVel.magnitude>moveSpeed){
            Vector3 limitedVelocity = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x,rb.velocity.y,limitedVelocity.z);
        }
    }
    void Jump(){
        rb.velocity=new Vector3(rb.velocity.x,0,rb.velocity.z);
        rb.AddForce(transform.up*jumpForce*movimentHelper/2, ForceMode.Impulse);
    }
    void ResetJump(){
        readyToJump = true;
    }

    public void ChangeVelocity(float vel){
        moveSpeed=vel;
    }
    public void ResetMoveSpeed(){
        ChangeVelocity(moveSpeedRegular);
    }

    public void LoadData(GameData data)
    {
        posDataLoadada=data.posData;
        Debug.Log($"pos = {posDataLoadada.pos} e rot= {posDataLoadada.rot}");
    }

    public void SaveData(GameData data)
    {
        PosData newPosData = new PosData(transform.position,orientation.rotation.eulerAngles);
        data.posData=newPosData;
    }
    void LoadPos(){
        transform.position=posDataLoadada.pos;
        orientation.rotation = Quaternion.Euler(posDataLoadada.rot);
        Debug.Log($"Colocando o gato na pos {transform.position}");
    }
}

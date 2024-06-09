using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour,IDataPersistance
{
    public int petiscos;
    public float fome, energia, higiene, felicidade, social;
    int nEnergeticos;

    public Slider fomeSldr;
    public Slider energiaSldr;
    public Slider higieneSldr;
    public Slider felicidadeSldr;
    public Slider socialSldr;
    public TextMeshProUGUI petiscoText;
    //Coisas para dormir
    bool dormindo;
    float timerDormindo;
    float tempoDormir = 5f;
    //fim coisas dormir
    //Coisas para benho
    bool banho;
    float timerBanho;
    float tempoBanho = 5f;
    //fim coisas banho
    //modificadores barrinhas (valor a ser multiplicado pelo rate, vlaores positivos diminuem a barra)
    float modEnergia = 10;
    float ModHigiene = 5;
    private int conversa = 0;
    private int brinc = 0;
    private int quebr = 0;
    private GameObject itemSegurado;
    private Rigidbody seguradoRB;
    public Transform boca;
    public InventarioDePeixes inventarioDePeixes;
    public LuzManager luzManager;
    LayerMask mask;
    public Transform rayCastOrigin;
    [Header("Painel Interação")]
    public GameObject painelInteracao;
    public TextMeshProUGUI textoInteracao;
    public GameObject painelInteracaoQuests;
    public TextMeshProUGUI textoInteracaoQuest;
    Animator animator;
    //variaveis de morte
    bool isMorrendo=false;
    float timerMorte=0f;
    public GameObject fundoMorrendo;
    


    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onPlantaColhida+=VenderPlanta;
        GameEventsManager.instance.uiEvents.onPainelInteracaoQuestChange+=AlterarPainelQuest;
        GameEventsManager.instance.rewardEvents.onEnergeticoRewardRecived+=GanharEnergetico;
        GameEventsManager.instance.rewardEvents.onGoldRewardRecived+=GanharPetiscos;
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onPlantaColhida-=VenderPlanta;
        GameEventsManager.instance.uiEvents.onPainelInteracaoQuestChange-=AlterarPainelQuest;
        GameEventsManager.instance.rewardEvents.onEnergeticoRewardRecived-=GanharEnergetico;
        GameEventsManager.instance.rewardEvents.onGoldRewardRecived-=GanharPetiscos;
    }
    void Start()
    {
        //SpawnaNoLocal();
        animator = GetComponent<Animator>();
        if(animator==null){
            Debug.LogError("Personagem sem animator");
        }
        painelInteracao.SetActive(false);
        mask = LayerMask.GetMask("Interactable");
        Debug.Log("Minha mask é" + mask.value);
        timerDormindo=tempoDormir;
        //AtualizaValoresESlidesComInfoDoManager();
        GameManager.Instance.ResetCoisasManagerParaJogar();
        GanharEnergetico(1);
    }
    
    void Update()
    {
        if(!GameManager.Instance.jogoPausado){
            if(GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque){
                RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayCastOrigin.position,rayCastOrigin.TransformDirection(Vector3.forward), out hit,3f,mask))
                {
                    painelInteracao.SetActive(true);
                    textoInteracao.text="Aperte E para interagir"; 
                    //Debug.Log("Estou olhando um "+ hit.transform.name);
                    if (hit.transform.tag == "PontoDeOnibus")
                    {
                        textoInteracao.text="Aperte E para ir trabalhar";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            TrocaCena(2);
                            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.MiniGamePao;
                        }
                    }
                    if (hit.transform.tag == "Casa")
                    {
                        //A casa do gato n existe mais
                        /* textoInteracao.text="Aperte E para entrar em casa";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            TrocaCena(3);
                            GameManager.Instance.janelaEmFoco=20;
                        } */
                    }
                    if (hit.transform.tag == "Pesca")
                    {
                        textoInteracao.text="Aperte E para pescar";
                        if (Input.GetKeyDown(KeyCode.E)&&ControllerMiniGamePesca.controllerMiniGamePesca.miniGameRodando==false)
                        {
                            if(energia>5f){
                                energia-=5f;
                                GameEventsManager.instance.playerEvents.PlayerTriesFishing();
                                ControllerMiniGamePesca.controllerMiniGamePesca.ProcuraPeixe();  
                            }
                        }
                    }
                    if (hit.transform.tag == "NPC")
                    {
                        textoInteracao.text="Aperte E para interagir";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            NPC npc = hit.transform.GetComponent<NPC>();
                            if (!npc.trabalhando)
                            {
                                social += npc.Conversar();
                            }
                            else
                            {
                                Debug.Log(petiscos);
                                npc.Vender(this.gameObject);
                                Debug.Log(petiscos);                               
                            }
                            
                        }                       
                    }
                    if (hit.transform.CompareTag("ObjetosDaCasa"))
                    {
                        if(hit.transform.name == "HouseCama3"){
                            textoInteracao.text="Aperte E para dormir";
                            if (Input.GetKeyDown(KeyCode.E)){
                                Dormir(hit.transform);
                            }
                        }
                        if(hit.transform.name == "HouseArranhador"){
                            textoInteracao.text="Aperte E para brincar";
                            if (Input.GetKeyDown(KeyCode.E)){
                                energia-=5;
                                felicidade+=5;
                            }
                        }
                    }
                    if (hit.transform.CompareTag("Interactable"))
                    {
                        if(hit.transform.name == "ParkVendingMachine"){
                            textoInteracao.text="Aperte E para comprar um Energético";
                            if (Input.GetKeyDown(KeyCode.E)){
                                if(petiscos>=20){
                                    petiscos-=20;
                                    GanharEnergetico(1);
                                }
                            }
                        }
                        if(hit.transform.name == "LixeiraGrande"){
                            textoInteracao.text="Aperte E para brincar com o lixo";
                            if (Input.GetKeyDown(KeyCode.E)){
                                energia-=5;
                                felicidade+=10;
                                higiene-=20;
                            }
                        }
                    }
                    
                }
                else{
                    painelInteracao.SetActive(false);
                }
                /* if (Input.GetButtonDown("Fire1"))
                {
                    if (itemSegurado != null)
                    {
                        if(itemSegurado.CompareTag("PeixePickupable")){
                            inventarioDePeixes.peixeNaBoca=null;
                        }
                        SoltaItem();
                    }
                } */
                if (Input.GetKeyDown(KeyCode.B))
                {
                    TomarBanho();
                }
                if(Input.GetKeyDown(KeyCode.K)){
                    UsarEneretico();
                }
                if(Input.GetKey(KeyCode.F9)){
                    if(Input.GetKeyDown(KeyCode.Alpha1)){
                        fome+=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha2)){
                        energia+=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha3)){
                        higiene+=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha4)){
                        felicidade+=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha5)){
                        social+=50;
                    }
                }
                if(Input.GetKey(KeyCode.F8)){
                    if(Input.GetKeyDown(KeyCode.Alpha1)){
                        fome-=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha2)){
                        energia-=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha3)){
                        higiene-=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha4)){
                        felicidade-=50;
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha5)){
                        social-=50;
                    }
                }
            }//Fim do Janela em foco == 1
            //O tempo passa com a pesca aberta, mas não com o jogo pausado
            AlteraValoresBarrinhas();
            //Passagem do tempo quando estiver dormindo, migue pra n usar corotina
            if(dormindo){
                if(timerDormindo>0){
                    timerDormindo-=Time.deltaTime;
                }
                else{
                    Acordar();
                }
            }
            if(banho){
                if(timerBanho>0){
                    timerBanho-=Time.deltaTime;
                }
                else{
                    TerminarBanho();
                }
            }
            if(isMorrendo){
                if(timerMorte<=20f){
                    timerMorte+=Time.deltaTime;
                }
                else{
                    GameManager.Instance.Perder();
                }
                if(felicidade>=5&&fome>=5){
                    isMorrendo=false;
                    timerMorte=0;
                    fundoMorrendo.SetActive(false);
                }
            }
        }//Fim do Jogo Pausado
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fisica"){
            if(quebr < 3)
            {
                felicidade += 5;
                quebr++;
            //depois adicionar como que isso diminui
            }
            else{
                felicidade -= 5;
                }
        }

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Seguravel")
        {
            if (brinc <3)
            {
                brinc++;
                felicidade += 10;
            }
            
            PegaItem(collider.gameObject);
        }
        /* if(collider.CompareTag("PeixePickupable")){
            inventarioDePeixes.peixeNaBoca=collider.gameObject;
            PegaItem(collider.gameObject);
        } */
    }
    void AlteraValoresBarrinhas()
    {
        if(luzManager==null) //Controle Para as barrinhas não serem alteradas se não ouver um
        return;              //luz manager na cena que foi passado como parametro ao jogador
        float ratioPassagemDoTempo= luzManager.ratioPassagemDoTempo;
        if (fome > 100) fome = 100;
        else if (fome < 0){CountDown("fome"); fome = 0;}
        if (energia > 100) energia = 100;
        else if (energia < 0){Desmaiar(); energia = 0;}
        if (higiene > 100) higiene = 100;
        else if (higiene < 0) higiene = 0;
        if (felicidade > 100) felicidade = 100;
        else if (felicidade < 0) {CountDown("felicidade"); felicidade = 0;}
        if (social > 100) social = 100;
        else if (social < 0) social = 0;

        //fome -= 0.0001f; //chamado todo frame, portanto valor*60= quantidade por segundos
        fome = fome - 25* Time.fixedDeltaTime/(ratioPassagemDoTempo*12);
        fomeSldr.value =fome;
        energia -= modEnergia* Time.fixedDeltaTime/(ratioPassagemDoTempo*12);
        energiaSldr.value =energia;
        higiene -= ModHigiene* Time.fixedDeltaTime/(ratioPassagemDoTempo*12);
        higieneSldr.value = higiene;
        felicidade -= 5* Time.fixedDeltaTime/(ratioPassagemDoTempo*12);
        felicidadeSldr.value =felicidade;
        social -= 5* Time.fixedDeltaTime/(ratioPassagemDoTempo*12);
        socialSldr.value = social;
        petiscoText.text = petiscos.ToString();
    }
    public void AtualizaValoresESlidesComInfoDoManager(){
        petiscos = GameManager.Instance.petiscos;
        fome = GameManager.Instance.fome;
        energia = GameManager.Instance.energia;
        higiene = GameManager.Instance.higiene;
        felicidade = GameManager.Instance.felicidade;
        social = GameManager.Instance.social;

        fomeSldr.value = fome;
        energiaSldr.value = energia;
        higieneSldr.value = higiene;
        felicidadeSldr.value = felicidade;
        socialSldr.value = social;
    }

    void TransferStatus()
    {
        GameManager.Instance.petiscos = petiscos;
        GameManager.Instance.fome = fome;
        GameManager.Instance.energia = energia;
        GameManager.Instance.higiene = higiene;
        GameManager.Instance.felicidade = felicidade;
        GameManager.Instance.social = social;
    }

    private void PegaItem(GameObject item)
    {
        itemSegurado = item;
        seguradoRB = itemSegurado.GetComponent<Rigidbody>();

        seguradoRB.isKinematic = true;
        seguradoRB.useGravity = false;
        itemSegurado.transform.SetParent(boca);
        itemSegurado.transform.localPosition = Vector3.zero;
        itemSegurado.transform.localRotation = Quaternion.identity;
    }

    private void SoltaItem()
    {
        seguradoRB.isKinematic = false;
        seguradoRB.useGravity = true;
        itemSegurado.transform.SetParent(null);
        seguradoRB.AddForce(boca.right * -20f, ForceMode.Impulse);
        itemSegurado = null;
    }
    void Dormir(Transform cama){
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Nula;
        dormindo=true;
        transform.position=cama.position;
        luzManager.ratioPassagemDoTempo=1;
        modEnergia=-400;
        animator.SetTrigger("Dormi");
        animator.ResetTrigger("Acordei");
    }
    void Acordar(){
        dormindo=false;
        timerDormindo = tempoDormir;
        modEnergia=10;
        luzManager.ratioPassagemDoTempo=20;
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
        animator.ResetTrigger("Dormi");
        animator.SetTrigger("Acordei");
    }
    void TomarBanho(){
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Nula;//basta ser difente de 1
        banho=true;
        luzManager.ratioPassagemDoTempo=1;
        ModHigiene=-400f;
        modEnergia=20f;
        animator.SetTrigger("Banho");
    }
    void TerminarBanho(){
        banho=false;
        timerBanho=tempoBanho;
        luzManager.ratioPassagemDoTempo=20;
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
        ModHigiene=5f;
        modEnergia=10;
        animator.ResetTrigger("Banho");
    }
    void TrocaCena(int cena){//obs: ao chamar vc precisa mudar seu proprio janela em foco
        TransferStatus();
        /* GameManager.Instance.posGatoNoLoad=transform.position;
        GameManager.Instance.rotGatoNoLoad=transform.rotation; */
        GameManager.Instance.HoraDoDiaAoTrocarCena=luzManager.HoraDoDia;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(cena);
    }
    /* void SpawnaNoLocal(){
        if(GameManager.Instance.posGatoNoLoad!=Vector3.zero){
            transform.position = GameManager.Instance.posGatoNoLoad;
            transform.rotation = GameManager.Instance.rotGatoNoLoad;
        }
        else{
            if(GameManager.Instance.spawnGatoNoLoad!=null){
                transform.position = GameManager.Instance.spawnGatoNoLoad.position;
                transform.rotation = GameManager.Instance.spawnGatoNoLoad.rotation;
            }
            else{//na frente da casa, contantando que a casa n seja movida
                transform.position = new Vector3(288.5f,16.3899994f,311.200012f);
                transform.rotation= Quaternion.Euler(new Vector3(0,-90,0));
            }
        }
    } */
    void Desmaiar(){
        Dormir(transform);
        petiscos-=100;
        if(petiscos<0){
            petiscos=0;
        }
    }
    void CountDown(string barrinha){
        if(barrinha=="fome"){
            isMorrendo=true;
        }
        if(barrinha=="felicidade"){
            isMorrendo=true;
        }
        fundoMorrendo.SetActive(true);
    }
    public void LoadData(GameData data)
    {
        if(GameManager.Instance.overrideSaveToGameManager){
            fome = GameManager.Instance.fome;
            energia = GameManager.Instance.energia;
            higiene = GameManager.Instance.higiene;
            felicidade = GameManager.Instance.felicidade;
            social = GameManager.Instance.social;
            petiscos = GameManager.Instance.petiscos;
            GameManager.Instance.overrideSaveToGameManager=false;//usado para garantir que o resultado do pão vai ser salvo
            nEnergeticos=data.statusData.nEnergeticos;
        }
        else{
            fome = data.statusData.fome;
            energia=data.statusData.energia;
            higiene = data.statusData.higiene;
            felicidade = data.statusData.felicidade;
            social = data.statusData.social;
            petiscos = data.statusData.petiscos;
            nEnergeticos=data.statusData.nEnergeticos;
        }

    }
    public void SaveData(GameData data)
    {
        StatusData novaData= new StatusData(fome,energia,higiene,felicidade,social,petiscos,timerMorte,nEnergeticos);
        data.statusData = novaData;
    }
    public void GanharEnergetico(int i){
        nEnergeticos+=i;
        GameEventsManager.instance.uiEvents.AtualizarEnergeticos(nEnergeticos);
    }
    public void UsarEneretico(){
        if(nEnergeticos>0){
            GameEventsManager.instance.playerEvents.PlayerUsesEnergyDrink();
            nEnergeticos--;
            GameEventsManager.instance.uiEvents.AtualizarEnergeticos(nEnergeticos);
        }
        else{
            //um barulinho, sei la
        }
    }
    public void VenderPlanta(int valor){
        petiscos+=valor;
    }
    void AlterarPainelQuest(bool ativarPainel,QuestState questState){
        if(ativarPainel){
            painelInteracaoQuests.SetActive(true);
            switch(questState){
            case QuestState.CAN_START:
                textoInteracaoQuest.text = "Aperte enter para comecar a quest";
            break;
            case QuestState.CAN_FINISH:
                textoInteracaoQuest.text = "Aperte enter para entregar a quest";
            break;
            default:painelInteracaoQuests.SetActive(false);
            break;
            }
        }
        else{
            painelInteracaoQuests.SetActive(false);
        }
    }
    void GanharPetiscos(int valor){
        petiscos+=valor;
    }
}

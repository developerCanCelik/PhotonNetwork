using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI logText;
    void Start()
    {        
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.ConnectToBestCloudServer();
    }
    /*OnDisconnected: Oyuncu bağlantıyı kayıp ettiğinde koptuğunda bu fonksiyon çalışacak. Ve bağlantıyı kayıp ettiği odaya tekrardan
    bağlama işlemi gerçekleştirmemiz gereken kısım.*/
    public override void OnDisconnected(DisconnectCause cause)
    {
        LogTextFunc("Bağlantı Koptu");
        //PhotonNetwork.ReconnectAndRejoin();
        /*
        Eğer aynı odaya değil de başka bir odaya bağlanmasını istiyorsak aşağıdaki kodu yazmamız gerekiyor
        PhotonNetwork.RejoinRoom("mevcut oda isimlerinden biri");
        */
        base.OnDisconnected(cause);
    }
    public override void OnConnectedToMaster()
    {
        LogTextFunc("Server bağlantısı gerçekleştirildi.");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        LogTextFunc("Lobby bağlantısı gerçekleştirildi.");
        /* PhotonNetwork.NickName: Odaya giriş yapan kişinin nickname buradan verebiliyoruz.*/
        PhotonNetwork.NickName="Can";
        PhotonNetwork.JoinOrCreateRoom("oda isim", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        LogTextFunc("Odaya giriş yapıldı");
        /*PhotonNetwork.LocalPlayer.NickName: Kendi nickname alıyoruz.*/
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);

        //Aşağıdaki değerler 5 saniyede bir yenilenir.
        /*PhotonNetwork.CountOfPlayers: Oyunda bulunan toplam oyuncu sayımızı alabiliyoruz.*/
        Debug.Log(PhotonNetwork.CountOfPlayers);
        /*PhotonNetwork.CountOfRooms: Oyunda olan toplam oda sayısını alabiliyoruz.*/
        Debug.Log(PhotonNetwork.CountOfRooms);
        /*PhotonNetwork.CountOfRooms: Anlık olarak odalarda oyun oynayan oyuncuların sayısını verir.*/
        Debug.Log(PhotonNetwork.CountOfPlayersInRooms);
        /*PhotonNetwork.CountOfRooms: Anlık olarak oda arayan oyuncu sayısını verir. Yani Lobby de bekleyen oyuncu sayısı.*/
        Debug.Log(PhotonNetwork.CountOfPlayersOnMaster);
        /*PhotonNetwork.CurrentRoom: Bulunmuş olduğumuz odanın option değerlerini bize verir. Örneğin oda iki kişilik odada bulunan kişi sayısı 1*/
        Debug.Log(PhotonNetwork.CurrentRoom);
        /*PhotonNetwork.CurrentLobby: Bulunmuş olduğumuz lobby alabiliriz.*/
        Debug.Log(PhotonNetwork.CurrentLobby);
        /*PhotonNetwork.CloudRegion: Bağlantı kurmuş olduğumuz bölgeyi alabiliyoruz.*/
        Debug.Log(PhotonNetwork.CloudRegion);
    }
    public override void OnLeftRoom()
    {
        LogTextFunc("Odadan çıkıldı.");
    }  
    public override void OnLeftLobby()
    {
        LogTextFunc("Lobby'den çıkıldı");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        LogTextFunc("Odaya girilemedi" + message + "-" + returnCode);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LogTextFunc("Random odaya girilemedi" + message + "-" + returnCode);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        LogTextFunc("Odaya oluşturulamadı" + message + "-" + returnCode);
    }
    void LogTextFunc(string value){
        logText.text = value;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PhotonNetwork.LeaveLobby();
        }
        /*
        Burada şu işlem gerçekleşiyor.
        Örneğin oyuncu oyunu bitirdi skoru ekliycez, eğer oyuncu bağlı değilse ve skor eklemeye çalışırsak hataya düşeriz yani hata alırız.
        Ondan kaynaklı bağlı olup olmadığını kontrol ediyoruz. Ve bağlı olduğu taktirde örneğin skor eklemesini yapabiliriz.
        */
        if(Input.GetKeyDown(KeyCode.Space)){
            if(PhotonNetwork.IsConnected)
                LogTextFunc("Bağlısın");
            else
                LogTextFunc("Bağlı Değilsin");
        }
         /*
        Burada şu işlem gerçekleşiyor.
        Bağlısındır ve oyuncunun eşleşmelere hazır olup olmadığını kontrol eder.
        */
        if(Input.GetKeyDown(KeyCode.A)){
            if(PhotonNetwork.IsConnectedAndReady)
                LogTextFunc("Bağlısın ve hazırsın");
            else
                LogTextFunc("hazır değilsin");
        }
        /*
        Burada şu işlem gerçekleşiyor.
        Oyuncu odada mı yoksa lobby de mi kontrolleri gerçekleştiriyoruz.
        Örneğin oyuncuyu odaya bağlıycaksın ama odaya bağlı olmadan önce lobby de bulunması gerekiyor. İşte o anda
        bu kodu çalıştırabilirsin.
        */
        if(Input.GetKeyUp(KeyCode.Space)){
            if(PhotonNetwork.InLobby)
                LogTextFunc("Lobbydesin");
            else if(PhotonNetwork.InRoom)
                LogTextFunc("Odadasın");
            else
                LogTextFunc("Bağlı Değilsin");
        }
         /*
        Burada şu işlem gerçekleşiyor.
        Odanın kurucusu olup olmadığımızı anlamamızı sağlıyor
        Bazı oyunlarda kurucunun bir önemi yoktur. Ancak ikili oyunların bazılarında odayı kuran oyuncuların önemi mevcut olabiliyor. İşte
        bu tarz durumlarda aşağıdaki method kullanılır.
        */
        if(Input.GetKeyUp(KeyCode.B)){
            if(PhotonNetwork.IsMasterClient)
                LogTextFunc("Kurucusun");
            else
                LogTextFunc("Kurucu değilsin");
        }

    }
    /*-----Butonlar ile işlemler yaptığımız bölüm.-----*/
    //Bağlantıyı koparıyoruz.
    public void DisconnectFunc(){
        PhotonNetwork.Disconnect();
    }
    //Yeniden bağlanıyoruz
    public void ReconnectFunc(){
        PhotonNetwork.Reconnect();
    }
    //İstatistikleri alıyoruz
    /*
    Neden istatistiklere ihtiyaç duyarız.
    Biz bağlantı sırasında sürekli veri alışverişi gerçekleşiyor. Ve bu işlemleri yaparken belirli istatistiklerimiz mevcut.
    Yani ne kadar zamandır veri alışverişi gerçekleşiyor? Veri gönderip almak ne kadar sürüyor? gibi gibi istatistiklerimiz mevcut. 
    Ve biz bunlara ihtiyaç duyabiliriz. 
    */
    public void GetStatisticsFunc(){
        //Oyun başlar başlamaz da istatistikleri alabilirsin.
        PhotonNetwork.NetworkStatisticsEnabled = true;
        Debug.Log(PhotonNetwork.NetworkStatisticsToString());
    }
    //İstatistikleri sıfırla
    public void ResetStatisticsFunc(){
        PhotonNetwork.NetworkStatisticsReset();
    }
    //Ping bilgilerini getiriyoruz.
    public void FetchPingFunc(){
        //Ping: Bizim gönderdiğimiz verinin sunucuya gitme sırasındaki arada geçen zamanı belirtir.
        //!Ping ne kadar düşük olursa oyun akıcı olacaktır. !Ping fazla olduğu taktirde oyunda atlamalara ve bugs sebep olacaktır.
        //Ping düşük ise server ile oyuncu arasındaki iletişim kuvvetli olduğunu gösterir.
        LogTextFunc(PhotonNetwork.GetPing().ToString());
    }


}

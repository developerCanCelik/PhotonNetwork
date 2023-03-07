using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerExample2 : MonoBehaviourPunCallbacks
{
   /*
   !Player --> Server --> Lobby --> Room --> Game
   */
   private void Start()
   {
      PhotonNetwork.ConnectUsingSettings();
   }
   /*OnConnected: Oyuncunun ham olarak bağlantının olup olmamasını bize döndürür.*/
   public override void OnConnected()
   {
      Debug.Log("Ham server bağlantı sağlandı");
      base.OnConnected();
   }
   /*OnConnectedToMaster: Oyuncunun bağlantısını yapar lobby ya da room'a bağlanmaya hazır mıyım gibi işlemleri sağlamak için hazır olup olmadığını
   sağlar.*/
   public override void OnConnectedToMaster()
   {
      Debug.Log("Oyuncu her şey için hazır. Server bağlantısı tamam lobby bağlanabilir.");
      PhotonNetwork.JoinLobby();
      base.OnConnected();
   }
   /*OnJoinedLobby: Oyuncu Lobby girip girmediğini bize bildirir.*/
   public override void OnJoinedLobby()
   {
      Debug.Log("Lobby bağlanıldı");
      Debug.Log("Eğer kayıtlı oda varsa giriş yapılıyor. Yok ise oluşturulup giriş yapılıyor");
      /*Lobby den sonrası tamamen isteğimize bağlıdır.*/
      PhotonNetwork.JoinOrCreateRoom("Random Name Create", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
      base.OnJoinedLobby();
   }
   /*OnCreateRoomFailed: Oda oluşturma esnasında sistemsel bir hata var ise buradan geri dönüş sağlanıyor.*/
   public override void OnCreateRoomFailed(short returnCode, string message)
   {
      Debug.Log("Oda oluşturulamadı ve giriş yapılamadı" + message + "-" + returnCode);
      base.OnCreateRoomFailed(returnCode, message);
   }
   /*OnJoinedRoom: Oda giriş yapma işlemi geçildi.*/
   public override void OnJoinedRoom(){
      Debug.Log("Odaya giriş yapıldı");
   }
   /*OnJoinRoomFailed: Eğer direkt giriş yapma isteği olursa giriş yapılamazsa bu fonksiyon çalışacak*/
   public override void OnJoinRoomFailed(short returnCode, string message){
      Debug.Log("Odaya girilemedi" + message + "-" + returnCode);
   }
   /*OnJoinRandomFailed: Eğer random odaya giriş yapma isteği olursa giriş yapılamazsa bu fonksiyon çalışacak*/
   public override void OnJoinRandomFailed(short returnCode, string message){
      Debug.Log("Random odaya girilemedi" + message + "-" + returnCode);
   }
   private void Update() {
      if(Input.GetKeyDown(KeyCode.Escape)){
         /*Odadan çıkma işleminde bulunuyoruz.*/
         PhotonNetwork.LeaveRoom();
      }
      if(Input.GetKeyDown(KeyCode.Tab)){
         /*Lobbyden çıkma işleminde bulunuyoruz.*/
         PhotonNetwork.LeaveLobby();
      }
   }
    public override void OnLeftRoom()
    {
      Debug.Log("Odadan çıkma işlemi gerçekleşti");
      base.OnLeftRoom();
    }
    public override void OnLeftLobby()
    {
      Debug.Log("Lobby den çıkma işlemi gerçekleşti");
      base.OnLeftLobby();
    }
}

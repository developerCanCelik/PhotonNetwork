using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerExample1 : MonoBehaviourPun
{
     /*
    !Multiplayer sistem şu şekilde ilerlemekte;
        1)Oyuncu servera bağlanır.
        2)Lobby'e giriş ypar.
        3)Odaya geçiş yapar.
        4)Oyunu oynamaya başlar.
    !Player --> Server --> Lobby --> Room --> Game
    !Server tarafında her durumun bir cevabı olması gerekir. Her duruma ait bir cevap döner. Yani bağlantı isteği gider bağlantı
    yapılıp yapılamadığını internetin olup olmadığının cevabı döner. Bu cevap dönme işlemi ise Callback fonks. gerçekleşir.
    */
    void Start()
    {
        /*PhotonNetwork.ConnectUsingSettings(); --> Sunucu ile bağlantı kuruyoruz*/
        PhotonNetwork.ConnectUsingSettings();
        /*PhotonNetwork.JoinLobby(); --> Sunucudan lobby'e geçiş sağlıyoruz.*/
        PhotonNetwork.JoinLobby();
        /*PhotonNetwork.JoinRoom(); --> Lobby'den odaya geçiş yapıyoruz. (Odaya geçiş yaparken random veya oda adına göre geçiş yapar ya da kendi bir oda kurar)*/
        //Oda ismine göre geçiş yapıyoruz.
        PhotonNetwork.JoinRoom("Room Name");
        //Random odaya geçiş yapıyoruz.
        PhotonNetwork.JoinRandomRoom();
        //Yeni bir oda oluşturuyoruz. (Oda ismi, Oda opsiyonlarını beliriliyoruz (kaç oyuncu olacak, oda girilebilip girilemediğini belirtilen kısım, oda görünebilir mi görünemez mi), Lobi türünü de belirliyoruz. Yani ana lobi olduğunu)
        PhotonNetwork.CreateRoom("Random Name Create", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        //Yukarıdakinden farklı olarak o isimde oda varsa gir eğer yoksa odayı kur ve içeriye gir demek
        PhotonNetwork.JoinOrCreateRoom("Random Name Create", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        //Odayı tek etme methodu
        PhotonNetwork.LeaveRoom();
        //Lobbyden çıkma
        PhotonNetwork.LeaveLobby();
    }
}

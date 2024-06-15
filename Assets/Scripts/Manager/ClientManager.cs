 
    using System;
    using System.Net.Sockets;
    using Common;
    using UnityEngine;

    public class ClientManager:BaseManager
    {
        Socket socket;
       const string ip= "127.0.0.1";
        const int  port=8888;
        private Message message;
        public override void OnDestory()
        {
            socket.Close();
        }
        public override void OnInit()
        {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect( ip, port);
        message = new Message();
        }
        public void ReceiveRequest()
        {
            socket.BeginReceive(message.Data,message.StartIndex,message.RemingSize,SocketFlags.None,ReceiveCallback,null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int count = socket.EndReceive(ar);
                message.ReadData(count,OnReceiveRequest);
                ReceiveRequest();
            } catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        private void OnReceiveRequest(RequestCode requestCode, string serverMessage)
        {
         GameFacade.Instance.HandleRequest(requestCode,serverMessage);   
        }
        public void SendRequest( RequestCode requestCode,string clientMessage)
        {
            byte[] data = Message.WriteData(requestCode,clientMessage); 
            socket.BeginSend(data,0,data.Length,SocketFlags.None, SendCallback, null);
        }

        private void SendCallback(IAsyncResult ar)
        {
            socket.EndSend(ar);
        }
    }
   
 
using System;
using System.Linq;
using System.Text;
using Common;
public class Message
{
    private byte[] data=new byte[1024];
    private int startIndex;//读取数据的起始位置
    
    public byte[] Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }
    public int StartIndex
    {
        get
        {
            return startIndex;
        }
        set
        {
            startIndex = value;
        }
    }
    public int RemingSize// 剩余可读字节数
    {
        get
        {
            return data.Length-startIndex;
        }
    }

    public void  ReadData(int newDataAmount,Action< ActionCode,string> action)
    {
        startIndex += newDataAmount;
        while ( true)
        {
            if (startIndex<=4)
            {
                return;
            }
            int count = BitConverter.ToInt32(data, 0);
            if (startIndex-4>count)
            {
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string message = Encoding.UTF8.GetString(data, 8, startIndex-4);
                action(actionCode,message);
                Array.Copy(data, count+4,data, 0, startIndex-4-count);
                startIndex -= count + 4;
            }
            else
            {
                break;
            }
        }
    }
     public static byte[] WriteData(ActionCode actionCode,string message)
    {
       byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
       byte[] messageBytes = Encoding.UTF8.GetBytes(message);
       int count = requestCodeBytes.Length+messageBytes.Length;
       byte[] countBytes = BitConverter.GetBytes(count);
       byte[] newbytes= countBytes.Concat(requestCodeBytes).ToArray<byte>();
      return  newbytes.Concat(messageBytes).ToArray();
    }
    public static byte[] WriteData(RequestCode requestCode,ActionCode actionCode,string message)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        int count = requestCodeBytes.Length+messageBytes.Length+ actionCodeBytes.Length;
        byte[] countBytes = BitConverter.GetBytes(count);
        byte[] newbytes= countBytes.Concat(requestCodeBytes).ToArray<byte>();
        newbytes=newbytes.Concat(actionCodeBytes).ToArray<byte>();
        return  newbytes.Concat(messageBytes).ToArray();
    }
}
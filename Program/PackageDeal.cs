using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace Zony_Lrc_Download_2._0
{
    /// <summary>
    /// 数据包包头
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Package_Head
    {
        public byte type;   // 请求类型
        public int lenght;  // 数据长度
    }

    /// <summary>
    /// 数据包
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Package
    {
        public Package_Head head; // 数据包头
        public byte[] data;        // 数据
    }
    public static class PackageDeal
    {
        /// <summary>
        /// 当前程序版本
        /// </summary>
        public const int CurrentVersion = 2700;
        public enum PType
        {
            /// <summary>
            /// 更新标识
            /// </summary>
            UPDATE=10,
            /// <summary>
            /// 请求更新地址
            /// </summary>
            Result = 11
        }
        /// <summary>
        /// 将字节流转换为结构体
        /// </summary>
        /// <param name="bytes">要转换的字节流</param>
        /// <param name="type">结构体类型</param>
        /// <returns>转换好的结构体</returns>
        private static object BytesToStruct(byte[] bytes,Type type)
        {
            int size = Marshal.SizeOf(type);
            if( size > bytes.Length)
            {
                return null;
            }
            IntPtr structPtr = Marshal.AllocHGlobal(size);  // 开辟内存空间
            Marshal.Copy(bytes, 0, structPtr, size);    // 复制到内存空间
            object obj=Marshal.PtrToStructure(structPtr,type);
            return obj;
        }
        /// <summary>
        /// 将结构体转换为字节流
        /// </summary>
        /// <param name="obj">传入的结构体</param>
        /// <returns>返回字节流</returns>
        private static byte[] StructToBytes(object obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, structPtr, false);
            
            Marshal.Copy(structPtr, bytes, 0, size);
            return bytes;
        }

        /// <summary>
        /// 从套接字读取数据包
        /// </summary>
        /// <param name="fd">Socket描述符</param>
        /// <returns>返回的数据包</returns>
        public static Package Package_Read(Socket fd)
        {
            Package p = new Package();
            // 获得包头数据
            byte[] head = new byte[Marshal.SizeOf(p.head)];
            fd.Receive(head);
            p.head = (Package_Head)BytesToStruct(head,p.head.GetType());
            // 接受数据
            byte[] data = new byte[p.head.lenght];
            fd.Receive(data);
            p.data = data;
            return p;
        }

        public static void Package_Write(Socket fd,Package p)
        {
            fd.Send(StructToBytes(p.head));
            if(p.data!=null)
            {
                fd.Send(p.data);
            }
        }

        public static Package Package_New(byte type,int lenght,byte[] data)
        {
            Package tmp = new Package();
            tmp.head.type = type;
            tmp.head.lenght = lenght;
            tmp.data = data;
            return tmp;
        }
    }
}

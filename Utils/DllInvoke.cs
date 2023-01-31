using System;
using System.IO;
using System.Runtime.InteropServices;

namespace OdeMod.Utils
{
    /// <summary>
    /// 此类用于加载非托管库
    /// </summary>
    internal class DllInvoke
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr lib);

        private IntPtr hLib;

        public DllInvoke(string dllPath)
        {
            hLib = LoadLibrary(dllPath);
            if (hLib == IntPtr.Zero)
                throw new Exception("非托管库加载错误！请检查文件格式是否正确或文件是否被损坏！");
        }

        ~DllInvoke()
        {
            FreeLibrary(hLib);
        }

        /// <summary>
        /// 用于将非托管库中的函数转换为委托
        /// </summary>
        /// <typeparam name="T">托管的非泛型类型(非Func或Action)</typeparam>
        /// <param name="apiName">函数名称</param>
        /// <returns>转换成的托管实例</returns>
        public T Invoke<T>(string apiName) where T : Delegate
        {
            return Marshal.GetDelegateForFunctionPointer<T>(GetProcAddress(hLib, apiName));
        }
    }
}
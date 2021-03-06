﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Text;


namespace client
{
    static class Program
    {
        public static int key = 0;
        public static bool notClosed = true;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread log = new Thread(() => Application.Run(new LogForm()));
            log.Start();
            Application.Run(new LoginScreen());


            var log2 = Application.OpenForms.OfType<LogForm>().Single();
            log2.Invoke((MethodInvoker)delegate
            {
                log2.closeLog();
            });
        }

        public static List<string> StrSplit(string str, char ch)
        {//split messages to get rid of '##'
            string[] values = str.Split(ch);
            List<string> toRet = new List<string>();

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != "")
                {
                    toRet.Add(values[i]);
                }
            }

            return toRet;
        }

        public static string Encrypto(string msg)
        {//same as server
            string t = "";
            for (int i = 0; i < msg.Length; i++)
            {
                t += (char)((int)(msg[i]) ^ key);
            }

            string a = "";

            for (int i = 0; i < t.Length; i++)
            {
                a += t[i];
            }
            a += (char)0;

            return a;
        }

        public static string Decrypto(string msg)
        {//same as server
            string t = "";

            for (int i = 0; i < msg.Length; i++)
            {
                t += (char)((int)(msg[i]) ^ key);
            }

            string a = "";

            for (int i = 0; i < t.Length; i++)
            {
                a += t[i];
            }

            return a;
        }

        public static void SendMsg(NetworkStream sock, string msg)
        {//send the messages for the whole program
            var log = Application.OpenForms.OfType<LogForm>().Single();
            log.Invoke((MethodInvoker)delegate { log.SetLog("Sent: " + msg + "\n"); });

            if (key != 0)
            {
                msg = Encrypto(msg);
            }

            byte[] buffer = new ASCIIEncoding().GetBytes(msg);
            sock.Write(buffer, 0, msg.Length);
            sock.Flush();
        }

        public static string RecvMsg(NetworkStream sock)
        {//recieve the messages for all the program
            bool endFound = false;

            //recive signin answer
            byte[] bufferIn = new byte[4096];
            int bytesRead = sock.Read(bufferIn, 0, 4096);
            string input = new ASCIIEncoding().GetString(bufferIn);
            for (int i = 0; i < input.Length && !endFound; i++)
            {
                if (input[i] == '\0')
                {
                    endFound = true;
                    input = input.Substring(0, i);
                }
            }

            if (key != 0)
            {
                input = Decrypto(input);
            }

            var log = Application.OpenForms.OfType<LogForm>().Single();
            log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "Recived: " + input + "\n\n"); });

            return input;
        }

        public static void sendAndRecieveKey(NetworkStream sock)
        {//same as server's explanation
            int g = 0, p = 0;

            while (key == 0)
            {
                string msg = RecvMsg(sock);
                List<string> inputs = StrSplit(msg, '#');

                g = Int32.Parse(inputs[0]);
                p = Int32.Parse(inputs[1]);

                Random rnd = new Random();
                int r2 = rnd.Next();

                int num2 = (int)(Math.Pow(g, r2)) % p;
                int num1 = Int32.Parse(RecvMsg(sock));

                SendMsg(sock, num1.ToString());

                int s2 = (int)(Math.Pow(num1, r2)) % p;
                s2 = s2 % 100;

                var log = Application.OpenForms.OfType<LogForm>().Single();
                log.Invoke((MethodInvoker)delegate { log.SetLog(log.GetLog() + "\n\nKey: " + Math.Abs(s2)); });

                key = Math.Abs(s2);
            }
        }
    }
}

using System;
using System.Net.Sockets;
using System.IO;
using Assets.Scripts.Util;
using Assets.Scripts.CBR.Model;
using System.Threading;
using System.Text;

namespace Assets.Scripts.Connection
{

    /**
     * Diese Klasse stellt die Verbindung via TCP/IP zum Java Projekt her.
     */
    public class Connection
    {

        /**
         * TCP-Client
         */
        private TcpClient mClient;
        /**
         * Data Stream.
         */
        private Stream mStream;

        /**
         * Diese Methode stellt konkret die Verbindung her.
         */
        private void InitiateConnection()
        {
            mClient = new TcpClient();
            mClient.Connect(Constants.HOST_ADDRESS, Constants.PORT);
            mStream = mClient.GetStream();
        }

        ~Connection()
        {
            Console.WriteLine("Connection closed");
            CloseConnection();

        }

        /**
         * Diese Methode schließt die Verbindung zwischen C# und Java.
         */
        private void CloseConnection()
        {
            if (mClient != null && mClient.Connected)
            {
                Console.WriteLine("Shutting down TCP/IP");
                mClient.Close();
            }
        }

        /**
         * Diese Methode sendet eine konkrete Anfrage des Playeragenten an das Java System, erhält als Antwort einen Plan, der ausgeführt werden soll
         * und gibt diesen als Response-Objekt zurück.
         */
        public Response Send(Request request)
        {

            InitiateConnection();


            string json = JsonParser<Request>.SerializeObject(request) + Environment.NewLine;
            
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] data = asen.GetBytes(json);

            mStream.Write(data, 0, data.Length);

            Thread.Sleep(100);

            byte[] responseData = new byte[1024];
            string textReceived = "";
            int read = 0;
            do
            {
                read = mStream.Read(responseData, 0, responseData.Length);
                for (int i = 0; i < read; i++)
                {
                    textReceived += (char)responseData[i];
                }
            } while (read > 0);

            Constants.WriteToFile("Answer is: " + textReceived);

            Response response = JsonParser<Response>.DeserializeObject(textReceived);

            string res = string.Format("\"{0}\" to \"{1}\" is \"{2}\"", request.ToString(), response.ToString(), Environment.NewLine);

            Constants.WriteToFile(res);


            CloseConnection();

            return response;
        }

    }

}

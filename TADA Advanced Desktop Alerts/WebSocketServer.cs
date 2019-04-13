using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TADA_Advanced_Desktop_Alerts
{
    internal class WebSocketServer
    {
        private HttpListener httpListener;
        private Dictionary<General.ALERTTYPES, List<WebSocket>> listeningWebSockets = new Dictionary<General.ALERTTYPES, List<WebSocket>>();
        private Dictionary<WebSocket, List<General.ALERTTYPES>> listeningWebSocketDictionary = new Dictionary<WebSocket, List<General.ALERTTYPES>>();

        Thread listeningThread;

        // Maybe make this not a magic number later.
        public WebSocketServer(int port = 21573)
        {
            listeningThread = new Thread(() => InitializeWebSocketServer(port));
            if (listeningThread != null)
            {
                listeningThread.Start();
                MainTADAForm.mainTADAForm.webSocketServer = this;
            }
        }

        private void InitializeWebSocketServer(int port = 21573)
        {
            General.LogFile(String.Format("Starting WebSocket on port {0}.", port));

            httpListener = new HttpListener();
            httpListener.Prefixes.Add(String.Format("http://localhost:{0}/", port));
            httpListener.Prefixes.Add(String.Format("http://127.0.0.1:{0}/", port));
            httpListener.Prefixes.Add(String.Format("http://[::1]:{0}/", port));

            try
            {
                httpListener.Start();
                ListenForever();
            }
            catch (HttpListenerException)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("The requested port, {0}, is already in use.  A websocket will not be set up for this TADA instance.", port), "Port In Use", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                httpListener = null;
                MainTADAForm.mainTADAForm.webSocketServer = null;
            }
        }

        public async void ListenForever()
        { 
            while (true)
            {
                try
                {
                    HttpListenerContext context = httpListener.GetContext();
                    if (context.Request.IsWebSocketRequest)
                    {
                        List<General.ALERTTYPES> listeningTypes = new List<General.ALERTTYPES>();

                        // Client-side, 'var ws = new WebSocket("ws://localhost:21573", ["alert type 1", "alert type 2", ...]);'
                        if (context.Request.Headers["Sec-WebSocket-Protocol"] != null)
                        {
                            foreach (String type in context.Request.Headers["Sec-WebSocket-Protocol"].Split(','))
                            {
                                String formattedType = type.Trim().Replace('_', ' ');
                                // Semi-convoluted way of getting the requested alert types.
                                if (General.alertTypes.ContainsValue(formattedType))
                                {
                                    listeningTypes.Add(General.alertStrings[formattedType]);
                                }
                            }
                        }

                        if (listeningTypes.Count > 0)
                        {
                            HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync("success");
                            WebSocket webSocket = webSocketContext.WebSocket;

                            foreach (General.ALERTTYPES alertType in listeningTypes)
                            {
                                if (!listeningWebSockets.ContainsKey(alertType))
                                {
                                    listeningWebSockets.Add(alertType, new List<WebSocket>());
                                }

                                listeningWebSockets[alertType].Add(webSocket);
                            }
                            listeningWebSocketDictionary.Add(webSocket, listeningTypes);
                        }
                        else
                        {
                            HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync("false");
                            await webSocketContext.WebSocket.CloseOutputAsync(WebSocketCloseStatus.ProtocolError, "No valid SubProtocols.", CancellationToken.None);
                        }
                    }
                }
                catch (System.Net.HttpListenerException)
                {
                    // This is to catch the "there is no listener anymore" exception that happens when the program is closed.
                    break;
                }
                catch (ObjectDisposedException)
                {
                    // You can't listen to a disposed object.  More cleanup safety.
                    break;
                }
            }
        }

        private void SendMessage(WebSocket webSocket, String message)
        {
            webSocket.SendAsync(new ArraySegment<byte>(System.Text.Encoding.ASCII.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async void TellConnectedSockets(SubstitutionObject subObject)
        {
            General.ALERTTYPES alertType = General.alertStrings[subObject.AlertType];
            if (listeningWebSockets.ContainsKey(alertType) && listeningWebSockets[alertType].Count > 0)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                String serializedObject = serializer.Serialize(subObject);
                foreach (WebSocket webSocket in listeningWebSockets[alertType])
                {
                    if (webSocket.State == WebSocketState.CloseSent || webSocket.State == WebSocketState.Closed)
                    {
                        foreach (General.ALERTTYPES type in listeningWebSocketDictionary[webSocket])
                        {
                            listeningWebSockets[type].Remove(webSocket);
                        }
                        listeningWebSocketDictionary.Remove(webSocket);

                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "WebSocket closed remotely.", CancellationToken.None);
                    }
                    else
                    {
                        SendMessage(webSocket, serializedObject);
                    }
                }
            }
        }

        public int Connections()
        {
            return listeningWebSocketDictionary.Keys.Count;
        }

        public void StopListening()
        {
            httpListener?.Close();
        }
    }
}

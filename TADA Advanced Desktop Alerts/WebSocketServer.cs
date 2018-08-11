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

        public WebSocketServer(int port = 21573)
        {
            General.LogFile(String.Format("Starting WebSocket on port {0}.", port));

            httpListener = new HttpListener();
            httpListener.Prefixes.Add(String.Format("http://localhost:{0}", port));
            httpListener.Prefixes.Add(String.Format("http://127.0.0.1:{0}", port));
            httpListener.Prefixes.Add(String.Format("http://[::1]:{0}", port));

            httpListener.Start();
            ListenForever();
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
                                // Semi-convoluted way of getting the requested alert types.
                                if (General.alertTypes.ContainsValue(type.Trim()))
                                {
                                    listeningTypes.Add(General.alertStrings[type.Trim()]);
                                }
                            }
                        }

                        if (listeningTypes.Count > 0)
                        {
                            HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                            foreach (General.ALERTTYPES alertType in listeningTypes)
                            {
                                listeningWebSockets[alertType].Add(webSocketContext.WebSocket);
                            }
                        }
                        else
                        {
                            HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
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

        public void StopListening()
        {
            httpListener.Close();
        }
    }
}

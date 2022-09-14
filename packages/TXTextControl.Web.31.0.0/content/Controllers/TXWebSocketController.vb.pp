Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web
Imports System.Web.Http
Imports TXTextControl.Web

Namespace $rootNamespace$.Controllers

	Public Class TXWebSocketController
		Inherits ApiController

		Public Function [Get]() As HttpResponseMessage
			If HttpContext.Current.IsWebSocketRequest Then
				Dim wsHandler = New WebSocketHandler()
				wsHandler.ProcessRequest(HttpContext.Current)
				Return New HttpResponseMessage(HttpStatusCode.SwitchingProtocols)
			End If
			Return New HttpResponseMessage(HttpStatusCode.OK)
		End Function

	End Class

End Namespace

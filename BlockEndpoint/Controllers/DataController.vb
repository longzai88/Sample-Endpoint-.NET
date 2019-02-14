Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json
Imports System.IO
Imports System.Web.Http.Results

Namespace Controllers
    Public Class DataController
        Inherits ApiController

        ' GET: api/Data
        Public Function GetData() As JsonResult(Of Object)
            Return FormatBlock()
        End Function

        ' GET: api/Data/5
        Public Function GetValue(ByVal id As Integer) As JsonResult(Of Object)
            Return FormatBlock(id)
        End Function

        Private Function FormatBlock(Optional ByVal id As Integer = 0) As JsonResult(Of Object)
            Dim raw As String
            Dim data As Data
            Dim result As Object = New Object

            Try
                raw = File.ReadAllText(HttpContext.Current.Server.MapPath("~/data.json"))
                data = JsonConvert.DeserializeObject(Of Data)(raw)

                If id = 0 Then
                    result = data
                Else
                    result = data.Result.Where(Function(s) s.BlockNumber = id)
                End If
            Catch ex As Exception
                'Handle exception
            End Try
            Return Json(result)
        End Function
    End Class
End Namespace
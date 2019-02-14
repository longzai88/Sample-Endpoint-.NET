Imports Newtonsoft.Json
Imports BlockEndpoint.Data
Imports Newtonsoft.Json.Linq

Public Class Data

    Private Property Status As String
    Private Property Message As String
    Public Property Result As List(Of Result)

End Class

Public Class Result
    Private Property Address As String
    Private Property Topics As String()
    Private Property Data As String

    <JsonConverter(GetType(HexStringJsonConverter))>
    Public Property BlockNumber As String

    <JsonConverter(GetType(HexTimestampJsonConverter))>
    Public Property TimeStamp As String

    <JsonConverter(GetType(HexStringJsonConverter))>
    Public Property GasPrice As String

    <JsonConverter(GetType(HexStringJsonConverter))>
    Public Property GasUsed As String

    Private Property LogIndex As String
    Public Property TransactionHash As String
    Private Property TransactionIndex As String
End Class

Public Class HexStringJsonConverter
    Inherits JsonConverter

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        writer.WriteValue(value)
    End Sub

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim result As String = ""

        If reader.Value.ToString.StartsWith("0x") Then
            result = Convert.ToUInt64(reader.Value, 16)
        End If

        Return result
    End Function

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Throw New NotImplementedException()
    End Function
End Class

Public Class HexTimestampJsonConverter
    Inherits JsonConverter

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        writer.WriteValue(value)
    End Sub

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim ticks As String = ""
        Dim dt As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0)

        If reader.Value.ToString.StartsWith("0x") Then
            ticks = Convert.ToInt32(reader.Value, 16)
            dt = dt.AddSeconds(ticks)
        End If

        Return dt.ToString("dd/MM/yyyy HH:mm:ss")
    End Function

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
<%@ WebHandler Language="VB" Class="UploadVB" %>


Imports System.Web
Imports System.IO

Public Class UploadVB : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim postedFile As HttpPostedFile = context.Request.Files(0)
        Dim savepath As String = ""
        Dim tempPath As String = ""

        tempPath = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
        savepath = context.Server.MapPath(tempPath)
        Dim filename As String = postedFile.FileName

        If Not Directory.Exists(savepath) Then
            Directory.CreateDirectory(savepath)
        End If

        Dim KeyGen As RandomKeyGenerator
        Dim NumKeys As Integer
        Dim i_Keys As Integer
        Dim RandomKey As String

        ''' MODIFY THIS TO GET MORE KEYS    - LAITH - 27/07/2005 22:48:30 -
        NumKeys = 20

        KeyGen = New RandomKeyGenerator()
        RandomKey = KeyGen.Generate()

        Dim fileExtension = Path.GetExtension(filename)


        postedFile.SaveAs((savepath & "\") + RandomKey + fileExtension)
        context.Response.ContentType = "text/plain"
        Dim serializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim result = New With {Key .name = filename, Key .fileName = RandomKey + fileExtension}
        context.Response.Write(serializer.Serialize(result))

        'context.Response.Write((tempPath & "/") + filename)
        'context.Response.StatusCode = 200
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
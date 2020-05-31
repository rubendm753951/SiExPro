Imports System.IO

Public Class PrinterHelper

    Public Sub New()
    End Sub

    Public Shared Sub PrintWebControl(ByVal ctrl As Control)
        PrintWebControl(ctrl, String.Empty)
    End Sub

    Public Shared Sub PrintWebControl(ByVal ctrl As Control, ByVal Script As String)
        Dim stringWrite As StringWriter = New StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(stringWrite)
        If TypeOf ctrl Is WebControl Then
            Dim w As Unit = New Unit(100, UnitType.Percentage)
            CType(ctrl, WebControl).Width = w
        End If
        Dim pg As Page = New Page()
        pg.EnableEventValidation = False
        If Script <> String.Empty Then
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script)
        End If
        Dim frm As HtmlForm = New HtmlForm()
        pg.Controls.Add(frm)
        frm.Attributes.Add("runat", "server")
        frm.Controls.Add(ctrl)
        pg.DesignerInitialize()
        pg.RenderControl(htmlWrite)
        Dim strHTML As String = stringWrite.ToString()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Write(strHTML)
        HttpContext.Current.Response.Write("<script>window.print();</script>")
        'HttpContext.Current.Response.End()
    End Sub

End Class